using Ardalis.Result;

namespace Clientes.Aplicacao.Queries.GetItemById;

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
