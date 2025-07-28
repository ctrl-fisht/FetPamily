using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;

namespace FetPamily.Domain.Volunteers.SharedValueObjects;

public record PhoneNumber
{
    public string Value { get; }

    // EF CORE
    private PhoneNumber()
    { }
    private PhoneNumber(string phoneNumber)
    {
        Value = phoneNumber;
    }
    
    public static Result<PhoneNumber, Error> Create(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Errors.General.ValidationNotNull("phoneNumber");
        
        var normalized = phoneNumber.Trim();

        if (!Regex.IsMatch(normalized, @"^\+7\d{10}$"))
            return Errors.General.ValidationInvalidFormat("phoneNumber", "+7XXXXXXXXXX");
        
        return new PhoneNumber(phoneNumber);
    }
}