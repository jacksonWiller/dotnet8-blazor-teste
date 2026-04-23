using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
