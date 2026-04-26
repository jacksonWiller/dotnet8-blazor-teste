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

            // Verificar IDs duplicados no request
            var idsDuplicados = request.Itens
                .GroupBy(i => i.ItemId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (idsDuplicados.Count != 0)
            {
                return Result<UpdatePedidoCommandResponse>.Error(
                    $"Itens duplicados no pedido: {string.Join(", ", idsDuplicados.Select(id => id.ToString()))}");
            }

            // Atualizar status do pedido
            pedido.AtualizarStatus(request.Status);

            // Remover todos os itens atuais
            pedido.RemoverTodosItens();

            // Buscar todos os itens de uma vez
            var items = await _itemRepository.GetItemsByIdsAsync(request.Itens.Select(i => i.ItemId).ToList());
            
            if (items.Count != request.Itens.Count)
            {
                var itensNaoEncontrados = request.Itens.Select(i => i.ItemId).Where(id => !items.Any(i => i.Id == id)).ToList();
                return Result<UpdatePedidoCommandResponse>.NotFound(
                    $"Itens não encontrados: {string.Join(", ", itensNaoEncontrados.Select(id => id.ToString()))}");
            }

            // Adicionar itens com as novas quantidades
            foreach (var itemRequest in request.Itens)
            {
                var item = items.FirstOrDefault(i => i.Id == itemRequest.ItemId);
                if (item != null)
                {
                    try
                    {
                        pedido.AdicionarItemComQuantidade(item, itemRequest.Quantidade);
                    }
                    catch (ArgumentException ex)
                    {
                        return Result.Error(ex.Message);
                    }
                }
            }

            // Salvar alterações
            try
            {
                await _pedidoRepository.AtualizarAsync(pedido);
            }
            catch (Exception)
            {

                throw;
            }
       
            var response = new UpdatePedidoCommandResponse
            {
                PedidoId = pedido.Id,
                Status = pedido.Status,
                Subtotal = pedido.Subtotal,
                Desconto = pedido.Desconto,
                Total = pedido.Total,
                Itens = pedido.Itens.Select(i => new Dominio.Dtos.PedidoItemDto
                {
                    ItemId = i.ItemId,
                    ItemNome = i.Nome,
                    Categoria = i.Categoria,
                    PrecoUnitario = i.PrecoUnitario,
                    Quantidade = i.Quantidade
                }).ToList()
            };

            return Result<UpdatePedidoCommandResponse>.Success(response, "Pedido atualizado com sucesso.");
        }
    }
}