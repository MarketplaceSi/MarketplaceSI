using MarketplaceSI.Core.Dto.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace MarketplaceSI.Core.Infrastructure.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var statusCode = 500;
            ErrorPayloadBase? errorResponse = null;
            switch (ex)
            {
                default:
                    errorResponse = new ErrorPayloadBase("Something went wrong!", HttpStatusCode.InternalServerError.ToString());
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}