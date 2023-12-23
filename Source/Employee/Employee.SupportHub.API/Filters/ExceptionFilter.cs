using System.Net;
using Employee.SupportHub.Domain.DTOs.Responses;
using Employee.SupportHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Employee.SupportHub.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
	public void OnException(ExceptionContext context)
	{
		if (context.Exception is DefaultException exception)
			context.Result = new ObjectResult(new { data = new ResponseException(exception.ErrorMessages!.ToList()) })
			{
				StatusCode = (int)HttpStatusCode.BadRequest
			};
		else
			context.Result = new ObjectResult(new
				{ data = new ResponseException([MessagesException.ERRO_DESCONHECIDO]) })
			{
				StatusCode = (int)HttpStatusCode.InternalServerError
			};
	}
}