using System.Buffers;
using System.Net;
using System.Text.Json;
using AutoRepairMainCore.Exceptions.AutoServiceExceptions;
using AutoRepairMainCore.Exceptions.GeneralCarsExceptions;
using Microsoft.IdentityModel.Tokens;

public class ExceptionResponseHelper
{
    public static async Task WriteErrorResponseAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = GetErrorResponseDetails(exception);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var errorResponse = new { statusCode = (int)statusCode, message };
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var jsonResponse = JsonSerializer.Serialize(errorResponse, options);
        await context.Response.WriteAsync(jsonResponse);
    }

    private static (HttpStatusCode statusCode, string message)
        GetErrorResponseDetails(Exception exception)
    {
        switch (exception)
        {
            case AutoServiceNotFoundException:
                return (HttpStatusCode.NotFound, exception.Message);

            case AutoServiceAlreadyExistException:
                return (HttpStatusCode.Conflict, exception.Message);

            case PasswordValidateException:
                return (HttpStatusCode.BadRequest, exception.Message);

            case SecurityTokenException:
                return (HttpStatusCode.BadRequest, exception.Message);

            case CarAlreadyExistException:
                return (HttpStatusCode.Conflict, exception.Message);

            case InvalidCarDataException:
                return (HttpStatusCode.BadRequest, exception.Message);

            default:
                return (HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }
}
