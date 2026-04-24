namespace GoodHamburger.Web.Components.Models;

public class CartItem
{
    public Guid ItemId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Icon { get; set; } = "burger";
    public decimal Preco { get; set; }
    public int Quantity { get; set; }
    public string UrlImagem { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    
    // Propriedades de conveniência para compatibilidade com UI existente
    public string Name => Nome;
    public string Description => Descricao;
    public string Price => Preco.ToString("C2");
    public string ImageUrl => UrlImagem;
}
