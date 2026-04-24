# Good Hamburger - Sistema de Pedidos

## 📋 Visão Geral

Sistema para gerenciamento de pedidos da lanchonete Good Hamburger, com regras de desconto baseadas na combinação de itens.

## 🎯 Cardápio Fixo

O sistema possui apenas 5 itens no cardápio:

| ID | Item | Categoria | Preço |
|----|------|-----------|-------|
| a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1 | X Burger | Sanduíche | R$ 5,00 |
| a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2 | X Egg | Sanduíche | R$ 4,50 |
| a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3 | X Bacon | Sanduíche | R$ 7,00 |
| b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1 | Batata frita | Acompanhamento | R$ 2,00 |
| c1c1c1c1-c1c1-c1c1-c1c1-c1c1c1c1c1c1 | Refrigerante | Bebida | R$ 2,50 |

## 🎁 Regras de Desconto

| Combinação | Desconto | Exemplo |
|------------|----------|---------|
| Sanduíche + Batata + Refrigerante | **20%** | X Burger + Batata + Refri |
| Sanduíche + Refrigerante | **15%** | X Egg + Refri |
| Sanduíche + Batata | **10%** | X Bacon + Batata |
| Apenas Sanduíche | **0%** | X Burger |
| Apenas Batata | **0%** | Batata |
| Apenas Refrigerante | **0%** | Refri |
| Batata + Refrigerante | **0%** | Batata + Refri |

## 📝 Regras de Negócio

1. **Apenas 1 sanduíche** por pedido
2. **Apenas 1 batata** por pedido
3. **Apenas 1 refrigerante** por pedido
4. **Itens duplicados não são permitidos**

## 🔌 Endpoints

### 1. Obter Cardápio

**GET** `/api/pedidos/cardapio`

Retorna todos os itens disponíveis no cardápio.

**Resposta:**
```json
{
  "success": true,
  "message": "Cardápio recuperado com sucesso.",
  "data": [
    {
      "id": "a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1",
      "nome": "X Burger",
      "categoria": "Sanduiche",
      "preco": 5.00
    },
    {
      "id": "a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2",
      "nome": "X Egg",
      "categoria": "Sanduiche",
      "preco": 4.50
    },
    {
      "id": "a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3",
      "nome": "X Bacon",
      "categoria": "Sanduiche",
      "preco": 7.00
    },
    {
      "id": "b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1",
      "nome": "Batata frita",
      "categoria": "Acompanhamento",
      "preco": 2.00
    },
    {
      "id": "c1c1c1c1-c1c1-c1c1-c1c1-c1c1c1c1c1c1",
      "nome": "Refrigerante",
      "categoria": "Bebida",
      "preco": 2.50
    }
  ]
}
```

### 2. Criar Pedido

**POST** `/api/pedidos`

Cria um novo pedido com os itens selecionados.

**Request Body:**
```json
{
  "itensIds": [
    "a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1",
    "b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1",
    "c1c1c1c1-c1c1-c1c1-c1c1-c1c1c1c1c1c1"
  ]
}
```

**Exemplos de Uso:**

#### Exemplo 1: Pedido Completo (20% desconto)
```json
{
  "itensIds": [
    "a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1",
    "b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1",
    "c1c1c1c1-c1c1-c1c1-c1c1-c1c1c1c1c1c1"
  ]
}
```

**Resposta:**
```json
{
  "success": true,
  "message": "Pedido criado com sucesso.",
  "data": {
    "pedidoId": "123e4567-e89b-12d3-a456-426614174000",
    "subtotal": 9.50,
    "desconto": 1.90,
    "total": 7.60,
    "itens": [
      {
        "itemId": "a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1",
        "nome": "X Burger",
        "categoria": "Sanduiche",
        "precoUnitario": 5.00
      },
      {
        "itemId": "b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1",
        "nome": "Batata frita",
        "categoria": "Acompanhamento",
        "precoUnitario": 2.00
      },
      {
        "itemId": "c1c1c1c1-c1c1-c1c1-c1c1-c1c1c1c1c1c1",
        "nome": "Refrigerante",
        "categoria": "Bebida",
        "precoUnitario": 2.50
      }
    ]
  }
}
```

#### Exemplo 2: Sanduíche + Refrigerante (15% desconto)
```json
{
  "itensIds": [
    "a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2",
    "c1c1c1c1-c1c1-c1c1-c1c1-c1c1c1c1c1c1"
  ]
}
```

**Resposta:**
```json
{
  "success": true,
  "message": "Pedido criado com sucesso.",
  "data": {
    "pedidoId": "223e4567-e89b-12d3-a456-426614174001",
    "subtotal": 7.00,
    "desconto": 1.05,
    "total": 5.95,
    "itens": [
      {
        "itemId": "a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2",
        "nome": "X Egg",
        "categoria": "Sanduiche",
        "precoUnitario": 4.50
      },
      {
        "itemId": "c1c1c1c1-c1c1-c1c1-c1c1-c1c1c1c1c1c1",
        "nome": "Refrigerante",
        "categoria": "Bebida",
        "precoUnitario": 2.50
      }
    ]
  }
}
```

#### Exemplo 3: Sanduíche + Batata (10% desconto)
```json
{
  "itensIds": [
    "a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3",
    "b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1"
  ]
}
```

**Resposta:**
```json
{
  "success": true,
  "message": "Pedido criado com sucesso.",
  "data": {
    "pedidoId": "323e4567-e89b-12d3-a456-426614174002",
    "subtotal": 9.00,
    "desconto": 0.90,
    "total": 8.10,
    "itens": [
      {
        "itemId": "a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3",
        "nome": "X Bacon",
        "categoria": "Sanduiche",
        "precoUnitario": 7.00
      },
      {
        "itemId": "b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1",
        "nome": "Batata frita",
        "categoria": "Acompanhamento",
        "precoUnitario": 2.00
      }
    ]
  }
}
```

#### Exemplo 4: Item Duplicado (Erro)
```json
{
  "itensIds": [
    "a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1",
    "a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1"
  ]
}
```

**Resposta de Erro:**
```json
{
  "success": false,
  "message": "Itens duplicados não são permitidos.",
  "errors": [
    {
      "code": "ValidationFailure",
      "detail": "Itens duplicados não são permitidos."
    }
  ]
}
```

#### Exemplo 5: Dois Sanduíches (Erro)
```json
{
  "itensIds": [
    "a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1",
    "a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2"
  ]
}
```

**Resposta de Erro:**
```json
{
  "success": false,
  "message": "Não é permitido adicionar mais de um sanduíche ao pedido.",
  "errors": [
    {
      "code": "ValidationFailure",
      "detail": "Não é permitido adicionar mais de um sanduíche ao pedido."
    }
  ]
}
```

## 🧪 Cenários de Teste

### Cenário 1: Pedido Completo
- **Itens:** X Burger + Batata frita + Refrigerante
- **Subtotal:** R$ 9,50
- **Desconto:** 20% = R$ 1,90
- **Total:** R$ 7,60

### Cenário 2: Sanduíche + Refrigerante
- **Itens:** X Egg + Refrigerante
- **Subtotal:** R$ 7,00
- **Desconto:** 15% = R$ 1,05
- **Total:** R$ 5,95

### Cenário 3: Sanduíche + Batata
- **Itens:** X Bacon + Batata frita
- **Subtotal:** R$ 9,00
- **Desconto:** 10% = R$ 0,90
- **Total:** R$ 8,10

### Cenário 4: Apenas Sanduíche
- **Itens:** X Burger
- **Subtotal:** R$ 5,00
- **Desconto:** 0%
- **Total:** R$ 5,00

### Cenário 5: Batata + Refrigerante
- **Itens:** Batata frita + Refrigerante
- **Subtotal:** R$ 4,50
- **Desconto:** 0%
- **Total:** R$ 4,50

## 🏗️ Arquitetura

O sistema segue os princípios de **Clean Architecture** e **CQRS**:

### Camadas
1. **Dominio** - Entidades, DTOs, Eventos de Domínio
2. **Aplicacao** - Commands, Queries, Handlers, Validators
3. **Infra** - Repositórios, Contexto do Banco de Dados
4. **Api** - Controllers, Middleware

### Padrões Utilizados
- **CQRS** (Command Query Responsibility Segregation)
- **MediatR** para tratamento de commands e queries
- **FluentValidation** para validação
- **Ardalis.Result** para tratamento de resultados

## 📦 Estrutura de Arquivos

```
backend/
├── Dominio/
│   ├── Entidades/
│   │   ├── ItemCardapio.cs
│   │   ├── Pedido.cs
│   │   └── Item.cs (existente)
│   ├── Eventos/
│   │   ├── EventoBase.cs (existente)
│   │   └── PedidoItemAdicionadoEvent.cs
│   ├── Dtos/
│   │   ├── ClienteDto.cs (existente)
│   │   └── PedidoDto.cs
│   └── Interfaces/
│       └── IClienteRepository.cs (existente)
├── Aplicacao/
│   ├── Commands/
│   │   ├── CreateCliente/ (existente)
│   │   └── CriarPedido/
│   │       ├── CriarPedidoCommand.cs
│   │       ├── CriarPedidoCommandResponse.cs
│   │       ├── CriarPedidoCommandValidator.cs
│   │       └── CriarPedidoCommandHandler.cs
│   └── Queries/ (existente)
├── Infra/
│   ├── Contexto/
│   │   └── GoodHamburgerContext.cs (existente)
│   └── Repositorio/
│       └── ClienteRepository.cs (existente)
└── Api/
    ├── Controllers/
    │   ├── ClienteController.cs (existente)
    │   └── PedidosController.cs
    └── Program.cs (existente)
```

## 🚀 Como Executar

1. **Aplicar migrations:**
   ```sql
   -- Executar o arquivo migrations.sql no banco de dados PostgreSQL
   ```

2. **Iniciar a API:**
   ```bash
   cd backend/Api
   dotnet run
   ```

3. **Acessar Swagger:**
   ```
   http://localhost:5000/swagger
   ```

## 📝 Notas Importantes

- O cardápio é **fixo** e não pode ser alterado dinamicamente
- As regras de desconto são aplicadas automaticamente com base na combinação de itens
- O desconto maior tem prioridade (20% > 15% > 10%)
- Mensagens de erro são claras e informativas para o usuário final
