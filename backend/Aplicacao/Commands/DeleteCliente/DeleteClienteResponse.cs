namespace Clientes.Aplicacao.Commands.DeleteCliente;

public class DeleteClienteResponse(Guid id)
{
    public Guid Id { get; } = id;
}
