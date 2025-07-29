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

    public static Result<Species, Error> Create(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValidationNotNull("name");
        
        if (name.Length > Constants.SPECIES_MAX_NAME_LENGTH)
            return Errors.General.ValidationMaxLength("name", Constants.SPECIES_MAX_NAME_LENGTH);

        if (id == Guid.Empty)
            return Errors.General.ValidationNotNull("id");
        
        var species = new Species(
            id: id,
            name: name);
        
        return species;
    }

    public UnitResult<Error> AddBreed(Breed breed)
    {
        if (_breeds.Any(b => b.Id == breed.Id))
            return Errors.General.AlreadyExist(breed.Id.ToString());
        
        _breeds.Add(breed);
        return UnitResult.Success<Error>();
    }
}