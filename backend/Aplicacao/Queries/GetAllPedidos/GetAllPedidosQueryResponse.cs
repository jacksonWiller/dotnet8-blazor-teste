using Dominio.Dtos;

namespace Clientes.Aplicacao.Queries.GetAllPedidos
{
    /// <summary>
    /// Response para a query GetAllPedidos
    /// </summary>
    public class GetAllPedidosQueryResponse
    {
        public List<PedidoDto> Pedidos { get; set; }
    }
}