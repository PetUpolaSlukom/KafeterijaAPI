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
    public class CreateOrderValidation : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidation(Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.FullName)
                .NotEmpty()
                .Matches(@"^[A-ZČĆŽŠĐa-zčćžšđ]{2,}(?:[\s-][A-ZČĆŽŠĐa-zčćžšđ]{2,})*$")
                .WithMessage("Invalid name format.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Invalid email format.");

            RuleFor(x => x.Address)
                .NotEmpty()
                .Matches(@"^[a-zA-Z0-9\s,.-/]+$")
                .WithMessage("Invalid address format.");

            RuleFor(x => x.City)
                .NotEmpty()
                .Matches(@"^[A-ZČĆŽŠĐa-zčćžšđ]+(?:[\s-][A-ZČĆŽŠĐa-zčćžšđ]+)*$")
                .WithMessage("Invalid city format.");

            RuleFor(x => x.FullPrice)
                .NotNull()
                .WithMessage("Price is required.")
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

        }
    }
}
