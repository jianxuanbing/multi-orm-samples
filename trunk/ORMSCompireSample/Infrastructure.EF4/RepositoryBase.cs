using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using ORM.Sample.Common;

namespace Infrastructure.EF4
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private Database database;
        private readonly IDbSet<T> dbset;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = Database.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected Database Database
        {
            get { return database ?? (database = DatabaseFactory.Get()); }
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            database.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }

        public virtual T GetById(long Id)
        {
            return dbset.Find(Id);
        }

        public virtual T GetById(string Id)
        {
            return dbset.Find(Id);
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            var query =dbset.Where<T>(where);
            if (query.Count() > 0)
                return query.First();
            return null;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();         
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();

        }
    }
}
