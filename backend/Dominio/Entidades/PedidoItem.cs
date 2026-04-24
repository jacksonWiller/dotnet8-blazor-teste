namespace Dominio.Entidades
{
    /// <summary>
    /// Representa um item dentro de um pedido
    /// </summary>
    public class PedidoItem
    {
        public Guid ItemId { get; private set; }
        public string ItemNome { get; private set; }
        public string Categoria { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public int Quantidade { get; private set; }

        protected PedidoItem() { }

        /// <summary>
        /// Cria um novo item de pedido
        /// </summary>
        public PedidoItem(Guid itemId, string itemNome, string categoria, decimal precoUnitario, int quantidade = 1)
        {
            ItemId = itemId;
            ItemNome = itemNome ?? throw new ArgumentNullException(nameof(itemNome));
            Categoria = categoria;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;
        }

        /// <summary>
        /// Calcula o subtotal deste item
        /// </summary>
        public decimal Subtotal => PrecoUnitario * Quantidade;
    }
}
