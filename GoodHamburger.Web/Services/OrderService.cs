using System.Net.Http.Json;
using GoodHamburger.Web.Components.Models;

namespace GoodHamburger.Web.Services;

/// <summary>
/// Service para gerenciar pedidos (criar, consultar, etc.)
/// </summary>
public interface IOrderService
{
    Task<OrderResponse> CreateOrderAsync(List<Guid> itemIds);
    Task<OrderResponse?> GetOrderByIdAsync(Guid orderId);
    Task<PagedOrderResponse> GetAllOrdersAsync(int pageNumber = 1, int pageSize = 10);
}

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    
    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    /// <summary>
    /// Cria um novo pedido com os itens selecionados
    /// </summary>
    public async Task<OrderResponse> CreateOrderAsync(List<Guid> itemIds)
    {
        if (itemIds == null || !itemIds.Any())
            throw new ArgumentException("É necessário pelo menos um item para criar um pedido.", nameof(itemIds));
        
        var request = new
        {
            itensIds = itemIds
        };
        
        var response = await _httpClient.PostAsJsonAsync("api/Pedidos", request);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Erro ao criar pedido: {response.StatusCode} - {errorContent}");
        }
        
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<OrderResponse>>();
        
        if (result?.Success != true)
        {
            throw new Exception(result?.SuccessMessage ?? "Erro ao criar pedido");
        }
        
        return result.Result;
    }
    
    /// <summary>
    /// Obtém um pedido pelo ID
    /// </summary>
    public async Task<OrderResponse?> GetOrderByIdAsync(Guid orderId)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<OrderResponse>>($"api/Pedidos/{orderId}");
        return response?.Result;
    }
    
    /// <summary>
    /// Obtém todos os pedidos com paginação
    /// </summary>
    public async Task<PagedOrderResponse> GetAllOrdersAsync(int pageNumber = 1, int pageSize = 10)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<PagedOrderResponse>>(
            $"api/Pedidos?pageNumber={pageNumber}&pageSize={pageSize}");
        
        return response?.Result ?? new PagedOrderResponse();
    }
}

/// <summary>
/// Resposta da criação de pedido
/// </summary>
public class OrderResponse
{
    public Guid PedidoId { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Desconto { get; set; }
    public decimal Total { get; set; }
    public List<OrderItemDto> Itens { get; set; } = new();
    public DateTime DataCriacao { get; set; }
}

/// <summary>
/// Item do pedido
/// </summary>
public class OrderItemDto
{
    public Guid ItemId { get; set; }
    public string ItemNome { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal PrecoUnitario { get; set; }
    public int Quantidade { get; set; }
    public decimal Subtotal => PrecoUnitario * Quantidade;
}

/// <summary>
/// Resposta paginada de pedidos
/// </summary>
public class PagedOrderResponse
{
    public List<OrderResponse> Pedidos { get; set; } = new();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
}
