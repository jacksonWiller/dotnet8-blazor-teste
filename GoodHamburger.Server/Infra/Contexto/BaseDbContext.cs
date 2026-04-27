using Microsoft.EntityFrameworkCore;

namespace Infra.Contexto;

public abstract class BaseDbContext<TContext>(DbContextOptions<TContext> dbOptions) : DbContext(dbOptions)
    where TContext : DbContext
{

}
