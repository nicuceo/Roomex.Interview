using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Roomex.Interview.Api.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var exceptionMessage = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
            int statusCode;

            if (exception is ArgumentException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception is HttpRequestException)
            {
                statusCode = (int)HttpStatusCode.ServiceUnavailable;
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.Result = new JsonResult(new {Message = exceptionMessage }){ StatusCode = statusCode};
        }
    }
}
