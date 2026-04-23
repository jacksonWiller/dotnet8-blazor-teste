using Clientes.Dominio.Entidades;
using Clientes.Dominio.Interfaces;
using Clientes.Dominio.ObjetosDeValor;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infra.Contexto;

public class GoodHamburgerContext(DbContextOptions<GoodHamburgerContext> dbOptions) : BaseDbContext<GoodHamburgerContext>(dbOptions), IClienteContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Documento> Documentos { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Telefone> Telefones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Cliente>().ToTable("Clientes");
        modelBuilder.Entity<Documento>().ToTable("Documento");
        modelBuilder.Entity<Email>().ToTable("Email");
        modelBuilder.Entity<Endereco>().ToTable("Endereco");
        modelBuilder.Entity<Telefone>().ToTable("Telefone");
        
        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Documento)
            .WithMany()
            .HasForeignKey(c => c.DocumentoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Email)
            .WithMany()
            .HasForeignKey(c => c.EmailId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Endereco)
            .WithMany()
            .HasForeignKey(c => c.EnderecoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Telefone)
            .WithMany()
            .HasForeignKey(c => c.TelefoneId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}