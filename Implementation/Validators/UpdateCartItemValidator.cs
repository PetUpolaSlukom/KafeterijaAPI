using DataAccess;
using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class UpdateCartItemValidator : AbstractValidator<UpdateCartItemDto>
    {
        public UpdateCartItemValidator(Context ctx) 
        {
            RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage("Quantity is required.")
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.PackingId)
                .Must(id => ctx.Packings.Any(c => c.Id == id))
                .WithMessage("Packing ID does not exist.");
        }
    }
}
