using Sysplan.Crosscutting.Common.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sysplan.Crosscutting.Domain.Interfaces.Repositories
{
    public interface IMongoDbRepository<TEntity> where TEntity : class
    {
        Task<PagedList<TEntity>> GetAllPagedAsync(Restriction restriction, Order order, Page page,
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, PagedList<TEntity>> whenNoExists = null,
            ProjectionDefinition<TEntity, TEntity> projection = null);

        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, List<TEntity>> whenNoExists = null,
            ProjectionDefinition<TEntity, TEntity> projection = null, Order order = null);

        Task<List<TEntity>> GetAllByCommandAsync(string mongoDbTextCommand);

        Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, TEntity> whenNoExists = null);

        Task<TEntity> GetLastOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, TEntity> whenNoExists = null);
        
        Task AddAsync(TEntity newEntity);
        Task AddManyAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(string key, TEntity newEntity);

        Task UpdateAsync(Guid key, TEntity newEntity);

        Task RemoveAsync(string key);

        Task RemoveAsync(Guid key);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<bool> ExistsByExpressionAsync(Expression<Func<TEntity, bool>> expression);

        Task<long> CountAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, List<TEntity>> whenNoExists = null,
            ProjectionDefinition<TEntity, TEntity> projection = null);
    }
}