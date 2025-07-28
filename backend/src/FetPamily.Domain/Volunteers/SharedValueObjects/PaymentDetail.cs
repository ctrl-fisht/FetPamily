using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;

namespace FetPamily.Domain.Volunteers.SharedValueObjects;

public record PaymentDetail
{
    public string Name { get;}
    public string Description { get;}
    public string Value { get;}
    private PaymentDetail(string name, string description, string value)
    {
        Name = name;
        Description = description;
        Value = value;
    }

    public static Result<PaymentDetail, Error> Create(string name, string description, string value)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValidationNotNull("name");
        
        if (name.Length > Constants.PAYMENT_MAX_NAME_LENGTH)
            return Errors.General.ValidationMaxLength("name", Constants.PAYMENT_MAX_NAME_LENGTH);
        
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValidationNotNull("description");
        
        if  (description.Length > Constants.PAYMENT_MAX_DESCRIPTION_LENGTH)
            return Errors.General.ValidationMaxLength("description", Constants.PAYMENT_MAX_DESCRIPTION_LENGTH);
        
        if  (value.Length > Constants.PAYMENT_MAX_VALUE_LENGTH)
            return Errors.General.ValidationMaxLength("value",  Constants.PAYMENT_MAX_VALUE_LENGTH);
        
        var paymentDetail = new PaymentDetail(name, description, value);
        return paymentDetail;
    }
};