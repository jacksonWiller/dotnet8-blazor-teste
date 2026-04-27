using Dominio.Dtos;

namespace Aplicacao.Queries.GetAllPedidos
{
    /// <summary>
    /// Response para a query GetAllPedidos
    /// </summary>
    public class GetAllPedidosQueryResponse
    {
        public List<PedidoDto> Pedidos { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}