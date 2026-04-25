using System.Net.Http.Json;
using GoodHamburger.Web.Components.Models;

namespace GoodHamburger.Web.Services;

/// <summary>
/// Service para gerenciar pedidos (criar, consultar, etc.)
/// </summary>
public interface IOrderService
{
    Task<CreatePedidoResponse> CreateOrderAsync(List<Guid> itemIds);
    Task<PedidoDetalhes?> GetOrderByIdAsync(Guid orderId);
    Task<PagedPedidoResponse> GetAllOrdersAsync(int pageNumber = 1, int pageSize = 10);
    Task<PedidoDetalhes?> UpdateOrderAsync(Guid orderId, List<UpdateOrderItemDto> itens, string novoStatus);
    Task<PedidoDetalhes?> CancelOrderAsync(Guid orderId);
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
    public async Task<CreatePedidoResponse> CreateOrderAsync(List<Guid> itemIds)
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
        
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<CreatePedidoResponse>>();
        
        if (result?.Success != true)
        {
            throw new Exception(result?.SuccessMessage ?? "Erro ao criar pedido");
        }
        
        return result.Result;
    }
    
    /// <summary>
    /// Obtém um pedido pelo ID
    /// </summary>
    public async Task<PedidoDetalhes?> GetOrderByIdAsync(Guid orderId)
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

    /// <summary>
    /// Atualiza um pedido com novos itens e status
    /// </summary>
    public async Task<PedidoDetalhes?> UpdateOrderAsync(Guid orderId, List<UpdateOrderItemDto> itens, string novoStatus)
    {
        var request = new
        {
            itens = itens.Select(i => new { i.ItemId, i.Quantidade }),
            status = novoStatus
        };
        
        var response = await _httpClient.PutAsJsonAsync($"api/Pedidos/{orderId}", request);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Erro ao atualizar pedido: {response.StatusCode} - {errorContent}");
        }
        
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PedidoResponse>>();
        
        if (result?.Success != true)
        {
            throw new Exception(result?.SuccessMessage ?? "Erro ao atualizar pedido");
        }
        
        return result?.Result?.Pedido;
    }

    /// <summary>
    /// Atualiza o status de um pedido (método legado)
    /// </summary>
    public async Task<PedidoDetalhes?> UpdateOrderStatusAsync(Guid orderId, string novoStatus)
    {
        var response = await _httpClient.PatchAsJsonAsync($"api/Pedidos/{orderId}/status", novoStatus);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Erro ao atualizar status: {response.StatusCode} - {errorContent}");
        }
        
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PedidoResponse>>();
        
        if (result?.Success != true)
        {
            throw new Exception(result?.SuccessMessage ?? "Erro ao atualizar status");
        }
        
        return result?.Result?.Pedido;
    }

    /// <summary>
    /// Cancela um pedido
    /// </summary>
    public async Task<PedidoDetalhes?> CancelOrderAsync(Guid orderId)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/Pedidos/{orderId}/cancelar", new { });
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Erro ao cancelar pedido: {response.StatusCode} - {errorContent}");
        }
        
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PedidoResponse>>();
        
        if (result?.Success != true)
        {
            throw new Exception(result?.SuccessMessage ?? "Erro ao cancelar pedido");
        }
        
        return result?.Result?.Pedido;
    }

    //Task<PedidoResponse?> IOrderService.GetOrderByIdAsync(Guid orderId)
    //{
    //    throw new NotImplementedException();
    //}
}
