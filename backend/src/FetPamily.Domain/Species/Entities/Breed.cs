using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;

namespace FetPamily.Domain.Species.Entities;

public sealed class Breed : Entity<Guid>
{
    public string Name { get; set; }
    
    
    public Guid SpeciesId { get; private set; }
    
    // navigation property
    public Species Species { get; set; }
    
    private Breed(Guid id, string name)
    {
        Name = name;
        Id = id;
    }

    public static Result<Breed> Create(Guid id, string name)
    {
        if (id == Guid.Empty)
            return  Result.Failure<Breed>("Id cannot be empty");
        
        if (string.IsNullOrWhiteSpace(name))
            return  Result.Failure<Breed>("Name cannot be empty");
        
        if (name.Length > Constants.BREED_MAX_NAME_LENGTH)
            return  Result.Failure<Breed>($"Name cannot be longer than {Constants.BREED_MAX_NAME_LENGTH} characters");

        var breed = new Breed(id, name);
        
        return Result.Success(breed);
    }
}