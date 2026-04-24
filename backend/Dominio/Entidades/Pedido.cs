using Dominio.Eventos;

namespace Dominio.Entidades
{
    /// <summary>
    /// Representa um pedido da Good Hamburger
    /// </summary>
    public class Pedido : EntidadeBase
    {
        public Guid Id { get; private set; }
        public List<PedidoItem> Itens { get; private set; }
        public decimal Subtotal { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal Total { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public Pedido() { }

        /// <summary>
        /// Cria um novo pedido
        /// </summary>
        public Pedido(Guid id)
        {
            Id = id;
            Itens = new List<PedidoItem>();
            Subtotal = 0;
            Desconto = 0;
            Total = 0;
            DataCriacao = DateTime.UtcNow;
        }

        /// <summary>
        /// Adiciona um item ao pedido
        /// </summary>
        public void AdicionarItem(Item item)
        {
            ValidarItem(item);

            var pedidoItem = new PedidoItem(Id, item.Id, item.Nome, item.Categoria, item.Preco);
            Itens.Add(pedidoItem);
            RecalcularTotais();

            AddDomainEvent(new PedidoItemAdicionadoEvent(Id, item.Id, item.Nome));
        }

        /// <summary>
        /// Valida se o item pode ser adicionado ao pedido
        /// </summary>
        private void ValidarItem(Item item)
        {
            if (item.Categoria.Contains("Sanduíche") && TemSanduiche())
                throw new ArgumentException("Não é permitido adicionar mais de um sanduíche ao pedido.");

            if (item.Categoria.Contains("Acompanhamento") && TemAcompanhamento())
                throw new ArgumentException("Não é permitido adicionar mais de um acompanhamento ao pedido.");

            if (item.Categoria.Contains("Bebida") && TemBebida())
                throw new ArgumentException("Não é permitido adicionar mais de uma bebida ao pedido.");
        }

        /// <summary>
        /// Verifica se o pedido já tem um sanduíche
        /// </summary>
        private bool TemSanduiche() => Itens.Any(i => i.Categoria.Contains("Sanduíche"));

        /// <summary>
        /// Verifica se o pedido já tem acompanhamento
        /// </summary>
        private bool TemAcompanhamento() => Itens.Any(i => i.Categoria.Contains("Acompanhamento"));

        /// <summary>
        /// Verifica se o pedido já tem bebida
        /// </summary>
        private bool TemBebida() => Itens.Any(i => i.Categoria.Contains("Bebida"));

        /// <summary>
        /// Recalcula subtotal, desconto e total
        /// </summary>
        private void RecalcularTotais()
        {
            Subtotal = CalcularSubtotal();
            Total = Subtotal - Desconto;
        }

        /// <summary>
        /// Calcula o subtotal de todos os itens
        /// </summary>
        private decimal CalcularSubtotal() => Itens.Sum(i => i.PrecoUnitario);
        

    }
}
