using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Application.Expenditures.DTOs;
using FluentValidation;

namespace AuthPlatform.Application.Expenditures.Validators
{
    public class ExpenditureHeadValidator : AbstractValidator<ExpenditureHeadDto>
    {
        public ExpenditureHeadValidator()
        {
            RuleFor(x => x.ExpenditureHeadName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}
