using AutoMapper;
using Lanches.Application.Commands.Ingredients.UpdateIngredient;
using Lanches.Core.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lanches.Application.Mappers
{
    [ExcludeFromCodeCoverage]
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, UpdateIngredientCommand>();
        }
    }
}
