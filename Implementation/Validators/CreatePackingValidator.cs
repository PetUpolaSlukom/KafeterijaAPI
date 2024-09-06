using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class CreatePackingValidator : AbstractValidator<CreatePackingDto>
    {
        public CreatePackingValidator(Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(product => product.UnitOfMeasurement)
            .NotEmpty().WithMessage("Unit of Measurement is required.")
            .Must(BeAValidUnit).WithMessage("Invalid Unit of Measurement.");

            RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage("Quantity is required.")
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("Price is required.")
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

            RuleFor(x => x.ProductId)
                .Must(id => ctx.Products.Any(c => c.Id == id))
                .WithMessage("Product ID does not exist.");

            RuleFor(x => x.ContainerId)
                .Must(id => ctx.Containers.Any(c => c.Id == id))
                .WithMessage("Container ID does not exist.");
        }

        private bool BeAValidUnit(string unit)
        {
            var validUnits = new List<string> { "g", "kg", "ml", "l" };
            return validUnits.Contains(unit);
        }
    }
}
