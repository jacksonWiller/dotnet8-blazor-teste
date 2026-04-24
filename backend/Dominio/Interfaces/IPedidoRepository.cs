using Dominio.Dtos;
using Dominio.Entidades;

namespace Dominio.Interfaces
{
    /// <summary>
    /// Repositório para operações com pedidos
    /// </summary>
    public interface IPedidoRepository
    {
        /// <summary>
        /// Obtém todos os pedidos com paginação
        /// </summary>
        Task<(List<PedidoDto>, int)> GetAllPedidosAsync(int pageNumber = 1, int pageSize = 10);
        
        /// <summary>
        /// Obtém um pedido pelo ID
        /// </summary>
        Task<Pedido> GetPedidoByIdAsync(Guid pedidoId);
        
        /// <summary>
        /// Adiciona um novo pedido
        /// </summary>
        Task AdicionarAsync(Pedido pedido);
        
        /// <summary>
        /// Atualiza um pedido existente
        /// </summary>
        Task AtualizarAsync(Pedido pedido);
        
        /// <summary>
        /// Remove um pedido
        /// </summary>
        Task RemoverAsync(Guid pedidoId);
        
        /// <summary>
        /// Verifica se um pedido existe pelo ID
        /// </summary>
        Task<bool> ExistePedidoAsync(Guid pedidoId);
    }
}
