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
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<ItemsListResponse>>("api/Itens");
           
            
            if (response?.Result?.Itens != null)
            {
                for (int i = 0; i < response.Result.Itens.Count; i++)
                {
                    var item = response.Result.Itens[i];
                }
            }
            
            var itens = response?.Result?.Itens ?? new List<ItemDto>();
            
            return itens;
        }
        catch (Exception ex)
        {
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
