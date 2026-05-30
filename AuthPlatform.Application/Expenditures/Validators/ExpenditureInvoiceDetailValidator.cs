using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Application.Expenditures.DTOs;
using FluentValidation;

namespace AuthPlatform.Application.Expenditures.Validators
{
    public class ExpenditureInvoiceDetailValidator : AbstractValidator<ExpenditureInvoiceDetailDto>
    {
        public ExpenditureInvoiceDetailValidator()
        {
            RuleFor(x => x.ExpenditureInvoiceId)
                .GreaterThan(0);

            RuleFor(x => x.ItemName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0);
        }
    }
}
