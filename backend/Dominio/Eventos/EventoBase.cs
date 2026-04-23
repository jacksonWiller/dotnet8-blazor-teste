namespace Clientes.Dominio.Eventos
{
    public abstract class EventoBase
    {
        public Guid Id { get; private set; }
        public DateTime DataOcorrencia { get; private set; }

    }
}
