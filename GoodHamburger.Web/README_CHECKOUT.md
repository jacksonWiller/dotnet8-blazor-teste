# Instruções para Testar a Tela de Checkout

## 1. Executar a Aplicação

Abra o terminal no diretório do projeto e execute:

```bash
dotnet run
```

A aplicação será iniciada e você verá uma URL como:
```
https://localhost:5001
```

## 2. Acessar a Tela de Checkout

Você pode acessar a tela de checkout de duas formas:

### Opção 1: Navegar diretamente
Digite na barra de endereço do navegador:
```
https://localhost:5001/checkout
```

### Opção 2: Adicionar ao menu de navegação
Se desejar adicionar um link no menu, edite o arquivo `NavMenu.razor` e adicione:

```razor
<li class="nav-item px-2">
    <NavLink class="nav-link" href="checkout">
        <span class="bi bi-cart"></span> Checkout
    </NavLink>
</li>
```

## 3. Funcionalidades para Testar

### ✅ Alerta de Item Duplicado
- No topo da página, há um alerta vermelho sobre item duplicado
- Clique no "X" para fechar o alerta

### ✅ Controle de Quantidade
- Para cada item, você pode:
  - Clicar no **+** para aumentar quantidade
  - Clicar no **-** para diminuir quantidade
  - Ver o total ser recalculado automaticamente

### ✅ Remover Item
- Clique em "Remove" para excluir um item do carrinho
- Os totais serão recalculados

### ✅ Botão Continuar Comprando
- Clique no botão tracejado para navegar mais opções

### ✅ Resumo do Pedido
- Subtotal: Soma de todos os itens
- Desconto: 20% se houver combo deal
- Taxa de entrega: $3.50 fixo
- Total: Cálculo automático

### ✅ Responsividade
- Abra o DevTools do navegador (F12)
- Ative o modo de dispositivo móvel
- Teste em diferentes tamanhos de tela

## 4. Observações Importantes

### Imagens
As imagens estão configuradas como placeholders. Para usar imagens reais:
1. Adicione as imagens na pasta `wwwroot/images/`
2. Atualize o arquivo `Checkout.razor` com os nomes corretos dos arquivos

### Dados
Os dados do carrinho estão hardcoded para demonstração. Para produção:
- Implemente um serviço de carrinho
- Conecte com API backend
- Adicione persistência (localStorage/sessionStorage)

### Ícones
Os ícones são do Bootstrap Icons, carregados via CDN no arquivo CSS.

## 5. Solução de Problemas

### Erro 404 na página
Verifique se o arquivo `Checkout.razor` está na pasta `Components/Pages/`

### Estilos não aplicados
Verifique se o arquivo `Checkout.razor.css` está na mesma pasta que o `.razor`

### Erro de compilação
Execute:
```bash
dotnet build
```

### Imagens não aparecem
Verifique se as imagens estão na pasta `wwwroot/images/` com os nomes corretos

## 6. Próximos Passos Sugeridos

1. **Personalizar dados**: Altere os itens do carrinho no arquivo `Checkout.razor`
2. **Adicionar validação**: Implemente validação de formulário
3. **Integrar backend**: Conecte com API de pedidos
4. **Adicionar animações**: Melhore a experiência do usuário
5. **Testar em produção**: Faça deploy e teste em ambiente real

## 7. Comandos Úteis

```bash
# Executar aplicação
dotnet run

# Build
dotnet build

# Limpar e rebuild
dotnet clean && dotnet build

# Verificar dependências
dotnet restore
```

## 8. Estrutura de Arquivos Criados

```
Components/
├── Pages/
│   ├── Checkout.razor          ← Componente principal
│   └── Checkout.razor.css      ← Estilos CSS
└── Models/
    └── CartItem.cs             ← Modelo de dados

wwwroot/
└── images/                     ← Pasta para imagens

COMPETITION.md                  ← Documentação completa
README_CHECKOUT.md             ← Este arquivo
```

---

**Tempo estimado de teste**: 5-10 minutos

**Nível de dificuldade**: Fácil

**Pré-requisitos**: .NET 8.0 SDK instalado
