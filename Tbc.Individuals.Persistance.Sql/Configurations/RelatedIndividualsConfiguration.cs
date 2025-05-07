using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tbc.Individuals.Domain.Entities;

namespace Tbc.Individuals.Persistance.Sql.Configurations;

public class RelatedIndividualsConfiguration : IEntityTypeConfiguration<RelatedIndividual>
{
    public void Configure(EntityTypeBuilder<RelatedIndividual> builder)
    {
        builder.ToTable("RelatedIndividuals");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd();
        builder.Property(i => i.RelationshipType).IsRequired();

        builder.HasOne(r => r.Individual)
               .WithMany()
               .HasForeignKey("RelatedIndividualId")
               .OnDelete(DeleteBehavior.Restrict);
    }
}
