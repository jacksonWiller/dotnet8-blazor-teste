# 🍔 GoodHamburger — Teste Técnico .NET

> Sistema de pedidos de hamburguer desenvolvido como **teste técnico**, utilizando **.NET 8**, **Blazor Server**, **PostgreSQL** e **Docker**, seguindo os princípios de **DDD** e **CQRS**.

---

## �️ Telas da Aplicação

Abaixo estão detalhadas todas as telas do sistema, com capturas de tela e os endpoints da API utilizados em cada uma:

### 🏠 Tela Inicial (Menu/Cardápio)

**URL:** `/`

**Descrição:** Tela principal que exibe o cardápio completo da hamburgueria, organizado por categorias (Sanduíches, Acompanhamentos, Bebidas e Sobremesas). Os usuários podem navegar entre as categorias, visualizar os produtos com suas imagens, nomes, descrições e preços. Cada produto pode ser adicionado ao carrinho de compras com um único clique.

**Funcionalidades:**
- Exibição de produtos filtrados por categoria
- Adição de itens ao carrinho
- Cálculo em tempo real do total do carrinho
- Notificações visuais ao adicionar itens

**Endpoints da API utilizados:**
| Endpoint | Método | Descrição |
|---|---|---|
| `GET /api/itens` | GET | Lista todos os itens do cardápio disponíveis |

**Captura de tela:**
![Menu/Cardápio](https://raw.githubusercontent.com/jacksonWiller/dotnet8-blazor-teste/refs/heads/main/img/menu.png)

---

### 🛒 Tela do Carrinho

**URL:** `/cart`

**Descrição:** Tela que exibe todos os itens adicionados pelo usuário, permitindo ajustar quantidades, remover itens e visualizar o resumo do pedido com subtotal, descontos aplicados, taxa de entrega e total. O usuário pode confirmar o pedido para prosseguir para o checkout.

**Funcionalidades:**
- Listagem de itens adicionados com imagens
- Controle de quantidade por item (+/-)
- Remoção de itens do carrinho
- Cálculo automático de descontos baseado nas regras de negócio
- Visualização do subtotal, desconto, taxa de entrega e total
- Confirmação do pedido

**Endpoints da API utilizados:**
| Endpoint | Método | Descrição |
|---|---|---|
| `POST /api/pedidos` | POST | Cria um novo pedido com os itens do carrinho |
| `GET /api/pedidos/{id}` | GET | Busca detalhes do pedido criado para exibição |

**Captura de tela:**
![Carrinho de Compras](https://raw.githubusercontent.com/jacksonWiller/dotnet8-blazor-teste/refs/heads/main/img/cart.png)

---

### ✅ Tela de Checkout/Confirmação

**URL:** `/checkout` ou `/checkout/{OrderId}`

**Descrição:** Tela exibida após a confirmação do pedido. Mostra a mensagem de sucesso com os detalhes do pedido criado (ID e total) e oferece a opção de realizar um novo pedido. Também permite visualizar detalhes de pedidos existentes caso um OrderId seja passado na URL.

**Funcionalidades:**
- Mensagem de confirmação de pedido criado
- Exibição do ID do pedido e valor total
- Opção de realizar novo pedido
- Visualização de detalhes de pedidos existentes

**Endpoints da API utilizados:**
| Endpoint | Método | Descrição |
|---|---|---|
| `POST /api/pedidos` | POST | Cria o pedido confirmado no carrinho |
| `GET /api/pedidos/{id}` | GET | Busca detalhes do pedido para exibição |

**Captura de tela:**
![Checkout/Confirmação](https://raw.githubusercontent.com/jacksonWiller/dotnet8-blazor-teste/refs/heads/main/img/checkout.png)

---

### 📋 Tela de Listagem de Pedidos

**URL:** `/orders`

**Descrição:** Tela que exibe uma tabela com todos os pedidos realizados, mostrando informações resumidas como ID do pedido, itens, valor total, data de criação e status. Permite ao usuário visualizar todos os seus pedidos e acessar os detalhes de cada um.

**Funcionalidades:**
- Listagem paginada de todos os pedidos
- Exibição de resumo dos itens de cada pedido
- Formatação de data e valores monetários
- Navegação para detalhes do pedido
- Estatísticas de pedidos

**Endpoints da API utilizados:**
| Endpoint | Método | Descrição |
|---|---|---|
| `GET /api/pedidos` | GET | Lista todos os pedidos com paginação |
| `GET /api/pedidos/{id}` | GET | Busca detalhes completos de um pedido específico |

**Captura de tela:**
![Listagem de Pedidos](https://raw.githubusercontent.com/jacksonWiller/dotnet8-blazor-teste/refs/heads/main/img/orders.png)

---

### 📊 Tela de Detalhes do Pedido

**URL:** `/orders/{OrderId}` ou `/order/{OrderId}`

**Descrição:** Tela que exibe informações detalhadas de um pedido específico, incluindo linha do tempo do status do pedido, lista completa de itens com quantidades e preços, resumo financeiro com subtotal, descontos e total. Oferece opções para rastrear o pedido ou realizar novos pedidos.

**Funcionalidades:**
- Visualização completa do pedido
- Linha do tempo com status do pedido (Confirmado, Preparando, Pronto para entrega)
- Lista detalhada de todos os itens
- Resumo financeiro completo
- Botão para rastrear pedido
- Navegação para listagem de pedidos

**Endpoints da API utilizados:**
| Endpoint | Método | Descrição |
|---|---|---|
| `GET /api/pedidos/{id}` | GET | Busca todos os detalhes do pedido específico |

**Captura de tela:**
![Detalhes do Pedido](https://raw.githubusercontent.com/jacksonWiller/dotnet8-blazor-teste/refs/heads/main/img/order.png)

---


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
