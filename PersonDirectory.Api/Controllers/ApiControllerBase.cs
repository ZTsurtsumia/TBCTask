using Microsoft.AspNetCore.Mvc;
using PersonDirectory.Domain.Models;

namespace PersonDirectory.Api.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected static ApiResponse<T> SuccessResponse<T>(T? payload, string? message = null)
        {
            return new ApiResponse<T>(payload, message);
        }
    }
}
