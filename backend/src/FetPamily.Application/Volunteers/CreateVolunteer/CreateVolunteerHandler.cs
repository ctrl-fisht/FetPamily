using CSharpFunctionalExtensions;
using FetPamily.Contracts.Volunteers.Create;
using FetPamily.Domain.Shared;
using FetPamily.Domain.Volunteers.Entities;
using FetPamily.Domain.Volunteers.SharedValueObjects;
using FetPamily.Domain.Volunteers.VolunteersValueObjects;

namespace FetPamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }
    public async Task<Result<Guid, Error>> Handle(CreateVolunteerCommand command, CancellationToken cancellationToken = default)
    {
        var fullNameResult = FullName.Create(command.Name, command.Surname);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error;
        
        var emailAddressResult = EmailAddress.Create(command.Email);
        if  (emailAddressResult.IsFailure)
            return emailAddressResult.Error;

        var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);
        if  (phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;
        
        // -- VolunteerDetails
        // ----List<PaymentDetail>
        // ----List<SocialNetwork>
        var paymentDetailsList = new List<PaymentDetail>();
        var socialNetworksList = new List<SocialNetwork>();

        foreach (var paymentDetail in command.PaymentDetails)
        {
            var pdResult = PaymentDetail.Create(
                paymentDetail.Name,
                paymentDetail.Description,
                paymentDetail.Value);
            
            if (pdResult.IsFailure)
                return pdResult.Error;
            
            paymentDetailsList.Add(pdResult.Value);
        }

        foreach (var socialNetwork in command.SocialNetworks)
        {
            var snResult = SocialNetwork.Create(socialNetwork.Name, socialNetwork.Link);
            
            if (snResult.IsFailure)
                return snResult.Error;
            
            socialNetworksList.Add(snResult.Value);
        }
        
        var volunteerDetails = VolunteerDetails.Create(paymentDetailsList, socialNetworksList);

        var volunteerResult = Volunteer.Create(
            fullName: fullNameResult.Value,
            email: emailAddressResult.Value,
            description: command.Description,
            experience: command.Experience,
            phoneNumber: phoneNumberResult.Value,
            volunteerDetails: volunteerDetails);
        
        if  (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        
        
        return await _volunteersRepository.Create(volunteerResult.Value, cancellationToken);
    }
}