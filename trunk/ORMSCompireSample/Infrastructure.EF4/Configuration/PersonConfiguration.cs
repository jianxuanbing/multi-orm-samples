using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

using Domain;

namespace Infrastructure.EF4.Configuration
{
    public class PersonConfiguration:EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.FirstName).IsUnicode().IsRequired().IsVariableLength().HasMaxLength(256);
            Property(p => p.LastName).IsUnicode().IsRequired().IsVariableLength().HasMaxLength(256);
            Property(p => p.Age);
            ToTable("Person");
        }
    }
}
