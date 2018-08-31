using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Operations.Queries.Client.Validators
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator(DbContext context)
        {
            RuleFor(r => r.Phone).NotEmpty();
            RuleFor(r => r.Email).NotEmpty();
            RuleFor(r => r.Email).Must(r => !context.Set<Entities.Client>().Any(z => z.Phone.PHone == r))
                .WithMessage("Phone already exist");
            RuleFor(r => r.Phone).Must(r => !context.Set<Entities.Client>().Any(z => z.Email.EMail == r))
                .WithMessage("Email already exist");
        }
    }
}