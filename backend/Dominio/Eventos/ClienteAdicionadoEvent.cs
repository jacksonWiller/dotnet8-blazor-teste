using Dominio.Entidades;

namespace Dominio.Eventos
{
    public class AdicionandoClienteEvent(Cliente cliente) : EventoBase
    {
        public Cliente Cliente { get; } = cliente;
    }
}
