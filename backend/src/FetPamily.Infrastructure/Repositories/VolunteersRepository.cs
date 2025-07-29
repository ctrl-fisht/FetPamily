using FetPamily.Application.Volunteers;
using FetPamily.Domain.Volunteers.Entities;
using Microsoft.EntityFrameworkCore;

namespace FetPamily.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly AppDbContext _dbContext;

    public VolunteersRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Create(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return volunteer.Id;
    }

    public async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Volunteers.AnyAsync(v => v.Id == id, cancellationToken);
    }
}