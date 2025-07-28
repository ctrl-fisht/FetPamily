using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;

namespace FetPamily.Domain.Volunteers.PetsValueObjects;

public record Address
{
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
            || city.Length > Constants.ADDRESS_MAX_CITY_LENGTH 
            || !Regex.IsMatch(city, @"^[a-zA-Zа-яА-ЯёЁ\s\-]+$"))
            return Result.Failure<Address>("Invalid city");
        
        // regex: буквы, цифры, пробелы, дефисы
        if (string.IsNullOrWhiteSpace(street) 
            || street.Length > Constants.ADDRESS_MAX_STREET_LENGTH 
            || !Regex.IsMatch(street, @"^[\wа-яА-ЯёЁ\s\-\.]+$"))
            return Result.Failure<Address>("Invalid street");
        
        // буквы, цифры (напр. 12А, 5Б)
        if (string.IsNullOrWhiteSpace(building) 
            || building.Length > Constants.ADDRESS_MAX_BUILDING_LENGTH 
            || !Regex.IsMatch(building, @"^[\wа-яА-ЯёЁ\-]+$"))
            return Result.Failure<Address>("Invalid building number");
        
        if (apartmentNumber.HasValue && apartmentNumber.Value < 1)
            return Result.Failure<Address>("Apartment number must be positive");


        return new Address(city, street, building, apartmentNumber);
    }
      
}