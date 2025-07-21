using FetPamily.Domain.PaymentDetails;

using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace FetPamily.Domain.Pets;

public sealed class Pet : Entity<Guid>
{
    private readonly List<PaymentDetail> _paymentDetails = new();
    
    public IReadOnlyList<PaymentDetail> PaymentDetails  => _paymentDetails;
    public string Name { get; private set; }
    public string Description { get; private set; }
    public PetInfo PetInfo { get; private set; }
    public TreatmentStatus TreatmentStatus { get; private set; }
    public string Address { get; private set; }
    public decimal Weight { get; private set; }
    public decimal Height { get; private set; }
    public string PhoneNumber { get; private set; }
    public bool IsNeutered  { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public bool IsVaccinated { get; private set; }
    public HelpStatus HelpStatus { get; private set; }
    public DateTime Created { get; } = DateTime.UtcNow;
    
    private Pet(
        Guid id,
        string name,
        string description,
        PetInfo petInfo,
        TreatmentStatus treatmentStatus,
        string address,
        decimal weight,
        decimal height,
        string phoneNumber,
        bool isNeutered,
        DateTime dateOfBirth,
        bool isVaccinated,
        HelpStatus helpStatus)
    {
        Id = id;
        Name = name;
        PetInfo = petInfo;
        Description = description;
        TreatmentStatus = treatmentStatus;
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
        string name,
        string description,
        PetInfo petInfo,
        TreatmentStatus treatmentStatus,
        string address,
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
            return Result.Failure<Pet>("Guid cannot be empty.");
        
        // name 
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Pet>("Name cannot be empty");
        
        // description 
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Pet>("Description cannot be empty");
        
        
        // address 
        if (string.IsNullOrWhiteSpace(address))
            return Result.Failure<Pet>("Address cannot be empty");
        
        // weight 
        if (weight < 0 )
            return Result.Failure<Pet>("Weight cannot be negative");
        
        // height 
        if (height < 0)
            return Result.Failure<Pet>("Height cannot be negative");
        
        // phoneNumber
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Result.Failure<Pet>("Phone number cannot be empty");
        
        if (Regex.IsMatch(phoneNumber, @"^\+?\d+$"))
            return Result.Failure<Pet>("Phone number must contain only digits and '+'");
        
        // dateOfBirth
        if (dateOfBirth > DateTime.UtcNow)
            return Result.Failure<Pet>("Date of birth cannot be in the future");
        
        var pet = new Pet(
            id: id,
            name: name,
            description: description,
            treatmentStatus: treatmentStatus,
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
    
    public Result AddPaymentDetail(PaymentDetail paymentDetail)
    {
        if (_paymentDetails.Contains(paymentDetail))
            return Result.Failure("Payment detail already exists");
        
        _paymentDetails.Add(paymentDetail);
        return Result.Success();
    }
}

