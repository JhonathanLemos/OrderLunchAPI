using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using MediatR;

namespace Lanches.Application.Commands.Ingredients.CreateIngredient
{
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, Guid>
    {
        private readonly IGenericRepository<Ingredient> _repository;

        public CreateIngredientCommandHandler(IGenericRepository<Ingredient> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var newIngredient = new Ingredient(request.Name);
            return await _repository.CreateAsync(newIngredient);
        }
    }
}
