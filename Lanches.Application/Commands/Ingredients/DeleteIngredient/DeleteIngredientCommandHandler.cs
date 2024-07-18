using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using MediatR;

namespace Lanches.Application.Commands.Ingredients.DeleteIngredient
{
    public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, Guid?>
    {
        private readonly IGenericRepository<Ingredient> _repository;

        public DeleteIngredientCommandHandler(IGenericRepository<Ingredient> repository)
        {
            _repository = repository;
        }

        public async Task<Guid?> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _repository.GetByIdAsync(request.Id);
            if (ingredient is not null)
                await _repository.Delete(ingredient);
            return ingredient is null ? null : ingredient.Id;
        }
    }
}
