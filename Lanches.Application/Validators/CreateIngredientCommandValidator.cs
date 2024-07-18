using FluentValidation;
using Lanches.Application.Commands.Orders.CreateOrder;
using Lanches.Infraestructure.Context;
using System.Diagnostics.CodeAnalysis;

namespace Lanches.Application.Validators
{
    [ExcludeFromCodeCoverage]
    public class CreateIngredientCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateIngredientCommandValidator(LanchesDbContext context)
        {
            RuleFor(x => x.Name)
                   .NotEmpty().WithMessage("O nome não pode estar vazio.")
                   .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres.");
        }
    }
}
