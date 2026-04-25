using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using GoodHamburger.Web.Components.Models;

namespace GoodHamburger.Web.Services;

/// <summary>
/// Service para gerenciar o carrinho de compras
/// </summary>
public interface ICartService
{
    IReadOnlyList<CartItem> Items { get; }
    int ItemCount { get; }
    decimal Subtotal { get; }
    decimal DeliveryFee { get; }
    decimal Discount { get; }
    decimal Total { get; }
    Guid? OrderId { get; }
    bool HasOrder { get; }
    
    event Action? OnChange;
    event Action? OnOrderCreated;
    
    Task AddItemAsync(ItemDto item);
    Task RemoveItemAsync(Guid itemId);
    Task UpdateQuantityAsync(Guid itemId, int quantity);
    Task ClearCartAsync();
    Task InitializeCartAsync();
    Task CreateOrderAsync(IOrderService orderService);
    Task CancelOrderAsync(IOrderService orderService);
    Task SetOrderIdAsync(Guid orderId);
    Task ClearOrderIdAsync();
}

public class CartService : ICartService
{
    private const string CartKey = "goodhamburger_cart";
    private const string OrderIdKey = "goodhamburger_orderid";
    private List<CartItem> _cartItems = new();
    private readonly IJSRuntime _jsRuntime;
    private readonly NavigationManager _navigation;
    private readonly IOrderService _orderService;
    
    private readonly decimal _deliveryFee = 3.50m;
    private readonly int _minComboItemsForDiscount = 2;
    private readonly decimal _discountPercentage = 0.20m;
    
    private Guid? _orderId;
    
    public event Action? OnChange;
    public event Action? OnOrderCreated;
    
    public IReadOnlyList<CartItem> Items => _cartItems;
    public int ItemCount => _cartItems.Sum(i => i.Quantity);
    public decimal Subtotal => _cartItems.Sum(i => i.Preco * i.Quantity);
    public decimal DeliveryFee => _deliveryFee;
    public decimal Discount { get; private set; }
    public decimal Total { get; private set; }
    public Guid? OrderId => _orderId;
    public bool HasOrder => _orderId.HasValue;
    
    public CartService(IJSRuntime jsRuntime, NavigationManager navigation, IOrderService orderService)
    {
        _jsRuntime = jsRuntime;
        _navigation = navigation;
        _orderService = orderService;
    }
    
    public async Task InitializeCartAsync()
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string>(
                "localStorage.getItem", CartKey);
            
            if (!string.IsNullOrEmpty(json) && json != "null")
            {
                _cartItems = System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(json) 
                    ?? new List<CartItem>();
            }
            else
            {
                _cartItems = new List<CartItem>();
            }
            
            // Carregar Order ID se existir
            var orderIdJson = await _jsRuntime.InvokeAsync<string>(
                "localStorage.getItem", OrderIdKey);
            
            if (!string.IsNullOrEmpty(orderIdJson) && Guid.TryParse(orderIdJson, out var orderId))
            {
                _orderId = orderId;
            }
            
            CalculateTotals();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading cart: {ex.Message}");
            _cartItems = new List<CartItem>();
        }
    }
    
    public async Task AddItemAsync(ItemDto item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        
        // Verificar se já existe item com mesma ID
        var existingItem = _cartItems.FirstOrDefault(i => i.ItemId == item.Id);
        
        if (existingItem != null)
        {
            // Não permitir adicionar mais de 1 item da mesma categoria
            var existingCategoryItem = _cartItems.FirstOrDefault(i => 
                i.Categoria == item.Categoria && i.ItemId != item.Id);
            
            if (existingCategoryItem != null)
            {
                throw new InvalidOperationException(
                    $"Você já tem um {existingCategoryItem.Categoria.ToLower()} no carrinho.");
            }
            
            existingItem.Quantity++;
        }
        else
        {
            // Verificar regra de negócio: máximo 1 por categoria
            var hasSameCategory = _cartItems.Any(i => i.Categoria == item.Categoria);
            
            if (hasSameCategory)
            {
                throw new InvalidOperationException(
                    $"Você já tem um {item.Categoria.ToLower()} no carrinho. " +
                    "É permitido apenas um item de cada categoria por pedido.");
            }
            
            _cartItems.Add(new CartItem
            {
                ItemId = item.Id,
                Nome = item.Nome,
                Descricao = item.Descricao,
                Preco = item.Preco,
                Categoria = item.Categoria,
                Tipo = item.Tipo,
                UrlImagem = item.UrlImagem ?? string.Empty,
                Icon = GetIconForType(item.Tipo),
                Quantity = 1
            });
        }
        
        await SaveCartAsync();
    }
    
    public async Task RemoveItemAsync(Guid itemId)
    {
        _cartItems.RemoveAll(i => i.ItemId == itemId);
        await SaveCartAsync();
    }
    
    public async Task UpdateQuantityAsync(Guid itemId, int quantity)
    {
        if (quantity <= 0)
        {
            await RemoveItemAsync(itemId);
            return;
        }
        
        var item = _cartItems.FirstOrDefault(i => i.ItemId == itemId);
        if (item != null)
        {
            item.Quantity = quantity;
            await SaveCartAsync();
        }
    }
    
    public async Task ClearCartAsync()
    {
        _cartItems.Clear();
        await SaveCartAsync();
    }
    
    /// <summary>
    /// Cria um pedido a partir do carrinho
    /// </summary>
    public async Task CreateOrderAsync(IOrderService orderService)
    {
        if (_cartItems == null || !_cartItems.Any())
            throw new InvalidOperationException("O carrinho está vazio.");
        
        var itemIds = _cartItems.Select(i => i.ItemId).ToList();
        var orderResponse = await orderService.CreateOrderAsync(itemIds);
        
        _orderId = orderResponse.PedidoId;
        
        // Salvar Order ID no localStorage
        await _jsRuntime.InvokeAsync<object>(
            "localStorage.setItem", OrderIdKey, _orderId.ToString());
        
        // Limpar carrinho após criar pedido
        _cartItems.Clear();
        await SaveCartAsync();
        
        OnOrderCreated?.Invoke();
    }
    
    /// <summary>
    /// Define o ID do pedido atual
    /// </summary>
    public async Task SetOrderIdAsync(Guid orderId)
    {
        _orderId = orderId;
        await _jsRuntime.InvokeAsync<object>("localStorage.setItem", OrderIdKey, orderId.ToString());
        OnChange?.Invoke();
    }

    /// <summary>
    /// Limpa o ID do pedido atual
    /// </summary>
    public async Task ClearOrderIdAsync()
    {
        _orderId = null;
        await _jsRuntime.InvokeAsync<object>("localStorage.removeItem", OrderIdKey);
        OnChange?.Invoke();
    }

    /// <summary>
    /// Cancela o pedido atual
    /// </summary>
    public async Task CancelOrderAsync(IOrderService orderService)
    {
        if (!_orderId.HasValue)
            throw new InvalidOperationException("Não há pedido para cancelar.");
        
        await orderService.CancelOrderAsync(_orderId.Value);
        
        // Remover Order ID do localStorage
        await _jsRuntime.InvokeAsync<object>(
            "localStorage.removeItem", OrderIdKey);
        
        _orderId = null;
    }
    
    private async Task SaveCartAsync()
    {
        CalculateTotals();
        
        try
        {
            var json = System.Text.Json.JsonSerializer.Serialize(_cartItems);
            await _jsRuntime.InvokeAsync<object>(
                "localStorage.setItem", CartKey, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error saving cart: {ex.Message}");
        }
        
        OnChange?.Invoke();
    }
    
    private void CalculateTotals()
    {
        var subtotal = _cartItems.Sum(i => i.Preco * i.Quantity);
        
        // Desconto para combo deals (20%)
        var comboItems = _cartItems.Where(i => 
            i.Tipo.ToLower().Contains("combo") || 
            i.Categoria.ToLower().Contains("combo")).ToList();
        
        if (comboItems.Count >= _minComboItemsForDiscount)
        {
            Discount = subtotal * _discountPercentage;
        }
        else
        {
            Discount = 0;
        }
        
        Total = subtotal - Discount + _deliveryFee;
    }
    
    private string GetIconForType(string tipo)
    {
        return tipo.ToLower() switch
        {
            "sanduiche" or "burger" or "hamburguer" => "burger",
            "acompanhamento" or "side" or "fritura" => "dish-fill",
            "bebida" or "drink" or "refrigerante" => "cup-hot-fill",
            "sobremesa" or "dessert" => "ice-cream",
            _ => "emoji-smile"
        };
    }
}
