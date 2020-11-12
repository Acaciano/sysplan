using Sysplan.Crosscutting.Domain.Interfaces.Repositories;
using Sysplan.Crosscutting.Domain.Model;
using Sysplan.Crosscutting.Infrastructure.Contexts.MongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sysplan.Crosscutting.Infrastructure.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        protected readonly IEventStoreContext _context;
        protected readonly IMongoCollection<EventStore> _collection;

        public EventStoreRepository(IEventStoreContext context)
        {
            _context = context;
            _collection = _context.GetCollection<EventStore>("EventStore");
        }

        public virtual async Task<List<EventStore>> GetAllAsync(
            Expression<Func<EventStore, bool>> filter = null,
            Func<Expression<Func<EventStore, bool>>, List<EventStore>> whenNoExists = null,
            ProjectionDefinition<EventStore, EventStore> projection = null)
        {
            var cursor = filter == null ? _collection.Find(t => true) : _collection.Find(filter);

            if (projection != null)
            {
                cursor = cursor.Project(projection);
            }

            var result = await cursor.ToListAsync();

            if (!result.Any() && whenNoExists != null) return whenNoExists(filter);

            return result;
        }

        public async Task<EventStore> GetOneAsync(
            Expression<Func<EventStore, bool>> filter = null,
            Func<Expression<Func<EventStore, bool>>, EventStore> whenNoExists = null)
        {
            var cursor = await _collection.FindAsync(filter);

            var result = await cursor.FirstOrDefaultAsync();

            if (result == null && whenNoExists != null) return whenNoExists(filter);

            return result;
        }

        public virtual Task AddAsync(EventStore newEntity)
        {
            return _collection
                .InsertOneAsync(newEntity);
        }

        public virtual Task UpdateAsync(string key, EventStore newEntity) => UpdateAsync(Guid.Parse(key), newEntity);

        public virtual Task UpdateAsync(Guid key, EventStore newEntity)
        {
            return _collection
                .ReplaceOneAsync(
                    t => t.Id == key,
                    newEntity,
                    new UpdateOptions { IsUpsert = true }
                );
        }

        public virtual Task RemoveAsync(string key) => RemoveAsync(Guid.Parse(key));

        public virtual Task RemoveAsync(Guid key)
        {
            return _collection
                .DeleteOneAsync(t => t.Id == key);
        }

        public virtual Task<EventStore> GetByIdAsync(Guid id) => GetOneAsync(t => t.Id == id);

        public virtual async Task<bool> ExistsByExpressionAsync(Expression<Func<EventStore, bool>> expression)
        {
            var reg = await GetOneAsync(expression);

            return reg != null;
        }

        public virtual Task<IClientSessionHandle> StartTransactionAsync()
        {
            return _context.Client.StartSessionAsync();
        }
    }
}