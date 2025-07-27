using CSharpFunctionalExtensions;

namespace FetPamily.Domain.Species.Entities;

public sealed class Breed : Entity<Guid>
{
    public const int MAX_NAME_LENGTH = 200;
    
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

        var breed = new Breed(id, name);
        
        return Result.Success(breed);
    }
}