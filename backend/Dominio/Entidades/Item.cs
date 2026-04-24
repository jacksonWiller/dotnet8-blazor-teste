namespace Dominio.Entidades
{
    /// <summary>
    /// Tipos de itens do menu da GoodHamburger
    /// </summary>
    public enum CategoriaItem
    {
        /// <summary>
        /// Sanduíches
        /// </summary>
        Sanduiche = 1,

        /// <summary>
        /// Acompanhamentos
        /// </summary>
        Acompanhamento = 2,

        /// <summary>
        /// Bebidas
        /// </summary>
        Bebida = 3
    }

    /// <summary>
    /// Representa um item do menu da GoodHamburger
    /// </summary>
    public class Item : EntidadeBase
    {
        /// <summary>
        /// Identificador único do item
        /// </summary>
        public Guid Id { get; private set; }
        
        /// <summary>
        /// Nome do produto
        /// </summary>
        public string Nome { get; private set; }
        
        /// <summary>
        /// Descrição detalhada do produto
        /// </summary>
        public string Descricao { get; private set; }
        
        /// <summary>
        /// Preço do produto
        /// </summary>
        public decimal Preco { get; private set; }
        
        /// <summary>
        /// Tipo do item: burger, side, drink, dessert, combo
        /// </summary>
        public string Tipo { get; private set; }
        
        /// <summary>
        /// Categoria para filtros
        /// </summary>
        public string Categoria { get; private set; }
        
        /// <summary>
        /// URL da imagem (opcional)
        /// </summary>
        public string? UrlImagem { get; private set; }
        
        /// <summary>
        /// Indica se o item está ativo no menu
        /// </summary>
        public bool Ativo { get; private set; } = true;

        /// <summary>
        /// Construtor protegido para EF Core
        /// </summary>
        protected Item() { }

        /// <summary>
        /// Cria um novo item do menu
        /// </summary>
        /// <param name="nome">Nome do produto</param>
        /// <param name="descricao">Descrição detalhada do produto</param>
        /// <param name="preco">Preço do produto</param>
        /// <param name="tipo">Tipo do item</param>
        /// <param name="categoria">Categoria para filtros</param>
        /// <param name="urlImagem">URL da imagem (opcional)</param>
        public Item(string nome, string descricao, decimal preco, string tipo, string categoria, string? urlImagem = null)
        {
            Id = Guid.NewGuid();
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Descricao = descricao ?? throw new ArgumentNullException(nameof(descricao));
            Preco = preco;
            Tipo = tipo ?? throw new ArgumentNullException(nameof(tipo));
            Categoria = categoria ?? throw new ArgumentNullException(nameof(categoria));
            UrlImagem = urlImagem;
            Ativo = true;

            Validar();
        }

        /// <summary>
        /// Valida as regras de negócio do item
        /// </summary>
        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new ArgumentException("O nome do item é obrigatório.");

            if (Nome.Length > 100)
                throw new ArgumentException("O nome do item não pode exceder 100 caracteres.");

            if (string.IsNullOrWhiteSpace(Descricao))
                throw new ArgumentException("A descrição do item é obrigatória.");

            if (Descricao.Length > 500)
                throw new ArgumentException("A descrição do item não pode exceder 500 caracteres.");

            if (Preco < 0)
                throw new ArgumentException("O preço deve ser maior ou igual a zero.");

            if (!IsValidTipo(Tipo))
                throw new ArgumentException($"Tipo inválido. Use: burger, side, drink, dessert, combo");

            if (string.IsNullOrWhiteSpace(Categoria))
                throw new ArgumentException("A categoria é obrigatória.");
        }

        /// <summary>
        /// Verifica se o tipo é válido
        /// </summary>
        private bool IsValidTipo(string tipo)
        {
            var tiposValidos = new[] { "burger", "side", "drink", "dessert", "combo" };
            return tiposValidos.Contains(tipo.ToLower());
        }

        /// <summary>
        /// Atualiza as informações do item
        /// </summary>
        /// <param name="nome">Novo nome do produto</param>
        /// <param name="descricao">Nova descrição do produto</param>
        /// <param name="preco">Novo preço do produto</param>
        /// <param name="tipo">Novo tipo do item</param>
        /// <param name="categoria">Nova categoria</param>
        /// <param name="urlImagem">Nova URL da imagem (opcional)</param>
        public void Atualizar(string nome, string descricao, decimal preco, string tipo, string categoria, string? urlImagem = null)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Descricao = descricao ?? throw new ArgumentNullException(nameof(descricao));
            Preco = preco;
            Tipo = tipo ?? throw new ArgumentNullException(nameof(tipo));
            Categoria = categoria ?? throw new ArgumentNullException(nameof(categoria));
            UrlImagem = urlImagem;

            Validar();
        }

        /// <summary>
        /// Desativa o item do menu
        /// </summary>
        public void Desativar() => Ativo = false;
        
        /// <summary>
        /// Ativa o item no menu
        /// </summary>
        public void Ativar() => Ativo = true;
    }
}
