using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Fop;
using Fop.FopExpression;
using Infra.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Repositorio
{
    /// <summary>
    /// Implementação do repositório para itens do menu
    /// </summary>
    public class ItemRepository : IItemRepository
    {
        private readonly GoodHamburgerContext _dataContext;

        public ItemRepository(GoodHamburgerContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Item>> GetByExpressionAsync(Expression<Func<Item, bool>> predicate)
        {
            return await _dataContext.Items
                .Where(predicate)   
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtém todos os itens com paginação, filtro e ordenação
        /// </summary>
        public async Task<(List<ItemDto>, int)> GetAllItemsAsync(
            string filter = null,
            string order = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var fopRequest = FopExpressionBuilder<Item>.Build(filter, order, pageNumber, pageSize);

            var query = _dataContext.Items
                .Where(x => x.Removido)
                .AsNoTracking();

            var (filteredItens, totalRecords) = query
                .ApplyFop(fopRequest);

            var itensLista = await filteredItens.ToListAsync();

            var itensListaDto = itensLista.Select(i => new ItemDto
            {
                Id = i.Id,
                Nome = i.Nome,
                Descricao = i.Descricao,
                Preco = i.Preco,
                Tipo = i.Tipo,
                Categoria = i.Categoria,
                UrlImagem = i.UrlImagem,
                Ativo = i.Removido
            }).ToList();

            return (itensListaDto, totalRecords);
        }

        /// <summary>
        /// Obtém um item pelo ID
        /// </summary>
        public async Task<Item> GetItemByIdAsync(Guid itemId)
        {
            var query = _dataContext.Items
                .Where(c => c.Id == itemId && c.Removido == false)
                .AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Adiciona um novo item
        /// </summary>
        public async Task AdicionarAsync(Item item)
        {
            _dataContext.Items.Add(item);
            await _dataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza um item existente
        /// </summary>
        public async Task AtualizarAsync(Item item)
        {
            _dataContext.Items.Update(item);
            await _dataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Remove um item (soft delete)
        /// </summary>
        public async Task RemoverAsync(Guid itemId)
        {
            var item = await _dataContext.Items
                .FirstOrDefaultAsync(x => x.Id == itemId);

            if (item != null)
            {
                item.Remover();
                await _dataContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verifica se existe item com o mesmo nome
        /// </summary>
        public async Task<bool> ExisteItemComNomeAsync(string nome, Guid itemId = default)
        {
            return await _dataContext.Items
                .AnyAsync(i => i.Nome == nome && i.Id != itemId && i.Removido);
        }

        /// <summary>
        /// Obtém múltiplos itens por IDs
        /// </summary>
        public async Task<List<Item>> GetItemsByIdsAsync(List<Guid> itemIds)
        {
            if (itemIds == null || !itemIds.Any())
            {
                return new List<Item>();
            }

            return await _dataContext.Items
                .Where(i => itemIds.Contains(i.Id) && i.Removido)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
