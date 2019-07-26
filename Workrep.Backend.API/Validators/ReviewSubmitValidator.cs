using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workrep.Backend.API.Models.HttpModels;

namespace Workrep.Backend.API.Validators
{
    public class ReviewSubmitValidator : AbstractValidator<ReviewSubmitBody>
    {

        public ReviewSubmitValidator()
        {
            //Rating Rule
            RuleFor(r => r.Rating).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().InclusiveBetween(1, 5);

            //Position Rule
            RuleFor(r => r.Position).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty()
                .Length(2, 30);

            //Date Rules
            RuleFor(r => r.EmploymentStart).InclusiveBetween(DateTime.Now.AddYears(-88), DateTime.Now);
            RuleFor(r => r.EmploymentEnd).InclusiveBetween(DateTime.Now.AddYears(-88), DateTime.Now);
            RuleFor(r => r).Must(r => r.EmploymentEnd > r.EmploymentStart).Unless(r => r.EmploymentEnd == null || r.EmploymentStart == null)
                .WithMessage("Employment start must be before employment end!");

            //Comment Rules
            RuleFor(r => r.Comment).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .Length(40, 2000);
        }

    }
}
