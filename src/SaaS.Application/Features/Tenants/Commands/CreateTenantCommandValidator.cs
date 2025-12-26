﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace SaaS.Application.Features.Tenants.Commands
{
	public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
	{
		public CreateTenantCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(v => v.Identifier)
                .NotEmpty().WithMessage("Identifier is required.")
                .Matches("^[a-z0-9-]+$").WithMessage("Identifier can only contain lowercase letters, numbers, and hyphens.");
        }
	}
}
