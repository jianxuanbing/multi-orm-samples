using System.Data.EntityClient;
using System.Data.Entity;
using System.Diagnostics;

namespace Infrastructure.EF4
{
    public class DbSetInitializer:DropCreateDatabaseIfModelChanges<Database>
    {
        protected override void Seed(Database context)
        {
            base.Seed(context);
        }
    }
}
