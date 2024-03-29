﻿using GameStore.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace GameStore.API.Extensions;

public static class ExceptionHandlerExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                if (exception is not null)
                {
                    switch (exception)
                    {
                        case NotFoundException:
                            context.Response.StatusCode = StatusCodes.Status404NotFound;
                            break;

                        default:
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            break;
                    }

                    await context.Response.WriteAsJsonAsync(new
                    {
                        Message = exception.Message
                    });
                }
            });
        });
    }
}