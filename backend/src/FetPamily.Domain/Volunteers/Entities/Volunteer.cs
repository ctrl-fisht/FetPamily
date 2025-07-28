using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;
using FetPamily.Domain.Volunteers.PetsValueObjects;
using FetPamily.Domain.Volunteers.SharedValueObjects;
using FetPamily.Domain.Volunteers.VolunteersValueObjects;

namespace FetPamily.Domain.Volunteers.Entities;

public sealed class Volunteer : Entity<Guid>
{
    private readonly List<Pet> _pets = new List<Pet>();
    
    public FullName FullName { get; private set; }
    public EmailAddress Email { get; private set; }
    public string Description { get; private set; }
    public int Experience { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    
    public VolunteerDetails? VolunteerDetails { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;
    
    public int PetsFoundHomeCount => _pets.Count(p => p.HelpStatus == HelpStatus.HomeFound);
    public int PetsFindingHomeCount => _pets.Count(p => p.HelpStatus == HelpStatus.FindingHome);
    public int PetsOnTreatment => _pets.Count(p => p.TreatmentStatus == TreatmentStatus.UnderTreatment);

    // EF CORE
    private Volunteer()
    { }
    
    private Volunteer(FullName fullName,  EmailAddress email, string description, int experience, PhoneNumber phoneNumber )
    {
        FullName = fullName;
        Email = email;
        Description = description;
        Experience = experience;
        PhoneNumber = phoneNumber;
    }

    public static Result<Volunteer, Error> Create(FullName fullName, EmailAddress email, string description, int experience,
        PhoneNumber phoneNumber)
    {
        
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValidationNotNull("description");
        
        if (description.Length > Constants.VOLUNTEER_MAX_DESCRIPTION_LENGTH)
            return Errors.General.ValidationMaxLength("description",  Constants.VOLUNTEER_MAX_DESCRIPTION_LENGTH);
        
        if  (experience <= 0)
            return Errors.General.ValidationNumberNegative("experience");
        
        if (experience > Constants.VOLUNTEER_MAX_EXPERIENCE)
            return Errors.General.ValidationGreaterThan("experience", Constants.VOLUNTEER_MAX_EXPERIENCE);
        
        var volunteer = new Volunteer(
            fullName: fullName,
            email: email,
            description: description,
            experience: experience,
            phoneNumber: phoneNumber);
        
        return volunteer;
    }
}