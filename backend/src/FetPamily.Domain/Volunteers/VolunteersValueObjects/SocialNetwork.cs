using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;

namespace FetPamily.Domain.Volunteers.VolunteersValueObjects;

public record SocialNetwork
{
    public string Name { get; set; }
    public string Link { get; set; }

    private SocialNetwork(string name, string link)
    {
        Name = name;
        Link = link;
    }

    public static Result<SocialNetwork, Error> Create(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValidationNotNull("name");
        
        if (string.IsNullOrWhiteSpace(link))
            return Errors.General.ValidationNotNull("link");

        return new SocialNetwork(name, link);
    }
}