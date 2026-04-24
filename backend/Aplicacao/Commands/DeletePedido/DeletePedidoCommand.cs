using Ardalis.Result;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Commands.DeletePedido
{
    /// <summary>
    /// Command para remover um pedido
    /// </summary>
    public class DeletePedidoCommand : IRequest<Result<DeletePedidoCommandResponse>>
    {
        [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
        public Guid Id { get; set; }
    }
}