using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.EF4;
using Domain;
using ORM.Sample.Common;

namespace Service.EF4.Repositories
{
    public class PeopleRepository:RepositoryBase<Person>,IPeopleRepository
    {
        public PeopleRepository(IDatabaseFactory databaseFactory)
         :base(databaseFactory)
        { 
         
        }
    }

    public interface IPeopleRepository : IRepository<Person>
    { 
    
    }
}
