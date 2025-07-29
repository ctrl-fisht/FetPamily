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

    public static Result<Address, Error> Create(string city, string street, string building, int? apartmentNumber)
    {
        // regex: только буквы, дефисы и пробелы
        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValidationNotNull("city");
                
        if (city.Length > Constants.ADDRESS_MAX_CITY_LENGTH)
            return Errors.General.ValidationMaxLength("city", Constants.ADDRESS_MAX_CITY_LENGTH);

        if (!Regex.IsMatch(city, @"^[a-zA-Zа-яА-ЯёЁ\s\-]+$"))
            return Errors.General.ValidationInvalidFormat("city", "Москва, Санкт-Петербург");
        
        
        // regex: буквы, цифры, пробелы, дефисы
        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValidationNotNull("street");
                
        if (street.Length > Constants.ADDRESS_MAX_STREET_LENGTH)
            return Errors.General.ValidationMaxLength("street", Constants.ADDRESS_MAX_STREET_LENGTH);

        if (!Regex.IsMatch(street, @"^[\wа-яА-ЯёЁ\s\-\.]+$"))
            return Errors.General.ValidationInvalidFormat("street", "Пушкина, 78-Бригады");

        
        // буквы, цифры (напр. 12А, 5Б)
        if (string.IsNullOrWhiteSpace(building))
            return Errors.General.ValidationNotNull("building");
                
        if (building.Length > Constants.ADDRESS_MAX_BUILDING_LENGTH)
            return Errors.General.ValidationMaxLength("building", Constants.ADDRESS_MAX_BUILDING_LENGTH);

        if (!Regex.IsMatch(building, @"^[\wа-яА-ЯёЁ\-]+$"))
            return Errors.General.ValidationInvalidFormat("building", "12А, 5Б");
        
        
        
        if (apartmentNumber.HasValue && apartmentNumber.Value < 1)
            return Errors.General.ValidationGreaterThan("apartmentNumber", 0);


        return new Address(city, street, building, apartmentNumber);
    }
      
}