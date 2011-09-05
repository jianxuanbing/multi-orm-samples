using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using global::NHibernate.Linq;
using ICriteria = global::NHibernate.ICriteria;
using ISession = global::NHibernate.ISession;
using ITransaction = global::NHibernate.ITransaction;

using Extensions;
using ORM.Sample.Common;
using Domain;

namespace Infrastructure.NHibernate
{
    public class Database:Disposable
    {
        private readonly ISession session;
        private readonly ICollection<IEntity> transientEntities;

        private ITransaction transaction;

        private IQueryable<Person> people;

        public Database(ISession session)
        {
            this.session = session;
            transientEntities = new List<IEntity>();
        }

        public IQueryable<Person> People
        {
            get
            {
                return people ?? (people = CreateQuery<Person>());
            }
        }

        public virtual IQueryable<T> CreateQuery<T>() where T:class,IEntity
        {
            return session.Linq<T>();
        }

        public virtual ICriteria CreateCriteria<T>() where T : class, IEntity
        {
            return session.CreateCriteria<T>();
        }

        public virtual T GetById<T>(long id) where T:class,IEntity
        {
            return session.Get<T>(id);
        }

        public virtual void Add<T>(T entity) where T:class,IEntity
        {
            try
            {
                EnsureTransaction();
                transientEntities.Add(entity);
            }
            catch
            {
                transaction.Rollback();
                transientEntities.Clear();
                throw;
            }
        }

        public virtual void Update<T>(T entity) where T:class,IEntity
        {
            try
            {
                EnsureTransaction();
                session.Update(entity);
            }
            catch
            {
                transaction.Rollback();
                transientEntities.Clear();
            }
        }

        public virtual void Delete<T>(T entity) where T:class,IEntity
        {
            try
            {
                EnsureTransaction();
                var item = this.GetById<T>(entity.Id);
                session.Delete(item);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public virtual void Commit()
        {
            EnsureTransaction();
            try
            {
                transientEntities.Each(e => session.Save(e));
                transaction.Commit();
                transientEntities.Clear();
            }
            catch
            {
                transaction.Rollback();
                transientEntities.Clear();
                throw;
            }
        }

        protected override void DisposeCore()
        {
            transientEntities.Clear();
            if (transaction != null)
            {
                if (transaction.IsActive)
                {
                    transaction.Rollback();
                }

                transaction.Dispose();
                transaction = null;
            }

            if (session != null)
            {
                if (session.IsOpen)
                {
                    session.Close();
                }

                session.Dispose();
            }
        }

        private void EnsureTransaction()
        {
            if (transaction == null || !transaction.IsActive || transaction.WasCommitted || transaction.WasRolledBack)
            {
                transaction = session.BeginTransaction();
            }
        }

    }
}
