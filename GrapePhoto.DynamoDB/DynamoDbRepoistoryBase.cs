using GrapePhoto.Infrasturcture.Entities;
using GrapePhoto.Infrasturcture.Repoistory;
using System;
using System.Linq;

namespace GrapePhoto.DynamoDB
{

    public abstract class DynamoDBRepoistoryBase<TEntity> : DynamoDBRepoistoryBase<TEntity, int>
       where TEntity : class, IEntity<int>
    {
        public DynamoDBRepoistoryBase()
        {
        }
    }
    public abstract class DynamoDBRepoistoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey> 
        where TEntity : class, IEntity<TPrimaryKey>
    {
    }
}
