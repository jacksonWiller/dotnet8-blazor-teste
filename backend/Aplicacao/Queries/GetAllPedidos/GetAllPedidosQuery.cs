using Ardalis.Result;
using Dominio.Dtos;
using MediatR;

namespace Aplicacao.Queries.GetAllPedidos
{
    /// <summary>
    /// Query para listar todos os pedidos
    /// </summary>
    public class GetAllPedidosQuery : IRequest<Result<GetAllPedidosQueryResponse>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetAllPedidosQuery(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}