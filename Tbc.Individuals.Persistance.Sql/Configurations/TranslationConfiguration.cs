using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tbc.Individuals.Domain.Entities;

namespace Tbc.Individuals.Persistance.Sql.Configurations;

public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder.ToTable("Translations");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        builder.HasMany(t => t.TranslationValues)
            .WithOne()
            .HasForeignKey("TranslationId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class TranslationValueConfiguration : IEntityTypeConfiguration<TranslationValue>
{
    public void Configure(EntityTypeBuilder<TranslationValue> builder)
    {
        builder.ToTable("TranslationValues");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.Language).HasMaxLength(2);
        builder.Property(t => t.Value).HasMaxLength(250);
    }
}
