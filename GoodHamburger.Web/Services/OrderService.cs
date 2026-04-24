using System.Net.Http.Json;
using GoodHamburger.Web.Components.Models;

namespace GoodHamburger.Web.Services;

/// <summary>
/// Service para gerenciar pedidos (criar, consultar, etc.)
/// </summary>
public interface IOrderService
{
    Task<PedidoResponse> CreateOrderAsync(List<Guid> itemIds);
    Task<PedidoResponse?> GetOrderByIdAsync(Guid orderId);
    Task<PagedPedidoResponse> GetAllOrdersAsync(int pageNumber = 1, int pageSize = 10);
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
    public async Task<PedidoResponse> CreateOrderAsync(List<Guid> itemIds)
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
        
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PedidoResponse>>();
        
        if (result?.Success != true)
        {
            throw new Exception(result?.SuccessMessage ?? "Erro ao criar pedido");
        }
        
        return result.Result;
    }
    
    /// <summary>
    /// Obtém um pedido pelo ID
    /// </summary>
    public async Task<PedidoResponse?> IOrderService.GetOrderByIdAsync(Guid orderId)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<PedidoResponse>>($"api/Pedidos/{orderId}");
        return response?.Result?.Pedido;
    }
    
    /// <summary>
    /// Obtém todos os pedidos com paginação
    /// </summary>
    public async Task<PagedPedidoResponse> GetAllOrdersAsync(int pageNumber = 1, int pageSize = 10)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<PagedPedidoResponse>>(
            $"api/Pedidos?pageNumber={pageNumber}&pageSize={pageSize}");
        
        return response?.Result ?? new PagedPedidoResponse();
    }

    //Task<PedidoResponse?> IOrderService.GetOrderByIdAsync(Guid orderId)
    //{
    //    throw new NotImplementedException();
    //}
}
