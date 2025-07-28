using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;
using FetPamily.Domain.Volunteers.PetsValueObjects;

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
    public string PhoneNumber { get; private set; }
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
        string phoneNumber,
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

    public static Result<Pet> Create(
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
        string phoneNumber,
        bool isNeutered,
        DateTime dateOfBirth,
        bool isVaccinated,
        HelpStatus helpStatus)
    {
        // id
        if (id == Guid.Empty)
            return Result.Failure<Pet>("Id cannot be empty.");
        
        // volunteer id
        if  (volunteerId == Guid.Empty)
            return Result.Failure<Pet>("VolunteerId cannot be empty.");
        
        // name 
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Pet>("Name cannot be empty");
        
        if (name.Length > Constants.PET_MAX_NAME_LENGTH)
            return Result.Failure<Pet>($"Name cannot be longer than {Constants.PET_MAX_NAME_LENGTH} symbols.");
        
        // description 
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Pet>("Description cannot be empty");
        
        if (description.Length > Constants.PET_MAX_DESCRIPTION_LENGTH)
            return Result.Failure<Pet>($"Description cannot be longer than {Constants.PET_MAX_DESCRIPTION_LENGTH} symbols.");
        
        // weight 
        if (weight < 0 )
            return Result.Failure<Pet>("Weight cannot be negative");
        
        if (weight > Constants.PET_MAX_WEIGHT)
            return Result.Failure<Pet>($"Weight cannot be greater than {Constants.PET_MAX_WEIGHT} symbols.");
        
        // height 
        if (height < 0)
            return Result.Failure<Pet>("Height cannot be negative");
        
        if (height > Constants.PET_MAX_HEIGHT)
            return Result.Failure<Pet>($"Height cannot be greater than {Constants.PET_MAX_HEIGHT} symbols.");
        
        // phoneNumber
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Result.Failure<Pet>("Phone number cannot be empty");
        
        if (!Regex.IsMatch(phoneNumber, @"^\+?\d+$"))
            return Result.Failure<Pet>("Phone number must contain only digits and '+'");
        
        // dateOfBirth
        if (dateOfBirth > DateTime.UtcNow)
            return Result.Failure<Pet>("Date of birth cannot be in the future");
        
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
        
        return Result.Success(pet);
    }
}