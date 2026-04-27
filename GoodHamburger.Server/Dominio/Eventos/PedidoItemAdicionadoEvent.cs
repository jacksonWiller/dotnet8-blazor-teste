namespace Dominio.Eventos
{
    /// <summary>
    /// Evento disparado quando um item é adicionado a um pedido
    /// </summary>
    public class PedidoItemAdicionadoEvent : EventoBase
    {
        public Guid PedidoId { get; }
        public Guid ItemId { get; }
        public string ItemNome { get; }

        public PedidoItemAdicionadoEvent(Guid pedidoId, Guid itemId, string itemNome)
        {
            PedidoId = pedidoId;
            ItemId = itemId;
            ItemNome = itemNome;
        }
    }
}
