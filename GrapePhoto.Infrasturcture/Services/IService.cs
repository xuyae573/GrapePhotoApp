using GrapePhoto.Infrasturcture.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Infrasturcture.Services
{
    public interface IService<TEntity> : IService<TEntity,int> where TEntity: Entity
    { 
    }

    public interface IService<TEntity, TPrimaryKey> where TEntity : Entity
    {
        TEntity Get(object id);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetAll();
    }
}
