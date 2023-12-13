﻿using SupportHub.Auth.Application.Services.Cryptography;
using SupportHub.Auth.Application.UseCases.Companies.Validators;
using SupportHub.Auth.Domain.Cache;
using SupportHub.Auth.Domain.DTOs.Requests.Companies;
using SupportHub.Auth.Domain.DTOs.Responses;
using SupportHub.Auth.Domain.Exceptions;
using SupportHub.Auth.Domain.Repositories;

namespace SupportHub.Auth.Application.UseCases.Companies.ForgotPassword.Confirmation;

public class ResetPasswordUseCase(ICompanyRepository repository, ICryptographyService cryptography, IOneTimePasswordCache oneTimePassword)
    : IResetPasswordUseCase
{
    public async Task<ResponseDefault> ExecuteAsync(RequestResetPassword request,string accountId, string code)
    {
        var validatorRequest = await new ResetPasswordValidator().ValidateAsync(request);
        if (!validatorRequest.IsValid)
            throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

        var validatorCode = oneTimePassword.ValidateOneTimePassword(accountId, code);
        if (!validatorCode)
            throw new DefaultException([MessagesException.CODIGO_INVALIDO]);
        
        var account = await repository.FindCompanyByIdAsync(Guid.Parse(accountId));
        if (account is null)
            throw new DefaultException([MessagesException.CONTA_NAO_ENCONTRADA]);
        
        if (request.Password != request.PasswordConfirmation)
            throw new DefaultException([MessagesException.SENHA_NAO_CONFERE]);

        account.Password = cryptography.EncryptPassword(request.Password!);
        
        await repository.UpdateCompanyAsync(account);
        
        return new ResponseDefault(accountId, MessagesResponse.SENHA_RESETADA);
    }
}