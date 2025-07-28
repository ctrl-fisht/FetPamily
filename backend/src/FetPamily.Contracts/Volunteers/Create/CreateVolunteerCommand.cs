using FetPamily.Contracts.Dto;

namespace FetPamily.Contracts.Volunteers.Create;

public class CreateVolunteerCommand
{
    public string Name { get; }
    public string Surname { get; }
    public string Email { get; }
    public string Description { get; }
    public int Experience { get; }
    public string PhoneNumber { get; }
    public List<PaymentDetailDto>  PaymentDetails { get; }
    public List<SocialNetworkDto> SocialNetworks { get; }
    
    public CreateVolunteerCommand(
        string name,
        string surname,
        string email,
        string description,
        int experience,
        string phoneNumber,
        List<PaymentDetailDto> paymentDetails,
        List<SocialNetworkDto> socialNetworks)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Description = description;
        Experience = experience;
        PhoneNumber = phoneNumber;
        PaymentDetails = paymentDetails;
        SocialNetworks = socialNetworks;
    }
}