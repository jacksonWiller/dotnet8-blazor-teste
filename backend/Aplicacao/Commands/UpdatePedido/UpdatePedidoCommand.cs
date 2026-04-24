using Ardalis.Result;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Commands.UpdatePedido
{
    /// <summary>
    /// Command para atualizar um pedido
    /// </summary>
    public class UpdatePedidoCommand : IRequest<Result<UpdatePedidoCommandResponse>>
    {
        [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "A lista de itens é obrigatória.")]
        public List<Guid> ItensIds { get; set; }
    }
}