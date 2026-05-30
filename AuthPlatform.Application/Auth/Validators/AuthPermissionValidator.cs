using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Application.Auth.DTOs;
using FluentValidation;

namespace AuthPlatform.Application.Auth.Validators
{
    public class AuthPermissionValidator : AbstractValidator<AuthPermissionDto>
    {
        public AuthPermissionValidator()
        {
            RuleFor(x => x.PermissionName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.PermissionCode)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}
