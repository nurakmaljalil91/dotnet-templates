using System.Net;
using System.Text.Json;
using Application.Common.Exceptions;
using Domain.Common;

namespace WebAPI.Middlewares;

/// <summary>
/// Middleware for handling exceptions globally in the application.
/// </summary>
public sealed class ExceptionHandlingMiddleware: IMiddleware
{
    private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web);

    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var response = BaseResponse<object>.Fail(
                message: ex.Message,
                errors: ex.Errors);

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response, JsonOptions));
        }
        catch (Exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = BaseResponse<object>.Fail(
                message: "An unexpected error occurred.");

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response, JsonOptions));
        }
    }
}
