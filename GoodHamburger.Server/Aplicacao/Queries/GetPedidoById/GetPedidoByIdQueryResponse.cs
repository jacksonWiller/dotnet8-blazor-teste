using Dominio.Dtos;

namespace Aplicacao.Queries.GetPedidoById
{
    /// <summary>
    /// Response para a query GetPedidoById
    /// </summary>
    public class GetPedidoByIdQueryResponse
    {
        public PedidoDto Pedido { get; set; }
    }
}