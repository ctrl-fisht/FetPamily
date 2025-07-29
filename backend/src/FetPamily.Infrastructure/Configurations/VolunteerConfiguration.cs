using CSharpFunctionalExtensions;
using FetPamily.Domain.Shared;
using FetPamily.Domain.Volunteers.VolunteersValueObjects;
using FetPamily.Domain.Volunteers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FetPamily.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers", tb =>
        {
            tb.HasCheckConstraint("CK_volunteers_email",
                "\"email\" ~ '^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$'");

            tb.HasCheckConstraint("CK_volunteers_experience",
                "\"experience\" >= 0");

            tb.HasCheckConstraint("CK_volunteers_phone_number",
                "\"phone_number\" ~ '^\\+7\\d{10}$'");
        });

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.ComplexProperty(v => v.FullName, fnb =>
        {
            fnb.Property(fn => fn.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(Constants.VOLUNTEER_MAX_NAME_LENGTH);
            
            fnb.Property(fn => fn.Surname)
                .HasColumnName("surname")
                .IsRequired()
                .HasMaxLength(Constants.VOLUNTEER_MAX_SURNAME_LENGTH);
        });

        builder.ComplexProperty(v => v.Email, eb =>
        {
            eb.Property(ea => ea.Value)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(Constants.VOLUNTEER_MAX_EMAIL_LENGTH);
        });
           
        builder.Property(v => v.Description)
            .HasColumnName("description")
            .HasMaxLength(Constants.VOLUNTEER_MAX_DESCRIPTION_LENGTH);

        builder.Property(v => v.Experience)
            .HasColumnName("experience");

        builder.ComplexProperty(v => v.PhoneNumber, pnb =>
            {
                pnb.Property(pn => pn.Value)
                    .HasColumnName("phone_number")
                    .IsRequired();
            });

        builder.OwnsOne(v => v.VolunteerDetails, vdb =>
        {
            vdb.ToJson("volunteer_details");

            vdb.OwnsMany(pi => pi.PaymentDetails, pdb =>
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
                    .HasMaxLength(Constants.PAYMENT_MAX_VALUE_LENGTH);
                ;
            });

            vdb.OwnsMany(pi => pi.SocialNetworks, snb =>
            {
                snb.Property(sn => sn.Name)
                    .HasColumnName("soc_name")
                    .IsRequired();

                snb.Property(sn => sn.Link)
                    .HasColumnName("soc_link")
                    .IsRequired();
            });
        });
        
        builder.HasMany(v => v.Pets)
            .WithOne(p => p.Volunteer)
            .HasForeignKey(p => p.VolunteerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}