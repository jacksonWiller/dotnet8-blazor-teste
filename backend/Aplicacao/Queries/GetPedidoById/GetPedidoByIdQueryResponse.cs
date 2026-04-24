using Clientes.Dominio.Dtos;

namespace Clientes.Aplicacao.Queries.GetPedidoById
{
    /// <summary>
    /// Response para a query GetPedidoById
    /// </summary>
    public class GetPedidoByIdQueryResponse
    {
        public PedidoDto Pedido { get; set; }
    }
}