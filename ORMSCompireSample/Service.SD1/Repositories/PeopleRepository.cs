using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.SimpleData;
using Domain;
using ORM.Sample.Common;

namespace Service.SD1.Repositories
{
    public class PeopleRepository:RepositoryBase<Person>,IPeopleRepository
    {
        public PeopleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IPeopleRepository : IRepository<Person>
    {

    }
}
