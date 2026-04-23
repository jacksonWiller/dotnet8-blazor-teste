using Ardalis.Result;
using MediatR;

namespace Clientes.Aplicacao.Queries.GetClienteById
{
    public class GetClienteByIdQuery : IRequest<Result<GetClienteByIdQueryResponse>>
    {
        public Guid Id { get; set; }

        public GetClienteByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}