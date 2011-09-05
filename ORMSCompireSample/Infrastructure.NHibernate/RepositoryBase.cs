using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORM.Sample.Common;
using Domain;

namespace Infrastructure.NHibernate
{
    public class RepositoryBase<T>: IRepository<T> where T:class,IEntity
    {
        private Database database;

        protected RepositoryBase(IDatabaseFactory databasFactory)
        {
            DatabaseFactory = databasFactory;
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected Database Database
        {
            get
            {
                return database ?? (database = DatabaseFactory.Get());
            }
        }

        public void Add(T entity)
        {
            Database.Add<T>(entity);
        }

        public void Update(T entity)
        {
            Database.Update<T>(entity);
        }

        public void Delete(T entity)
        {
            Database.Delete<T>(entity);
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public T GetById(long Id)
        {
           return Database.GetById<T>(Id);
        }

        public T GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return Database.CreateQuery<T>();
        }

        public IEnumerable<T> GetMany(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }
    }
}
