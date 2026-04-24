# Atualização de Pedidos - Documentação

## Visão Geral

Esta documentação descreve como atualizar pedidos existentes, incluindo a alteração do status e dos itens do pedido.

## API Backend

### Endpoint

```
PUT /api/pedidos/{id}
```

### Corpo da Requisição

```json
{
  "itens": [
    {
      "itemId": "guid-do-item-1",
      "quantidade": 2
    },
    {
      "itemId": "guid-do-item-2",
      "quantidade": 1
    }
  ],
  "status": 1
}
```

### Parâmetros

- `id` (GUID): ID do pedido a ser atualizado
- `itens` (Array): Lista de itens com seus IDs e quantidades
  - `itemId` (GUID): ID do item do cardápio
  - `quantidade` (Int): Quantidade do item (mínimo 1)
- `status` (Enum): Novo status do pedido

### Status do Pedido

O status é um enum com os seguintes valores:

| Valor | Nome | Descrição |
|-------|------|-----------|
| 0 | Pendente | Pedido criado, aguardando confirmação |
| 1 | EmPreparacao | Pedido confirmado, em preparação |
| 2 | Pronto | Pedido pronto para entrega/retirada |
| 3 | Entregue | Pedido entregue ao cliente |
| 4 | Cancelado | Pedido cancelado pelo cliente ou restaurante |

### Resposta de Sucesso

```json
{
  "success": true,
  "successMessage": "Pedido atualizado com sucesso.",
  "result": {
    "pedidoId": "guid-do-pedido",
    "status": 1,
    "subtotal": 50.00,
    "desconto": 5.00,
    "total": 45.00,
    "itens": [
      {
        "itemId": "guid-do-item-1",
        "itemNome": "X-Burger",
        "categoria": "Sanduíche",
        "precoUnitario": 25.00,
        "quantidade": 2,
        "subtotal": 50.00
      }
    ]
  }
}
```

## Frontend (Blazor)

### Método UpdateOrderAsync

O serviço `IOrderService` fornece o método `UpdateOrderAsync` para atualizar pedidos:

```csharp
public async Task<PedidoDetalhes?> UpdateOrderAsync(
    Guid orderId, 
    List<UpdateOrderItemDto> itens, 
    string novoStatus)
```

### Exemplo de Uso

```csharp
@inject IOrderService OrderService

@code {
    private async Task AtualizarPedidoAsync()
    {
        try
        {
            // Criar lista de itens para atualização
            var itensAtualizados = _orderDetails.Itens.Select(i => new UpdateOrderItemDto
            {
                ItemId = i.ItemId,
                Quantidade = i.Quantidade
            }).ToList();

            // Atualizar pedido com novo status
            var novoStatus = "EmPreparacao"; // ou "Pronto", "Entregue", etc.
            
            var response = await OrderService.UpdateOrderAsync(
                CartService.OrderId.Value, 
                itensAtualizados, 
                novoStatus);

            // Recarregar dados do pedido
            await CheckOrderStatus();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao atualizar pedido: {ex.Message}");
            // Implementar notificação ao usuário
        }
    }
}
```

### Exemplo no Cart.razor

O componente `Cart.razor` já inclui o método `UpdateOrderAsync`:

```csharp
/// <summary>
/// Atualiza o pedido com novos itens e status
/// </summary>
private async Task UpdateOrderAsync(string novoStatus)
{
    try
    {
        if (_orderDetails == null || CartService.OrderId == null)
            return;

        var itensAtualizados = _orderDetails.Itens.Select(i => new UpdateOrderItemDto
        {
            ItemId = i.ItemId,
            Quantidade = i.Quantidade
        }).ToList();

        await OrderService.UpdateOrderAsync(CartService.OrderId.Value, itensAtualizados, novoStatus);
        
        // Recarrega o status do pedido
        await CheckOrderStatus();
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"Erro ao atualizar pedido: {ex.Message}");
    }
}
```

## Validações

### Backend

O validator `UpdatePedidoCommandValidator` garante:

1. ID do pedido é obrigatório e não pode ser vazio
2. Status do pedido é obrigatório e deve ser um valor válido do enum
3. Lista de itens é obrigatória e não pode estar vazia
4. Cada item deve ter:
   - ID do item obrigatório e não vazio
   - Quantidade mínima de 1

### Frontend

O modelo `UpdateOrderItemDto` usa atributos de validação:

```csharp
public class UpdateOrderItemDto
{
    [JsonPropertyName("itemId")]
    public Guid ItemId { get; set; }
    
    [JsonPropertyName("quantidade")]
    public int Quantidade { get; set; }
}
```

## Exemplos de Cenários

### 1. Atualizar apenas o status

```json
{
  "itens": [
    {
      "itemId": "abc123",
      "quantidade": 1
    }
  ],
  "status": 2
}
```

### 2. Atualizar itens e status

```json
{
  "itens": [
    {
      "itemId": "abc123",
      "quantidade": 2
    },
    {
      "itemId": "def456",
      "quantidade": 1
    }
  ],
  "status": 1
}
```

### 3. Cancelar pedido

```json
{
  "itens": [
    {
      "itemId": "abc123",
      "quantidade": 1
    }
  ],
  "status": 4
}
```

## Notas Importantes

1. **Regras de Negócio**: O backend mantém as regras de negócio existentes, como:
   - Apenas um sanduíche por pedido
   - Apenas um acompanhamento por pedido
   - Apenas uma bebida por pedido
   - Descontos baseados na combinação de itens

2. **Transições de Status**: O método `AtualizarStatus` na entidade `Pedido` permite qualquer transição de status, diferentemente do método `MudarStatus` que valida as transições permitidas.

3. **Recálculo Automático**: Ao atualizar os itens, o sistema recalcula automaticamente:
   - Subtotal
   - Desconto (baseado nas regras de negócio)
   - Total

## Arquivos Modificados

### Backend

1. `backend/Aplicacao/Commands/UpdatePedido/UpdatePedidoCommand.cs`
2. `backend/Aplicacao/Commands/UpdatePedido/UpdatePedidoCommandValidator.cs`
3. `backend/Aplicacao/Commands/UpdatePedido/UpdatePedidoCommandHandler.cs`
4. `backend/Aplicacao/Commands/UpdatePedido/UpdatePedidoCommandResponse.cs`
5. `backend/Dominio/Entidades/Pedido.cs`

### Frontend

1. `GoodHamburger.Web/Components/Models/Pedido.cs`
2. `GoodHamburger.Web/Services/OrderService.cs`
3. `GoodHamburger.Web/Components/Pages/Cart.razor`

## Testes

Para testar a funcionalidade:

1. Crie um pedido via POST `/api/pedidos`
2. Atualize o pedido via PUT `/api/pedidos/{id}`
3. Verifique a resposta contendo o novo status e itens atualizados
4. Confirme que os cálculos de subtotal, desconto e total estão corretos

## Suporte

Para dúvidas ou problemas, entre em contato com a equipe de desenvolvimento.
