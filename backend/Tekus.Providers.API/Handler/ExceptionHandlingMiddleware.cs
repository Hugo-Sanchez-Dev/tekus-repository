#region MyRegion
using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using Tekus.Providers.Application.DTOs.Response;
using Tekus.Providers.Application.Enum;
using Tekus.Providers.Application.Extensions; 
#endregion

namespace PriceCore.WebApi.Handlers;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            ResponseDTO<object> result = ResponseCodeEnum.BAD_REQUEST.AsResponseDTO(ex.Message);

            context.Response.StatusCode = (int)result.Header.ResponseCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(result);
        }
        catch (SqlException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        ResponseDTO<object> errorResult = ResponseCodeEnum.ERROR.AsResponseDTO(ex.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)errorResult.Header.ResponseCode;

        string json = JsonSerializer.Serialize(errorResult);

        return context.Response.WriteAsync(json);
    }
}