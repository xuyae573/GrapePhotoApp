using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapePhoto.DynamoDB
{

    public abstract class DynamoDBRepoistoryBase<TEntity> : DynamoDBRepoistoryBase<TEntity, int>
       where TEntity : class, IEntity<int>
    {
        public DynamoDBRepoistoryBase(IDynamoDbProvider databaseProvider):base(databaseProvider)
        {
        }
    }
    public abstract class DynamoDBRepoistoryBase<TEntity, TPrimaryKey> : AbpRepositoryBase<TEntity,TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {

        private readonly IDynamoDbProvider _databaseProvider;

        public DynamoDBRepoistoryBase(IDynamoDbProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            List<ScanCondition> conditions = new List<ScanCondition>();
            var allDocs = await _databaseProvider.DBContext.ScanAsync<TEntity>(conditions).GetRemainingAsync();
            return allDocs;
        }

        public override async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _databaseProvider.DBContext.SaveAsync<TEntity>(entity);
            return entity;
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await _databaseProvider.DBContext.SaveAsync<TEntity>(entity);
            return entity;
        }

        public override Task DeleteAsync(TPrimaryKey id)
        {
            return _databaseProvider.DBContext.DeleteAsync(id);
        }

        public override async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            List<ScanCondition> conditions = new List<ScanCondition>();
            conditions.Add(new ScanCondition("Id", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, id));
            var allDocs = await _databaseProvider.DBContext.ScanAsync<TEntity>(conditions).GetRemainingAsync();
            var result = allDocs.FirstOrDefault();
            return result;
        }
    }
}
