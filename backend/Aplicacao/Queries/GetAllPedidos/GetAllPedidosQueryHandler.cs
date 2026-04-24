using Ardalis.Result;
using Clientes.Dominio.Dtos;
using Clientes.Dominio.Interfaces;
using MediatR;

namespace Clientes.Aplicacao.Queries.GetAllPedidos
{
    /// <summary>
    /// Handler para a query GetAllPedidos
    /// </summary>
    public class GetAllPedidosQueryHandler : IRequestHandler<GetAllPedidosQuery, Result<GetAllPedidosQueryResponse>>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public GetAllPedidosQueryHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Processa a query para listar todos os pedidos
        /// </summary>
        public async Task<Result<GetAllPedidosQueryResponse>> Handle(
            GetAllPedidosQuery request,
            CancellationToken cancellationToken)
        {
            var pedidos = await _pedidoRepository.GetAllAsync();
            
            var pedidosDto = pedidos.Select(pedido =>
            {
                var pedidoInfo = pedido.ObterInfo();
                return new PedidoDto
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
            }).ToList();

            return Result<GetAllPedidosQueryResponse>.Success(new GetAllPedidosQueryResponse
            {
                Pedidos = pedidosDto
            });
        }
    }
}