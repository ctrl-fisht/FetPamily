using FetPamily.Domain.Volunteers.SharedValueObjects;

namespace FetPamily.Domain.Volunteers.VolunteersValueObjects;

public record VolunteerDetails
{
    public List<PaymentDetail> PaymentDetails {get;}
    public List<SocialNetwork> SocialNetworks  {get;}
}