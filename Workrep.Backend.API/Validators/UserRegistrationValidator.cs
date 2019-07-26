using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workrep.Backend.API.Models.HttpModels;

namespace Workrep.Backend.API.Validators
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationBody>
    {

        public UserRegistrationValidator()
        {
            //Email Rules
            RuleFor(u => u.Email).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .EmailAddress();

            //PhoneNumber Rules
            RuleFor(u => u.PhoneNumber).ExclusiveBetween(10000000, 99999999).Unless(u => u.PhoneNumber == 0);

            //Name Rules
            RuleFor(u => u.Name).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty()
                .MaximumLength(50)
                .MinimumLength(4)
                .Matches(@"^[a-zA-Z ]+$")
                .WithMessage("Name can only contain letters a through z");

            //Birthdate Rules
            RuleFor(u => u.Birthdate).ExclusiveBetween(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(-12));

            //Password Rules -> Validates it is BCrypt hashed
            RuleFor(u => u.Password).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .Matches("^\\$2a\\$10\\$.{53}$").WithMessage("Password is not correctly encrypted!");

            //Gender Rules
            RuleFor(u => u.Gender).Must(g => (new string[] {"male", "female", "other", "unspecified"}).Contains(g.ToLower()));

        }

    }
}
