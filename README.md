# 🍔 GoodHamburger — Teste Técnico .NET

> Sistema de pedidos de hamburguer desenvolvido como **teste técnico**, utilizando **.NET 8**, **Blazor Server**, **PostgreSQL** e **Docker**, seguindo os princípios de **DDD** e **CQRS**.

---

## 📌 Sobre o Desafio

Este projeto foi desenvolvido como resposta a um teste técnico que solicitava a criação de uma API REST para gerenciamento de pedidos de uma hamburgueria fictícia, a **GoodHamburger**. O sistema permite montar pedidos com itens do cardápio (sanduíches, acompanhamentos e bebidas), aplicando regras de desconto automáticas com base na combinação de itens escolhidos.

### Regras de Negócio

| Combinação | Desconto |
|---|---|
| Sanduíche + Batata + Refrigerante | **20%** no total |
| Sanduíche + Refrigerante | **15%** no total |
| Sanduíche + Batata | **10%** no total |

- Apenas **1 sanduíche**, **1 acompanhamento** e **1 bebida** por pedido.
- Pedidos sem sanduíche não recebem desconto.

---

## 🛠️ Tecnologias

### Back-end
| Tecnologia | Versão | Descrição |
|---|---|---|
| .NET | 8.0 | Framework principal |
| Entity Framework Core | 8.0 | ORM para PostgreSQL |
| PostgreSQL | 12 | Banco de dados relacional |
| MediatR | 12.x | Padrão Mediator / CQRS |
| FluentValidation | 11.x | Validação de comandos |
| Ardalis.Result | 5.x | Padronização de respostas |
| Swagger | - | Documentação interativa da API |

### Front-end
| Tecnologia | Descrição |
|---|---|
| Blazor Server (.NET 8) | Interface web interativa server-side |
| Bootstrap 5 | Estilização e responsividade |

### DevOps
| Tecnologia | Descrição |
|---|---|
| Docker | Containerização |
| Docker Compose | Orquestração dos serviços |

---

## 🚀 Como Executar

### Pré-requisitos

- **Docker** 20.10+
- **Docker Compose** 2.0+

### Passo a passo

#### 1. Clone o repositório

```bash
git clone https://github.com/jacksonWiller/dotnet8-blazor-teste.git
cd dotnet8-blazor-teste
```

#### 2. Suba os containers

```bash
docker-compose up -d
```

> Na primeira execução o Docker irá compilar as imagens e inicializar o banco. Aguarde cerca de 30 segundos para tudo estar pronto.

#### 3. Acesse a aplicação

| Serviço | URL | Descrição |
|---|---|---|
| 🍔 **Front-end Blazor** | http://localhost:5000 | Interface web do sistema |
| 🔌 **API REST** | http://localhost:8090 | Back-end .NET 8 |
| 📚 **Swagger** | http://localhost:8090/swagger | Documentação interativa da API |
| 🗄️ **PostgreSQL** | localhost:5434 | Banco de dados (usuário: `postgres`, senha: `postgres`) |

#### 4. Parar os containers

```bash
docker-compose down
```

#### 5. Limpeza completa (remove dados do banco)

```bash
docker-compose down -v
```

### Troubleshooting

```bash
# Ver logs da API
docker-compose logs -f api

# Ver logs do Blazor
docker-compose logs -f blazor

# Ver logs de inicialização do banco
docker-compose logs db-migration

# Reiniciar todos os serviços
docker-compose restart
```

---

## 📁 Estrutura do Projeto

```
📦 dotnet8-blazor-teste/
├── 📂 backend/                        # Back-end .NET 8
│   ├── 📂 Api/                        # Camada de entrada — Controllers, Dockerfile
│   ├── 📂 Aplicacao/                  # Camada de Aplicação — Commands, Queries (CQRS)
│   ├── 📂 Dominio/                    # Camada de Domínio — Entidades, Regras, Interfaces
│   ├── 📂 Infra/                      # Camada de Infraestrutura — EF Core, Repositórios
│   └── 📂 Testes/                     # Testes unitários (xUnit)
│
├── 📂 GoodHamburger.Web/              # Front-end Blazor Server
│   ├── 📂 Components/                 # Componentes Razor reutilizáveis
│   ├── 📂 Pages/                      # Páginas: Home, Cart, Checkout, Orders
│   └── 📂 Services/                   # Serviços HTTP que consomem a API
│
├── 📂 Scripts/                        # Scripts SQL (migrations + seed)
├── 🐳 docker-compose.yml              # Orquestração dos 4 serviços
└── 📄 README.md
```

---

## 🏛️ Decisões de Arquitetura

### Por que DDD?

O projeto utiliza **Domain-Driven Design** para colocar as **regras de negócio no centro** da solução. A entidade `Pedido` encapsula toda a lógica de adição de itens, validação de restrições (máximo 1 sanduíche, 1 acompanhamento, 1 bebida) e cálculo automático de descontos — sem expor essa lógica para as camadas externas.

```
Dominio (núcleo)
  └── Entidades: Pedido, Item, PedidoItem
  └── Regras encapsuladas: AdicionarItem(), CalcularDesconto()
  └── Interfaces: IPedidoRepository, IItemRepository

Aplicacao
  └── Orquestra os casos de uso via Commands/Queries

Infra
  └── Implementa os repositórios com EF Core

Api
  └── Recebe HTTP, delega para MediatR
```

### Por que CQRS?

O **Command Query Responsibility Segregation** separa explicitamente as operações de **escrita** (Commands) das de **leitura** (Queries), tornando cada caso de uso isolado, testável e de fácil manutenção:

| Tipo | Exemplos |
|---|---|
| **Commands** | `CriarPedido`, `UpdatePedido`, `DeletePedido` |
| **Queries** | `GetAllPedidos`, `GetPedidoById`, `GetAllItems`, `GetItemById` |

Cada Command/Query possui seu próprio Handler e Validator, sem acoplamento entre eles.

### O que ficou de fora

Por se tratar de um teste técnico com escopo definido, os seguintes itens **não foram implementados**:

- ❌ Autenticação / Autorização (JWT)
- ❌ Testes de integração (apenas unitários)
- ❌ Cache de queries
- ❌ Paginação no front-end Blazor
- ❌ CI/CD pipeline completo (apenas workflow básico no GitHub Actions)

---

## 🔌 API Endpoints

| Método | Endpoint | Descrição |
|---|---|---|
| `GET` | `/api/itens` | Listar todos os itens do cardápio |
| `GET` | `/api/itens/{id}` | Buscar item por ID |
| `GET` | `/api/pedidos` | Listar todos os pedidos |
| `GET` | `/api/pedidos/{id}` | Buscar pedido por ID |
| `POST` | `/api/pedidos` | Criar novo pedido |
| `PUT` | `/api/pedidos/{id}` | Atualizar pedido |
| `DELETE` | `/api/pedidos/{id}` | Remover pedido |

### Exemplo — Criar Pedido

```bash
curl -X POST http://localhost:8090/api/pedidos \
  -H "Content-Type: application/json" \
  -d '{
    "itensIds": [
      "id-do-sanduiche",
      "id-da-batata",
      "id-do-refrigerante"
    ]
  }'
```

**Resposta de sucesso:**
```json
{
  "id": "guid-do-pedido",
  "subtotal": 35.00,
  "desconto": 7.00,
  "total": 28.00,
  "status": "Pendente"
}
```

---

## 🧪 Testes

Os testes unitários cobrem as **regras de negócio do domínio** (entidade `Pedido`):

```bash
cd backend
dotnet test
```

### O que é testado

- Adição de itens ao pedido
- Restrição de duplicidade por categoria
- Cálculo correto dos descontos (10%, 15%, 20%)
- Casos sem desconto

---

## 🗄️ Banco de Dados

O banco é inicializado automaticamente pelo serviço `db-init` no Docker Compose, que executa:

1. `Scripts/migrations.sql` — Criação das tabelas `Pedido`, `Item`, `PedidoItem`
2. `Scripts/insert.sql` — Seed com os itens do cardápio

### Schema simplificado

```
Pedido (Id, Subtotal, Desconto, Total, Status, DataCriacao)
  └── PedidoItem (Id, PedidoId, ItemId, Nome, Categoria, PrecoUnitario)

Item (Id, Nome, Descricao, Preco, Tipo, Categoria, UrlImagem, Removido)
```

---

## 👨‍💻 Desenvolvedor

Desenvolvido por **Jackson Willer** como resposta ao teste técnico .NET da **Stgen**.

- GitHub: [@jacksonWiller](https://github.com/jacksonWiller)
