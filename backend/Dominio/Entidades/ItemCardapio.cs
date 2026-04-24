namespace Clientes.Dominio.Entidades
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

        /// <summary>
        /// Cria um sanduíche X Burger
        /// </summary>
        public static ItemCardapio CreateXBurger() => 
            new(Guid.Parse("a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1"), "X Burger", CategoriaItem.Sanduiche, 5.00m);

        /// <summary>
        /// Cria um sanduíche X Egg
        /// </summary>
        public static ItemCardapio CreateXEgg() => 
            new(Guid.Parse("a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2"), "X Egg", CategoriaItem.Sanduiche, 4.50m);

        /// <summary>
        /// Cria um sanduíche X Bacon
        /// </summary>
        public static ItemCardapio CreateXBacon() => 
            new(Guid.Parse("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"), "X Bacon", CategoriaItem.Sanduiche, 7.00m);

        /// <summary>
        /// Cria batata frita
        /// </summary>
        public static ItemCardapio CreateBatataFrita() => 
            new(Guid.Parse("b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1"), "Batata frita", CategoriaItem.Acompanhamento, 2.00m);

        /// <summary>
        /// Cria refrigerante
        /// </summary>
        public static ItemCardapio CreateRefrigerante() => 
            new(Guid.Parse("c1c1c1c1-c1c1-c1c1-c1c1-c1c1c1c1c1c1"), "Refrigerante", CategoriaItem.Bebida, 2.50m);

        /// <summary>
        /// Retorna todos os itens do cardápio
        /// </summary>
        public static IEnumerable<ItemCardapio> GetAllItens()
        {
            yield return CreateXBurger();
            yield return CreateXEgg();
            yield return CreateXBacon();
            yield return CreateBatataFrita();
            yield return CreateRefrigerante();
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
