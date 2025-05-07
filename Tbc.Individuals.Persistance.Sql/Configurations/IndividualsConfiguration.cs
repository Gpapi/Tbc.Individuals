using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tbc.Individuals.Domain.Entities;

namespace Tbc.Individuals.Persistance.Sql.Configurations;

public class IndividualsConfiguration : IEntityTypeConfiguration<Individual>
{
    public void Configure(EntityTypeBuilder<Individual> builder)
    {
        builder.ToTable("Individuals");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).ValueGeneratedOnAdd();
        builder.Property(i => i.ProfileImage).HasMaxLength(128);

        builder.Property(i => i.FirstName)
            .HasConversion(name => name.Value, value => FirstName.Create(value))
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(i => i.LastName)
            .HasConversion(name => name.Value, value => LastName.Create(value))
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(i => i.PersonalId)
            .HasConversion(id => id.Value, value => PersonalId.Create(value))
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(i => i.DateOfBirth)
            .HasConversion(date => date.Value, value => DateOfBirth.Create(value))
            .IsRequired();

        builder.HasOne(i => i.City)
            .WithMany()
            .HasForeignKey("CityId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsMany(i => i.PhoneNumbers, n =>
        {
            n.ToTable("PhoneNumbers");
            n.WithOwner().HasForeignKey("IndividualId");
            n.Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(50);
            n.Property(p => p.Type)
                .IsRequired();

            n.HasKey("IndividualId", "Number", "Type");
        });

        builder.HasMany(i => i.RelatedIndividuals)
               .WithOne()
               .HasForeignKey("IndividualId")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
