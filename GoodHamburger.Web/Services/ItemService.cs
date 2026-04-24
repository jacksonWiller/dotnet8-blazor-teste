using System.Net.Http.Json;
using GoodHamburger.Web.Components.Models;

namespace GoodHamburger.Web.Services;

public interface IItemService
{
    Task<List<ItemDto>> GetAllItemsAsync();
    Task<ItemDto?> GetItemByIdAsync(Guid id);
}

public class ItemService : IItemService
{
    private readonly HttpClient _httpClient;

    public ItemService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ItemDto>> GetAllItemsAsync()
    {
        try
        {
            Console.WriteLine($"========================================");
            Console.WriteLine($"ItemService.GetAllItemsAsync - Iniciando");
            Console.WriteLine($"BaseAddress: {_httpClient.BaseAddress}");
            Console.WriteLine($"========================================");
            
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<ItemsListResponse>>("api/Itens");
            
            Console.WriteLine($"========================================");
            Console.WriteLine($"ItemService.GetAllItemsAsync - Response recebido");
            Console.WriteLine($"Response object: {response?.GetType().Name}");
            Console.WriteLine($"Response.Success: {response?.Success}");
            Console.WriteLine($"Response.Result type: {response?.Result?.GetType().Name}");
            Console.WriteLine($"Response.Result: {response?.Result}");
            Console.WriteLine($"Response.Result.Itens: {response?.Result?.Itens}");
            Console.WriteLine($"Response.Result.Itens type: {response?.Result?.Itens?.GetType().Name}");
            Console.WriteLine($"Count Itens: {response?.Result?.Itens?.Count}");
            Console.WriteLine($"========================================");
            
            if (response?.Result?.Itens != null)
            {
                for (int i = 0; i < response.Result.Itens.Count; i++)
                {
                    var item = response.Result.Itens[i];
                    Console.WriteLine($"Item {i + 1}:");
                    Console.WriteLine($"  Id: {item.Id}");
                    Console.WriteLine($"  Nome: {item.Nome}");
                    Console.WriteLine($"  Descricao: {item.Descricao}");
                    Console.WriteLine($"  Preco: {item.Preco}");
                    Console.WriteLine($"  Tipo: {item.Tipo}");
                    Console.WriteLine($"  Categoria: {item.Categoria}");
                    Console.WriteLine($"  UrlImagem: {item.UrlImagem}");
                    Console.WriteLine($"  Removido: {item.Removido}");
                    Console.WriteLine($"----------------------------------------");
                }
            }
            
            var itens = response?.Result?.Itens ?? new List<ItemDto>();
            Console.WriteLine($"========================================");
            Console.WriteLine($"ItemService.GetAllItemsAsync - Retornando {itens.Count} itens");
            Console.WriteLine($"========================================");
            
            return itens;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"========================================");
            Console.WriteLine($"ERRO no ItemService.GetAllItemsAsync:");
            Console.WriteLine($"  Mensagem: {ex.Message}");
            Console.WriteLine($"  Stack: {ex.StackTrace}");
            Console.WriteLine($"========================================");
            return new List<ItemDto>();
        }
    }

    public async Task<ItemDto?> GetItemByIdAsync(Guid id)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<GetItemByIdResponse>>($"api/Itens/{id}");
        return response?.Result?.Item;
    }
}

public class GetItemByIdResponse
{
    public ItemDto Item { get; set; } = null!;
}
