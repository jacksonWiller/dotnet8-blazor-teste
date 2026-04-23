using FluentValidation;

namespace Clientes.Aplicacao.Queries.GetAllClientes;

public class GetAllClientesQueryValidator : AbstractValidator<GetAllClientesQuery>
{
    public GetAllClientesQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("O número da página deve ser maior que zero");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("O tamanho da página deve ser maior que zero");

        RuleFor(x => x.Filter)
            .MaximumLength(500)
            .WithMessage("O filtro não pode exceder 500 caracteres");

        RuleFor(x => x.Order)
            .MaximumLength(100)
            .WithMessage("A ordenação não pode exceder 100 caracteres");
    }
}
