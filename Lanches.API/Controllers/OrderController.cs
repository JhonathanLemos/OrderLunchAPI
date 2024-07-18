using AutoMapper;
using Lanches.Application.Commands.Orders.CreateOrder;
using Lanches.Application.Commands.Orders.DeleteOrder;
using Lanches.Application.Commands.Orders.UpdateOrder;
using Lanches.Application.Dtos;
using Lanches.Application.Dtos.ViewModels;
using Lanches.Application.Queries.Orders.GetAllOrder;
using Lanches.Application.Queries.Orders.GetLunchByOrderIds;
using Lanches.Application.Queries.Orders.GetOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lanches.API.Controllers
{
    [Authorize(Roles = "Admin, Customer")]
    [Route("api/Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Orders
        ///     {
        ///         "name": "Novo Pedido"
        ///     }
        ///
        /// </remarks>
        /// <param name="createOrderCommand">Dados do pedido a ser criado.</param>
        /// <response code="201">Retorna o pedido criado.</response>
        [HttpPost]
        [Authorize(Policy = "Permissions.Order.Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateOrderCommand createOrderCommand)
        {
            var userClaims = HttpContext.User;
            var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            createOrderCommand.SetUserId(userId);
            var id = await _mediator.Send(createOrderCommand);
            createOrderCommand.SetId(id);
            return CreatedAtAction(nameof(GetById), new { Id = id }, _mapper.Map<CreateOrUpdateOrderViewModel>(createOrderCommand));
        }

        /// <summary>
        /// Atualiza um pedido existente pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Orders/1
        ///     {
        ///         "name": "Pedido Atualizado"
        ///     }
        ///
        /// </remarks>
        /// <param name="OrderId">ID do pedido a ser atualizado.</param>
        /// <param name="updateOrderCommand">Dados atualizados do pedido.</param>
        /// <response code="200">Retorna o ID do pedido atualizado.</response>
        /// <response code="400">Se o pedido com o ID especificado não foi encontrado.</response>
        [HttpPut("{OrderId}")]
        [Authorize(Policy = "Permissions.Order.Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid orderId, [FromBody] UpdateOrderCommand updateOrderCommand)
        {
            if (orderId != updateOrderCommand.Id)
            {
                return BadRequest("O ID do ingrediente na URL não corresponde ao ID no corpo da requisição.");
            }
            var userClaims = HttpContext.User;
            var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            updateOrderCommand.SetUserId(userId);
            updateOrderCommand.SetId(orderId);
            var id = await _mediator.Send(updateOrderCommand);
            return CreatedAtAction(nameof(GetById), new { Id = id }, _mapper.Map<CreateOrUpdateOrderViewModel>(updateOrderCommand));
        }

        /// <summary>
        /// Busca um pedido pelo ID.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <response code="200">Retorna o pedido encontrado.</response>
        /// <response code="400">Se o pedido com o ID especificado não foi encontrado.</response>
        [HttpGet("{id}")]
        [Authorize(Policy = "Permissions.Order.View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetOrderQuery(id);
            var order = await _mediator.Send(query);
            return order != null ? Ok(order) : BadRequest($"Pedido com ID {id} não foi encontrado.");
        }

        /// <summary>
        /// Busca todos os pedidos do usuário autenticado.
        /// </summary>
        /// <response code="200">Retorna os pedidos encontrados.</response>
        /// <response code="400">Se ocorrer um erro ao buscar os pedidos.</response>
        [HttpGet("GetAllMyOrders")]
        [Authorize(Policy = "Permissions.Order.View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllMyOrders()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = new GetAllMyOrderQuery(userId);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        /// <summary>
        /// Busca todos os pedidos do sistema.
        /// </summary>
        /// <response code="200">Retorna todos os pedidos do sistema.</response>
        [HttpGet]
        [Authorize(Policy = "Permissions.Order.View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAll getAll)
        {
            var ordersQuery = new GetAllOrderQuery(getAll);
            var orders = await _mediator.Send(ordersQuery);
            return Ok(orders);
        }

        /// <summary>
        /// Busca um lanche pelo ID do pedido.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <response code="200">Retorna os lanches do pedido.</response>
        /// <response code="400">Se o pedido com o ID especificado não foi encontrado.</response>
        [HttpGet("GetLunchByOrderId/{id}")]
        [Authorize(Policy = "Permissions.Order.View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLunchByOrderIdQuery(Guid id)
        {
            var query = new GetLunchByOrderIdQuery(id);
            var order = await _mediator.Send(query);
            return order != null ? Ok(order) : BadRequest($"Pedido com ID {id} não foi encontrado.");
        }

        /// <summary>
        /// Deleta um pedido pelo ID.
        /// </summary>
        /// <param name="id">ID do pedido a ser deletado.</param>
        /// <response code="200">Retorna o ID do pedido deletado.</response>
        /// <response code="400">Se o pedido com o ID especificado não foi encontrado.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Permissions.Order.Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteOrderCommand(id);
            var result = await _mediator.Send(command);
            return result != null ? Ok($"Pedido com ID {result} deletado com sucesso!") : BadRequest($"Pedido com ID {id} não foi encontrado.");
        }
    }
}
