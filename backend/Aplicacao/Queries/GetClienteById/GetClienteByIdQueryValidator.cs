using Clientes.Aplicacao.Queries.GetClienteById;
using FluentValidation;

namespace Clientes.Aplicacao.Queries.GetProductById;
public class GetProductByIdQueryValidator : AbstractValidator<GetClienteByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(command => command.Id)
           .NotEmpty();
    }
}