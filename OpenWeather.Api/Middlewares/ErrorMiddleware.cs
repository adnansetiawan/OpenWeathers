using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;
using System.Runtime.InteropServices;
using OpenWeather.Core.Exceptions;
using System.Net;
using Newtonsoft.Json;

namespace OpenWeather.Api.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            var currentBody = context.Response.Body;
            await using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;
            ValidationResultModel? error = null;
            try
            {
                await _next(context);

            }
            catch (BadRequestException e)
            {
                context.Response.StatusCode = int.Parse(e.Code);
                var modelState = new ModelStateDictionary();
                modelState.AddModelError(string.Empty, e.Message);
                error = new ValidationResultModel(modelState, int.Parse(e.Code));

            }
            catch (DataNotFoundException e)
            {
                context.Response.StatusCode = int.Parse(e.Code);
                var modelState = new ModelStateDictionary();
                modelState.AddModelError(string.Empty, e.Message);
                error = new ValidationResultModel(modelState, int.Parse(e.Code));

            }
            catch (Exception e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var modelState = new ModelStateDictionary();
                modelState.AddModelError(string.Empty, e.Message);
                error = new ValidationResultModel(modelState, (int)HttpStatusCode.InternalServerError);
            }

            context.Response.Body = currentBody;
            memoryStream.Seek(0, SeekOrigin.Begin);

            if (error != null)
            {
                context.Response.ContentType = "application/json;charset=utf-8";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                return;
            }
            var readToEnd = await new StreamReader(memoryStream).ReadToEndAsync();
            await context.Response.WriteAsync(readToEnd);
        }
    }
}
