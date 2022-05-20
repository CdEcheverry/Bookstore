using Aplication.HandlerExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class HandlerErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandlerErrorMiddleware> _logger;
        public HandlerErrorMiddleware(RequestDelegate next, ILogger<HandlerErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandlerExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandlerExceptionAsync(HttpContext context, Exception ex, ILogger<HandlerErrorMiddleware> logger)
        {
            object errors = null;
            switch (ex)
            {
                case CustomHandlerException che:
                    logger.LogError(ex, "Handler Error");
                    errors = che.Errors;
                    context.Response.StatusCode = (int)che.Code;
                    break;

                case Exception e:
                    logger.LogError(ex, "Error of Server");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                var result = JsonConvert.SerializeObject(new { errors });
                await context.Response.WriteAsync(result);
            }    
        }
    }
}
