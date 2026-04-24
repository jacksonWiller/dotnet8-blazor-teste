using Microsoft.EntityFrameworkCore;

namespace Dominio.Interfaces
{
    public interface IGoodHamburgerContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
