using Dominio.Entidades;
using Dominio.ObjetosDeValor;

namespace Testes;

public class PedidoTestes
{
    // ── Helpers ──────────────────────────────────────────────────────────────

    private static Item CriarSanduiche(decimal preco = 20m) =>
        new("X-Burguer", "Sanduíche clássico", preco, "burger", "Sanduíche");

    private static Item CriarAcompanhamento(decimal preco = 10m) =>
        new("Batata Frita", "Batata crocante", preco, "side", "Acompanhamento");

    private static Item CriarBebida(decimal preco = 8m) =>
        new("Refrigerante", "Coca gelada", preco, "drink", "Bebida");

    private static Pedido NovoPedido() => new(Guid.NewGuid());

    // ── Restrições de itens ───────────────────────────────────────────────────

    [Fact]
    public void AdicionarSanduiche_DevePermitir_QuandoPedidoVazio()
    {
        var pedido = NovoPedido();
        pedido.AdicionarItem(CriarSanduiche());
        Assert.Single(pedido.Itens);
    }

    [Fact]
    public void AdicionarSegundoSanduiche_DeveLancarExcecao()
    {
        var pedido = NovoPedido();
        pedido.AdicionarItem(CriarSanduiche());
        Assert.Throws<ArgumentException>(() => pedido.AdicionarItem(CriarSanduiche()));
    }

    [Fact]
    public void AdicionarSegundoAcompanhamento_DeveLancarExcecao()
    {
        var pedido = NovoPedido();
        pedido.AdicionarItem(CriarAcompanhamento());
        Assert.Throws<ArgumentException>(() => pedido.AdicionarItem(CriarAcompanhamento()));
    }

    [Fact]
    public void AdicionarSegundaBebida_DeveLancarExcecao()
    {
        var pedido = NovoPedido();
        pedido.AdicionarItem(CriarBebida());
        Assert.Throws<ArgumentException>(() => pedido.AdicionarItem(CriarBebida()));
    }

    // ── Cálculo de totais ─────────────────────────────────────────────────────

    [Fact]
    public void Subtotal_DeveSerSomaDosPrecos()
    {
        var pedido = NovoPedido();
        pedido.AdicionarItem(CriarSanduiche(20m));
        pedido.AdicionarItem(CriarAcompanhamento(10m));
        Assert.Equal(30m, pedido.Subtotal);
    }

    // ── Regras de desconto ────────────────────────────────────────────────────

    [Fact]
    public void Desconto_SanduicheAcompanhamentoBebida_Deve_Ser_20Porcento()
    {
        var pedido = NovoPedido();
        pedido.AdicionarItem(CriarSanduiche(20m));
        pedido.AdicionarItem(CriarAcompanhamento(10m));
        pedido.AdicionarItem(CriarBebida(8m));

        var subtotal = 38m;
        Assert.Equal(subtotal * 0.20m, pedido.Desconto);
        Assert.Equal(subtotal - subtotal * 0.20m, pedido.Total);
    }

    [Fact]
    public void Desconto_SanduicheBebida_Deve_Ser_15Porcento()
    {
        var pedido = NovoPedido();
        pedido.AdicionarItem(CriarSanduiche(20m));
        pedido.AdicionarItem(CriarBebida(8m));

        var subtotal = 28m;
        Assert.Equal(subtotal * 0.15m, pedido.Desconto);
        Assert.Equal(subtotal - subtotal * 0.15m, pedido.Total);
    }

    [Fact]
    public void Desconto_SanduicheAcompanhamento_Deve_Ser_10Porcento()
    {
        var pedido = NovoPedido();
        pedido.AdicionarItem(CriarSanduiche(20m));
        pedido.AdicionarItem(CriarAcompanhamento(10m));

        var subtotal = 30m;
        Assert.Equal(subtotal * 0.10m, pedido.Desconto);
        Assert.Equal(subtotal - subtotal * 0.10m, pedido.Total);
    }

    [Fact]
    public void Desconto_ApenasSanduiche_Deve_Ser_Zero()
    {
        var pedido = NovoPedido();
        pedido.AdicionarItem(CriarSanduiche(20m));

        Assert.Equal(0m, pedido.Desconto);
        Assert.Equal(20m, pedido.Total);
    }

    [Fact]
    public void Desconto_ApenasBebida_Deve_Ser_Zero()
    {
        var pedido = NovoPedido();
        pedido.AdicionarItem(CriarBebida(8m));

        Assert.Equal(0m, pedido.Desconto);
    }

    // ── Transições de status ──────────────────────────────────────────────────

    [Fact]
    public void MudarStatus_Pendente_Para_EmPreparacao_DevePermitir()
    {
        var pedido = NovoPedido();
        pedido.MudarStatus(PedidoStatus.EmPreparacao);
        Assert.Equal(PedidoStatus.EmPreparacao, pedido.Status);
    }

    [Fact]
    public void MudarStatus_EmPreparacao_Para_Pronto_DevePermitir()
    {
        var pedido = NovoPedido();
        pedido.MudarStatus(PedidoStatus.EmPreparacao);
        pedido.MudarStatus(PedidoStatus.Pronto);
        Assert.Equal(PedidoStatus.Pronto, pedido.Status);
    }

    [Fact]
    public void MudarStatus_Pronto_Para_Entregue_DevePermitir()
    {
        var pedido = NovoPedido();
        pedido.MudarStatus(PedidoStatus.EmPreparacao);
        pedido.MudarStatus(PedidoStatus.Pronto);
        pedido.MudarStatus(PedidoStatus.Entregue);
        Assert.Equal(PedidoStatus.Entregue, pedido.Status);
    }

    [Fact]
    public void MudarStatus_Pendente_Para_Entregue_DeveLancarExcecao()
    {
        var pedido = NovoPedido();
        Assert.Throws<InvalidOperationException>(() => pedido.MudarStatus(PedidoStatus.Entregue));
    }

    [Fact]
    public void MudarStatus_Entregue_Para_Cancelado_DeveLancarExcecao()
    {
        var pedido = NovoPedido();
        pedido.MudarStatus(PedidoStatus.EmPreparacao);
        pedido.MudarStatus(PedidoStatus.Pronto);
        pedido.MudarStatus(PedidoStatus.Entregue);
        Assert.Throws<InvalidOperationException>(() => pedido.MudarStatus(PedidoStatus.Cancelado));
    }

    // ── Cancelamento ──────────────────────────────────────────────────────────

    [Fact]
    public void CancelarPedido_Pendente_DevePermitir()
    {
        var pedido = NovoPedido();
        pedido.CancelarPedido();
        Assert.Equal(PedidoStatus.Cancelado, pedido.Status);
    }

    [Fact]
    public void CancelarPedido_JaCancelado_DeveLancarExcecao()
    {
        var pedido = NovoPedido();
        pedido.CancelarPedido();
        Assert.Throws<InvalidOperationException>(() => pedido.CancelarPedido());
    }

    [Fact]
    public void CancelarPedido_Entregue_DeveLancarExcecao()
    {
        var pedido = NovoPedido();
        pedido.MudarStatus(PedidoStatus.EmPreparacao);
        pedido.MudarStatus(PedidoStatus.Pronto);
        pedido.MudarStatus(PedidoStatus.Entregue);
        Assert.Throws<InvalidOperationException>(() => pedido.CancelarPedido());
    }

    // ── Status inicial ────────────────────────────────────────────────────────

    [Fact]
    public void NovoPedido_StatusInicial_DeveSer_Pendente()
    {
        var pedido = NovoPedido();
        Assert.Equal(PedidoStatus.Pendente, pedido.Status);
    }

    [Fact]
    public void NovoPedido_TotalInicial_DeveSer_Zero()
    {
        var pedido = NovoPedido();
        Assert.Equal(0m, pedido.Total);
        Assert.Equal(0m, pedido.Subtotal);
        Assert.Equal(0m, pedido.Desconto);
    }
}
