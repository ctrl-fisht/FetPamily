using CSharpFunctionalExtensions;

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

    public static Result<PetInfo> Create(Guid speciesId, Guid breedId, string color)
    {
        if (speciesId == Guid.Empty)
            return Result.Failure<PetInfo>("SpeciesId cannot be empty");
        
        if (breedId == Guid.Empty)
            return Result.Failure<PetInfo>("BreedId cannot be empty");
        
        if (string.IsNullOrWhiteSpace(color))
            return Result.Failure<PetInfo>("Color cannot be empty");

        var petInfo = new PetInfo(speciesId, breedId, color);

        return Result.Success<PetInfo>(petInfo);
    }
}
