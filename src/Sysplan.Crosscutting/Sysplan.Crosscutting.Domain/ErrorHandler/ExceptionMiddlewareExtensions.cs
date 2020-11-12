using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Sysplan.Crosscutting.Domain.ErrorHandler
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var serviceProvider = context.Features.Get<IServiceProvidersFeature>();

                    var host = context.Request.Host;
                    var scheme = context.Request.Scheme;
                    var path = context.Request.Path;
                    var query = context.Request.QueryString;

                    var logger = serviceProvider.RequestServices.GetService<ILogger<ErrorDetails>>();

                    if (contextFeature != null)
                    {
                        var errorResult = new ErrorResult
                        {
                            Url = $"{scheme}://{host}{path}{query}",
                            Errors = new[] {
                                "Erro ao processar requisição. Verifique os valores passados e tente novamente."
                            },
                            MessageException = $"{contextFeature.Error.Message} | {contextFeature.Error.StackTrace}"
                        };

                        logger.LogError($"Something went wrong: {errorResult.ToString()}");

                        await context.Response.WriteAsync(errorResult.ToString());
                    }
                });
            });
        }
    }
}