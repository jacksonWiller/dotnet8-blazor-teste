namespace Dominio.Entidades
{
    /// <summary>
    /// Representa um item dentro de um pedido
    /// </summary>
    public class PedidoItem : EntidadeBase
    {
        public Guid PedidoId { get; private set; }
        public Guid ItemId { get; private set; }
        public string Nome { get; private set; }
        public string Categoria { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Total { get; private set; }

        protected PedidoItem() { }

        /// <summary>
        /// Cria um novo item de pedido
        /// </summary>
        public PedidoItem(Guid pedidoId, Guid itemId, string itemNome, string categoria, decimal precoUnitario, int quantidade = 1)
        {
            PedidoId = pedidoId;
            ItemId = itemId;
            Nome = itemNome ?? throw new ArgumentNullException(nameof(itemNome));
            Categoria = categoria;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;
            Total = precoUnitario * quantidade;
        }

        /// <summary>
        /// Calcula o subtotal deste item
        /// </summary>
        public decimal Subtotal => PrecoUnitario * Quantidade;
    }
}
