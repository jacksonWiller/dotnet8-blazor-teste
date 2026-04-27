using Ardalis.Result;
using Aplicacao.Queries.GetAllPedidos;
using Dominio.Dtos;
using Dominio.Interfaces;
using MediatR;

namespace Aplicacao.Queries.GetAllPedidos
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
            var (pedidos, totalCount) = await _pedidoRepository.GetAllPedidosAsync(request.PageNumber, request.PageSize);
            
            var response = new GetAllPedidosQueryResponse
            {
                Pedidos = pedidos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return Result<GetAllPedidosQueryResponse>.Success(response);
        }
    }
}