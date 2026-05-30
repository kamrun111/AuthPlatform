using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Application.Expenditures.DTOs;
using FluentValidation;

namespace AuthPlatform.Application.Expenditures.Validators
{
    public class ExpenditureInvoiceValidator : AbstractValidator<ExpenditureInvoiceDto>
    {
        public ExpenditureInvoiceValidator()
        {
            RuleFor(x => x.InvoiceNumber)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.InvoiceDate)
                .NotEmpty();

            RuleFor(x => x.ExpenditureHeadId)
                .GreaterThan(0);

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0);
        }
    }
}
