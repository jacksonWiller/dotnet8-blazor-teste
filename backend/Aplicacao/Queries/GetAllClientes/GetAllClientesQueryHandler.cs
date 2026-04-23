using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Clientes.Dominio.Dtos;
using Clientes.Dominio.Interfaces;
using FluentValidation;
using MediatR;

namespace Clientes.Aplicacao.Queries.GetAllClientes;

public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, Result<GetAllClientesQueryResponse>>
{
    private readonly IValidator<GetAllClientesQuery> _validator;
    private readonly IClienteRepository _clienteRepository;

    public GetAllClientesQueryHandler(IClienteRepository clienteRepository, IValidator<GetAllClientesQuery> validator)
    {
        _clienteRepository = clienteRepository;
        _validator = validator;
    }

    public async Task<Result<GetAllClientesQueryResponse>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<GetAllClientesQueryResponse>.Invalid(validationResult.AsErrors());
        }

        var (clientes, totalRecords) = await _clienteRepository.GetAllClientesAsync(
            request.Filter,
            request.Order,
            request.PageNumber,
            request.PageSize
        );

        var pagedInfo = new PagedInfo(
                                        request.PageNumber,
                                        request.PageSize,
                                        (int)Math.Ceiling((double)totalRecords / request.PageSize),
                                        totalRecords
                                        );

        var response = new GetAllClientesQueryResponse
        {
            PagedInfo = pagedInfo,
            Clientes = clientes
        };

        return Result<GetAllClientesQueryResponse>.Success(response, "Clientes retrieved successfully.");
    }
}


