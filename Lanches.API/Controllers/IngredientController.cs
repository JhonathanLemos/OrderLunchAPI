using Lanches.Application.Commands.Ingredients.CreateIngredient;
using Lanches.Application.Commands.Ingredients.DeleteIngredient;
using Lanches.Application.Commands.Ingredients.UpdateIngredient;
using Lanches.Application.Dtos;
using Lanches.Application.Queries.GetIngredient;
using Lanches.Application.Queries.Ingredients.GetIngredient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/Ingredients")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo ingrediente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Ingredients
        ///     {
        ///         "name": "Novo Ingrediente"
        ///     }
        ///
        /// </remarks>
        /// <param name="createIngredientCommand">Dados do ingrediente a ser criado.</param>
        /// <response code="201">Retorna o ingrediente criado.</response>
        [HttpPost]
        [Authorize(Policy = "Permissions.Ingredient.Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateIngredientCommand createIngredientCommand)
        {
            var id = await _mediator.Send(createIngredientCommand);
            createIngredientCommand.SetId(id);
            return CreatedAtAction(nameof(GetById), new { Id = id }, createIngredientCommand);
        }

        /// <summary>
        /// Atualiza um ingrediente existente pelo ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Ingredients/1
        ///     {
        ///         "name": "Ingrediente Atualizado"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do ingrediente a ser atualizado.</param>
        /// <param name="updateIngredientCommand">Dados atualizados do ingrediente.</param>
        /// <response code="200">Retorna o ID do ingrediente atualizado.</response>
        /// <response code="400">Se o ingrediente com o ID especificado não foi encontrado.</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "Permissions.Ingredient.Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateIngredientCommand updateIngredientCommand)
        {
            if (id != updateIngredientCommand.Id)
            {
                return BadRequest("O ID do ingrediente na URL não corresponde ao ID no corpo da requisição.");
            }

            updateIngredientCommand.SetId(id);
            var ingredientId = await _mediator.Send(updateIngredientCommand);
            return Ok($"Ingrediente com id {ingredientId} atualizao com sucesso!");
        }

        /// <summary>
        /// Busca um ingrediente pelo ID.
        /// </summary>
        /// <param name="id">ID do ingrediente.</param>
        /// <response code="200">Retorna o ingrediente encontrado.</response>
        /// <response code="400">Se o ingrediente com o ID especificado não foi encontrado.</response>
        [HttpGet("{id}")]
        [Authorize(Policy = "Permissions.Ingredient.View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetIngredientQuery(id);
            var ingredient = await _mediator.Send(query);

            return ingredient != null
                ? Ok(ingredient)
                : BadRequest($"Ingrediente com ID {id} não encontrado.");
        }

        /// <summary>
        /// Busca todos os ingredientes do sistema.
        /// </summary>
        /// <response code="200">Retorna todos os ingredientes do sistema.</response>
        [HttpGet]
        [Authorize(Policy = "Permissions.Ingredient.View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAll Query)
        {
            var query = new GetAllIngredientQuery(Query);
            var ingredients = await _mediator.Send(query);
            return Ok(ingredients);
        }

        /// <summary>
        /// Deleta um ingrediente pelo ID.
        /// </summary>
        /// <param name="id">ID do ingrediente a ser deletado.</param>
        /// <response code="200">Retorna o ID do ingrediente deletado.</response>
        /// <response code="400">Se o ingrediente com o ID especificado não foi encontrado.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Permissions.Ingredient.Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteIngredientCommand(id);
            var result = await _mediator.Send(command);

            return result != Guid.Empty ?
                Ok($"Ingrediente com Id {result} deletado com sucesso!") : BadRequest($"Ingrediente com ID {id} não encontrado.");
        }
    }
}
