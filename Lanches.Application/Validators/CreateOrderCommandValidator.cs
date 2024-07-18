using FluentValidation;
using Lanches.Application.Commands.Orders.CreateOrder;
using System.Diagnostics.CodeAnalysis;

namespace Lanches.Application.Validators
{
    [ExcludeFromCodeCoverage]
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("O nome do pedido não pode estar vazio.")
               .MaximumLength(100).WithMessage("O nome do pedido não pode ter mais de 100 caracteres.");

            RuleFor(x => x.OrderDate)
                .NotEmpty().WithMessage("A data do pedido não pode estar vazia.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data do pedido não pode estar no futuro.");

            RuleFor(x => x.TotalPrice)
                .GreaterThan(0).WithMessage("O preço total do pedido deve ser maior que zero.");

            RuleFor(p => p.Quantity)
            .GreaterThan(0)
            .WithMessage("A quantidade deve ser superior à 0!");
        }
    }
}
