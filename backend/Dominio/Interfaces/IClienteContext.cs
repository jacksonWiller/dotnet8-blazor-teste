using Microsoft.EntityFrameworkCore;

namespace Clientes.Dominio.Interfaces
{
    public interface IClienteContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
