-- =========================================
-- LIMPEZA DOS DADOS
-- =========================================
DELETE FROM "Clientes";
DELETE FROM "Telefone";
DELETE FROM "Endereco";
DELETE FROM "Email";
DELETE FROM "Documento";

-- =========================================
-- TABELAS
-- =========================================

CREATE TABLE IF NOT EXISTS "Documento" (
    "Id" uuid NOT NULL,
    "Numero" text NOT NULL,
    "Tipo" integer NOT NULL,
    CONSTRAINT "PK_Documento" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Email" (
    "Id" uuid NOT NULL,
    "Endereco" text NOT NULL,
    CONSTRAINT "PK_Email" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Endereco" (
    "Id" uuid NOT NULL,
    "Cep" text NOT NULL,
    "Logradouro" text NOT NULL,
    "Numero" text NOT NULL,
    "Bairro" text NOT NULL,
    "Cidade" text NOT NULL,
    "Estado" text NOT NULL,
    CONSTRAINT "PK_Endereco" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Telefone" (
    "Id" uuid NOT NULL,
    "Numero" text NOT NULL,
    CONSTRAINT "PK_Telefone" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Clientes" (
    "Id" uuid NOT NULL,
    "Nome" text NOT NULL,
    "DocumentoId" uuid NOT NULL,
    "DataNascimento" timestamp with time zone NOT NULL,
    "TelefoneId" uuid NOT NULL,
    "EmailId" uuid NOT NULL,
    "EnderecoId" uuid NOT NULL,
    "InscricaoEstadual" text NOT NULL,
    "Isento" boolean NOT NULL,
    "Removido" boolean NOT NULL,
    CONSTRAINT "PK_Clientes" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Clientes_Documento_DocumentoId"
        FOREIGN KEY ("DocumentoId") REFERENCES "Documento" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Clientes_Email_EmailId"
        FOREIGN KEY ("EmailId") REFERENCES "Email" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Clientes_Endereco_EnderecoId"
        FOREIGN KEY ("EnderecoId") REFERENCES "Endereco" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Clientes_Telefone_TelefoneId"
        FOREIGN KEY ("TelefoneId") REFERENCES "Telefone" ("Id") ON DELETE CASCADE
);

-- =========================================
-- ÍNDICES
-- =========================================
CREATE INDEX IF NOT EXISTS "IX_Clientes_DocumentoId" ON "Clientes" ("DocumentoId");
CREATE INDEX IF NOT EXISTS "IX_Clientes_EmailId" ON "Clientes" ("EmailId");
CREATE INDEX IF NOT EXISTS "IX_Clientes_EnderecoId" ON "Clientes" ("EnderecoId");
CREATE INDEX IF NOT EXISTS "IX_Clientes_TelefoneId" ON "Clientes" ("TelefoneId");

-- =========================================
-- DADOS: DOCUMENTO
-- =========================================
INSERT INTO "Documento" ("Id", "Numero", "Tipo")
VALUES
  -- CPFs
  ('a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890', '12345678900', 1),
  ('b2c3d4e5-f6a7-8901-b2c3-d4e5f6a78901', '98765432100', 1),
  ('c1d2e3f4-a5b6-7890-c1d2-e3f4a5b67891', '45678912300', 1),
  ('d1e2f3a4-b5c6-7890-d1e2-f3a4b5c67890', '78912345600', 1),
  ('e1f2a3b4-c5d6-7890-e1f2-a3b4c5d67891', '32165498700', 1),
  ('f1a2b3c4-d5e6-7890-f1a2-b3c4d5e67890', '65432198700', 1),
  ('a2b3c4d5-e6f7-8901-a2b3-c4d5e6f78901', '15935785200', 1),
  ('b3c4d5e6-f7a8-9012-b3c4-d5e6f7a89012', '95175385200', 1),
  ('c4d5e6f7-a8b9-0123-c4d5-e6f7a8b90123', '75315985200', 1),
  ('d5e6f7a8-b9c0-1234-d5e6-f7a8b9c01234', '35795175300', 1),

  -- CNPJs
  ('c3d4e5f6-a7b8-9012-c3d4-e5f6a7b89012', '11222333444555', 2),
  ('d4e5f6a7-b8c9-0123-d4e5-f6a7b8c90123', '22333444555666', 2),
  ('e5f6a7b8-c9d0-1234-e5f6-a7b8c9d01234', '33444555666777', 2),
  ('f6a7b8c9-d0e1-2345-f6a7-b8c9d0e12346', '44555666777888', 2),
  ('a7b8c9d0-e1f2-3456-a7b8-c9d0e1f23457', '55666777888999', 2),
  ('b8c9d0e1-f2a3-4567-b8c9-d0e1f2a34568', '66777888999000', 2),
  ('c9d0e1f2-a3b4-5678-c9d0-e1f2a3b45679', '77888999000111', 2),
  ('d0e1f2a3-b4c5-6789-d0e1-f2a3b4c5678a', '88999000111222', 2);

-- =========================================
-- DADOS: EMAIL
-- =========================================
INSERT INTO "Email" ("Id", "Endereco")
VALUES
  ('f6a7b8c9-d0e1-2345-f6a7-b8c9d0e12345', 'maria.silva@email.com'),
  ('a7b8c9d0-e1f2-3456-a7b8-c9d0e1f23456', 'joao.santos@email.com'),
  ('b8c9d0e1-f2a3-4567-b8c9-d0e1f2a34567', 'empresa1@empresa.com.br'),
  ('c9d0e1f2-a3b4-5678-c9d0-e1f2a3b45678', 'contato@empresa2.com.br'),
  ('d0e1f2a3-b4c5-6789-d0e1-f2a3b4c56789', 'financeiro@empresa3.com.br'),
  ('e1f2a3b4-c5d6-7890-e1f2-a3b4c5d67892', 'ana.oliveira@email.com'),
  ('f2a3b4c5-d6e7-8901-f2a3-b4c5d6e78902', 'carlos.souza@email.com'),
  ('a3b4c5d6-e7f8-9012-a3b4-c5d6e7f89013', 'luciana.costa@gmail.com'),
  ('b4c5d6e7-f8a9-0123-b4c5-d6e7f8a90124', 'rafael.lima@hotmail.com'),
  ('c5d6e7f8-a9b0-1234-c5d6-e7f8a9b01235', 'fernanda.alves@outlook.com'),
  ('d6e7f8a9-b0c1-2345-d6e7-f8a9b0c12346', 'bruno.mendes@yahoo.com'),
  ('e7f8a9b0-c1d2-3456-e7f8-a9b0c1d23457', 'juliana.dias@email.com'),
  ('f8a9b0c1-d2e3-4567-f8a9-b0c1d2e34568', 'pedro.martins@gmail.com'),
  ('a9b0c1d2-e3f4-5678-a9b0-c1d2e3f45679', 'vendas@comercio.com.br'),
  ('b0c1d2e3-f4a5-6789-b0c1-d2e3f4a5678a', 'atendimento@industria.com.br'),
  ('c1d2e3f4-a5b6-7890-c1d2-e3f4a5b6789b', 'contabilidade@servicos.com.br'),
  ('d2e3f4a5-b6c7-8901-d2e3-f4a5b6c7890c', 'adm@tecnologia.com.br'),
  ('e3f4a5b6-c7d8-9012-e3f4-a5b6c7d8901d', 'rh@consultoria.com.br');

-- =========================================
-- DADOS: ENDERECO
-- =========================================
INSERT INTO "Endereco" ("Id", "Cep", "Logradouro", "Numero", "Bairro", "Cidade", "Estado")
VALUES
  ('e1f2a3b4-c5d6-7890-e1f2-a3b4c5d67890', '01001-000', 'Praça da Sé', '123', 'Sé', 'São Paulo', 'SP'),
  ('f2a3b4c5-d6e7-8901-f2a3-b4c5d6e78901', '22021-001', 'Avenida Atlântica', '456', 'Copacabana', 'Rio de Janeiro', 'RJ'),
  ('a3b4c5d6-e7f8-9012-a3b4-c5d6e7f89012', '30130-110', 'Rua dos Carijós', '789', 'Centro', 'Belo Horizonte', 'MG'),
  ('b4c5d6e7-f8a9-0123-b4c5-d6e7f8a90123', '70070-120', 'Esplanada dos Ministérios', '1000', 'Zona Cívico-Administrativa', 'Brasília', 'DF'),
  ('c5d6e7f8-a9b0-1234-c5d6-e7f8a9b01234', '80010-010', 'Rua XV de Novembro', '250', 'Centro', 'Curitiba', 'PR'),
  ('d6e7f8a9-b0c1-2345-d6e7-f8a9b0c12346', '40010-000', 'Avenida Sete de Setembro', '555', 'Centro', 'Salvador', 'BA'),
  ('e7f8a9b0-c1d2-3456-e7f8-a9b0c1d23457', '50030-150', 'Avenida Boa Viagem', '1200', 'Boa Viagem', 'Recife', 'PE'),
  ('f8a9b0c1-d2e3-4567-f8a9-b0c1d2e34568', '60170-001', 'Avenida Beira Mar', '300', 'Meireles', 'Fortaleza', 'CE'),
  ('a9b0c1d2-e3f4-5678-a9b0-c1d2e3f45679', '90010-190', 'Rua dos Andradas', '1234', 'Centro Histórico', 'Porto Alegre', 'RS'),
  ('b0c1d2e3-f4a5-6789-b0c1-d2e3f4a5678a', '04538-132', 'Avenida Brigadeiro Faria Lima', '3477', 'Itaim Bibi', 'São Paulo', 'SP'),
  ('c1d2e3f4-a5b6-7890-c1d2-e3f4a5b6789a', '69050-001', 'Avenida Eduardo Ribeiro', '620', 'Centro', 'Manaus', 'AM'),
  ('d2e3f4a5-b6c7-8901-d2e3-f4a5b6c7890b', '66010-020', 'Avenida Presidente Vargas', '800', 'Campina', 'Belém', 'PA'),
  ('e3f4a5b6-c7d8-9012-e3f4-a5b6c7d8901c', '20031-170', 'Avenida Rio Branco', '156', 'Centro', 'Rio de Janeiro', 'RJ'),
  ('f4a5b6c7-d8e9-0123-f4a5-b6c7d8e9012d', '88015-600', 'Rua Felipe Schmidt', '315', 'Centro', 'Florianópolis', 'SC'),
  ('a5b6c7d8-e9f0-1234-a5b6-c7d8e9f0123e', '13330-000', 'Avenida Brasil', '1500', 'Centro', 'Indaiatuba', 'SP'),
  ('b6c7d8e9-f0a1-2345-b6c7-d8e9f0a1234f', '59020-100', 'Avenida Prudente de Morais', '744', 'Tirol', 'Natal', 'RN');

-- =========================================
-- DADOS: TELEFONE
-- =========================================
INSERT INTO "Telefone" ("Id", "Numero")
VALUES
  ('d6e7f8a9-b0c1-2345-d6e7-f8a9b0c12345', '(11) 98765-4321'),
  ('e7f8a9b0-c1d2-3456-e7f8-a9b0c1d23456', '(21) 99876-5432'),
  ('f8a9b0c1-d2e3-4567-f8a9-b0c1d2e34567', '(31) 3333-4444'),
  ('a9b0c1d2-e3f4-5678-a9b0-c1d2e3f45678', '(61) 2222-3333'),
  ('b0c1d2e3-f4a5-6789-b0c1-d2e3f4a56789', '(41) 4444-5555'),
  ('c1d2e3f4-a5b6-7890-c1d2-e3f4a5b6789b', '(71) 99765-4321'),
  ('d2e3f4a5-b6c7-8901-d2e3-f4a5b6c7890c', '(81) 98877-6655'),
  ('e3f4a5b6-c7d8-9012-e3f4-a5b6c7d8901d', '(85) 97766-5544'),
  ('f4a5b6c7-d8e9-0123-f4a5-b6c7d8e9012e', '(51) 96655-4433'),
  ('a5b6c7d8-e9f0-1234-a5b6-c7d8e9f0123f', '(11) 95544-3322'),
  ('b6c7d8e9-f0a1-2345-b6c7-d8e9f0a1234f', '(92) 94433-2211'),
  ('e9f0a1b2-c3d4-5678-e9f0-a1b2c3d45678', '(48) 5555-6666'),
  ('f0a1b2c3-d4e5-6789-f0a1-b2c3d4e56789', '(19) 3344-5566'),
  ('a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890', '(84) 3456-7890'),
  ('b2c3d4e5-f6a7-8901-b2c3-d4e5f6a78901', '(62) 7890-1234'),
  ('c3d4e5f6-a7b8-9012-c3d4-e5f6a7b89012', '(27) 0123-4567');

-- =========================================
-- DADOS: CLIENTES
-- =========================================
INSERT INTO "Clientes" (
    "Id",
    "Nome",
    "DocumentoId",
    "DataNascimento",
    "TelefoneId",
    "EmailId",
    "EnderecoId",
    "InscricaoEstadual",
    "Isento",
    "Removido"
)
VALUES
  -- Pessoas Físicas
  (
    'c1d2e3f4-a5b6-7890-c1d2-e3f4a5b67890',
    'Maria Silva',
    'a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890',
    '1985-06-15 00:00:00+00',
    'd6e7f8a9-b0c1-2345-d6e7-f8a9b0c12345',
    'f6a7b8c9-d0e1-2345-f6a7-b8c9d0e12345',
    'e1f2a3b4-c5d6-7890-e1f2-a3b4c5d67890',
    '',
    TRUE,
    FALSE
  ),
  (
    'd2e3f4a5-b6c7-8901-d2e3-f4a5b6c78901',
    'João Santos',
    'b2c3d4e5-f6a7-8901-b2c3-d4e5f6a78901',
    '1990-02-28 00:00:00+00',
    'e7f8a9b0-c1d2-3456-e7f8-a9b0c1d23456',
    'a7b8c9d0-e1f2-3456-a7b8-c9d0e1f23456',
    'f2a3b4c5-d6e7-8901-f2a3-b4c5d6e78901',
    '',
    TRUE,
    FALSE
  ),
  (
    'a5b6c7d8-e9f0-1234-a5b6-c7d8e9f01234',
    'Luciana Costa',
    'e1f2a3b4-c5d6-7890-e1f2-a3b4c5d67891',
    '1992-11-05 00:00:00+00',
    'e3f4a5b6-c7d8-9012-e3f4-a5b6c7d8901d',
    'a3b4c5d6-e7f8-9012-a3b4-c5d6e7f89013',
    'f8a9b0c1-d2e3-4567-f8a9-b0c1d2e34568',
    '',
    TRUE,
    FALSE
  ),
  (
    'd8e9f0a1-b2c3-4567-d8e9-f0a1b2c34567',
    'Bruno Mendes',
    'b3c4d5e6-f7a8-9012-b3c4-d5e6f7a89012',
    '1980-12-03 00:00:00+00',
    'b6c7d8e9-f0a1-2345-b6c7-d8e9f0a1234f',
    'd6e7f8a9-b0c1-2345-d6e7-f8a9b0c12346',
    'c1d2e3f4-a5b6-7890-c1d2-e3f4a5b6789a',
    '',
    TRUE,
    FALSE
  ),
  (
    'e9f0a1b2-c3d4-5678-e9f0-a1b2c3d45678',
    'Juliana Dias',
    'c4d5e6f7-a8b9-0123-c4d5-e6f7a8b90123',
    '1987-05-30 00:00:00+00',
    'a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890',
    'e7f8a9b0-c1d2-3456-e7f8-a9b0c1d23457',
    'd2e3f4a5-b6c7-8901-d2e3-f4a5b6c7890b',
    '',
    TRUE,
    FALSE
  ),

  -- Pessoas Jurídicas
  (
    'e3f4a5b6-c7d8-9012-e3f4-a5b6c7d89012',
    'Empresa ABC Ltda',
    'c3d4e5f6-a7b8-9012-c3d4-e5f6a7b89012',
    '2010-01-10 00:00:00+00',
    'f8a9b0c1-d2e3-4567-f8a9-b0c1d2e34567',
    'b8c9d0e1-f2a3-4567-b8c9-d0e1f2a34567',
    'a3b4c5d6-e7f8-9012-a3b4-c5d6e7f89012',
    '123456789',
    FALSE,
    FALSE
  ),
  (
    'f4a5b6c7-d8e9-0123-f4a5-b6c7d8e90123',
    'Distribuidora XYZ',
    'd4e5f6a7-b8c9-0123-d4e5-f6a7b8c90123',
    '2005-05-20 00:00:00+00',
    'a9b0c1d2-e3f4-5678-a9b0-c1d2e3f45678',
    'c9d0e1f2-a3b4-5678-c9d0-e1f2a3b45678',
    'b4c5d6e7-f8a9-0123-b4c5-d6e7f8a90123',
    '987654321',
    FALSE,
    FALSE
  ),
  (
    'b6c7d8e9-f0a1-2345-b6c7-d8e9f0a12345',
    'Indústria Brasileira S.A.',
    'f6a7b8c9-d0e1-2345-f6a7-b8c9d0e12346',
    '2000-11-15 00:00:00+00',
    'e9f0a1b2-c3d4-5678-e9f0-a1b2c3d45678',
    'a9b0c1d2-e3f4-5678-a9b0-c1d2e3f45679',
    'f4a5b6c7-d8e9-0123-f4a5-b6c7d8e9012d',
    '123789456',
    FALSE,
    FALSE
  ),
  (
    'c7d8e9f0-a1b2-3456-c7d8-e9f0a1b23456',
    'Tech Solutions ME',
    'a7b8c9d0-e1f2-3456-a7b8-c9d0e1f23457',
    '2017-03-05 00:00:00+00',
    'f0a1b2c3-d4e5-6789-f0a1-b2c3d4e56789',
    'b0c1d2e3-f4a5-6789-b0c1-d2e3f4a5678a',
    'a5b6c7d8-e9f0-1234-a5b6-c7d8e9f0123e',
    '456123789',
    FALSE,
    FALSE
  ),
  (
    '11111111-2222-3333-4444-555555555555',
    'Agropecuária Rural Ltda',
    'c9d0e1f2-a3b4-5678-c9d0-e1f2a3b45679',
    '2008-04-10 00:00:00+00',
    'b2c3d4e5-f6a7-8901-b2c3-d4e5f6a78901',
    'd2e3f4a5-b6c7-8901-d2e3-f4a5b6c7890c',
    'e3f4a5b6-c7d8-9012-e3f4-a5b6c7d8901c',
    '321654987',
    FALSE,
    FALSE
  ),
  (
    'f0a1b2c3-d4e5-6789-f0a1-b2c3d4e56789',
    'Transportes Expressos S.A.',
    'd0e1f2a3-b4c5-6789-d0e1-f2a3b4c5678a',
    '2003-12-25 00:00:00+00',
    'c3d4e5f6-a7b8-9012-c3d4-e5f6a7b89012',
    'e3f4a5b6-c7d8-9012-e3f4-a5b6c7d8901d',
    'b6c7d8e9-f0a1-2345-b6c7-d8e9f0a1234f',
    '654987321',
    FALSE,
    FALSE
  );