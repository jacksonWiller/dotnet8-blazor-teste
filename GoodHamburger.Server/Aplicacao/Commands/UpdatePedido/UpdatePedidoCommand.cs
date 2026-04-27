using Ardalis.Result;
using Dominio.ObjetosDeValor;
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
        public List<UpdatePedidoItemDto> Itens { get; set; }
        
        [Required(ErrorMessage = "O status do pedido é obrigatório.")]
        public PedidoStatus Status { get; set; }
    }
    
    /// <summary>
    /// DTO para representar um item na atualização do pedido
    /// </summary>
    public class UpdatePedidoItemDto
    {
        [Required(ErrorMessage = "O ID do item é obrigatório.")]
        public Guid ItemId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
        public int Quantidade { get; set; }
    }
}