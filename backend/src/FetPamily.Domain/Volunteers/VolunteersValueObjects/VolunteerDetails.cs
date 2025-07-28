using FetPamily.Domain.Volunteers.SharedValueObjects;

namespace FetPamily.Domain.Volunteers.VolunteersValueObjects;

public record VolunteerDetails
{
    public List<PaymentDetail> PaymentDetails {get;}
    public List<SocialNetwork> SocialNetworks  {get;}

    // EF Core
    private VolunteerDetails()
    {
        
    }
    
    private VolunteerDetails(List<PaymentDetail> paymentDetails, List<SocialNetwork> socialNetworks)
    {
        PaymentDetails = paymentDetails;
        SocialNetworks = socialNetworks;
    }

    public static VolunteerDetails Create(List<PaymentDetail> paymentDetails, List<SocialNetwork> socialNetworks)
    {
        return new VolunteerDetails(paymentDetails, socialNetworks);
    }
}