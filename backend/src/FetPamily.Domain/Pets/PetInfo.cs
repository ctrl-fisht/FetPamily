using CSharpFunctionalExtensions;

namespace FetPamily.Domain.Pets;

public record PetInfo
{
    public string SpeciesId { get;}
    public string BreedId { get;}
    public string Color { get;}
    
    private PetInfo(string speciesId, string breedId, string color)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
        Color = color;
    }

    public static Result<PetInfo> Create(string speciesId, string breedId, string color)
    {
        if (string.IsNullOrWhiteSpace(speciesId))
            return Result.Failure<PetInfo>("SpeciesId cannot be empty");
        
        if (string.IsNullOrWhiteSpace(breedId))
            return Result.Failure<PetInfo>("BreedId cannot be empty");
        
        if (string.IsNullOrWhiteSpace(color))
            return Result.Failure<PetInfo>("Color cannot be empty");

        var petInfo = new PetInfo(speciesId, breedId, color);

        return Result.Success<PetInfo>(petInfo);
    }
}
