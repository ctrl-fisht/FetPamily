using FetPamily.API.Extensions;
using FetPamily.Application.Volunteers.CreateVolunteer;
using FetPamily.Contracts.Volunteers.Create;
using Microsoft.AspNetCore.Mvc;

namespace FetPamily.API.Controllers;

public class CreateVolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest  request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateVolunteerCommand(
            name: request.Name,
            surname: request.Surname,
            email: request.Email,
            description: request.Description,
            experience: request.Experience,
            phoneNumber: request.PhoneNumber,
            paymentDetails: request.PaymentDetails,
            socialNetworks: request.SocialNetworks);
        
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToActionResult();
        
        return Ok(result.Value);

    }
}