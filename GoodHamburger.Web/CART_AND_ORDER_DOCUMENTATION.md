# GoodHamburger - Telas de Carrinho e Pedido

## 📋 Visão Geral

Esta documentação descreve as novas telas implementadas para o sistema de e-commerce do GoodHamburger.

## 🛒 Tela de Carrinho (`/cart`)

### Funcionalidades

- **Visualização de Itens**: Lista todos os itens adicionados ao carrinho com imagem, descrição e preço
- **Controle de Quantidade**: 
  - Botão `+` para aumentar quantidade
  - Botão `-` para diminuir quantidade (mínimo 1)
- **Remoção de Itens**: Botão de lixeira para remover itens do carrinho
- **Resumo do Pedido**:
  - Subtotal
  - Descontos aplicáveis (combos)
  - Taxa de entrega
  - Total final
- **Carrinho Vazio**: Mensagem amigável com botão para continuar comprando

### Componentes

```razor
@page "/cart"
@rendermode InteractiveServer
```

### Estados

- **Carrinho com itens**: Exibe lista de produtos e resumo
- **Carrinho vazio**: Exibe mensagem de carrinho vazio com CTA

### Cálculos

```csharp
subtotal = Σ (preço × quantidade)
discount = subtotal × 0.20 (se houver 2+ combo deals)
total = subtotal - discount + deliveryFee
```

## 📦 Tela de Detalhe do Pedido (`/order-detail/{orderId}`)

### Funcionalidades

- **Informações do Pedido**: Número, data e status atual
- **Timeline de Status**: 
  - ✅ Confirmado
  - 🍴 Preparando
  - 🛵 Entregando (ativo)
  - 🏠 Desfrutar
- **Estimativa de Entrega**: Tempo restante em minutos
- **Lista de Itens**: Todos os itens do pedido com quantidades
- **Resumo Financeiro**: Detalhamento completo dos custos
- **Ações**:
  - Rastrear pedido em tempo real
  - Baixar recibo
  - Editar pedido
  - Cancelar pedido

### Componentes

```razor
@page "/order-detail/{orderId:int}"
@rendermode InteractiveServer
```

### Parâmetros

- `OrderId`: Número inteiro do pedido (ex: 88291)

### Estados da Timeline

| Estado | Classe CSS | Ícone |
|--------|-----------|-------|
| Confirmado | `done` | ✓ |
| Preparando | `done` | 🍴 |
| Entregando | `active` | 🛵 |
| Desfrutar | - | 🏠 |

## 🎨 Padrões de Design

### Cores

- **Primária**: `#ef4444` → `#dc2626` (gradiente vermelho)
- **Secundária**: `#fbbf24` → `#f59e0b` (gradiente amarelo)
- **Sucesso**: `#10b981` → `#059669` (verde)
- **Fundo**: `#f8f9fa` (cinza claro)
- **Branco**: `#ffffff`

### Tipografia

- **Títulos**: 32px, weight 700
- **Subtítulos**: 24px, weight 700
- **Texto**: 15px, weight 400
- **Pequeno**: 13px, weight 600

### Espaçamento

- **Padding cards**: 20-24px
- **Gap items**: 16-20px
- **Border radius**: 12px (cards), 8px (botões)

### Sombras

```css
box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
```

## 📱 Responsividade

### Desktop (> 1024px)
- Layout em grid 2 colunas (itens + resumo)
- Timeline horizontal com mapa

### Tablet (768px - 1024px)
- Layout em coluna única
- Timeline empilhada

### Mobile (< 768px)
- Elementos empilhados verticalmente
- Botões em largura total
- Tamanhos de fonte reduzidos

## 🔧 Implementação Técnica

### Blazor InteractiveServer

Ambas as páginas utilizam `@rendermode InteractiveServer` para:
- Atualizações em tempo real
- Interação do lado do servidor
- Estado mantido na sessão

### Gerenciamento de Estado

```csharp
// Exemplo: Atualizar quantidade
private void IncreaseQuantity(int itemId)
{
    var item = cartItems.FirstOrDefault(i => i.Id == itemId);
    if (item != null)
    {
        item.Quantity++;
        CalculateTotals();
        SaveCartToSession();
    }
}
```

### Validações

- Quantidade mínima: 1
- Desconto automático para combos (2+ itens)
- Cálculo em tempo real do total

## 🚀 Próximos Passos

1. **Integração com Backend**:
   - API para salvar carrinho
   - Persistência no banco de dados
   - WebSockets para atualizações em tempo real

2. **Funcionalidades Adicionais**:
   - Cupons de desconto
   - Múltiplos endereços de entrega
   - Histórico de pedidos
   - Notificações push

3. **Otimizações**:
   - Lazy loading de imagens
   - Cache do carrinho no localStorage
   - PWA (Progressive Web App)

## 📂 Estrutura de Arquivos

```
Components/
  Pages/
    Cart.razor              ← Tela de carrinho
    OrderDetail.razor       ← Detalhe do pedido
  Models/
    CartItem.cs            ← Modelo de item do carrinho
Layout/
  MainLayout.razor         ← Layout principal (atualizado)
wwwroot/
  css/
    cart.css               ← Estilos do carrinho e pedido
    menu.css               ← Estilos principais
    orders.css             ← Estilos de pedidos
```

## 🎯 URLs das Páginas

- **Carrinho**: `http://localhost:5289/cart`
- **Detalhe do Pedido**: `http://localhost:5289/order-detail/{id}`
- **Home/Menu**: `http://localhost:5289/`

## 📝 Notas Importantes

1. As páginas estão configuradas com dados mockados para demonstração
2. Em produção, substituir por chamadas de API reais
3. Implementar autenticação e autorização
4. Adicionar tratamento de erros robusto
5. Implementar logging e analytics

---

**Versão**: 1.0.0  
**Data**: 23 de abril de 2026  
**Autor**: GoodHamburger Development Team
