using System.Data.EntityClient;
using System.Data.Entity;
using System.Diagnostics;
using Infrastructure.EF4.Configuration;

using Domain;

namespace Infrastructure.EF4
{
    public class Database:DbContext
    {
        private DbSet<Person> people;

        public Database()
            : base("Database")
        { 
          //Do something here
        }


        public DbSet<Person> People
        {
            [DebuggerStepThrough]
            get
            {
                return people ?? (people = Set<Person>());
            }
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }



}
