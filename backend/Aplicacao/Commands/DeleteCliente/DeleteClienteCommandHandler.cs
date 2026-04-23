using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Clientes.Dominio.Interfaces;
using FluentValidation;
using MediatR;

namespace Clientes.Aplicacao.Commands.DeleteCliente;

public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, Result<DeleteClienteResponse>>
{
    private readonly IValidator<DeleteClienteCommand> _validator;
    private readonly IClienteRepository _clienteRepository;

    public DeleteClienteCommandHandler(IValidator<DeleteClienteCommand> validator, IClienteContext context, IClienteRepository clienteRepository)
    {
        _validator = validator;
        _clienteRepository = clienteRepository;
    }

    public async Task<Result<DeleteClienteResponse>> Handle(DeleteClienteCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<DeleteClienteResponse>.Invalid(validationResult.AsErrors());
        }

        var cliente = await _clienteRepository.GetClienteByIdAsync(command.Id);
        if (cliente == null)
        {
            return Result.NotFound($"Cliente com ID {command.Id} não encontrado");
        }

        cliente.Remover();

        await _clienteRepository.AtualizarAsync(cliente);

        var response = new DeleteClienteResponse(cliente.Id);
        return Result<DeleteClienteResponse>.Success((DeleteClienteResponse)response, "Product created successfully.");
    }

}
