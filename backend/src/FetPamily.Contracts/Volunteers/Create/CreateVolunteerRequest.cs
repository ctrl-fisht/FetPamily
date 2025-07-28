using FetPamily.Contracts.Dto;

namespace FetPamily.Contracts.Volunteers.Create;

public class CreateVolunteerRequest
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string Description { get; set; }
    public int Experience { get; set; }
    public required string PhoneNumber { get; set; }
    public List<PaymentDetailDto>  PaymentDetails { get; set; } = new();
    public List<SocialNetworkDto> SocialNetworks { get; set; } = new();
}