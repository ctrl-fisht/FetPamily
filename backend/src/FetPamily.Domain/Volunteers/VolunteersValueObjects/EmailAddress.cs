using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;

namespace FetPamily.Domain.Volunteers.VolunteersValueObjects;

public record EmailAddress
{
    public string Value { get; }
    
    private EmailAddress(string value)
    {
        Value = value;
    }

    public static Result<EmailAddress, Error> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Errors.General.ValidationNotNull("email");
        
        if (email.Length > Constants.VOLUNTEER_MAX_EMAIL_LENGTH)
            return Errors.General.ValidationMaxLength("email", Constants.VOLUNTEER_MAX_EMAIL_LENGTH);

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return Errors.General.ValidationInvalidFormat("email", "email@mail.com");

        return new EmailAddress(email);
    }
}