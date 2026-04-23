using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Clientes.Dominio.Entidades;
using Clientes.Dominio.Interfaces;
using Clientes.Dominio.ObjetosDeValor;
using FluentValidation;
using MediatR;

namespace Clientes.Aplicacao.Commands.CreateCliente;

public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Result<CreateClienteResponse>>
{
    private readonly IValidator<CreateClienteCommand> _validator;
    private readonly IClienteRepository _clienteRepository;

    public CreateClienteCommandHandler(
        IValidator<CreateClienteCommand> validator,
        IClienteRepository clienteRepository
    )
    {
        _clienteRepository = clienteRepository;
        _validator = validator;
    }

    public async Task<Result<CreateClienteResponse>> Handle(CreateClienteCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<CreateClienteResponse>.Invalid(validationResult.AsErrors());
        }

        var documento = new Documento(
            command.Documento, 
            command.TipoDocumento
            );
        var telefone = new Telefone(command.Telefone);
        var email = new Email(command.Email);
        var endereco = new Endereco(
            command.Cep, 
            command.Logradouro, 
            command.Numero,                   
            command.Bairro, 
            command.Cidade, 
            command.Estado);

        var cliente = new Cliente(
            command.Nome, 
            documento, 
            command.DataNascimento,
            telefone, 
            email, 
            endereco, 
            command.InscricaoEstadual, 
            command.Isento);

        await _clienteRepository.AdicionarAsync(cliente);

        var response = new CreateClienteResponse(cliente.Id);
        return Result<CreateClienteResponse>.Success(response, "Product created successfully.");
    }
}
