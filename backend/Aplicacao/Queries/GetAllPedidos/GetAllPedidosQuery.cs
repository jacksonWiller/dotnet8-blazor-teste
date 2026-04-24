using Ardalis.Result;
using Clientes.Dominio.Dtos;
using MediatR;

namespace Clientes.Aplicacao.Queries.GetAllPedidos
{
    /// <summary>
    /// Query para listar todos os pedidos
    /// </summary>
    public class GetAllPedidosQuery : IRequest<Result<GetAllPedidosQueryResponse>>
    {
    }
}