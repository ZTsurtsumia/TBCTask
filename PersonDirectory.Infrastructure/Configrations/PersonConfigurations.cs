using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using PersonDirectory.Domain.Persons;

namespace PersonDirectory.Infrastructure.Configrations
{
    public class PersonConfigurations : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("dbo.Persons");

            var mobileNumbersConverter = new ValueConverter<List<MobilePhone>, string>(
                v => JsonConvert.SerializeObject(v ?? new List<MobilePhone>()),
                v => string.IsNullOrEmpty(v) ? new List<MobilePhone>() : JsonConvert.DeserializeObject<List<MobilePhone>>(v) ?? new List<MobilePhone>()
            );

            var connectedPersonsConverter = new ValueConverter<List<ConnectedPerson>, string>(
                v => JsonConvert.SerializeObject(v ?? new List<ConnectedPerson>()),
                v => string.IsNullOrEmpty(v) ? new List<ConnectedPerson>() : JsonConvert.DeserializeObject<List<ConnectedPerson>>(v) ?? new List<ConnectedPerson>()
            );

            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.FirstName, fn =>
            {
                fn.Property(f => f.Value).HasColumnName(nameof(FirstName))
                    .HasMaxLength(200);
            });

            builder.OwnsOne(p => p.LastName, ln =>
            {
                ln.Property(l => l.Value).HasColumnName(nameof(LastName))
                    .HasMaxLength(200);
            });

            builder.OwnsOne(p => p.City, city =>
            {
                city.Property(c => c.Value).HasColumnName(nameof(City))
                    .HasMaxLength(200);
            });

            builder.Property(p => p.MobilePhones)
                .HasConversion(mobileNumbersConverter)
                .IsRequired(false);

            builder.Property(p => p.ConnectedPersons)
                .HasConversion(connectedPersonsConverter)
                .IsRequired(false);

            builder.OwnsOne(p => p.DateOfBirth, dob =>
            {
                dob.Property(d => d.Value).HasColumnName(nameof(DateOfBirth))
                    .HasColumnType("Datetime");
            });

            builder.OwnsOne(p => p.PersonalN, personalN =>
            {
                personalN.Property(pn => pn.Value).HasColumnName(nameof(PersonalN))
                    .HasMaxLength(200);
            });

            builder.OwnsOne(p => p.Picture, picture =>
            {
                picture.Property(p => p.Value).HasColumnName(nameof(Picture))
                    .HasMaxLength(200)
                    .IsRequired(false);

            });
        }
    }
}
