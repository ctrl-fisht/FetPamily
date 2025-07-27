namespace FetPamily.Domain.Volunteers.VolunteersValueObjects;

public record PaymentDetail(string Name, string Description, string Value)
{
    public const int MAX_NAME_LENGTH = 100;
    public const int MAX_DESCRIPTION_LENGTH = 500;
    public const int MAX_VALUE_LENGTH = 100;
};