using Aplicacao.Queries.GetAllItems;
using Aplicacao.Queries.GetItemById;
using Ardalis.Result;
using Clientes.Api.Extensions;
using Clientes.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace Clientes.Api.Controllers;

/// <summary>
/// Controller para gerenciamento de itens do menu da GoodHamburger
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ItensController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItensController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //////////////////////
    // GET: /api/items
    //////////////////////

    /// <summary>
    /// Obtém uma lista paginada de itens do menu com filtro e ordenação
    /// </summary>
    /// <param name="query">Parâmetros de paginação, filtro e ordenação</param>
    /// <param name="query.Filter">Filtro para busca (ex: "Nome:burger", "Preco>10")</param>
    /// <param name="query.Order">Ordenação (ex: "Nome", "Preco DESC")</param>
    /// <param name="query.PageNumber">Número da página (padrão: 1)</param>
    /// <param name="query.PageSize">Tamanho da página (padrão: 10)</param>
    /// <response code="200">Retorna a lista paginada de itens.</response>
    /// <response code="400">Retorna lista de erros se a requisição for inválida.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<GetAllItemsQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllItemsQuery query) =>
        (await _mediator.Send(query)).ToActionResult();

    ////////////////////////////
    // GET: /api/items/{id}
    ////////////////////////////

    /// <summary>
    /// Obtém um item pelo ID
    /// </summary>
    /// <param name="id">ID do item a ser buscado</param>
    /// <response code="200">Retorna o item encontrado.</response>
    /// <response code="404">Quando nenhum item é encontrado pelo ID informado.</response>
    /// <response code="400">Retorna lista de erros se o ID for inválido.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpGet("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<GetItemByIdQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([Required] Guid id) =>
        (await _mediator.Send(new GetItemByIdQuery { Id = id })).ToActionResult();
}
