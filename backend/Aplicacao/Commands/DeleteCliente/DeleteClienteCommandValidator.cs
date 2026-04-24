using FluentValidation;

namespace Clientes.Aplicacao.Commands.DeleteCliente;

public class DeleteClienteCommandValidator : AbstractValidator<DeleteClienteCommand>
{
    public DeleteClienteCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("O Id do cliente é obrigatório");
    }
}
