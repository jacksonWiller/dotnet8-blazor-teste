
DROP TABLE IF EXISTS "PedidoItem" CASCADE;
DROP TABLE IF EXISTS "Pedido" CASCADE;
DROP TABLE IF EXISTS "Item" CASCADE;

-- =========================================
-- TABELA: PEDIDOS
-- =========================================
CREATE TABLE IF NOT EXISTS "Pedido" (
    "Id" uuid NOT NULL,
    "Subtotal" decimal(18,2) NOT NULL,
    "Desconto" decimal(18,2) NOT NULL,
    "Total" decimal(18,2) NOT NULL,
    "Status" integer NOT NULL DEFAULT 0,
    "DataCriacao" timestamp with time zone NOT NULL,

    CONSTRAINT "PK_Pedido" PRIMARY KEY ("Id")
);

CREATE INDEX IF NOT EXISTS "IX_Pedido_DataCriacao" 
ON "Pedido" ("DataCriacao");


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
    "Removido" boolean NOT NULL DEFAULT FALSE,

    CONSTRAINT "PK_Item" PRIMARY KEY ("Id")
);

CREATE INDEX IF NOT EXISTS "IX_Item_Tipo" 
ON "Item" ("Tipo");

CREATE INDEX IF NOT EXISTS "IX_Item_Categoria" 
ON "Item" ("Categoria");


-- =========================================
-- TABELA: PEDIDO_ITENS
-- =========================================
CREATE TABLE IF NOT EXISTS "PedidoItem" (
    "Id" uuid NOT NULL,
    "PedidoId" uuid NOT NULL,
    "ItemId" uuid NOT NULL,
    "Nome" text NOT NULL,
    "Categoria" text NOT NULL,
    "PrecoUnitario" decimal(18,2) NOT NULL,
    "Quantidade" integer NOT NULL,
    "Total" decimal(18,2) NOT NULL,

    CONSTRAINT "PK_PedidoItem" PRIMARY KEY ("Id"),

    CONSTRAINT "FK_PedidoItem_Pedido_PedidoId" 
        FOREIGN KEY ("PedidoId") 
        REFERENCES "Pedido" ("Id") 
        ON DELETE CASCADE,

    CONSTRAINT "FK_PedidoItem_Item_ItemId" 
        FOREIGN KEY ("ItemId") 
        REFERENCES "Item" ("Id"),

    CONSTRAINT "CK_PedidoItem_Quantidade" 
        CHECK ("Quantidade" > 0)
);

CREATE INDEX IF NOT EXISTS "IX_PedidoItem_PedidoId" 
ON "PedidoItem" ("PedidoId");

CREATE INDEX IF NOT EXISTS "IX_PedidoItem_ItemId" 
ON "PedidoItem" ("ItemId");

-- =========================================
-- DADOS: ITENS
-- =========================================
INSERT INTO public."Item" ("Id","Nome","Descricao","Preco","Tipo","Categoria","UrlImagem","Removido") VALUES
	 ('a55d3b09-2096-4da5-99ef-504354ff63c8'::uuid,'X Egg','Hambúrguer de 180g, queijo cheddar, bacon crocante, cebola caramelizada e BBQ',4.50,'Sanduiches','Sanduíches','https://raw.githubusercontent.com/jacksonWiller/dotnet8-blazor-teste/refs/heads/main/img/sanduiches.jpg',false),
	 ('6ab078b9-0874-44cd-898f-93a7fc4702f4'::uuid,'Batata frita','Porção de 300g de batatas fritas crocantes com sal',2.00,'Batata frita','Acompanhamento','https://raw.githubusercontent.com/jacksonWiller/dotnet8-blazor-teste/refs/heads/main/img/batata-frita.jpg',false),
	 ('9938c548-faa4-494c-ab45-e84871782d6f'::uuid,'X-Burger','Hambúrguer de 180g, queijo cheddar, alface, tomate e molho especial',5.00,'Sanduiches','Sanduíches','https://raw.githubusercontent.com/jacksonWiller/dotnet8-blazor-teste/refs/heads/main/img/sanduiches.jpg',false),
	 ('8f4f71bd-e94a-4487-9f9e-c88e09f3e98c'::uuid,'X Bacon','Hambúrguer de 200g, queijo prato, alface americana, tomate italiano e maionese da casa',7.00,'Sanduiches','Sanduíches','https://raw.githubusercontent.com/jacksonWiller/dotnet8-blazor-teste/refs/heads/main/img/sanduiches.jpg',false),
	 ('96c4b3b3-47e8-44ee-afbd-d52777399e5e'::uuid,'Refrigerante','Lata 350ml bem geladinha',2.00,'Refrigerante','Bebida','https://raw.githubusercontent.com/jacksonWiller/dotnet8-blazor-teste/refs/heads/main/img/refrigerante.jpg',false);
