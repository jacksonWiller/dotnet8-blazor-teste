using Ardalis.Result;

namespace Clientes.Aplicacao.Commands.CriarPedido
{
    /// <summary>
    /// Response para o command CriarPedido
    /// </summary>
    public class CriarPedidoCommandResponse
    {
        public Guid PedidoId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public List<Dominio.Dtos.PedidoItemDto> Itens { get; set; }
    }
}
