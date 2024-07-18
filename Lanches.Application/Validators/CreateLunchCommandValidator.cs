using FluentValidation;
using Lanches.Application.Commands.Orders.CreateOrder;
using System.Diagnostics.CodeAnalysis;

namespace Lanches.Application.Validators
{
    [ExcludeFromCodeCoverage]
    public class CreateLunchCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateLunchCommandValidator()
        {
            RuleFor(l => l.Id)
                .NotEmpty().WithMessage("O ID do lunch não pode estar vazio.");

            RuleFor(l => l.Name)
                .NotEmpty().WithMessage("O nome do lunch não pode estar vazio.")
                .MaximumLength(100).WithMessage("O nome do lunch não pode ter mais de 100 caracteres.");

            RuleFor(l => l.TotalPrice)
                .GreaterThan(0).WithMessage("O preço do lunch deve ser maior que zero.");
        }
    }
}
