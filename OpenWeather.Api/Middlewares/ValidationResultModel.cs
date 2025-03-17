using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace OpenWeather.Api.Middlewares
{
    public class ValidationResultModel
    {
        [JsonProperty("status")]
        public string Status { get; }

        [JsonProperty("errors")]
        public List<ValidationError> Errors { get; }

        public ValidationResultModel(ModelStateDictionary modelState, int statusCode)
        {
            Status = statusCode.ToString();
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }
    public class ValidationError
    {
        public string? Field { get; }

        public string Message { get; }

        public ValidationError(string? field, string message)
        {
            Field = field;
            Message = message;
        }
    }
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState, (int)HttpStatusCode.BadRequest))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }

    }
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }
}
