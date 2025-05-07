using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using Tbc.Individuals.Application.Helpers;
using Tbc.Individuals.Domain.Utils;

namespace Tbc.Individuals.Api.Exceptions;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception occurred while processing the request.");

        return exception switch
        {
            BadHttpRequestException br => await HandleBadRequestException(httpContext, br, cancellationToken),
            ApplicationValidationException ave => await HandleApplicationValidationException(httpContext, ave, cancellationToken),
            DomainValidationException dve => await HandleDomainValidationException(httpContext, dve, cancellationToken),
            NotFoundException nfe => await HandleNotFoundException(httpContext, new DomainValidationException(nfe.Message), cancellationToken),
            _ => await HandleGeneralException(httpContext, exception, cancellationToken)
        };
    }

    private async ValueTask<bool> HandleNotFoundException(HttpContext httpContext, DomainValidationException domainValidationException, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = domainValidationException,
            ProblemDetails = new ProblemDetails
            {
                Type = "https://httpstatuses.com/404",
                Detail = domainValidationException.Message,
                Title = "An error occurred while processing your request.",
                Status = StatusCodes.Status404NotFound
            }
        });
    }

    private ValueTask<bool> HandleDomainValidationException(HttpContext httpContext, DomainValidationException exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        return problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = "https://httpstatuses.com/400",
                Detail = exception.Message,
                Title = "An error occurred while processing your request.",
                Status = StatusCodes.Status400BadRequest,
                Extensions = new Dictionary<string, object?>
                {
                    ["errors"] = new string[] { exception.Message }
                }
            }
        });
    }

    private ValueTask<bool> HandleApplicationValidationException(HttpContext httpContext, ApplicationValidationException exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        return problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = "https://httpstatuses.com/400",
                Detail = exception.Message,
                Title = "An error occurred while processing your request.",
                Status = StatusCodes.Status400BadRequest,
                Extensions = new Dictionary<string, object?>
                {
                    ["errors"] = exception.Errors
                }
            }
        });
    }

    private ValueTask<bool> HandleBadRequestException(HttpContext httpContext, BadHttpRequestException exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        return problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = "https://httpstatuses.com/400",
                Detail = "Invalid Request",
                Title = "An error occurred while processing your request.",
                Status = StatusCodes.Status400BadRequest
            }
        });
    }

    private ValueTask<bool> HandleGeneralException(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = "https://httpstatuses.com/500",
                Detail = "General Error",
                Title = "An error occurred while processing your request.",
                Status = StatusCodes.Status500InternalServerError
            }
        });
    }
}
