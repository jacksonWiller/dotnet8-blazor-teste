-- =========================================
-- LIMPEZA DOS DADOS
-- =========================================

DELETE FROM "Iten";
DELETE FROM "Pedido";
DELETE FROM "PedidoItens";

-- =========================================
-- TABELAS
-- =========================================

-- =========================================
-- TABELA: PEDIDOS
-- =========================================
CREATE TABLE IF NOT EXISTS "Pedido" (
    "Id" uuid NOT NULL,
    "Subtotal" decimal(18,2) NOT NULL,
    "Desconto" decimal(18,2) NOT NULL,
    "Total" decimal(18,2) NOT NULL,
    "DataCriacao" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Pedidos" PRIMARY KEY ("Id")
);

CREATE INDEX IF NOT EXISTS "IX_Pedidos_DataCriacao" ON "Pedido" ("DataCriacao");

-- =========================================
-- TABELA: PEDIDO_ITENS
-- =========================================
CREATE TABLE IF NOT EXISTS "PedidoIten" (
    "Id" uuid NOT NULL,
    "PedidoId" uuid NOT NULL,
    "ItemId" uuid NOT NULL,
    "Nome" text NOT NULL,
    "Categoria" integer NOT NULL,
    "PrecoUnitario" decimal(18,2) NOT NULL,
    CONSTRAINT "PK_PedidoItens" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PedidoItens_Pedidos_PedidoId" FOREIGN KEY ("PedidoId") REFERENCES "Pedidos" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_PedidoItens_PedidoId" ON "PedidoItens" ("PedidoId");
CREATE INDEX IF NOT EXISTS "IX_PedidoItens_ItemId" ON "PedidoItens" ("ItemId");


-- =========================================
-- TABELA: ITENS
-- =========================================
CREATE TABLE IF NOT EXISTS "Iten" (
    "Id" uuid NOT NULL,
    "Nome" text NOT NULL,
    "Descricao" text NOT NULL,
    "Preco" decimal(18,2) NOT NULL,
    "Tipo" text NOT NULL,
    "Categoria" text NOT NULL,
    "UrlImagem" text,
    "Removido" boolean NOT NULL,
    CONSTRAINT "PK_Itens" PRIMARY KEY ("Id")
);

CREATE INDEX IF NOT EXISTS "IX_Itens_Tipo" ON "Iten" ("Tipo");
CREATE INDEX IF NOT EXISTS "IX_Itens_Categoria" ON "Iten" ("Categoria");

-- =========================================
-- DADOS: ITENS
-- =========================================
INSERT INTO "Iten" ("Id", "Nome", "Descricao", "Preco", "Tipo", "Categoria", "UrlImagem", "Ativo")
VALUES
  -- Hamburgueres
  (
    'a1a1a1a1-b1b1-c1c1-d1d1-e1e1e1e1e1e1',
    'X-Burger',
    'Hambúrguer de 180g, queijo cheddar, alface, tomate e molho especial',
    25.90,
    'Sanduíches',
    'Sanduíches',
    'https://example.com/images/x-burger-classico.jpg',
    TRUE
  ),
  (
    'a2a2a2a2-b2b2-c2c2-d2d2-e2e2e2e2e2e2',
    'X Egg',
    'Hambúrguer de 180g, queijo cheddar, bacon crocante, cebola caramelizada e BBQ',
    29.90,
    'Sanduíches',
    'Sanduíches',
    'https://example.com/images/x-egg.jpg',
    TRUE
  ),
  (
    'a3a3a3a3-b3b3-c3c3-d3d3-e3e3e3e3e3e3',
    'X Bacon',
    'Hambúrguer de 200g, queijo prato, alface americana, tomate italiano e maionese da casa',
    27.50,
    'Sanduíches',
    'Sanduíches',
    'https://example.com/images/x-bacon.jpg',
    TRUE
  ),
  
  -- Acompanhamentos
  (
    newid(),
    'Batata frita',
    'Porção de 300g de batatas fritas crocantes com sal',
    15.90,
    'Batata frita',
    'Acompanhamento',
    'https://example.com/images/batata-frita.jpg',
    TRUE
  ),
  
  -- Bebidas
  (
    newid(),
    'Refrigerante',
    'Lata 350ml bem geladinha',
    2.50,
    'Refrigerante',
    'Acompanhamento',
    'https://example.com/images/coca-cola.jpg',
    TRUE
  );

