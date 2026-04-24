using Ardalis.Result;
using MediatR;

namespace Aplicacao.Commands.DeleteCliente;

public class DeleteClienteCommand : IRequest<Result<DeleteClienteResponse>>
{
    public Guid Id { get; }

    public DeleteClienteCommand(Guid id)
    {
        Id = id;
    }
}
