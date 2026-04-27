using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.ObjetosDeValor;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contexto;

public class GoodHamburgerContext(DbContextOptions<GoodHamburgerContext> dbOptions) : BaseDbContext<GoodHamburgerContext>(dbOptions), IGoodHamburgerContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");
            
            entity.Property(e => e.Nome)
                .HasColumnType("text")
                .IsRequired();
            
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .IsRequired();
            
            entity.Property(e => e.Preco)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            entity.Property(e => e.Tipo)
                .HasColumnType("text")
                .IsRequired();
            
            entity.Property(e => e.Categoria)
                .HasColumnType("text")
                .IsRequired();
            
            entity.Property(e => e.UrlImagem)
                .HasColumnType("text");
            
            entity.Property(e => e.Removido)
                .IsRequired()
                .HasDefaultValue(false);
            
            entity.HasIndex(e => e.Tipo);
            entity.HasIndex(e => e.Categoria);

            entity.ToTable("Item");
        });
        

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");
            
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            entity.Property(e => e.Desconto)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            entity.Property(e => e.DataCriacao)
                .HasColumnType("timestamp with time zone")
                .IsRequired();
            
            entity.HasMany(p => p.Itens)
                .WithOne()
                .HasForeignKey("PedidoId")
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasIndex(e => e.DataCriacao);

            entity.ToTable("Pedido");
        });
        
        // Configurar entidade PedidoItem
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