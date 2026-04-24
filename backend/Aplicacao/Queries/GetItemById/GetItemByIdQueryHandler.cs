using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Dominio.Interfaces;
using FluentValidation;
using MediatR;

namespace Aplicacao.Queries.GetItemById;

/// <summary>
/// Handler para a query GetItemById
/// </summary>
public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, Result<GetItemByIdQueryResponse>>
{
    private readonly IValidator<GetItemByIdQuery> _validator;
    private readonly Dominio.Interfaces.IItemRepository _itemRepository;

    public GetItemByIdQueryHandler(
        Dominio.Interfaces.IItemRepository itemRepository,
        IValidator<GetItemByIdQuery> validator)
    {
        _itemRepository = itemRepository;
        _validator = validator;
    }

    /// <summary>
    /// Processa a query e retorna o item
    /// </summary>
    public async Task<Result<GetItemByIdQueryResponse>> Handle(
        GetItemByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<GetItemByIdQueryResponse>.Invalid(validationResult.AsErrors());
        }

        var item = await _itemRepository.GetItemByIdAsync(request.Id);

        if (item == null)
        {
            return Result<GetItemByIdQueryResponse>.NotFound("Item não encontrado.");
        }

        var response = new GetItemByIdQueryResponse
        {
            Item = new Dominio.Dtos.ItemDto
            {
                Id = item.Id,
                Nome = item.Nome,
                Descricao = item.Descricao,
                Preco = item.Preco,
                Tipo = item.Tipo,
                Categoria = item.Categoria,
                UrlImagem = item.UrlImagem,
                Removido = item.Removido
            }
        };

        return Result<GetItemByIdQueryResponse>.Success(response, "Item recuperado com sucesso.");
    }
}
