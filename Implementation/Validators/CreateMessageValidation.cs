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
    public class CreateMessageValidation : AbstractValidator<CreateMessageDto>
    {
        public CreateMessageValidation(Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Field is required")
                .MaximumLength(500).WithMessage("Maximum 500 caracters");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Field is required")
                .Matches("^[A-Z][a-z]{1,19} [A-Z][a-z]{1,19}$")
                .WithMessage("Invalid format.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Field is required")
                .Matches("^[a-z0-9._%+-]+@[a-z.-]+\\.[a-z]{2,}$")
                .WithMessage("Invalid format.");
        }
    }
}
