using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySwaggerUIServer.Exceptions
{
    public class UserExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public UserExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (UserNameException ex)
            {
                await WriteProblemDetailsResponse(context, ex);
            }
            catch (UserAgeException ex)
            {
                await WriteProblemDetailsResponse(context, ex);
            }


        }

        private static async Task WriteProblemDetailsResponse<T>(HttpContext context, T ex)
            where T : UserValidationException
        {


            Console.WriteLine($"Error: {ex.Message}. {ex.FieldName}." +
                              $"{ ex.ProblemFieldValue} has been entered");
            ProblemDetails problem = new ProblemDetails()
            {
                Type = "ttps://httpstatuses.com/500",
                Title = ex.Message,
                Status = 500,
                Detail = $"Error: {ex.Message}. {ex.FieldName}." +
                         $" {ex.ProblemFieldValue} has been entered",

                Instance = context.Request.Path

            };

            problem.Extensions.Add(nameof(T), ex.ToString());
            context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
