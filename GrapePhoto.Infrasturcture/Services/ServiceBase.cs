using GrapePhoto.Infrasturcture.Entities;
using GrapePhoto.Infrasturcture.Repoistory;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Infrasturcture.Services
{

    public abstract class ServiceBase<TEntity> : ServiceBase<TEntity, int> where TEntity : Entity<int>
    {


    }

    public abstract class ServiceBase<TEntity, TPrimaryKey> 
        : IService<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        IRepository<TEntity, TPrimaryKey> repository;

        public ServiceBase()
        {
        }

        protected ServiceBase(IRepository<TEntity, TPrimaryKey> repository)
        {
            this.repository = repository;
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
