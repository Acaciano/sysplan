using Sysplan.Crosscutting.Domain.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sysplan.Crosscutting.Domain.Interfaces.Repositories
{
    public interface IEventStoreRepository
    {
        Task<List<EventStore>> GetAllAsync(
            Expression<Func<EventStore, bool>> filter = null,
            Func<Expression<Func<EventStore, bool>>, List<EventStore>> whenNoExists = null,
            ProjectionDefinition<EventStore, EventStore> projection = null);

        Task<EventStore> GetOneAsync(
            Expression<Func<EventStore, bool>> filter = null,
            Func<Expression<Func<EventStore, bool>>, EventStore> whenNoExists = null);

        Task AddAsync(EventStore newEntity);

        Task UpdateAsync(string key, EventStore newEntity);

        Task UpdateAsync(Guid key, EventStore newEntity);

        Task RemoveAsync(string key);

        Task RemoveAsync(Guid key);

        Task<EventStore> GetByIdAsync(Guid id);

        Task<bool> ExistsByExpressionAsync(Expression<Func<EventStore, bool>> expression);

        Task<IClientSessionHandle> StartTransactionAsync();
    }
}