using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Unitofworkdemo.Errors;

namespace Unitofworkdemo.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next,
                                    ILogger<ExceptionMiddleware> logger,
                                    IHostEnvironment env)
                                    
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try {
                await next(context);
            }
            catch (Exception ex)
            {
                ApiError response;
                HttpStatusCode statuscode = HttpStatusCode.InternalServerError;
                string message;
                var exceptionType = ex.GetType();

                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    statuscode = HttpStatusCode.Forbidden;
                    message = "You are Not Authorised.";
                }
                else {
                    statuscode = HttpStatusCode.InternalServerError;
                    message = "Someunknown error occured.";
                }

                if (env.IsDevelopment())
                {
                    response = new ApiError((int)statuscode, ex.Message, ex.StackTrace.ToString());
                }
                else {
                    response = new ApiError((int)statuscode, message);
                }
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)statuscode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response.ToString());
            }
        }
    }
}
