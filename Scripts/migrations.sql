
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
    'https://teste-jackson-duarte.s3.us-east-1.amazonaws.com/x-burger.jpg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Credential=ASIA3FLDZJ6LIFVVVCQZ%2F20260424%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20260424T085043Z&X-Amz-Expires=300&X-Amz-Security-Token=IQoJb3JpZ2luX2VjELH%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEaCXVzLWVhc3QtMSJHMEUCIQDATM1Ov45dUK5s31h%2FvM%2Fwlt6bRLdC%2F4SPmKvOc6zX8gIgfuZoG6zgOwOIIDx8Pz%2FaWqjwO7mjLOhC%2BKLisYPSH%2Fkq2gIIehAAGgw3NjczOTc4MTAwNzAiDAmhslJGr7T2DX99%2FSq3AqG9jn3doYxjjrtcDVxY3azDaoWQ6%2Fs6fgNvWyrvErNcXeUfrf8pCHGyDKjDXyUZ4n75JOEyPNkjT8yluXQ8kdYao%2FIWQBxefgK0ei7fty4IKNVOPlpBYbVXba9G3SaSHFXWtWrAP4SWILLYOz3kzVhw3GVWE5OwEcHfsMurPPwwhGXAe0HhAZHHoDZ90lzcnql6bJ6u%2BRlUdRaawpc3HLC0mlgKQ5oKG5fEfqsCZyqaPYHMBU1FujWFaUvsdB5GqUoi62fYInAycTgJWgG8lYybE4nEFNAOarhfoMX%2Btja7%2B6uWKpY8vR76MT3zYnYSpYLdBilIKyaeydcbKd96nMq7%2FmNfIqN%2BfG3HgAQNKRfvSnuDw3jALbo%2F6HNyuO0ym5fgeLrDb1cKHIRXwEa8HSv5ng236TM4MNTXrM8GOq0CXk1BszQdUncKBBpOi2RPUfufnaS1Zea9ZnSjf1BxykoNAtL3LS4iAX2lGaOiJRDgz4JxUgDRDdNpSz8lQy5wKt1%2FRHBrMcbcXcFtDow3C7m4BRLLlHX2HYYwOrgfe7MDn1BJlDv%2FjYvtBCKrrCvHwi%2BBc5F4lbf%2BVjgxgrg7E3D0a1xndpajv8vNvZWOeuXgZRZzig1iv3Uh1Wzv3JJltCLzE9a2p1Q4ZX3hUsb0uLHoPkZRWCVlAewHy88QZfCL4ihXh4KdR%2BxM0qG2lFPU3o4noSAaQYKhxyYjIjdZ1t8Mx%2FAd%2Bv3SL2uMUIg7WT2zJl95WpyCnqlbA%2FiTMXAVCb0ufcoqi%2FfpPfvdT7ZceI0ZRWwFiHz02EMTKHdEUCnxc9KCLOcOnWsaNi4SyQ%3D%3D&X-Amz-Signature=10d715a90d6d0f536748b8012d4a3211a44843948d643df74fc355667e4fc233&X-Amz-SignedHeaders=host&response-content-disposition=inline',
    TRUE
  ),
  (
    gen_random_uuid(),
    'X Egg',
    'Hambúrguer de 180g, queijo cheddar, bacon crocante, cebola caramelizada e BBQ',
    29.90,
    'Sanduíches',
    'Sanduíches',
    'https://teste-jackson-duarte.s3.us-east-1.amazonaws.com/x-egg.jpeg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Credential=ASIA3FLDZJ6LAJ4DQBRY%2F20260424%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20260424T085003Z&X-Amz-Expires=300&X-Amz-Security-Token=IQoJb3JpZ2luX2VjELH%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEaCXVzLWVhc3QtMSJGMEQCIFAdTkksFe%2FsmS3ucWagObk4DyAj%2BjsRNazDkeXsuxCxAiAI7zZFItE8twLQgK9BNIHo3bkamOpheldL%2FzHJomTiESraAgh6EAAaDDc2NzM5NzgxMDA3MCIMTwl%2Br3Nxe6IJchEvKrcC6DF3AN8Vdc91fzKEwr0JHN%2Ftk7Wn%2BzDKZpsGMbAQ4NpKq1Pime7KWfZ4d%2ByhEthPl1NWqrrqFrEZVNMsfYJUjc9f5WmicfCt56J%2BwCESmu62yHTfFmdE%2Bd2Esmvrplt%2BBfY0wNkEtOtMY3EsJHkHQJKiaByLkvguRzgrzP6g3h%2BSSkeXWVDTYcSmWa%2Fs2m8KPE22tuhtPVrN5dIBokzKUIusMW5Xkg3xaXyjMKJ9RWCwGP3DUZCfW4Az2MBKcGMp13OsfoDZDSgVZe6hcDSdkrfkHeD90zxRqFZgB9j4zoreDSz8E7lSP5iytCgJWMt9sPqF20Wo1JyjWl80f0AjqIT%2FDCAsYeodm3UzofB1JUIqQK1ze1B9fkNrPbdY8x7IhtCpbaTQ5RGg0OJNANue3rlvMk4oPV8w1NeszwY6rgKksfQSdstLxlay83TEMzVLCH0I7LhXtCnomQsTBmsofyNa3MIDRQ%2BBGQgdsST7YMIIe7b%2BrUmhm4owcHshqizm8Rf20DE8P%2BNNy6AZGy6btS6E2PULAduIy3R00%2B6SrMqqeT0h5G7D5ZIQnDswyeV0jkeIxnI5XtO6f%2BcnqdSq7punGPFJjXcVxSlqTLPPy%2FKdlMY4ViANv0BMacFe8QFF1U1qjQkJigDWqttFPYFtd7KzWraOlMtypxLWaL0egZXp0DB48igfM9omGM3ujy6B2ckYC9TQH0t8m4qIAjktK7T8UuUBGv%2BMtIqmkf8FoTy2MY1nK7PweguYar5JyMfuc%2BrlF47W2kqvuqEiQE47hGjb0sICqKlTfq2HBFDoIHBBMEHuDc0pkXpo5nXPCg%3D%3D&X-Amz-Signature=866eff1682cc5005c8f02a08778f7af50376f4bfd2fdd33ca337db088c67df50&X-Amz-SignedHeaders=host&response-content-disposition=inline',
    TRUE
  ),
  (
    gen_random_uuid(),
    'X Bacon',
    'Hambúrguer de 200g, queijo prato, alface americana, tomate italiano e maionese da casa',
    27.50,
    'Sanduíches',
    'Sanduíches',
    'https://teste-jackson-duarte.s3.us-east-1.amazonaws.com/x-bacon.jpg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Credential=ASIA3FLDZJ6LA5XI7I5P%2F20260424%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20260424T084927Z&X-Amz-Expires=300&X-Amz-Security-Token=IQoJb3JpZ2luX2VjELH%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEaCXVzLWVhc3QtMSJIMEYCIQCvYZdpF6ZqO3bV7oM4uNMtHbkSR8NQcJwnbDz49Gv64gIhAM3i26KGfwDmUyOWXYnnAkDfbwQPgSuuezvdz6qqXdSQKtoCCHoQABoMNzY3Mzk3ODEwMDcwIgz04X%2FjSW6nEgdsvbEqtwLVEpOPQzXgrnW9AGT2QTlbf2ff3eY3FbSAI%2B79I%2Bxm902gCJOVUNuXdRP7yUHpzdB9m2HfcMczLPmXTBG88hbKyrokQpASjZTd791G1NqByiBOr%2FA%2FYNU2MdqqlcZSjG712cWz7qqzhJA%2BsWsdclXV1LJaq8MGPzQ3bzjvm9JvmWasbNOq1kOY4NrJVG%2BnMsz3oUuWgND4i3lqYrkXXvIO2NYiPnANkkgPruJIVSbAp%2B6S8GalQ7URlq3bpkrhkyhAjJo1D3EvMfUXtulRlC1iio5Yv1BxDo%2FdGZpbX9oNj77rVGqZF8tLPdvmjeTIAMnOwn6ZyWnaM%2BFIbCMGDw2FBf24A%2FTqd5LocVZpwGHQ2Xo314QDrltBvfHFlFlSF4Jej5iDngY3bnVdcMWi2cKkorxP5lyWgjDU16zPBjqsAgR6KoW7C4IpcA1Zfjbrc6%2FzE%2FEQdBjP%2BWvEAMxc0mJbiQshKvftJEQx1Bicw%2FhKURTsFd9l3XOPZ5XHuxe2wymeE8GboyDr0u79BuVO37G1Z4W0iBf50zYlHzbhtSxcem1PijSs5eYHXIJu5DutjSApoDQTG18qUUuLjK%2BdnQn1%2FGcPMP6osIhWLtRLpzsAvHkxCRoanMvV7JE0Im2pHpkAO3ifZcY2UklLWPDNHrVY5DaxfAp5kw2iThVuilorcAYfopJOBnfmAxuYkAScFHrCPBWWmHNtxUb7xemhSnTKsPEXW4ISdUzZdMSzoLGFo1YkiTb6kVpY0zEauVhVPkW2CouSBL36sI%2FnlonVTJ8JFFCZgD159ONWfl9oxu4vA7wcD8q2fuAz7pv7Jw%3D%3D&X-Amz-Signature=f6292fb7946376378786ad070bf4341bf3c101c9e7f4ac9f6d4a3e0327d6bfca&X-Amz-SignedHeaders=host&response-content-disposition=inline',
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
    'https://teste-jackson-duarte.s3.us-east-1.amazonaws.com/batata-frita.jpg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Credential=ASIA3FLDZJ6LCQUEKANS%2F20260424%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20260424T084849Z&X-Amz-Expires=300&X-Amz-Security-Token=IQoJb3JpZ2luX2VjELH%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEaCXVzLWVhc3QtMSJHMEUCIEp6663xtCila9LaHaMSEZOgL72cIffhu3Wiif6SdcnIAiEA5XuqN6XvH5nu1dxsKfcbA%2Fh146f5yyArBwr2nyScpdQq2gIIehAAGgw3NjczOTc4MTAwNzAiDBL794I47gmvJzu%2FDiq3AtveDuCOPZEbrfZ41a5oeKjIvTMxcuzD3r0oE6oSbeX38VoYpk7W7vLoYka2rIE2vWHZ8fG%2FRrXAoo43qkU%2FkLDe3IbvS6SUtJCgKcMG9F8OxXjQH4ItO5auIN0TvlTCkjzJ8ftTePgguGBUZ8PdBjtNdS33297%2BKdxdLEczktSeYA2hUkPQ5iZ%2FA3zaBZx14v89uhZDa4jPHYTl1Sa1uPYjd976cyvTywVcnF5BjWLlhVFyvkM%2BP8R3UPQ69g%2BqxFLLVf%2BzZKU1ER%2Bbn%2FwWLWigNYqVyLdRl6pwqp0TAcgFUGoGvP1zT4rj1NZbvO77kxv5aank%2F2RqBD%2BPRRCjT9iZ2%2BOoqvc5oK4PBllHeVEk%2Bg0MlC11GkPtPcEz3Brc59RTRrqL%2Bcjs8a9Y3aTty5q7u3UoEv0pMNTXrM8GOq0CckFlbagBaDIPEQRntKY3cSyXBjpUM2GfNKkwwgOsuXsIQ2TmXl06qrkEHupqhmrQwCCM4Cr3mmefqh%2B9xPrmZ3%2FYMVGfbrzn5bHscciGt5ZeNpY6vsXDZlLVvV%2BWH12VGk%2F22Naz5atQYuWS%2BgcVSw%2B3yJvXomtREHobPQ6WOlKDmZOu7PAXxkG71BdJnw4hlCEzviowMk%2BNaGj0rAPs27viUP9xZgsjd2xoY81GonlU9Zp7Q188TCpvYvvSJi%2BNQAAOYZHed7iwkFobJt3CoVhwiOEbRCPIzvyRHn9UkNRKIUJhbVLB5qzx%2B5wnpW0iqO24KLG7iPo7VBDY77qq4rBhCOrHjK%2FGGct%2FMBZ12FQccJNlF76%2BdPxejyJKMqBRU6LJoQhWp3lfaS4epA%3D%3D&X-Amz-Signature=b7d1da61372c13adff49089bae81d4e79c16e254db98cfddc65a67248c0eb762&X-Amz-SignedHeaders=host&response-content-disposition=inline',
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
    'https://teste-jackson-duarte.s3.us-east-1.amazonaws.com/refrigerante.jpg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Credential=ASIA3FLDZJ6LA3B2BQQD%2F20260424%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20260424T084645Z&X-Amz-Expires=300&X-Amz-Security-Token=IQoJb3JpZ2luX2VjELH%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEaCXVzLWVhc3QtMSJHMEUCIBnyYuKK2bkO3gq0kBDvK0iuQ%2B94yShaeFhXUK3TCwCXAiEA6GztTz8zllT2XpBHVK6DkcUsbslZj9tPAIpXZ2dgiBAq2gIIehAAGgw3NjczOTc4MTAwNzAiDFoHVQqmocvImqfJWSq3AsrIllQvc2FsR5khnZhY67bfGDB90SGRTdLLl6w%2BGIL3%2B%2F8FuZrwKPkxVxJ4gUvq0iSmuiCeuU29HZRPYgdLchUAV%2BTKsTqBDpszzNh8hBY2uSWczk%2BofEEodvzASkkN1oplvI7jkZ%2BiBICp%2FtnFYi2AaCv7W8j4cYMMxyCdy%2Fd3JOZeB5ki%2FtZDsO%2BmmKnCLiBiPOxc6EwV3fGHBKdkliKeuZfHye5%2BuophDzwWqOiR4dB%2BdP1n2q%2Fqwt0c7hR%2Bu5dOjBqAjECdvMcLBLall4aEDu7li1EK5ICBE%2FvT%2FbjL4d0nVuxSAGHqCg4jKvpujGayhejYwaHQIjvK92yiYWlm%2Bk8QCwuaGq0dJsPATJV49roIhftFRS1WS8yH8UFEAtonUleSQJnQnME9NXtbuV4X5rX%2FK0SbMNTXrM8GOq0CmFhmX3zN86XJQZns7Mw1oPhE9xyyhb5yq8eBrVucTn63MiqO0M7pvV0shPAKPBITnQ2J3GlPiw5xfXY0mFdYzKAJ2Dfg4JDsu%2BBNNQbajt7r9JMG5zxDJ3cGjEDpfq7REhU8SRvde7BD%2F3w4jt6H7tfholQXe%2Fzk8mxKclj7WaG8pqoQdFLOP2AdZ219briMeswx7Wny%2BQ%2BOhJxWI%2BkCQOovOF%2Bea0kasPxOioSaQzrBFOujfzXwHSfRELMBsAvQnl64PmKsrqtx9e3gqNRYWHrsmeuLmnv14ksi6lNS1g1HsAy43%2FUb1EYO9NbI2pndO6X9Za%2Bk7VAxecIVo2pAGCXdNsyEpElDpyJwqkt2IGJ7XfqoVuQUiTa%2FCUWPMpidsZWR5gfNlzmiKKGJjQ%3D%3D&X-Amz-Signature=682d2b039a02bb56d0f0882c4968b6d6fc37711eb1f64c54ab44ca835d2897c8&X-Amz-SignedHeaders=host&response-content-disposition=inline',
    FALSE
  );