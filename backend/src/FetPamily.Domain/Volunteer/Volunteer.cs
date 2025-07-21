using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

using FetPamily.Domain.PaymentDetails;
using FetPamily.Domain.Pets;


namespace FetPamily.Domain.Volunteer;

public sealed class Volunteer : Entity<Guid>
{
    private readonly List<SocialNetwork> _socialNetworks = new List<SocialNetwork>();
    private readonly List<Pet> _pets = new List<Pet>();
    private readonly List<PaymentDetail> _paymentDetails = new List<PaymentDetail>();
    
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public int Experience { get; private set; }
    public string PhoneNumber { get; private set; }
    
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<Pet> Pets => _pets;
    public IReadOnlyList<PaymentDetail> PaymentDetails => _paymentDetails;
    
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
        
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Volunteer>("Volunteer description cannot be empty");
        
        if  (experience <= 0)
            return Result.Failure<Volunteer>("Volunteer experience cannot be negative");
        
        if  (string.IsNullOrWhiteSpace(phoneNumber))
            return Result.Failure<Volunteer>("Volunteer phone number cannot be empty");
        
        if (Regex.IsMatch(phoneNumber, @"^\+?\d+$"))
            return Result.Failure<Volunteer>("Phone number must contain only digits and '+'");

        var volunteer = new Volunteer(
            fullName: fullName,
            email: email,
            description: description,
            experience: experience,
            phoneNumber: phoneNumber);
        
        return Result.Success(volunteer);
    }

    public Result AddPet(Pet pet)
    {
        if (_pets.Any(p => p.Id == pet.Id))
            return  Result.Failure<Pet>("Pet already exists (id)");
        
        _pets.Add(pet);
        return Result.Success();
    }

    public Result AddSocialNetwork(SocialNetwork socialNetwork)
    {
        if (_socialNetworks.Contains(socialNetwork))
            return Result.Failure("Social network already exists");
        
        _socialNetworks.Add(socialNetwork);
        return Result.Success();
    }

    public Result AddPaymentDetail(PaymentDetail paymentDetail)
    {
        if (_paymentDetails.Contains(paymentDetail))
            return Result.Failure("Payment detail already exists");
        
        _paymentDetails.Add(paymentDetail);
        return Result.Success();
    }
    
}