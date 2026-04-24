using Ardalis.Result;
using MediatR;

namespace Clientes.Aplicacao.Commands.CriarPedido
{
    /// <summary>
    /// Command para criar um novo pedido
    /// </summary>
    public class CriarPedidoCommand : IRequest<Result<CriarPedidoCommandResponse>>
    {
        public List<Guid> ItensIds { get; set; }
    }
}
