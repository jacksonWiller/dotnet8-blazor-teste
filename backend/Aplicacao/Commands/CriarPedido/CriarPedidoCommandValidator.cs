using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;

namespace Aplicacao.Commands.CriarPedido
{
    /// <summary>
    /// Validator para o command CriarPedido
    /// </summary>
    public class CriarPedidoCommandValidator : AbstractValidator<CriarPedidoCommand>
    {
        public CriarPedidoCommandValidator()
        {
            RuleFor(x => x.ItensIds)
                .NotEmpty()
                .WithMessage("É necessário adicionar pelo menos um item ao pedido.");

            RuleFor(x => x.ItensIds)
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .WithMessage("Itens duplicados não são permitidos.");

            RuleFor(x => x.ItensIds)
                .MaximumLength(3)
                .WithMessage("Um pedido pode conter no máximo 3 itens (1 sanduíche, 1 acompanhamento, 1 bebida).");
        }
    }
}
