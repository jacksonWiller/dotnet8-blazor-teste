using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.ObjetosDeValor;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contexto;

public class GoodHamburgerContext(DbContextOptions<GoodHamburgerContext> dbOptions) : BaseDbContext<GoodHamburgerContext>(dbOptions), IGoodHamburgerContext
{
    public DbSet<Item> Itens { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<PedidoItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");
            
            entity.Property(e => e.PedidoId)
                .HasColumnType("uuid")
                .IsRequired();
            
            entity.Property(e => e.ItemId)
                .HasColumnType("uuid")
                .IsRequired();
            
            entity.Property(e => e.Nome)
                .HasColumnType("text")
                .IsRequired();
            
            entity.Property(e => e.Categoria)
                .HasColumnType("text")
                .IsRequired();
            
            entity.Property(e => e.PrecoUnitario)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            entity.Property(e => e.Quantidade)
                .IsRequired();
            
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            entity.HasOne<Pedido>()
                .WithMany(p => p.Itens)
                .HasForeignKey(e => e.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne<Item>()
                .WithMany()
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasIndex(e => e.PedidoId);
            entity.HasIndex(e => e.ItemId);
            
            entity.ToTable("PedidoItem");
        });
    }
}