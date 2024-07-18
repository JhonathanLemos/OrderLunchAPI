using AutoMapper;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using MediatR;

namespace Lanches.Application.Commands.Ingredients.UpdateIngredient
{
    public class UpdateIngredientsCommandHandler : IRequestHandler<UpdateIngredientCommand, Guid?>
    {
        private readonly IGenericRepository<Ingredient> _repository;
        private readonly IMapper _mapper;

        public UpdateIngredientsCommandHandler(IGenericRepository<Ingredient> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid?> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var existingIngredient = await _repository.GetByIdAsync(request.Id);

            if (existingIngredient is null)
                throw new Exception($"Ingredient with ID {existingIngredient.Id} not found.");

            _mapper.Map(existingIngredient, request);
            return existingIngredient is null ? null : await _repository.Update(existingIngredient);
        }
    }
}
