using Ardalis.Result;

namespace Clientes.Aplicacao.Queries.GetAllItems;

/// <summary>
/// Resposta da query GetAllItems
/// </summary>
public class GetAllItemsQueryResponse()
{
    /// <summary>
    /// Informações de paginação
    /// </summary>
    public PagedInfo PagedInfo { get; set; }
    
    /// <summary>
    /// Lista de itens do menu
    /// </summary>
    public List<Dominio.Dtos.ItemDto> Itens { get; set; }
}

/// <summary>
/// Classe para informações de paginação
/// </summary>
public class PagedInfo
{
    public PagedInfo(int pageNumber, int pageSize, int totalPages, int totalRecords)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalRecords = totalRecords;
    }

    /// <summary>
    /// Número da página atual
    /// </summary>
    public int PageNumber { get; set; }
    
    /// <summary>
    /// Tamanho da página
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Total de páginas
    /// </summary>
    public int TotalPages { get; set; }
    
    /// <summary>
    /// Total de registros
    /// </summary>
    public int TotalRecords { get; set; }
}
