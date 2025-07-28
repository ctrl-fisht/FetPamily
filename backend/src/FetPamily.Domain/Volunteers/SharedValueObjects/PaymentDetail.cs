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

    public static Result<PaymentDetail> Create(string name, string description, string value)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<PaymentDetail>("Name cannot be empty");
        
        if (name.Length > Constants.PAYMENT_MAX_NAME_LENGTH)
            return Result.Failure<PaymentDetail>($"Maximum name length is {Constants.PAYMENT_MAX_NAME_LENGTH} symbols");
        
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<PaymentDetail>($"Description cannot be empty");
        
        if  (description.Length > Constants.PAYMENT_MAX_DESCRIPTION_LENGTH)
            return Result.Failure<PaymentDetail>($"Description cannot be longer than {Constants.PAYMENT_MAX_DESCRIPTION_LENGTH} symbols");
        
        if  (value.Length > Constants.PAYMENT_MAX_VALUE_LENGTH)
            return Result.Failure<PaymentDetail>($"Value cannot be longer than {Constants.PAYMENT_MAX_VALUE_LENGTH} symbols");
       
        var paymentDetail = new PaymentDetail(name, description, value);
        return Result.Success(paymentDetail);
    }
};