using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Dominio.Interfaces;
using FluentValidation;
using MediatR;

namespace Aplicacao.Queries.GetAllItems;

/// <summary>
/// Handler para a query GetAllItems
/// </summary>
public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, Result<GetAllItemsQueryResponse>>
{
    private readonly IValidator<GetAllItemsQuery> _validator;
    private readonly Dominio.Interfaces.IItemRepository _itemRepository;

    public GetAllItemsQueryHandler(
        Dominio.Interfaces.IItemRepository itemRepository, 
        IValidator<GetAllItemsQuery> validator)
    {
        _itemRepository = itemRepository;
        _validator = validator;
    }

    /// <summary>
    /// Processa a query e retorna os itens paginados
    /// </summary>
    public async Task<Result<GetAllItemsQueryResponse>> Handle(
        GetAllItemsQuery request, 
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<GetAllItemsQueryResponse>.Invalid(validationResult.AsErrors());
        }

        var (itens, totalRecords) = await _itemRepository.GetAllItemsAsync(
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

        var response = new GetAllItemsQueryResponse
        {
            PagedInfo = pagedInfo,
            Itens = itens
        };

        return Result<GetAllItemsQueryResponse>.Success(response, "Itens do menu recuperados com sucesso.");
    }
}
