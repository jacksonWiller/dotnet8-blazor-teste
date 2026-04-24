using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.ObjetosDeValor;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contexto;

public class GoodHamburgerContext(DbContextOptions<GoodHamburgerContext> dbOptions) : BaseDbContext<GoodHamburgerContext>(dbOptions), IGoodHamburgerContext
{
    public DbSet<Documento> Documentos { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Telefone> Telefones { get; set; }
    public DbSet<Item> Itens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
    }
}