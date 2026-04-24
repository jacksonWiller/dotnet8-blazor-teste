using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Dominio.Interfaces;
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
        private readonly IItemRepository _itemRepository;

        public UpdatePedidoCommandHandler(
            IValidator<UpdatePedidoCommand> validator,
            IPedidoRepository pedidoRepository,
            IItemRepository itemRepository)
        {
            _validator = validator;
            _pedidoRepository = pedidoRepository;
            _itemRepository = itemRepository;
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
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(request.Id);
            if (pedido == null)
            {
                return Result<UpdatePedidoCommandResponse>.NotFound($"Pedido com ID {request.Id} não encontrado.");
            }


            // Buscar todos os itens de uma vez
            var items = await _itemRepository.GetItemsByIdsAsync(request.ItensIds);
            
            if (items.Count != request.ItensIds.Count)
            {
                var itensNaoEncontrados = request.ItensIds.Where(id => !items.Any(i => i.Id == id)).ToList();
                return Result<UpdatePedidoCommandResponse>.NotFound(
                    $"Itens não encontrados: {string.Join(", ", itensNaoEncontrados.Select(id => id.ToString()))}");
            }

            // Adicionar novos itens ao pedido
            foreach (var item in items)
            {
                try
                {
                    pedido.AdicionarItem(item);
                }
                catch (ArgumentException ex)
                {
                    return Result.Error(ex.Message);
                }
            }

            // Salvar alterações
            await _pedidoRepository.AtualizarAsync(pedido);

            var response = new UpdatePedidoCommandResponse
            {
                PedidoId = pedido.Id,
                Subtotal = pedido.Subtotal,
                Desconto = pedido.Desconto,
                Total = pedido.Total,
                Itens = pedido.Itens.Select(i => new Dominio.Dtos.PedidoItemDto
                {
                    ItemId = i.ItemId,
                    ItemNome = i.ItemNome,
                    Categoria = i.Categoria,
                    PrecoUnitario = i.PrecoUnitario,
                    Quantidade = i.Quantidade
                }).ToList()
            };

            return Result<UpdatePedidoCommandResponse>.Success(response, "Pedido atualizado com sucesso.");
        }
    }
}