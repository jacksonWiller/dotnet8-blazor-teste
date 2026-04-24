namespace Dominio.Dtos
{
    /// <summary>
    /// DTO para representar um item do cardápio
    /// </summary>
    public class ItemCardapioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
    }

    /// <summary>
    /// DTO para representar um item no pedido
    /// </summary>
    public class PedidoItemDto
    {
        public Guid ItemId { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public decimal PrecoUnitario { get; set; }
    }

    /// <summary>
    /// DTO para representar um pedido completo
    /// </summary>
    public class PedidoDto
    {
        public Guid Id { get; set; }
        public List<PedidoItemDto> Itens { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
