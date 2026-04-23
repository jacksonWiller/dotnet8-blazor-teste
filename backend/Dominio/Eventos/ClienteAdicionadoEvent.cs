using Clientes.Dominio.Entidades;

namespace Clientes.Dominio.Eventos
{
    public class AdicionandoClienteEvent(Cliente cliente) : EventoBase
    {
        public Cliente Cliente { get; } = cliente;
    }
}
