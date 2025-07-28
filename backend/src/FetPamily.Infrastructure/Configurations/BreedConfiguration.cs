using FetPamily.Domain.Shared;
using FetPamily.Domain.Species.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FetPamily.Infrastructure.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds", tb =>
        {

        });
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName("id")
            .IsRequired();
        
        builder.Property(b => b.SpeciesId)
            .HasColumnName("species_id")
            .IsRequired();

        builder.Property(b => b.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(Constants.BREED_MAX_NAME_LENGTH);
    }
}