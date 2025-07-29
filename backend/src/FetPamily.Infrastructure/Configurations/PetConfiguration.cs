using FetPamily.Domain.Shared;
using FetPamily.Domain.Volunteers.Entities;
using FetPamily.Domain.Volunteers.PetsValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FetPamily.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(p => p.Id);

        builder.ToTable("pets", tb =>
        {
            // 0 < weight < MAX_WEIGHT
            tb.HasCheckConstraint(
                "CK_pets_weight",
                $"\"weight\" > 0 AND \"weight\" <= {Constants.PET_MAX_WEIGHT}");
            
            // 0 < height < MAX_HEIGHT
            tb.HasCheckConstraint("CK_pets_height",
                $"\"height\" > 0 AND \"height\" <= {Constants.PET_MAX_HEIGHT}");

            // phone_number regex check 
            tb.HasCheckConstraint("CK_pets_phone_number",
                "\"phone_number\" ~ '^\\+7\\d{10}$'");

            // dob < CURRENT_DATE
            tb.HasCheckConstraint("CK_pets_dob",
                "\"dob\" <= CURRENT_DATE");

            // city = буквы, дефисы, пробелы
            tb.HasCheckConstraint("CK_pets_address_city",
                "\"address_city\" ~ '^[a-zA-Zа-яА-ЯёЁ\\s\\-]+$'");

            // street = буквы, цифры, пробелы, дефисы
            tb.HasCheckConstraint("CK_pets_address_street",
                "\"address_street\" ~ '^[a-zA-ZА-Яа-яЁё0-9\\-\\. ]+$'");
            
            // address_building = буквы, цифры, дефис
            tb.HasCheckConstraint("CK_pets_address_building",
                "\"address_building\" ~ '^[a-zA-ZА-Яа-яЁё0-9\\-]+$'");

            // address_appartmentNumber > 0, либо null
            tb.HasCheckConstraint("address_apartment_number",
                "\"address_apartment_number\" IS NULL OR \"address_apartment_number\" > 0");
        });

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(p => p.VolunteerId)
            .HasColumnName("volunteer_id")
            .IsRequired();
        
        builder.Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(Constants.PET_MAX_NAME_LENGTH);

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(Constants.PET_MAX_DESCRIPTION_LENGTH);

        builder.ComplexProperty(p => p.PetInfo, pib =>
        {
            pib.Property(pi => pi.BreedId)
                .HasColumnName("info_breed_id")
                .IsRequired();
            
            pib.Property(pi => pi.SpeciesId)
                .HasColumnName("info_species_id")
                .IsRequired();
            
            pib.Property(pi => pi.Color)
                .HasColumnName("info_color")
                .IsRequired();
        });
        
        
        builder.Property(p => p.TreatmentStatus)
            .HasColumnName("treatment_status")
            .IsRequired()
            .HasConversion<string>();
        
        builder.OwnsOne(p => p.PaymentInfo, pib =>
        {
            pib.ToJson("payment_info");

            pib.OwnsMany(pi => pi.PaymentDetails, pdb =>
            {
                pdb.Property(pd => pd.Name)
                    .HasColumnName("payment_name")
                    .IsRequired()
                    .HasMaxLength(Constants.PAYMENT_MAX_NAME_LENGTH);
                
                pdb.Property(pd => pd.Description)
                    .HasColumnName("payment_description")
                    .IsRequired()
                    .HasMaxLength(Constants.PAYMENT_MAX_DESCRIPTION_LENGTH);
                
                pdb.Property(pd => pd.Value)
                    .HasColumnName("payment_value")
                    .IsRequired()
                    .HasMaxLength(Constants.PAYMENT_MAX_VALUE_LENGTH);;
            });
        });
        
        builder.ComplexProperty(p => p.Address, ab =>
        {
            ab.Property(a => a.City)
                .HasColumnName("address_city")
                .IsRequired()
                .HasMaxLength(Constants.ADDRESS_MAX_CITY_LENGTH);
            
            ab.Property(a => a.Street)
                .HasColumnName("address_street")
                .IsRequired()
                .HasMaxLength(Constants.ADDRESS_MAX_STREET_LENGTH);
            
            ab.Property(a => a.Building)
                .HasColumnName("address_building")
                .IsRequired()
                .HasMaxLength(Constants.ADDRESS_MAX_BUILDING_LENGTH);

            ab.Property(a => a.ApartmentNumber)
                .HasColumnName("address_apartment_number");
        });
        
        builder.Property(p => p.Weight)
            .HasColumnName("weight")
            .IsRequired();
        
        builder.Property(p => p.Height)
            .HasColumnName("height")
            .IsRequired();

        builder.ComplexProperty(p => p.PhoneNumber, pnb =>
            {
                pnb.Property(pn => pn.Value)
                    .HasColumnName("phone_number")
                    .IsRequired();
            });
        
        builder.Property(p => p.IsNeutered)
            .HasColumnName("is_neutered")
            .IsRequired();
        
        builder.Property(p => p.DateOfBirth)
            .HasColumnName("dob")
            .IsRequired();
        
        builder.Property(p => p.IsVaccinated)
            .HasColumnName("is_vaccinated")
            .IsRequired();
        
        builder.Property(p => p.HelpStatus)
            .HasColumnName("help_status")
            .IsRequired()
            .HasConversion<string>();
    }
}