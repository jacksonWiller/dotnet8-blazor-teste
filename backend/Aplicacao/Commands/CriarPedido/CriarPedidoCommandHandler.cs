using Aplicacao.Commands.CriarPedido;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Interfaces;
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
        private readonly IItemRepository _itemRepository;

        public CriarPedidoCommandHandler(
            IValidator<CriarPedidoCommand> validator,
            IItemRepository itemRepository)
        {
            _validator = validator;
            _itemRepository = itemRepository;
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
            var pedido = new Pedido();

            // Adicionar itens ao pedido
            foreach (var itemId in request.ItensIds)
            {
                var item = await _itemRepository.GetItemByIdAsync(itemId);
                
                if (item == null)
                {
                    return Result<CriarPedidoCommandResponse>.NotFound($"Item com ID {itemId} não encontrado no cardápio.");
                }

                try
                {
                    pedido.AdicionarItem(item);
                }
                catch (ArgumentException ex)
                {
                    return Result.Error(ex.Message);
                }
            }

            var response = new CriarPedidoCommandResponse
            {
                PedidoId = pedido.Id,
                Subtotal = pedido.Subtotal,
                Desconto = pedido.Desconto,
                Total = pedido.Total,
                Itens = pedido.Itens.Select(i => new PedidoItemDto
                {
                    ItemId = i.ItemId,
                    ItemNome = i.ItemNome,
                    Categoria = i.Categoria.ToString(),
                    PrecoUnitario = i.PrecoUnitario
                }).ToList()
            };

            return Result<CriarPedidoCommandResponse>.Success(response, "Pedido criado com sucesso.");
        }
    }
}
