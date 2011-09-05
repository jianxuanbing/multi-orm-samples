using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.SimpleData
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private Database database;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected Database Database
        {
            get { return database ?? (database = databaseFactory.Get()); }
        }

        public void Commit()
        {
            Database.Commit();
        }
    }
}
