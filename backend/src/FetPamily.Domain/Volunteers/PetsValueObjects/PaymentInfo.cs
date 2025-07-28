using FetPamily.Domain.Volunteers.SharedValueObjects;

namespace FetPamily.Domain.Volunteers.PetsValueObjects;

public record PaymentInfo
{
    public List<PaymentDetail> PaymentDetails { get; }
}