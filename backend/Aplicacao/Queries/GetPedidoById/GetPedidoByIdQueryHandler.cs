using Ardalis.Result;
using Clientes.Dominio.Dtos;
using Clientes.Dominio.Entidades;
using Clientes.Dominio.Interfaces;
using MediatR;

namespace Clientes.Aplicacao.Queries.GetPedidoById
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
            var pedido = await _pedidoRepository.GetByIdAsync(request.Id);
            if (pedido == null)
            {
                return Result<GetPedidoByIdQueryResponse>.NotFound($"Pedido com ID {request.Id} não encontrado.");
            }

            var pedidoInfo = pedido.ObterInfo();
            var pedidoDto = new PedidoDto
            {
                Id = pedidoInfo.Id,
                Itens = pedidoInfo.Itens.Select(i => new PedidoItemDto
                {
                    ItemId = i.ItemId,
                    Nome = i.Nome,
                    Categoria = i.Categoria.ToString(),
                    PrecoUnitario = i.PrecoUnitario
                }).ToList(),
                Subtotal = pedidoInfo.Subtotal,
                Desconto = pedidoInfo.Desconto,
                Total = pedidoInfo.Total,
                DataCriacao = pedidoInfo.DataCriacao
            };

            return Result<GetPedidoByIdQueryResponse>.Success(new GetPedidoByIdQueryResponse
            {
                Pedido = pedidoDto
            });
        }
    }
}