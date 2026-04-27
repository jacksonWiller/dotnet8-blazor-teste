using Ardalis.Result;
using Dominio.Dtos;
using Dominio.ObjetosDeValor;

namespace Aplicacao.Commands.UpdatePedido
{
    /// <summary>
    /// Response para o command UpdatePedido
    /// </summary>
    public class UpdatePedidoCommandResponse
    {
        public Guid PedidoId { get; set; }
        public PedidoStatus Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public List<PedidoItemDto> Itens { get; set; }
    }
}