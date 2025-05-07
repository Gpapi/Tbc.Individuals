using Microsoft.AspNetCore.Http.Features;

namespace Tbc.Individuals.Api.Exceptions;

public static class ExceptionHandlerExtensions
{
    public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddProblemDetails(o =>
        {
            o.CustomizeProblemDetails = (ctx) =>
            {
                ctx.ProblemDetails.Instance = $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}";
                ctx.ProblemDetails.Extensions["requestId"] = ctx.HttpContext.TraceIdentifier;

                var activity = ctx.HttpContext?.Features?.Get<IHttpActivityFeature>()?.Activity;

                if (activity != null)
                {
                    ctx.ProblemDetails.Extensions["traceId"] = activity.Id;
                }
            };
        });

        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}
