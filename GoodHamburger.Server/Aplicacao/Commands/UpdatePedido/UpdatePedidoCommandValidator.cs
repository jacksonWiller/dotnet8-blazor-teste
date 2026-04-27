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

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("O status do pedido é inválido.");

            RuleFor(x => x.Itens)
                .NotEmpty()
                .WithMessage("A lista de itens é obrigatória.");

            RuleForEach(x => x.Itens)
                .SetValidator(new UpdatePedidoItemDtoValidator());
        }
    }

    /// <summary>
    /// Validator para o DTO de item de atualização
    /// </summary>
    public class UpdatePedidoItemDtoValidator : AbstractValidator<UpdatePedidoItemDto>
    {
        public UpdatePedidoItemDtoValidator()
        {
            RuleFor(x => x.ItemId)
                .NotEmpty()
                .WithMessage("O ID do item é obrigatório.");

            RuleFor(x => x.Quantidade)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A quantidade deve ser pelo menos 1.");
        }
    }
}