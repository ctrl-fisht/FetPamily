using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace FetPamily.Domain.Volunteers.PetsValueObjects;

public record Address
{
    public const int MAX_CITY_LENGTH = 100;
    public const int MAX_STREET_LENGTH = 100;
    public const int MAX_BUILDING_LENGTH = 10;
    
    public string City { get; }
    public string Street { get; }
    public string Building { get; }
    public int? ApartmentNumber { get; }
        
    private Address(string city, string street, string building,  int? apartmentNumber)
    {
        City = city;
        Street = street;
        Building = building;
        ApartmentNumber = apartmentNumber;
    }

    public static Result<Address> Create(string city, string street, string building, int? apartmentNumber)
    {
        // regex: только буквы, дефисы и пробелы
        if (string.IsNullOrWhiteSpace(city) 
            || city.Length > MAX_CITY_LENGTH 
            || !Regex.IsMatch(city, @"^[a-zA-Zа-яА-ЯёЁ\s\-]+$"))
            return Result.Failure<Address>("Invalid city");
        
        // regex: буквы, цифры, пробелы, дефисы
        if (string.IsNullOrWhiteSpace(street) 
            || street.Length > MAX_STREET_LENGTH 
            || !Regex.IsMatch(street, @"^[\wа-яА-ЯёЁ\s\-\.]+$"))
            return Result.Failure<Address>("Invalid street");
        
        // буквы, цифры (напр. 12А, 5Б)
        if (string.IsNullOrWhiteSpace(building) 
            || building.Length > MAX_BUILDING_LENGTH 
            || !Regex.IsMatch(building, @"^[\wа-яА-ЯёЁ\-]+$"))
            return Result.Failure<Address>("Invalid building number");
        
        if (apartmentNumber.HasValue && apartmentNumber.Value < 1)
            return Result.Failure<Address>("Apartment number must be positive");


        return new Address(city, street, building, apartmentNumber);
    }
      
}