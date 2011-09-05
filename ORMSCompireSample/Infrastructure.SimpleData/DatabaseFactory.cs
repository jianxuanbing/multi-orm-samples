using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORM.Sample.Common;
using Extensions;
using Domain;

namespace Infrastructure.SimpleData
{
    public class DatabaseFactory:Disposable,IDatabaseFactory
    {
        private Database database;

        public Database Get()
        {
            if (database == null)
            {
                database = new Database();
            }
            return database;
        }


        protected override void DisposeCore()
        {
            if (database != null)
                database.Dispose();
        }
    }
}
