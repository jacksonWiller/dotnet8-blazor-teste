using Ardalis.Result;
using MediatR;

namespace Clientes.Aplicacao.Queries.GetItemById;

/// <summary>
/// Query para obter um item pelo ID
/// </summary>
public class GetItemByIdQuery : IRequest<Result<GetItemByIdQueryResponse>>
{
    /// <summary>
    /// ID do item a ser buscado
    /// </summary>
    public Guid Id { get; set; }
}
