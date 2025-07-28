using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;

namespace FetPamily.Domain.Volunteers.VolunteersValueObjects;

public record FullName
{
    public string Name { get; }
    public string Surname { get; }
    
    private FullName(string name, string surname)
    {
            Name = name;
            Surname = surname;
    }

    public static Result<FullName, Error> Create(string name, string surname)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValidationNotNull("name");

        if (name.Length < Constants.VOLUNTEER_MAX_NAME_LENGTH)
            return Errors.General.ValidationMaxLength("name", Constants.VOLUNTEER_MAX_NAME_LENGTH);
        
        if (!Regex.IsMatch(name, @"^\p{L}+$"))
            return Errors.General.ValidationInvalidFormat("name", "Иван");
        
        
        
        
        if (string.IsNullOrWhiteSpace(surname))
            return Errors.General.ValidationNotNull("surname");
        
        if (surname.Length > Constants.VOLUNTEER_MAX_SURNAME_LENGTH)
            return Errors.General.ValidationMaxLength("surname",  Constants.VOLUNTEER_MAX_SURNAME_LENGTH);
        
        if (!Regex.IsMatch(surname, @"^\p{L}+$"))
            return Errors.General.ValidationInvalidFormat("surname", "Иванов");

        return new FullName(name, surname);
    }
    
}