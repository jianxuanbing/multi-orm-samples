using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORM.Sample.Common;

namespace Infrastructure.EF4
{
    public class DatabaseFactory:Disposable,IDatabaseFactory
    {
        private Database database;

        public Database Get()
        {
            return database ?? (database = new Database());
        }

        protected override void DisposeCore()
        {
            if (database != null)
                database.Dispose();
        }
    }
}
