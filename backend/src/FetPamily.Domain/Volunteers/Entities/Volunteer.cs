using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using FetPamily.Domain.Volunteers.PetsValueObjects;
using FetPamily.Domain.Volunteers.VolunteersValueObjects;

namespace FetPamily.Domain.Volunteers.Entities;

public sealed class Volunteer : Entity<Guid>
{
    public const int MAX_FULLNAME_LENGTH = 150;
    public const int MAX_EMAIL_LENGTH = 320;
    public const int MAX_DESCRIPTION_LENGTH = 2000;
    
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

    public static Result<Volunteer> Create(string fullName, string email, string description, int experience,
        string phoneNumber)
    {
        if  (string.IsNullOrWhiteSpace(fullName))
            return Result.Failure<Volunteer>("Volunteer name cannot be empty");
        
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Volunteer>("Volunteer email cannot be empty");
        
        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return Result.Failure<Volunteer>("Invalid email address");
        
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Volunteer>("Volunteer description cannot be empty");
        
        if  (experience <= 0)
            return Result.Failure<Volunteer>("Volunteer experience cannot be negative");
        
        if  (string.IsNullOrWhiteSpace(phoneNumber))
            return Result.Failure<Volunteer>("Volunteer phone number cannot be empty");
        
        if (!Regex.IsMatch(phoneNumber, @"^\+?\d+$"))
            return Result.Failure<Volunteer>("Phone number must contain only digits and '+'");

        var volunteer = new Volunteer(
            fullName: fullName,
            email: email,
            description: description,
            experience: experience,
            phoneNumber: phoneNumber);
        
        return Result.Success(volunteer);
    }
}