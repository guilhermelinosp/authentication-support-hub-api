﻿using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.Exceptions;
using Customer.SupportHub.Domain.Messages;
using FluentValidation;

namespace Customer.SupportHub.Application.UseCases.Validators;

public class ForgotPasswordValidator : AbstractValidator<RequestForgotPassword>
{
	public ForgotPasswordValidator()
	{
		RuleFor(e => e.Email)
			.NotEmpty()
			.WithMessage(MessageException.EMAIL_NAO_INFORMADO)
			.EmailAddress()
			.WithMessage(MessageException.EMAIL_INVALIDO);
	}
}