using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Clientes.Dominio.Dtos;
using Clientes.Dominio.Entidades;
using Clientes.Dominio.Interfaces;
using FluentValidation;
using MediatR;

namespace Aplicacao.Commands.UpdatePedido
{
    /// <summary>
    /// Handler para o command UpdatePedido
    /// </summary>
    public class UpdatePedidoCommandHandler : IRequestHandler<UpdatePedidoCommand, Result<UpdatePedidoCommandResponse>>
    {
        private readonly IValidator<UpdatePedidoCommand> _validator;
        private readonly IPedidoRepository _pedidoRepository;

        public UpdatePedidoCommandHandler(
            IValidator<UpdatePedidoCommand> validator,
            IPedidoRepository pedidoRepository)
        {
            _validator = validator;
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Processa o command para atualizar um pedido
        /// </summary>
        public async Task<Result<UpdatePedidoCommandResponse>> Handle(
            UpdatePedidoCommand request,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<UpdatePedidoCommandResponse>.Invalid(validationResult.AsErrors());
            }

            // Buscar o pedido existente
            var pedido = await _pedidoRepository.GetByIdAsync(request.Id);
            if (pedido == null)
            {
                return Result<UpdatePedidoCommandResponse>.NotFound($"Pedido com ID {request.Id} não encontrado.");
            }

            // Limpar itens existentes
            pedido.LimparItens();

            // Adicionar novos itens ao pedido
            foreach (var itemId in request.ItensIds)
            {
                var itemCardapio = ItemCardapio.GetAllItens().FirstOrDefault(i => i.Id == itemId);
                
                if (itemCardapio == null)
                {
                    return Result<UpdatePedidoCommandResponse>.NotFound($"Item com ID {itemId} não encontrado no cardápio.");
                }

                try
                {
                    pedido.AdicionarItem(itemCardapio);
                }
                catch (ArgumentException ex)
                {
                    return Result<UpdatePedidoCommandResponse>.BadRequest(ex.Message);
                }
            }

            // Salvar alterações
            await _pedidoRepository.UpdateAsync(pedido);

            // Obter informações do pedido
            var pedidoInfo = pedido.ObterInfo();

            var response = new UpdatePedidoCommandResponse
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

            return Result<UpdatePedidoCommandResponse>.Success(response, "Pedido atualizado com sucesso.");
        }
    }
}