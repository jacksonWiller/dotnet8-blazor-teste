using Ardalis.Result;
using Aplicacao.Queries.GetPedidoById;
using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Interfaces;
using MediatR;

namespace Aplicacao.Queries.GetPedidoById
{
    /// <summary>
    /// Handler para a query GetPedidoById
    /// </summary>
    public class GetPedidoByIdQueryHandler : IRequestHandler<GetPedidoByIdQuery, Result<GetPedidoByIdQueryResponse>>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public GetPedidoByIdQueryHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Processa a query para buscar um pedido pelo ID
        /// </summary>
        public async Task<Result<GetPedidoByIdQueryResponse>> Handle(
            GetPedidoByIdQuery request,
            CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(request.Id);
            if (pedido == null)
            {
                return Result<GetPedidoByIdQueryResponse>.NotFound($"Pedido com ID {request.Id} não encontrado.");
            }

            var pedidoDto = new PedidoDto
            {
                Id = pedido.Id,
                Itens = pedido.Itens.Select(i => new PedidoItemDto
                {
                    ItemId = i.ItemId,
                    ItemNome = i.Nome,
                    Categoria = i.Categoria,
                    PrecoUnitario = i.PrecoUnitario,
                    Quantidade = i.Quantidade
                }).ToList(),
                Subtotal = pedido.Subtotal,
                Desconto = pedido.Desconto,
                Total = pedido.Total,
                Status = pedido.Status,
                DataCriacao = pedido.DataCriacao
            };

            return Result<GetPedidoByIdQueryResponse>.Success(new GetPedidoByIdQueryResponse
            {
                Pedido = pedidoDto
            });
        }
    }
}