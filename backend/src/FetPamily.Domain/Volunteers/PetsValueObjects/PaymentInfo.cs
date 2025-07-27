namespace FetPamily.Domain.Volunteers.PetsValueObjects;

public record PaymentInfo
{
    public List<PaymentDetail> PaymentDetails { get; }
}