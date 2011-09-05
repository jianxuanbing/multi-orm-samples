using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.NHibernate;
using ORM.Sample.Common;
using Domain;

namespace Service.NH3.Repositories
{
    public class PeopleRepository : RepositoryBase<PersonN>, IPeopleRepository
    {
        public PeopleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IPeopleRepository : IRepository<PersonN>
    {

    }
}
