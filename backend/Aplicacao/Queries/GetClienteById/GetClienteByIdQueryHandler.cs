using Ardalis.Result;
using Clientes.Aplicacao.Queries.GetProductById;
using Clientes.Dominio.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clientes.Aplicacao.Queries.GetClienteById
{
    public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, Result<GetClienteByIdQueryResponse>>
    {
        private readonly IClienteRepository _clienteRepository;

        public GetClienteByIdQueryHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Result<GetClienteByIdQueryResponse>> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(request.Id);

            if (cliente == null)
            {
                return Result.NotFound($"Cliente com ID {request.Id} não encontrado");
            }

            var response = GetClienteByIdQueryResponse.MapFrom(cliente);
            return Result<GetClienteByIdQueryResponse>.Success(response);
        }
    }
}