using System.Text.Json;
using DGII.API.Models;
using DGII.Domain.Exceptions;
using FluentValidation;

namespace DGII.API.Middleware;

// Centraliza el manejo de errores para que el frontend siempre reciba
// el mismo formato de respuesta, sin importar dónde falle la request.
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var traceId = context.TraceIdentifier;

        var (statusCode, message) = ex switch
        {
            ValidationException ve => (
                StatusCodes.Status400BadRequest,
                string.Join("; ", ve.Errors.Select(e => e.ErrorMessage))
            ),
            ContribuyenteNotFoundException => (
                StatusCodes.Status404NotFound,
                ex.Message
            ),
            DomainException => (
                StatusCodes.Status400BadRequest,
                ex.Message
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Ha ocurrido un error interno."
            )
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
            _logger.LogError(ex, "Error no controlado. TraceId: {TraceId}", traceId);
        else
            _logger.LogWarning(ex, "Error de negocio. TraceId: {TraceId}", traceId);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new ApiResponse<object>(
            Success: false,
            Data: null,
            Error: new ApiError(message)
        );

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}
