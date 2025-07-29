using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;

namespace FetPamily.Domain.Volunteers.PetsValueObjects;

public record PetInfo
{
    public Guid SpeciesId { get;}
    public Guid BreedId { get;}
    public string Color { get;}
    
    private PetInfo(Guid speciesId, Guid breedId, string color)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
        Color = color;
    }

    public static Result<PetInfo, Error> Create(Guid speciesId, Guid breedId, string color)
    {
        if (speciesId == Guid.Empty)
            return Errors.General.ValidationNotNull("speciesId");
        
        if (breedId == Guid.Empty)
            return Errors.General.ValidationNotNull("breedId");

        if (string.IsNullOrWhiteSpace(color))
            return Errors.General.ValidationNotNull("color");

        var petInfo = new PetInfo(speciesId, breedId, color);

        return petInfo;
    }
}
