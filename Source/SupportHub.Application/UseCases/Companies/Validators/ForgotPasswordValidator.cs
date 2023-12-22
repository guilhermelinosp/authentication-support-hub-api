﻿using FluentValidation;
using SupportHub.Domain.DTOs.Requests.Companies;
using SupportHub.Domain.Exceptions;

namespace SupportHub.Authentication.Application.UseCases.Companies.Validators;

public class ForgotPasswordValidator : AbstractValidator<RequestForgotPassword>
{
	public ForgotPasswordValidator()
	{
		RuleFor(e => e.Email)
			.NotEmpty()
			.WithMessage(MessagesException.EMAIL_NAO_INFORMADO)
			.EmailAddress()
			.WithMessage(MessagesException.EMAIL_INVALIDO);
	}
}