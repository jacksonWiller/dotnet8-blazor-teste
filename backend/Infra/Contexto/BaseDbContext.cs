using Microsoft.EntityFrameworkCore;

namespace Clientes.Infra.Contexto;

public abstract class BaseDbContext<TContext>(DbContextOptions<TContext> dbOptions) : DbContext(dbOptions)
    where TContext : DbContext
{

}
