using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ReadingIsGood.API.Models;
using ReadingIsGood.Domain.Exceptions;
using System.Net;

namespace ReadingIsGood.API.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = contextFeature.Error;

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var response = new HttpServiceResponseBase();

                    switch (exception)
                    {
                        case ApiException apiException:
                            response.Error = new ErrorModel
                            {
                                Code = apiException.ErrorCode,
                                Message = apiException.ErrorMessage,
                                Exception = apiException.ErrorMessage
                            };
                            break;
                        default:
                            response.Error = new ErrorModel
                            {
                                Code = HttpStatusCode.InternalServerError,
                                Message = "Oops!",
                                Exception = exception.Message
                            };
                            break; ;
                    }

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));

                });
            });
        }
    }
}
