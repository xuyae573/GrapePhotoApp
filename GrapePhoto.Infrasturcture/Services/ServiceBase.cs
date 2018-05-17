using GrapePhoto.Infrasturcture.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Infrasturcture.Services
{
    public abstract class ServiceBase<TEntity> : IService<TEntity> where TEntity : Entity
    {
        public ServiceBase()
        {
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
