using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;
using FetPamily.Domain.Volunteers.PetsValueObjects;
using FetPamily.Domain.Volunteers.VolunteersValueObjects;

namespace FetPamily.Domain.Volunteers.Entities;

public sealed class Volunteer : Entity<Guid>
{
    private readonly List<Pet> _pets = new List<Pet>();
    
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public int Experience { get; private set; }
    public string PhoneNumber { get; private set; }
    
    public VolunteerDetails? VolunteerDetails { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;
    
    public int PetsFoundHomeCount => _pets.Count(p => p.HelpStatus == HelpStatus.HomeFound);
    public int PetsFindingHomeCount => _pets.Count(p => p.HelpStatus == HelpStatus.FindingHome);
    public int PetsOnTreatment => _pets.Count(p => p.TreatmentStatus == TreatmentStatus.UnderTreatment);

    private Volunteer(string fullName,  string email, string description, int experience, string phoneNumber )
    {
        FullName = fullName;
        Email = email;
        Description = description;
        Experience = experience;
        PhoneNumber = phoneNumber;
    }

    public static Result<Volunteer, Error> Create(string fullName, string email, string description, int experience,
        string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return Errors.General.ValidationNotNull("fullName");
        
        if (fullName.Length > Constants.VOLUNTEER_MAX_FULLNAME_LENGTH)
            return Errors.General.ValidationMaxLength("fullName", Constants.VOLUNTEER_MAX_FULLNAME_LENGTH);
        
        
        if (string.IsNullOrWhiteSpace(email))
            return Errors.General.ValidationNotNull("email");
        
        if (email.Length > Constants.VOLUNTEER_MAX_EMAIL_LENGTH)
            return Errors.General.ValidationMaxLength("email", Constants.VOLUNTEER_MAX_EMAIL_LENGTH);

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return Errors.General.ValidationInvalidFormat("email");
        
        
        
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValidationNotNull("description");
        
        if (description.Length > Constants.VOLUNTEER_MAX_DESCRIPTION_LENGTH)
            return Errors.General.ValidationMaxLength("description",  Constants.VOLUNTEER_MAX_DESCRIPTION_LENGTH);
        
        
        
        if  (experience <= 0)
            return Errors.General.ValidationNumberNegative("experience");
        
        if  (string.IsNullOrWhiteSpace(phoneNumber))
            return Errors.General.ValidationNotNull("phoneNumber");
        
        if (!Regex.IsMatch(phoneNumber, @"^\+?\d+$"))
            return Errors.General.ValidationInvalidFormat("phoneNumber");
        
        var volunteer = new Volunteer(
            fullName: fullName,
            email: email,
            description: description,
            experience: experience,
            phoneNumber: phoneNumber);
        
        return volunteer;
    }
}