using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Aplicacao.Commands.DeletePedido;
using Dominio.Interfaces;
using FluentValidation;
using MediatR;

namespace Aplicacao.Commands.DeletePedido
{
    /// <summary>
    /// Handler para o command DeletePedido
    /// </summary>
    public class DeletePedidoCommandHandler : IRequestHandler<DeletePedidoCommand, Result<DeletePedidoCommandResponse>>
    {
        private readonly IValidator<DeletePedidoCommand> _validator;
        private readonly IPedidoRepository _pedidoRepository;

        public DeletePedidoCommandHandler(
            IValidator<DeletePedidoCommand> validator,
            IPedidoRepository pedidoRepository)
        {
            _validator = validator;
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Processa o command para remover um pedido
        /// </summary>
        public async Task<Result<DeletePedidoCommandResponse>> Handle(
            DeletePedidoCommand request,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<DeletePedidoCommandResponse>.Invalid(validationResult.AsErrors());
            }

            // Buscar o pedido existente
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(request.Id);
            if (pedido == null)
            {
                return Result<DeletePedidoCommandResponse>.NotFound($"Pedido com ID {request.Id} não encontrado.");
            }

            // Remover o pedido
            await _pedidoRepository.RemoverAsync(request.Id);

            var response = new DeletePedidoCommandResponse
            {
                PedidoId = pedido.Id
            };

            return Result<DeletePedidoCommandResponse>.Success(response, "Pedido removido com sucesso.");
        }
    }
}