using Ardalis.Result;
using Clientes.Dominio.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Queries.GetPedidoById
{
    /// <summary>
    /// Query para buscar um pedido pelo ID
    /// </summary>
    public class GetPedidoByIdQuery : IRequest<Result<GetPedidoByIdQueryResponse>>
    {
        [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
        public Guid Id { get; set; }
    }
}