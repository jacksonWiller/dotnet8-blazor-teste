using FluentValidation;

namespace Aplicacao.Commands.DeletePedido
{
    public class DeletePedidoCommandValidator : AbstractValidator<DeletePedidoCommand>
    {
        public DeletePedidoCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O ID do pedido é obrigatório.");
        }
    }
}