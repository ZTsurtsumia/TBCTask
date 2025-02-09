using Microsoft.AspNetCore.Mvc;
using PersonDirectory.Domain.Abstractions;
using PersonDirectory.Domain.Errors;
using System.Net;

namespace PersonDirectory.Api.Extensions
{
    public static class ErrorCodeExtension
    {
        public static IActionResult AsBadRequest(this Error error)
        {
            return ResponseExtension.CreateResponse(HttpStatusCode.BadRequest, error);
        }

        public static IActionResult AsNotFound(this Error error)
        {
            return ResponseExtension.CreateResponse(HttpStatusCode.NotFound, error);
        }

        public static IActionResult AsGeneric(this Error error)
        {
            return ResponseExtension.CreateResponse(HttpStatusCode.BadRequest, error);
        }

        public static IActionResult HandleError(this Error error)
        {
            return error.Code switch
            {
                ErrorList.PersonNotFound => error.AsNotFound(),
                ErrorList.ConnectedPersonNotExist => error.AsBadRequest(),
                _ => error.AsGeneric()
            };
        }
    }

    public static class ResponseExtension
    {
        public static IActionResult CreateResponse(HttpStatusCode statusCode, object body)
        {
            return new ObjectResult(body) { StatusCode = (int)statusCode };
        }
    }
}
