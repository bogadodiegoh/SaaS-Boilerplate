﻿using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using SaaS.WebApi.Models;

namespace SaaS.WebApi.Middleware
{
	public class GlobalExceptionHandler : IExceptionHandler
	{
		private readonly ILogger<GlobalExceptionHandler> _logger;

		public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
		{
			_logger = logger;
		}

		public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An unhandled error occurred: {Message}", exception.Message);

            var (statusCode, title) = exception switch
            {
                ValidationException => ((int)HttpStatusCode.BadRequest, "Validation Error"),
                KeyNotFoundException => ((int)HttpStatusCode.NotFound, "Resource not found"),
                UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, "Not Authorized"),
                _ => ((int)HttpStatusCode.InternalServerError, "Server Internal Error")
            };

            string message = exception.Message;
            
            if (exception is ValidationException valEx)
            {
                message = string.Join(" ", valEx.Errors.Select(e => e.ErrorMessage));
            }

            var response = new ErrorDetails(
                statusCode,
                title,
                message
            );

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
	}
}
