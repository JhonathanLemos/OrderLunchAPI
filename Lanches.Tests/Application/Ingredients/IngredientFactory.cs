using Lanches.Application.Commands.Ingredients.CreateIngredient;
using Lanches.Application.Commands.Ingredients.DeleteIngredient;
using Lanches.Application.Commands.Ingredients.UpdateIngredient;
using Lanches.Application.Dtos;
using Lanches.Application.Queries.GetIngredient;
using Lanches.Application.Queries.Ingredients.GetIngredient;
using Lanches.Application.Queries.Lunchs.GetIngredientsByLunchs;

namespace Lanches.Tests.Application.Ingredients
{
    public static class IngredientFactory
    {
        public static CreateIngredientCommand GetIngredientToCreate()
        {
            return new CreateIngredientCommand()
            {
                Name = "name",
            };
        }

        public static CreateIngredientCommand GetInvalidIngredient()
        {
            return new CreateIngredientCommand()
            {
                Name = "name",
            };
        }

        public static UpdateIngredientCommand GetIngredientToUpdate()
        {
            return new UpdateIngredientCommand(Guid.NewGuid(), "Name");
        }

        public static DeleteIngredientCommand GetIngredientToDelete(Guid id)
        {
            return new DeleteIngredientCommand(id);
        }

        public static GetIngredientQuery GetIngredient()
        {
            return new GetIngredientQuery(Guid.NewGuid());
        }

        public static GetIngredientsByLunchIdQuery GetIngredientsByLunchIdQuery()
        {
            return new GetIngredientsByLunchIdQuery(Guid.NewGuid());
        }

        public static GetAllIngredientQuery GetAllIngredients(GetAll getAll)
        {
            return new GetAllIngredientQuery(getAll);
        }
    }
}
