# Tela de Checkout - Burger HQ

## Descrição
Esta tela de checkout foi implementada na aplicação Blazor seguindo fielmente o design da imagem fornecida.

## Funcionalidades Implementadas

### 1. **Lista de Itens do Carrinho**
- Exibição de produtos com imagem, nome, descrição e preço
- Badge "COMBO DEAL" para itens em promoção
- Controle de quantidade (aumentar/diminuir)
- Botão de remover item
- Alerta de item duplicado no topo

### 2. **Resumo do Pedido**
- Subtotal calculado automaticamente
- Desconto de combo (20%) quando aplicável
- Total final com cálculo dinâmico
- Tempo estimado de entrega (25-35 min)

### 3. **Botão de Confirmação**
- Design com gradiente laranja
- Efeito hover com sombra
- Texto de termos de serviço abaixo

### 4. **Método de Pagamento**
- Cartão de crédito exibido (Visa ending in 4421)
- Link para alterar método de pagamento

### 5. **Banner de Fidelidade**
- Design com gradiente amarelo
- Ícone de estrela
- Mensagem sobre pontos de recompensa

### 6. **Botão de Continuar Comprando**
- Estilo com borda tracejada
- Link para navegar mais opções

## Estrutura de Arquivos

### Componentes Criados:
- `Components/Pages/Checkout.razor` - Componente principal da tela
- `Components/Pages/Checkout.razor.css` - Estilos CSS personalizados
- `Components/Models/CartItem.cs` - Modelo de dados do carrinho

### Rota:
- URL: `/checkout`
- Acessível através do menu de navegação ou diretamente pela URL

## Design System

### Cores:
- **Primária**: Laranja (#c2410c a #9a3412) - Gradiente para botões principais
- **Alerta**: Vermelho claro (#fee2e2) - Para mensagens de erro
- **Sucesso**: Verde (#16a34a) - Para descontos
- **Fundo**: Cinza claro (#faf9f8) - Background da página
- **Branco**: (#ffffff) - Cards e resumo

### Tipografia:
- Títulos: 20px, peso 600
- Preços: 16-24px, peso 600-700
- Textos: 12-14px, peso 400-500

### Espaçamento:
- Cards com padding de 24px
- Border radius de 8-12px
- Sombra suave (0 1px 3px rgba(0,0,0,0.1))

## Responsividade

A tela é totalmente responsiva com breakpoints para:
- **Desktop (>992px)**: Layout de duas colunas (itens à esquerda, resumo à direita)
- **Tablet (768px-992px)**: Layout ajustado com resumo abaixo
- **Mobile (<576px)**: Layout de uma coluna com elementos empilhados

## Dados de Exemplo

Os dados são hardcoded para demonstração:
- 2 itens no carrinho (1 combo deal, 1 item normal)
- Subtotal: $59.00
- Desconto: $11.80 (20%)
- Total: $50.70

## Próximos Passos (Sugestões)

1. **Integração com Backend**: Conectar com API de carrinho de compras
2. **Validação de Formulário**: Adicionar validação para checkout
3. **Persistência**: Salvar carrinho no localStorage ou banco de dados
4. **Autenticação**: Verificar se usuário está logado
5. **Múltiplos Métodos de Pagamento**: Permitir seleção de diferentes cartões
6. **Endereço de Entrega**: Adicionar formulário de endereço
7. **Cupons de Desconto**: Implementar sistema de cupons
8. **Acessibilidade**: Melhorar contraste e navegação por teclado

## Como Testar

1. Execute a aplicação: `dotnet run`
2. Navegue para: `https://localhost:5001/checkout`
3. Teste as funcionalidades:
   - Aumentar/diminuir quantidade
   - Remover itens
   - Verificar cálculo automático de totais
   - Testar responsividade (F12 > Device Toolbar)

## Dependências

- Bootstrap 5 (já incluído no projeto)
- Bootstrap Icons (via CDN)
- .NET 8.0
