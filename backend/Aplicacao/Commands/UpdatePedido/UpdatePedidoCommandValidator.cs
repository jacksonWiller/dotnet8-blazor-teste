using FluentValidation;

namespace Aplicacao.Commands.UpdatePedido
{
    /// <summary>
    /// Validator para o command UpdatePedido
    /// </summary>
    public class UpdatePedidoCommandValidator : AbstractValidator<UpdatePedidoCommand>
    {
        public UpdatePedidoCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O ID do pedido é obrigatório.");

            RuleFor(x => x.ItensIds)
                .NotEmpty()
                .WithMessage("A lista de itens é obrigatória.");
        }
    }
}