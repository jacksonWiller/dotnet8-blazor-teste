using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Clientes.Dominio.Entidades;
using Clientes.Dominio.Interfaces;
using Clientes.Dominio.ObjetosDeValor;
using FluentValidation;
using MediatR;

namespace Clientes.Aplicacao.Commands.UpadateCliente
{
    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, Result<UpdateClienteResponse>>
    {
        private readonly IValidator<UpdateClienteCommand> _validator;
        private readonly IClienteRepository _clienteRepository;

        public UpdateClienteCommandHandler(
            IValidator<UpdateClienteCommand> validator,
            IClienteRepository clienteRepository)
        {
            _validator = validator;
            _clienteRepository = clienteRepository;
        }

        public async Task<Result<UpdateClienteResponse>> Handle(UpdateClienteCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var cliente = await _clienteRepository.GetClienteByIdAsync(command.Id);
            if (cliente == null)
            {
                return Result.NotFound($"Cliente com ID {command.Id} não encontrado");
            }

            if (cliente.Documento.Numero != command.Documento)
            {
                var clienteExistente = await _clienteRepository.GetClienteByDocumentoAsync(command.Documento);
                if (clienteExistente != null && clienteExistente.Id != command.Id)
                {
                    return Result.Error("Já existe um cliente com este documento");
                }
            }

            if (cliente.Email.Endereco != command.Email)
            {
                var clienteExistente = await _clienteRepository.GetClienteByEmailAsync(command.Email);
                if (clienteExistente != null && clienteExistente.Id != command.Id)
                {
                    return Result.Error("Já existe um cliente com este e-mail");
                }
            }

            var documento = new Documento(command.Documento, command.TipoDocumento);
            var telefone = new Telefone(command.Telefone);
            var email = new Email(command.Email);
            var endereco = new Endereco(command.Cep, command.Logradouro, command.Numero,
                                      command.Bairro, command.Cidade, command.Estado);

            cliente.AtualizarDados(
                command.Nome,
                documento,
                command.DataNascimento,
                telefone,
                email,
                endereco,
                command.InscricaoEstadual,
                command.Isento
            );


            await _clienteRepository.AtualizarAsync(cliente);

            var response = new UpdateClienteResponse(cliente.Id);
            return Result<UpdateClienteResponse>.Success(response, "Product created successfully.");
        }
    }
}
