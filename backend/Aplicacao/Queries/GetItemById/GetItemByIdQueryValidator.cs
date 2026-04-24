using FluentValidation;

namespace Clientes.Aplicacao.Queries.GetItemById;

/// <summary>
/// Validator para a query GetItemById
/// </summary>
public class GetItemByIdQueryValidator : AbstractValidator<GetItemByIdQuery>
{
    public GetItemByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O ID do item é obrigatório.");
    }
}
