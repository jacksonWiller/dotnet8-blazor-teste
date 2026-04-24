using Aplicacao.Commands.CriarPedido;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Dominio.Entidades;
using FluentValidation;
using MediatR;

namespace Aplicacao.Commands.CriarPedido
{
    /// <summary>
    /// Handler para o command CriarPedido
    /// </summary>
    public class CriarPedidoCommandHandler : IRequestHandler<CriarPedidoCommand, Result<CriarPedidoCommandResponse>>
    {
        private readonly IValidator<CriarPedidoCommand> _validator;

        public CriarPedidoCommandHandler(IValidator<CriarPedidoCommand> validator)
        {
            _validator = validator;
        }

        /// <summary>
        /// Processa o command para criar um novo pedido
        /// </summary>
        public async Task<Result<CriarPedidoCommandResponse>> Handle(
            CriarPedidoCommand request, 
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<CriarPedidoCommandResponse>.Invalid(validationResult.AsErrors());
            }

            // Criar pedido
            var pedido = new Dominio.Entidades.Pedido();

            // Adicionar itens ao pedido
            foreach (var itemId in request.ItensIds)
            {
                var itemCardapio = Item.GetAllItens().FirstOrDefault(i => i.Id == itemId);
                
                if (itemCardapio == null)
                {
                    return Result<CriarPedidoCommandResponse>.NotFound($"Item com ID {itemId} não encontrado no cardápio.");
                }

                try
                {
                    pedido.AdicionarItem(itemCardapio);
                }
                catch (ArgumentException ex)
                {
                    return Result<CriarPedidoCommandResponse>.BadRequest(ex.Message);
                }
            }

            // Obter informações do pedido
            var pedidoInfo = pedido.ObterInfo();

            var response = new CriarPedidoCommandResponse
            {
                PedidoId = pedidoInfo.Id,
                Subtotal = pedidoInfo.Subtotal,
                Desconto = pedidoInfo.Desconto,
                Total = pedidoInfo.Total,
                Itens = pedidoInfo.Itens.Select(i => new Dominio.Dtos.PedidoItemDto
                {
                    ItemId = i.ItemId,
                    Nome = i.Nome,
                    Categoria = i.Categoria.ToString(),
                    PrecoUnitario = i.PrecoUnitario
                }).ToList()
            };

            return Result<CriarPedidoCommandResponse>.Success(response, "Pedido criado com sucesso.");
        }
    }
}
