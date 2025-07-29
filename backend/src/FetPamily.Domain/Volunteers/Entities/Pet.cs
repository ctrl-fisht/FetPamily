using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;
using FetPamily.Domain.Volunteers.PetsValueObjects;
using FetPamily.Domain.Volunteers.SharedValueObjects;

namespace FetPamily.Domain.Volunteers.Entities;

public sealed class Pet : Entity<Guid>
{
    public Guid VolunteerId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public PetInfo PetInfo { get; private set; }
    public TreatmentStatus TreatmentStatus { get; private set; }
    public PaymentInfo PaymentInfo { get; private set; }
    public Address Address { get; private set; }
    public decimal Weight { get; private set; }
    public decimal Height { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public bool IsNeutered  { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public bool IsVaccinated { get; private set; }
    public HelpStatus HelpStatus { get; private set; }
    public DateTime Created { get; } = DateTime.UtcNow;
    
    public Volunteer Volunteer { get; set; }
    // EF CORE
    private Pet()
    {
        
    }
    private Pet(
        Guid id,
        Guid volunteerId,
        string name,
        string description,
        PetInfo petInfo,
        TreatmentStatus treatmentStatus,
        PaymentInfo paymentInfo,
        Address address,
        decimal weight,
        decimal height,
        PhoneNumber phoneNumber,
        bool isNeutered,
        DateTime dateOfBirth,
        bool isVaccinated,
        HelpStatus helpStatus)
    {
        Id = id;
        VolunteerId = volunteerId;
        Name = name;
        PetInfo = petInfo;
        Description = description;
        TreatmentStatus = treatmentStatus;
        PaymentInfo = paymentInfo;
        Address = address;
        Weight = weight;
        Height = height;
        PhoneNumber = phoneNumber;
        IsNeutered = isNeutered;
        DateOfBirth = dateOfBirth;
        IsVaccinated = isVaccinated;
        HelpStatus = helpStatus;
    }

    public static Result<Pet, Error> Create(
        Guid id,
        Guid volunteerId,
        string name,
        string description,
        PetInfo petInfo,
        TreatmentStatus treatmentStatus,
        PaymentInfo paymentInfo,
        Address address,
        decimal weight,
        decimal height,
        PhoneNumber phoneNumber,
        bool isNeutered,
        DateTime dateOfBirth,
        bool isVaccinated,
        HelpStatus helpStatus)
    {
        // id
        if (id == Guid.Empty)
            return Errors.General.ValidationNotNull("id");
        
        // volunteer id
        if  (volunteerId == Guid.Empty)
            return Errors.General.ValidationNotNull("volunteerId");
        
        // name 
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValidationNotNull("name");
        
        if (name.Length > Constants.PET_MAX_NAME_LENGTH)
            return Errors.General.ValidationMaxLength("name", Constants.PET_MAX_NAME_LENGTH);
        
        if (!Regex.IsMatch(name, @"^\p{L}+$"))
        {
            return Errors.General.ValidationInvalidFormat("name", "Барсик");
        }
        
        // description 
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValidationNotNull("description");
        
        if (description.Length > Constants.PET_MAX_DESCRIPTION_LENGTH)
            return Errors.General.ValidationMaxLength("description", Constants.PET_MAX_DESCRIPTION_LENGTH);
        
        // weight 
        if (weight < 0)
            return Errors.General.ValidationNumberNegative("weight");
        
        if (weight > Constants.PET_MAX_WEIGHT)
            return Errors.General.ValidationGreaterThan("weight", Constants.PET_MAX_WEIGHT);  
        
        // height 
        if (height < 0)
            return Errors.General.ValidationNumberNegative("height");
        
        if (height > Constants.PET_MAX_HEIGHT)
            return Errors.General.ValidationGreaterThan("height", Constants.PET_MAX_HEIGHT);
        
        
        // dateOfBirth
        if (dateOfBirth > DateTime.UtcNow)
            return Errors.General.ValidationDateInFuture("dateOfBirth");
        
        var pet = new Pet(
            id: id,
            volunteerId: volunteerId,
            name: name,
            description: description,
            treatmentStatus: treatmentStatus,
            paymentInfo: paymentInfo,
            petInfo: petInfo,
            address: address,
            weight: weight,
            height: height,
            phoneNumber: phoneNumber,
            isNeutered: isNeutered,
            dateOfBirth: dateOfBirth,
            isVaccinated: isVaccinated,
            helpStatus: helpStatus);
        
        return pet;
    }
}