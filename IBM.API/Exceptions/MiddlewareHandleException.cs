using IBM.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace IBM.API.Exceptions
{
    public static class MiddlewareHandleException
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(
                appError =>
                {
                    appError.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                        if (contextFeature != null)
                        {
                            await context.Response.WriteAsync(new ErrorDetail()
                            {
                                Message = "Ocurrio un error interno en el servidor",
                                StatusCode = context.Response.StatusCode
                            }.ToString());
                        }
                    });
                }
                
                );
        }
    }
}
