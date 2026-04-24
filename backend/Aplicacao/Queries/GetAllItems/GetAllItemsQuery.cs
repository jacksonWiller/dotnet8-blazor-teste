using Ardalis.Result;
using MediatR;

namespace Clientes.Aplicacao.Queries.GetAllItems;

/// <summary>
/// Query para obter todos os itens do menu com paginação
/// </summary>
public class GetAllItemsQuery : IRequest<Result<GetAllItemsQueryResponse>>
{
    /// <summary>
    /// Filtro para busca (ex: "Nome:burger", "Preco>10")
    /// </summary>
    public string Filter { get; set; } = string.Empty;
    
    /// <summary>
    /// Ordenação (ex: "Nome", "Preco DESC")
    /// </summary>
    public string Order { get; set; } = string.Empty;
    
    /// <summary>
    /// Número da página (padrão: 1)
    /// </summary>
    public int PageNumber { get; set; } = 1;
    
    /// <summary>
    /// Tamanho da página (padrão: 10)
    /// </summary>
    public int PageSize { get; set; } = 10;
}
