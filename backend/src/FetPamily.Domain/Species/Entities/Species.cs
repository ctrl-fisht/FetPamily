using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;

namespace FetPamily.Domain.Species.Entities;

public sealed class Species : Entity<Guid>
{
    private readonly List<Breed> _breeds = new List<Breed>();
    public string Name { get; private set; }
    
    public IReadOnlyList<Breed> Breeds => _breeds;

    private Species(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Result<Species> Create(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Species>("Name cannot be empty");
        
        if (name.Length > Constants.SPECIES_MAX_NAME_LENGTH)
            return Result.Failure<Species>($"Name cannot be longer than {Constants.SPECIES_MAX_NAME_LENGTH}");
        
        if (id == Guid.Empty)
            return Result.Failure<Species>("Guid cannot be empty");
        
        var species = new Species(
            id: id,
            name: name);
        
        return Result.Success(species);
    }

    public Result AddBreed(Breed breed)
    {
        if (_breeds.Any(b => b.Id == breed.Id))
            return Result.Failure<Breed>("Duplicate breed");
        
        _breeds.Add(breed);
        return Result.Success();
    }
}