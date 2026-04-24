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

        protected Pedido() { }

        /// <summary>
        /// Cria um novo pedido
        /// </summary>
        private Pedido(Guid id)
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
        public void AdicionarItem(ItemCardapio item)
        {
            ValidarItem(item);

            var pedidoItem = new PedidoItem(item.Id, item.Nome, item.Categoria, item.Preco);
            Itens.Add(pedidoItem);
            RecalcularTotais();

            AddDomainEvent(new PedidoItemAdicionadoEvent(Id, item.Id, item.Nome));
        }

        /// <summary>
        /// Valida se o item pode ser adicionado ao pedido
        /// </summary>
        private void ValidarItem(ItemCardapio item)
        {
            if (item.Categoria == CategoriaItem.Sanduiche && TemSanduiche())
                throw new ArgumentException("Não é permitido adicionar mais de um sanduíche ao pedido.");

            if (item.Categoria == CategoriaItem.Acompanhamento && TemBatata())
                throw new ArgumentException("Não é permitido adicionar mais de uma batata ao pedido.");

            if (item.Categoria == CategoriaItem.Bebida && TemRefrigerante())
                throw new ArgumentException("Não é permitido adicionar mais de um refrigerante ao pedido.");
        }

        /// <summary>
        /// Verifica se o pedido já tem um sanduíche
        /// </summary>
        private bool TemSanduiche() => Itens.Any(i => i.Categoria == CategoriaItem.Sanduiche);

        /// <summary>
        /// Verifica se o pedido já tem batata
        /// </summary>
        private bool TemBatata() => Itens.Any(i => i.Categoria == CategoriaItem.Acompanhamento);

        /// <summary>
        /// Verifica se o pedido já tem refrigerante
        /// </summary>
        private bool TemRefrigerante() => Itens.Any(i => i.Categoria == CategoriaItem.Bebida);

        /// <summary>
        /// Recalcula subtotal, desconto e total
        /// </summary>
        private void RecalcularTotais()
        {
            Subtotal = CalcularSubtotal();
            Desconto = CalcularDesconto();
            Total = Subtotal - Desconto;
        }

        /// <summary>
        /// Calcula o subtotal de todos os itens
        /// </summary>
        private decimal CalcularSubtotal() => Itens.Sum(i => i.PrecoUnitario);

        /// <summary>
        /// Calcula o desconto baseado nas regras de negócio
        /// </summary>
        private decimal CalcularDesconto()
        {
            var temSanduiche = TemSanduiche();
            var temBatata = TemBatata();
            var temRefrigerante = TemRefrigerante();

            // Regra 1: Sanduíche + Batata + Refrigerante → 20% de desconto
            if (temSanduiche && temBatata && temRefrigerante)
                return Subtotal * 0.20m;

            // Regra 2: Sanduíche + Refrigerante → 15% de desconto
            if (temSanduiche && temRefrigerante)
                return Subtotal * 0.15m;

            // Regra 3: Sanduíche + Batata → 10% de desconto
            if (temSanduiche && temBatata)
                return Subtotal * 0.10m;

            // Nenhuma regra aplicada
            return 0;
        }

        /// <summary>
        /// Obtém informações detalhadas do pedido
        /// </summary>
        public PedidoInfo ObterInfo()
        {
            return new PedidoInfo
            {
                Id = Id,
                Itens = Itens.Select(i => new PedidoItemInfo
                {
                    ItemId = i.ItemId,
                    Nome = i.Nome,
                    Categoria = i.Categoria,
                    PrecoUnitario = i.PrecoUnitario
                }).ToList(),
                Subtotal = Subtotal,
                Desconto = Desconto,
                Total = Total,
                DataCriacao = DataCriacao
            };
        }
    }

    /// <summary>
    /// Representa um item dentro de um pedido
    /// </summary>
    public class PedidoItem
    {
        public Guid ItemId { get; private set; }
        public string Nome { get; private set; }
        public CategoriaItem Categoria { get; private set; }
        public decimal PrecoUnitario { get; private set; }

        public PedidoItem(Guid itemId, string nome, CategoriaItem categoria, decimal precoUnitario)
        {
            ItemId = itemId;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Categoria = categoria;
            PrecoUnitario = precoUnitario;
        }
    }

    /// <summary>
    /// Informações detalhadas de um pedido
    /// </summary>
    public class PedidoInfo
    {
        public Guid Id { get; set; }
        public List<PedidoItemInfo> Itens { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    /// <summary>
    /// Informações detalhadas de um item no pedido
    /// </summary>
    public class PedidoItemInfo
    {
        public Guid ItemId { get; set; }
        public string Nome { get; set; }
        public CategoriaItem Categoria { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
