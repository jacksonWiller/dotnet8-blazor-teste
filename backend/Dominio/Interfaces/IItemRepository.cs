using Clientes.Dominio.Dtos;
using Clientes.Dominio.Entidades;

namespace Clientes.Dominio.Interfaces
{
    /// <summary>
    /// Repositório para operações com itens do menu
    /// </summary>
    public interface IItemRepository
    {
        /// <summary>
        /// Obtém todos os itens com paginação, filtro e ordenação
        /// </summary>
        Task<(List<ItemDto>, int)> GetAllItemsAsync(
            string filter = null, 
            string order = null, 
            int pageNumber = 1, 
            int pageSize = 10);
        
        /// <summary>
        /// Obtém um item pelo ID
        /// </summary>
        Task<Item> GetItemByIdAsync(Guid itemId);
        
        /// <summary>
        /// Adiciona um novo item
        /// </summary>
        Task AdicionarAsync(Item item);
        
        /// <summary>
        /// Atualiza um item existente
        /// </summary>
        Task AtualizarAsync(Item item);
        
        /// <summary>
        /// Remove um item (soft delete)
        /// </summary>
        Task RemoverAsync(Guid itemId);
        
        /// <summary>
        /// Verifica se existe item com o mesmo nome
        /// </summary>
        Task<bool> ExisteItemComNomeAsync(string nome, Guid itemId = default);
    }
}
