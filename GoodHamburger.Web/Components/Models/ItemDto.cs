using System.Text.Json.Serialization;

namespace GoodHamburger.Web.Components.Models;

public class ItemDto
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [System.Text.Json.Serialization.JsonPropertyName("nome")]
    public string Nome { get; set; } = string.Empty;
    
    [System.Text.Json.Serialization.JsonPropertyName("descricao")]
    public string Descricao { get; set; } = string.Empty;
    
    [System.Text.Json.Serialization.JsonPropertyName("preco")]
    public decimal Preco { get; set; }
    
    [System.Text.Json.Serialization.JsonPropertyName("tipo")]
    public string Tipo { get; set; } = string.Empty;
    
    [System.Text.Json.Serialization.JsonPropertyName("categoria")]
    public string Categoria { get; set; } = string.Empty;
    
    [System.Text.Json.Serialization.JsonPropertyName("urlImagem")]
    public string? UrlImagem { get; set; }
    
    [System.Text.Json.Serialization.JsonPropertyName("removido")]
    public bool Removido { get; set; }
}

public class PagedInfo
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
}

public class ApiResponse<T>
{
    [System.Text.Json.Serialization.JsonPropertyName("success")]
    public bool Success { get; set; }
    
    [System.Text.Json.Serialization.JsonPropertyName("successMessage")]
    public string? SuccessMessage { get; set; }
    
    [System.Text.Json.Serialization.JsonPropertyName("result")]
    public T? Result { get; set; }
    
    [System.Text.Json.Serialization.JsonPropertyName("errors")]
    public List<ApiErrorResponse> Errors { get; set; } = new();
}

public class ApiErrorResponse
{
    [System.Text.Json.Serialization.JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; set; }
    
    public ApiErrorResponse() { }
    
    public ApiErrorResponse(string message)
    {
        ErrorMessage = message;
    }
}

// Modelo simplificado para resposta direta de lista de itens
public class ItemsListResponse
{
    [System.Text.Json.Serialization.JsonPropertyName("pagedInfo")]
    public PagedInfo PagedInfo { get; set; } = null!;
    
    [System.Text.Json.Serialization.JsonPropertyName("itens")]
    public List<ItemDto> Itens { get; set; } = new();
}

public class GetAllItemsQueryResponse
{
    public PagedInfo PagedInfo { get; set; } = null!;
    public List<ItemDto> Itens { get; set; } = new();
}
