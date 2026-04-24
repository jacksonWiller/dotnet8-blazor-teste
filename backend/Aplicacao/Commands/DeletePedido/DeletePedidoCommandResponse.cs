using Ardalis.Result;

namespace Aplicacao.Commands.DeletePedido
{
    /// <summary>
    /// Response para o command DeletePedido
    /// </summary>
    public class DeletePedidoCommandResponse
    {
        public Guid PedidoId { get; set; }
    }
}