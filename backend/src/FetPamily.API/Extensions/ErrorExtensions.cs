using FetPamily.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FetPamily.API.Extensions;

public static class ErrorExtensions
{
    public static ActionResult ToActionResult(this Error error)
    {
        var problem = new
        {
            error.Code,
            error.Message
        };

        return error.ErrorType switch
        {
            ErrorType.ValidationError => new BadRequestObjectResult(problem),
            ErrorType.NotFound => new NotFoundObjectResult(problem),
            ErrorType.Conflict => new ConflictObjectResult(problem),
            ErrorType.InternalError => new ObjectResult(problem) { StatusCode = 500 },
            _ => new ObjectResult(problem) { StatusCode = 500 }
        };
    }
}