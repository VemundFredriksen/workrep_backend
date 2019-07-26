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
            RuleFor(u => u.Name).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().Must(s => s.Contains("arne"));
        }

    }
}
