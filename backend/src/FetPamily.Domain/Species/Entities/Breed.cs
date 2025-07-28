using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;

namespace FetPamily.Domain.Species.Entities;

public sealed class Breed : Entity<Guid>
{
    public string Name { get; private set; }
    
    
    public Guid SpeciesId { get; private set; }
    
    // navigation property
    public Species Species { get; set; }
    
    private Breed(Guid id, string name)
    {
        Name = name;
        Id = id;
    }

    public static Result<Breed, Error> Create(Guid id, string name)
    {
        if (id == Guid.Empty)
            return Errors.General.ValidationNotNull("id");
        
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValidationNotNull("name");
        
        if (name.Length > Constants.BREED_MAX_NAME_LENGTH)
            return Errors.General.ValidationMaxLength("name", Constants.BREED_MAX_NAME_LENGTH);

        var breed = new Breed(id, name);
        
        return breed;
    }
}