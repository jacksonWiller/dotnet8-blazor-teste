using FluentValidation;

namespace Aplicacao.Queries.GetAllItems;

/// <summary>
/// Validator para a query GetAllItems
/// </summary>
public class GetAllItemsQueryValidator : AbstractValidator<GetAllItemsQuery>
{
    public GetAllItemsQueryValidator()
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
