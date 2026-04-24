using System.Text.Json.Serialization;

namespace GoodHamburger.Web.Components.Models;

/// <summary>
/// DTO para atualizar um item no pedido
/// </summary>
public class UpdateOrderItemDto
{
    [JsonPropertyName("itemId")]
    public Guid ItemId { get; set; }
    
    [JsonPropertyName("quantidade")]
    public int Quantidade { get; set; }
}

public class PedidoItemDto
{
    [JsonPropertyName("itemId")]
    public Guid ItemId { get; set; }
    
    [JsonPropertyName("itemNome")]
    public string ItemNome { get; set; } = string.Empty;
    
    [JsonPropertyName("categoria")]
    public string Categoria { get; set; } = string.Empty;
    
    [JsonPropertyName("precoUnitario")]
    public decimal PrecoUnitario { get; set; }
    
    [JsonPropertyName("quantidade")]
    public int Quantidade { get; set; }
    
    [JsonPropertyName("subtotal")]
    public decimal Subtotal { get; set; }
}

public class PedidoResponse
{
    [JsonPropertyName("pedido")]
    public PedidoDetalhes? Pedido { get; set; }
}

public class PedidoDetalhes
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("itens")]
    public List<PedidoItemDto> Itens { get; set; } = new();
    
    [JsonPropertyName("subtotal")]
    public decimal Subtotal { get; set; }
    
    [JsonPropertyName("desconto")]
    public decimal Desconto { get; set; }
    
    [JsonPropertyName("total")]
    public decimal Total { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; } = "Pendente";
    
    [JsonPropertyName("dataCriacao")]
    public DateTime DataCriacao { get; set; }
}

public class PagedPedidoResponse
{
    [JsonPropertyName("pedidos")]
    public List<PedidoDetalhes> Pedidos { get; set; } = new();
    
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
    
    [JsonPropertyName("pageNumber")]
    public int PageNumber { get; set; }
    
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }
}
