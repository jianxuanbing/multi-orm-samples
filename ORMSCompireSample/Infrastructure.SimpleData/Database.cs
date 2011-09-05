using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Simple.Data;
using Simple.Data.Ado;
using Simple.Data.SqlServer;

using ORM.Sample.Common;
using Domain;

namespace Infrastructure.SimpleData
{
    public class Database:Disposable
    {
        private  dynamic session;
        
        private  dynamic transaction;

        private IEnumerable<Person> people;

        public Database()
        {
            session = DatabaseHelper.Open();           
        }

        public IEnumerable<Person> People
        {
            get
            {
                return people ?? (people = CreateQuery<Person>());
            }
        }

        public virtual T GetById<T>(long id) where T:class,IEntity
        {
            return session[typeof(T).Name].FindById(id);
        }

        public virtual void Add<T>(T entity) where T:class,IEntity
        {
            try
            {
                EnsureTransaction();
                transaction[typeof(T).Name].Insert(entity);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Can not use transaction when update T ,
        /// This is a bug of Simple.Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public virtual void Update<T>(T entity) where T:class,IEntity
        {
            try
            {
                session[typeof(T).Name].Update(entity);   
            }
            catch
            {                
                throw;
            }
        }

        public virtual void Delete<T>(T entity) where T:class,IEntity
        {
            try
            {
                session[typeof(T).Name].DeleteById(entity.Id);
            }
            catch
            {                
                throw;
            }
        }

        public virtual void Commit()
        {
            EnsureTransaction();
            try
            {
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public virtual IEnumerable<T> CreateQuery<T>() where T:class, IEntity
        {
            return session[typeof(T).Name].All().Cast<T>() as IEnumerable<T>;
        }
        protected override void DisposeCore()
        {
            if (session != null)
            {
               //if(transaction!=null)
               //    transaction.Rollback();              
            }
            base.DisposeCore();
        }

        private void EnsureTransaction()
        {
            if(transaction==null)
            transaction = session.BeginTransaction();
        }
    }
}
