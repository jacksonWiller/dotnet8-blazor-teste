using Dominio.Eventos;
using Dominio.ObjetosDeValor;

namespace Dominio.Entidades
{
    /// <summary>
    /// Representa um pedido da Good Hamburger
    /// </summary>
    public class Pedido : EntidadeBase
    {
        public Guid Id { get; private set; }
        public List<PedidoItem> Itens { get; private set; } = [];
        public decimal Subtotal { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal Total { get; private set; }
        public PedidoStatus Status { get; private set; }
        public DateTime DataCriacao { get; private set; }

        protected Pedido() { }

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
            Status = PedidoStatus.Pendente;
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
            if (item.Categoria.Contains("Sanduíche", StringComparison.OrdinalIgnoreCase) && TemSanduiche())
                throw new ArgumentException("Não é permitido adicionar mais de um sanduíche ao pedido.");

            if (item.Categoria.Contains("Acompanhamento", StringComparison.OrdinalIgnoreCase) && TemAcompanhamento())
                throw new ArgumentException("Não é permitido adicionar mais de um acompanhamento ao pedido.");

            if (item.Categoria.Contains("Bebida", StringComparison.OrdinalIgnoreCase) && TemBebida())
                throw new ArgumentException("Não é permitido adicionar mais de uma bebida ao pedido.");
        }

        /// <summary>
        /// Verifica se o pedido já tem um sanduíche
        /// </summary>
        private bool TemSanduiche() => Itens.Any(i => i.Categoria.Contains("Sanduíche", StringComparison.OrdinalIgnoreCase));

        private bool TemAcompanhamento() => Itens.Any(i => i.Categoria.Contains("Acompanhamento", StringComparison.OrdinalIgnoreCase));

        private bool TemBebida() => Itens.Any(i => i.Categoria.Contains("Bebida", StringComparison.OrdinalIgnoreCase));

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
        /// Verifica se o pedido já tem batata
        /// </summary>
        private bool TemBatata() => Itens.Any(i => i.Categoria.Contains("Acompanhamento", StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Verifica se o pedido já tem refrigerante
        /// </summary>
        private bool TemRefrigerante() => Itens.Any(i => i.Categoria.Contains("Bebida", StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Muda o status do pedido com validação de transição
        /// </summary>
        public void MudarStatus(PedidoStatus novoStatus)
        {
            ValidarTransicaoDeStatus(Status, novoStatus);
            Status = novoStatus;
        }

        /// <summary>
        /// Cancela o pedido
        /// </summary>
        public void CancelarPedido()
        {
            if (Status == PedidoStatus.Cancelado)
                throw new InvalidOperationException("O pedido já está cancelado.");

            if (Status == PedidoStatus.Entregue)
                throw new InvalidOperationException("Não é possível cancelar um pedido entregue.");

            MudarStatus(PedidoStatus.Cancelado);
        }

        /// <summary>
        /// Valida se a transição de status é permitida
        /// </summary>
        private void ValidarTransicaoDeStatus(PedidoStatus statusAtual, PedidoStatus novoStatus)
        {
            if (statusAtual == novoStatus)
                return; // Não há mudança

            // Transições permitidas
            var transicoesPermitidas = new Dictionary<PedidoStatus, List<PedidoStatus>>
            {
                { PedidoStatus.Pendente, new List<PedidoStatus> 
                    { PedidoStatus.EmPreparacao, PedidoStatus.Cancelado } },
                
                { PedidoStatus.EmPreparacao, new List<PedidoStatus> 
                    { PedidoStatus.Pronto, PedidoStatus.Cancelado } },
                
                { PedidoStatus.Pronto, new List<PedidoStatus> 
                    { PedidoStatus.Entregue, PedidoStatus.Cancelado } },
                
                { PedidoStatus.Entregue, new List<PedidoStatus>() }, // Não permite mais transições
                
                { PedidoStatus.Cancelado, new List<PedidoStatus>() } // Não permite mais transições
            };

            if (!transicoesPermitidas.ContainsKey(statusAtual))
                throw new InvalidOperationException($"Status inicial inválido: {statusAtual}");

            if (!transicoesPermitidas[statusAtual].Contains(novoStatus))
                throw new InvalidOperationException(
                    $"Não é possível transicionar de {statusAtual} para {novoStatus}");
        }

        /// <summary>
        /// Atualiza o status do pedido (sem validação de transição)
        /// </summary>
        public void AtualizarStatus(PedidoStatus novoStatus)
        {
            Status = novoStatus;
        }

        /// <summary>
        /// Adiciona um item com quantidade específica
        /// </summary>
        public void AdicionarItemComQuantidade(Item item, int quantidade)
        {
            ValidarItem(item);

            var pedidoItem = new PedidoItem(Id, item.Id, item.Nome, item.Categoria, item.Preco, quantidade);
            Itens.Add(pedidoItem);
            RecalcularTotais();

            AddDomainEvent(new PedidoItemAdicionadoEvent(Id, item.Id, item.Nome));
        }

        /// <summary>
        /// Remove todos os itens do pedido
        /// </summary>
        public void RemoverTodosItens()
        {
            Itens.Clear();
            RecalcularTotais();
        }

        /// <summary>
        /// Verifica se o pedido pode ser atualizado (apenas pendente)
        /// </summary>
        public bool PodeSerAtualizado() => Status == PedidoStatus.Pendente;

        /// <summary>
        /// Verifica se o pedido pode ser cancelado
        /// </summary>
        public bool PodeSerCancelado() => 
            Status == PedidoStatus.Pendente || 
            Status == PedidoStatus.EmPreparacao || 
            Status == PedidoStatus.Pronto;

    }
}
