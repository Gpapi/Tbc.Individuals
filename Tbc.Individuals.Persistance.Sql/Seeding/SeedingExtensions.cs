using Microsoft.EntityFrameworkCore;
using Tbc.Individuals.Domain.Entities;

namespace Tbc.Individuals.Persistance.Sql.Seeding;

public static class SeedingExtensions
{
    private static readonly dynamic[] _cities = [
        new { Georgian = "თბილისი", English = "Tbilisi" },
        new { Georgian = "ბათუმი", English = "Batumi" },
        new { Georgian = "ქუთაისი", English = "Kutaisi" },
        new { Georgian = "რუსთავი", English = "Rustavi" },
        new { Georgian = "გორი", English = "Gori" },
        new { Georgian = "ზუგდიდი", English = "Zugdidi" },
        new { Georgian = "ფოთი", English = "Poti" },
        new { Georgian = "თელავი", English = "Telavi" },
        new { Georgian = "ახალციხე", English = "Akhaltsikhe" },
        new { Georgian = "ოზურგეთი", English = "Ozurgeti" },
        new { Georgian = "ამბროლაური", English = "Ambrolauri" },
        new { Georgian = "ახალქალაქი", English = "Akhalkalaki" },
        new { Georgian = "ბორჯომი", English = "Borjomi" },
        new { Georgian = "ლანჩხუთი", English = "Lanchkhuti" },
        new { Georgian = "მარნეული", English = "Marneuli" },
        new { Georgian = "საჩხერე", English = "Sachkhere" },
        new { Georgian = "საგარეჯო", English = "Sagarejo" },
        new { Georgian = "ჩხოროწყუ", English = "Chkhorotsku" },
        new { Georgian = "ხაშური", English = "Khashuri" },
        new { Georgian = "წალკა", English = "Tsalka" }
    ];



    public static void SeedData(this ModelBuilder builder)
    {
        builder.SeedCities();
    }

    private static void SeedCities(this ModelBuilder builder)
    {
        var id = 1;
        var valueId = 1;

        foreach (var city in _cities)
        {
            builder.Entity<Translation>().HasData(new
            {
                Id = id
            });

            builder.Entity<TranslationValue>().HasData(new
            {
                Id = valueId++,
                Language = "ka",
                Value = city.Georgian,
                TranslationId = id
            });

            builder.Entity<TranslationValue>().HasData(new
            {
                Id = valueId++,
                Language = "en",
                Value = city.English,
                TranslationId = id
            });

            builder.Entity<City>().HasData(new
            {
                Id = id, 
                NameId = id
            });

            id++;
        }
    }
}
