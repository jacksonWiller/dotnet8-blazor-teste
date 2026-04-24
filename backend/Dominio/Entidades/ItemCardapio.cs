namespace Dominio.Entidades
{
    /// <summary>
    /// Categorias dos itens do cardápio da Good Hamburger
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
    /// Representa um item do cardápio fixo da Good Hamburger
    /// </summary>
    public class ItemCardapio : EntidadeBase
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public CategoriaItem Categoria { get; private set; }
        public decimal Preco { get; private set; }

        protected ItemCardapio() { }

        /// <summary>
        /// Cria um item do cardápio
        /// </summary>
        private ItemCardapio(Guid id, string nome, CategoriaItem categoria, decimal preco)
        {
            Id = id;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Categoria = categoria;
            Preco = preco;

            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new ArgumentException("O nome do item é obrigatório.");

            if (Preco < 0)
                throw new ArgumentException("O preço deve ser maior ou igual a zero.");
        }
    }
}
