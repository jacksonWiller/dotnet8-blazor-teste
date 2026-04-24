using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Aplicacao.Commands.UpdatePedido;
using Dominio.Dtos;
using Dominio.Entidades;
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
                return Result.Invalid(validationResult.AsErrors());
            }

            // Buscar o pedido existente
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(request.Id);
            if (pedido == null)
            {
                return Result.NotFound($"Pedido com ID {request.Id} não encontrado.");
            }


            var item = await _itemRepository.GetAllItemsAsync(x => x.Id == itemId);

            // Salvar alterações
            await _pedidoRepository.AtualizarAsync(pedido);

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