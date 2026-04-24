using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio
{
    /// <summary>
    /// Implementação do repositório para pedidos
    /// </summary>
    public class PedidoRepository : IPedidoRepository
    {
        private readonly GoodHamburgerContext _dataContext;

        public PedidoRepository(GoodHamburgerContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _dataContext.Pedidos.AddAsync(pedido);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> ExistePedidoAsync(Guid pedidoId)
        {
            return await _dataContext.Pedidos.AnyAsync(p => p.Id == pedidoId);
        }

        public async Task<Pedido> GetPedidoByIdAsync(Guid pedidoId)
        {
            return await _dataContext.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);
        }

        public async Task<(List<PedidoDto>, int)> GetAllPedidosAsync(int pageNumber = 1, int pageSize = 10)
        {
            var query = _dataContext.Pedidos
                .Include(p => p.Itens)
                .OrderByDescending(p => p.DataCriacao);

            var totalCount = await query.CountAsync();
            var pedidos = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtos = pedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                Itens = p.Itens.Select(i => new PedidoItemDto
                {
                    ItemId = i.ItemId,
                    ItemNome = i.Nome,
                    Categoria = i.Categoria,
                    PrecoUnitario = i.PrecoUnitario,
                    Quantidade = i.Quantidade
                }).ToList(),
                Subtotal = p.Subtotal,
                Desconto = p.Desconto,
                Total = p.Total,
                DataCriacao = p.DataCriacao
            }).ToList();

            return (dtos, totalCount);
        }

        public async Task RemoverAsync(Guid pedidoId)
        {
            var pedido = await _dataContext.Pedidos.FindAsync(pedidoId);
            if (pedido != null)
            {
                _dataContext.Pedidos.Remove(pedido);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            _dataContext.Pedidos.Update(pedido);
            await _dataContext.SaveChangesAsync();
        }
    }
}
