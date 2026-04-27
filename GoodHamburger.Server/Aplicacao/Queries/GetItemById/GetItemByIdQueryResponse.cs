using Ardalis.Result;

namespace Aplicacao.Queries.GetItemById;

/// <summary>
/// Resposta da query GetItemById
/// </summary>
public class GetItemByIdQueryResponse
{
    /// <summary>
    /// Item do menu
    /// </summary>
    public Dominio.Dtos.ItemDto Item { get; set; }
}
