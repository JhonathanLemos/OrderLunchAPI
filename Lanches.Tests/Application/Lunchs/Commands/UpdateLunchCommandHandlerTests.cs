using AutoMapper;
using Lanches.Application.Commands.Lunchs.UpdateLunch;
using Lanches.Application.Services;
using Lanches.Core.Entities;
using Lanches.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Lanches.Tests.Application.Lunchs.Commands
{
    public class UpdateLunchCommandHandlerTests
    {
        [Fact]
        public async Task UpdateLunchHandler_WhenCalled_ReturnsValidId()
        {
            // Arrange
            var (mockLunchRepository, mockIngredientRepository, mockLunchIngredientRepository, mockMapper) = CreateMocks();

            var lunchService = new UpdateLunchService(
                mockLunchRepository.Object,
                mockLunchIngredientRepository.Object,
                mockIngredientRepository.Object,
                mockMapper.Object);

            var updateLunchCommandHandler = new UpdateLunchCommandHandler(lunchService);
            var lunch = new Lunch("Lunch", 10, "description");
            var ingredient = new Ingredient("Ingredient");
            var updateLunchCommand = LunchFactory.GetLunchToUpdate(lunch.Id, ingredient.Id);

            // Act
            var id = await updateLunchCommandHandler.Handle(updateLunchCommand, new CancellationToken());

            // Assert
            Assert.True(id >= Guid.Empty);
            mockLunchRepository.Verify(repo => repo.Update(It.IsAny<Lunch>()), Times.Once);
        }

        private (Mock<IGenericRepository<Lunch>>, Mock<IGenericRepository<Ingredient>>, Mock<IGenericRepository<LunchIngredient>>, Mock<IMapper>) CreateMocks()
        {
            var lunch = new Lunch("Lunch", 10, "description");
            var ingredient = new Ingredient("Ingredient");
            var lunchIngredient = new LunchIngredient(lunch, ingredient);

            var mockLunchRepository = new Mock<IGenericRepository<Lunch>>();
            var mockIngredientRepository = new Mock<IGenericRepository<Ingredient>>();
            var mockLunchIngredientRepository = new Mock<IGenericRepository<LunchIngredient>>();
            var mockMapper = new Mock<IMapper>();

            mockIngredientRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                                    .ReturnsAsync(ingredient);
            mockLunchRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(lunch);
            mockLunchIngredientRepository.Setup(repo => repo.GetAll())
                                         .Returns(new List<LunchIngredient> { lunchIngredient }.AsQueryable());

            return (mockLunchRepository, mockIngredientRepository, mockLunchIngredientRepository, mockMapper);
        }
    }
}
