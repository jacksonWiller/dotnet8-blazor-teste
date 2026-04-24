namespace Dominio.Dtos
{
    /// <summary>
    /// DTO para representar um item do menu da GoodHamburger
    /// </summary>
    public class ItemDto
    {
        /// <summary>
        /// Identificador único do item
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Nome do produto
        /// </summary>
        public string Nome { get; set; }
        
        /// <summary>
        /// Descrição detalhada do produto
        /// </summary>
        public string Descricao { get; set; }
        
        /// <summary>
        /// Preço do produto
        /// </summary>
        public decimal Preco { get; set; }
        
        /// <summary>
        /// Tipo do item: burger, side, drink, dessert, combo
        /// </summary>
        public string Tipo { get; set; }
        
        /// <summary>
        /// Categoria para filtros
        /// </summary>
        public string Categoria { get; set; }
        
        /// <summary>
        /// URL da imagem (opcional)
        /// </summary>
        public string? UrlImagem { get; set; }
        
        /// <summary>
        /// Indica se o item está ativo no menu
        /// </summary>
        public bool Removido { get; set; }
    }
}
