using Lanches.Application.Commands.Lunchs.CreateLunch;
using Lanches.Application.Commands.Lunchs.UpdateLunch;
using Lanches.Application.Commands.Orders.DeleteLunch;
using Lanches.Application.Dtos;
using Lanches.Application.Queries.Lunchs.GetAllLunchs;
using Lanches.Application.Queries.Lunchs.GetLunch;
using Lanches.Core.Entities;

namespace Lanches.Tests.Application.Lunchs
{
    public static class LunchFactory
    {
        public static CreateLunchCommand GetLunchToCreate()
        {
            return new CreateLunchCommand()
            {
                Description = "Descrição",
                Ingredients = new List<Guid>(),
                Price = 10,
                Name = "name",
            };
        }

        public static UpdateLunchCommand GetLunchToUpdate(Guid id, Guid ingredientId)
        {
            return new UpdateLunchCommand(id, "Lunch", 10, "Description", new List<Guid> { ingredientId });
        }

        public static DeleteLunchCommand GetLunchToDelete(Guid id)
        {
            return new DeleteLunchCommand(id);
        }
        public static GetLunchQuery GetLunch(Guid id)
        {
            return new GetLunchQuery(id);
        }

        public static GetAllLunchsQuery GetAllLunchs(GetAll getAll)
        {
            return new GetAllLunchsQuery(getAll);
        }
    }
}
