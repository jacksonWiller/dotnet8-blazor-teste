using Aplicacao.Commands.CriarPedido;
using Aplicacao.Commands.DeletePedido;
using Aplicacao.Commands.UpdatePedido;
using Aplicacao.Queries.GetAllPedidos;
using Aplicacao.Queries.GetPedidoById;
using Ardalis.Result;
using Clientes.Api.Extensions;
using Clientes.Api.Models;
using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Api.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de pedidos da Good Hamburger
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IItemRepository _itemRepository;

        public PedidosController(IMediator mediator, IItemRepository itemRepository)
        {
            _mediator = mediator;
            _itemRepository = itemRepository;
        }

        //////////////////////
        // GET: /api/pedidos
        //////////////////////

        /// <summary>
        /// Obtém todos os pedidos com paginação
        /// </summary>
        /// <param name="pageNumber">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10)</param>
        /// <response code="200">Retorna a lista de pedidos.</response>
        /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<List<PedidoDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllPedidosQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }

        //////////////////////
        // GET: /api/pedidos/{id}
        //////////////////////

        /// <summary>
        /// Obtém um pedido pelo ID
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <response code="200">Retorna os detalhes do pedido.</response>
        /// <response code="404">Quando o pedido não é encontrado.</response>
        /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
        [HttpGet("{id:guid}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<PedidoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetPedidoByIdQuery(id);
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }

        ////////////////////////////
        // POST: /api/pedidos
        ////////////////////////////

        /// <summary>
        /// Cria um novo pedido com os itens selecionados
        /// </summary>
        /// <param name="command">Command com os IDs dos itens do pedido</param>
        /// <response code="200">Retorna o pedido criado com subtotal, desconto e total.</response>
        /// <response code="400">Retorna lista de erros se a requisição for inválida.</response>
        /// <response code="404">Quando algum item não é encontrado no cardápio.</response>
        /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<CriarPedidoCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CriarPedidoCommand command) =>
            (await _mediator.Send(command)).ToActionResult();

        ////////////////////////////
        // PUT: /api/pedidos/{id}
        ////////////////////////////

        /// <summary>
        /// Atualiza um pedido existente
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <param name="command">Command com os dados atualizados</param>
        /// <response code="200">Retorna o pedido atualizado.</response>
        /// <response code="400">Retorna lista de erros se a requisição for inválida.</response>
        /// <response code="404">Quando o pedido não é encontrado.</response>
        /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
        [HttpPut("{id:guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<UpdatePedidoCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePedidoCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }
    }
}
