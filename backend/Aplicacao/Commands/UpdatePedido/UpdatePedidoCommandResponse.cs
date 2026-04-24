using Ardalis.Result;

namespace Clientes.Aplicacao.Commands.UpdatePedido
{
    /// <summary>
    /// Response para o command UpdatePedido
    /// </summary>
    public class UpdatePedidoCommandResponse
    {
        public Guid PedidoId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public List<Dominio.Dtos.PedidoItemDto> Itens { get; set; }
    }
}