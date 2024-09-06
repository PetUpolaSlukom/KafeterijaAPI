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
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator(Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("^[A-ZČĆŽŠĐa-zčćžšđ]+(?:[-\\s][A-ZČĆŽŠĐa-zčćžšđ]+)*$")
                .WithMessage("Invalid category format.")
                .Must(x => !ctx.Categories.Any(c => c.Name == x))
                .WithMessage("Category name already exist.");

        }
    }
}
