using FetPamily.Domain.Volunteers.Entities;

namespace FetPamily.Application.Volunteers;

public interface IVolunteersRepository
{
    public Task<Guid> Create(Volunteer volunteer, CancellationToken cancellationToken);
    public Task<bool> Exists(Guid id, CancellationToken cancellationToken);
}