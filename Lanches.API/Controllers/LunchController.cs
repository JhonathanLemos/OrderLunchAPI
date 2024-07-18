using AutoMapper;
using Lanches.Application.Commands.Lunchs.CreateLunch;
using Lanches.Application.Commands.Lunchs.UpdateLunch;
using Lanches.Application.Commands.Orders.DeleteLunch;
using Lanches.Application.Dtos;
using Lanches.Application.Dtos.ViewModels;
using Lanches.Application.Queries.Lunchs.GetAllLunchs;
using Lanches.Application.Queries.Lunchs.GetIngredientsByLunchs;
using Lanches.Application.Queries.Lunchs.GetLunch;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.API.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
    [Route("api/Lunchs")]
    [ApiController]
    public class LunchController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LunchController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo Lunch.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Lunchs
        ///     {
        ///         "name": "Novo Lanche"
        ///     }
        ///
        /// </remarks>
        /// <param name="createLunchCommand">Dados do Lunch a ser criado.</param>
        /// <response code="201">Retorna o Lunch cadastrado.</response>
        [HttpPost]
        [Authorize(Policy = "Permissions.Lunch.Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateLunchCommand createLunchCommand)
        {
            var id = await _mediator.Send(createLunchCommand);
            createLunchCommand.SetId(id);
            return CreatedAtAction(nameof(GetById), new { Id = id }, _mapper.Map<LunchViewModel>(createLunchCommand));
        }

        /// <summary>
        /// Atualiza um Lunch existente pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Lunchs/1
        ///     {
        ///         "name": "Lanche Atualizado"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do Lunch a ser atualizado.</param>
        /// <param name="updateLunchCommand">Dados atualizados do Lunch.</param>
        /// <response code="200">Retorna o ID do Lunch atualizado.</response>
        /// <response code="400">Se o Lunch com o ID especificado não foi encontrado.</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "Permissions.Lunch.Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateLunchCommand updateLunchCommand)
        {
            if (id != updateLunchCommand.Id)
            {
                return BadRequest("O ID do ingrediente na URL não corresponde ao ID no corpo da requisição.");
            }
            var lunchId = await _mediator.Send(updateLunchCommand);
            updateLunchCommand.SetId(id);
            return CreatedAtAction(nameof(GetById), new { Id = lunchId }, _mapper.Map<LunchViewModel>(updateLunchCommand));
        }

        /// <summary>
        /// Busca um Lunch pelo ID.
        /// </summary>
        /// <param name="id">ID do Lunch.</param>
        /// <response code="200">Retorna o Lunch encontrado.</response>
        /// <response code="400">Se o Lunch com o ID especificado não foi encontrado.</response>
        [HttpGet("{id}")]
        [Authorize(Policy = "Permissions.Lunch.View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetLunchQuery(id);
            var lunch = await _mediator.Send(query);

            return lunch != null ? Ok(lunch) : BadRequest($"Item com Id {id} não foi encontrado");
        }

        /// <summary>
        /// Busca todos os Lunchs do sistema.
        /// </summary>
        /// <response code="200">Retorna todos os Lunchs do sistema.</response>
        [HttpGet]
        [Authorize(Policy = "Permissions.Lunch.View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAll GetAll)
        {
            var LanchesList = new GetAllLunchsQuery(GetAll);
            var order = await _mediator.Send(LanchesList);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        /// <summary>
        /// Busca os ingredients do Lunch pelo ID.
        /// </summary>
        /// <param name="id">ID do Lunch.</param>
        /// <response code="200">Retorna os ingredients do Lunch pelo ID.</response>
        [HttpGet("GetIngredientsByLunchId/{id}")]
        [Authorize(Policy = "Permissions.Lunch.View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetIngredientsByLunchId(Guid id)
        {
            var query = new GetIngredientsByLunchIdQuery(id);
            var lunch = await _mediator.Send(query);

            return lunch != null ? Ok(lunch) : BadRequest($"Item com Id {id} não foi encontrado");
        }

        /// <summary>
        /// Deleta um Lunch pelo ID.
        /// </summary>
        /// <param name="id">ID do Lunch a ser deletado.</param>
        /// <response code="200">Retorna o ID do Lunch deletado.</response>
        /// <response code="400">Se o Lunch com o ID especificado não foi encontrado.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Permissions.Lunch.Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteLunchCommand(id);
            var result = await _mediator.Send(command);

            return result != null
                ? Ok($"Lunch com Id {result} deletado com sucesso!")
                : BadRequest($"Lunch com ID {id} não encontrado.");
        }
    }
}
