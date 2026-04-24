
-- =========================================
-- TABELA: PEDIDOS
-- =========================================
CREATE TABLE IF NOT EXISTS "Pedido" (
    "Id" uuid NOT NULL,
    "Subtotal" decimal(18,2) NOT NULL,
    "Desconto" decimal(18,2) NOT NULL,
    "Total" decimal(18,2) NOT NULL,
    "DataCriacao" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Pedido" PRIMARY KEY ("Id")
);

CREATE INDEX IF NOT EXISTS "IX_Pedido_DataCriacao" ON "Pedido" ("DataCriacao");

-- =========================================
-- TABELA: PEDIDO_ITENS
-- =========================================
CREATE TABLE IF NOT EXISTS "PedidoItem" (
    "Id" uuid NOT NULL,
    "PedidoId" uuid NOT NULL,
    "ItemId" uuid NOT NULL,
    "Nome" text NOT NULL,
    "Categoria" integer NOT NULL,
    "PrecoUnitario" decimal(18,2) NOT NULL,
    CONSTRAINT "PK_PedidoItem" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PedidoItem_Pedido_PedidoId" FOREIGN KEY ("PedidoId") REFERENCES "Pedido" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_PedidoItem_PedidoId" ON "PedidoItem" ("PedidoId");
CREATE INDEX IF NOT EXISTS "IX_PedidoItem_ItemId" ON "PedidoItem" ("ItemId");


-- =========================================
-- TABELA: ITENS
-- =========================================
CREATE TABLE IF NOT EXISTS "Item" (
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

CREATE INDEX IF NOT EXISTS "IX_Itens_Tipo" ON "Item" ("Tipo");
CREATE INDEX IF NOT EXISTS "IX_Itens_Categoria" ON "Item" ("Categoria");

-- =========================================
-- DADOS: ITENS
-- =========================================
INSERT INTO "Item" ("Id", "Nome", "Descricao", "Preco", "Tipo", "Categoria", "UrlImagem", "Removido")
VALUES
  -- Hamburgueres
  (
    gen_random_uuid(),
    'X-Burger',
    'Hambúrguer de 180g, queijo cheddar, alface, tomate e molho especial',
    25.90,
    'Sanduíches',
    'Sanduíches',
    'https://example.com/images/x-burger-classico.jpg',
    TRUE
  ),
  (
    gen_random_uuid(),
    'X Egg',
    'Hambúrguer de 180g, queijo cheddar, bacon crocante, cebola caramelizada e BBQ',
    29.90,
    'Sanduíches',
    'Sanduíches',
    'https://example.com/images/x-egg.jpg',
    TRUE
  ),
  (
    gen_random_uuid(),
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
    gen_random_uuid(),  
    'Batata frita',
    'Porção de 300g de batatas fritas crocantes com sal',
    15.90,
    'Batata frita',
    'Acompanhamento',
    'https://example.com/images/batata-frita.jpg',
    FALSE
  ),
  
  -- Bebidas
  (
    gen_random_uuid(),
    'Refrigerante',
    'Lata 350ml bem geladinha',
    2.50,
    'Refrigerante',
    'Acompanhamento',
    'https://example.com/images/coca-cola.jpg',
    FALSE
  );