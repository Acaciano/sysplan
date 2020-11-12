using Sysplan.Crosscutting.Common.Data;
using Sysplan.Crosscutting.Domain.Interfaces.Repositories;
using Sysplan.Crosscutting.Domain.Model;
using Sysplan.Crosscutting.Infrastructure.Contexts.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sysplan.Crosscutting.Infrastructure.Repositories
{
    public abstract class MongoRepository<TEntity> : IMongoDbRepository<TEntity> where TEntity : Entity
    {
        protected readonly IMongoDbContext _context;
        protected readonly IMongoCollection<TEntity> _collection;
        protected readonly IMongoCollection<BsonDocument> _collectionBson;

        protected MongoRepository(string collectionName, IMongoDbContext context)
        {
            _context = context;
            _collection = _context.GetCollection<TEntity>(collectionName);
            _collectionBson = _context.GetCollection<BsonDocument>(collectionName);
        }

        public virtual async Task<PagedList<TEntity>> GetAllPagedAsync(Restriction restriction, Order order, Page page,
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, PagedList<TEntity>> whenNoExists = null,
            ProjectionDefinition<TEntity, TEntity> projection = null)
        {
            SortDefinition<TEntity> sortDefinition = null;

            sortDefinition = new BsonDocument(string.IsNullOrEmpty(order.Property) ? "Id" : order.Property, order.Crescent ? 1 : -1);

            FindOptions findOptions = new FindOptions();
            findOptions.Collation = new Collation(locale: "en", strength: CollationStrength.Primary, caseFirst: CollationCaseFirst.Off);

            IFindFluent<TEntity, TEntity> cursor = filter == null ?
                _collection.Find(t => true, findOptions).Sort(sortDefinition)
                :
                _collection.Find(filter, findOptions)
                .Sort(sortDefinition);

            if (projection != null)
                cursor = cursor.Project(projection);

            var count = cursor.CountDocumentsAsync();
            var result = cursor.Skip((page.Index - 1) * page.Quantity).Limit(page.Quantity).ToListAsync();

            Task.WaitAll(count, result);

            IQueryable<TEntity> list = result.Result.AsQueryable();

            PagedList<TEntity> pagedResult = new PagedList<TEntity>
            {
                TotalRecords = (int)(await count),
                CurrentPage = page.Index,
                PageSize = page.Quantity
            };

            pagedResult.TotalPages = (int)Math.Ceiling(pagedResult.TotalRecords / (double)page.Quantity);
            pagedResult.Results = list.ToList();

            return pagedResult;
        }

        public virtual async Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, List<TEntity>> whenNoExists = null,
            ProjectionDefinition<TEntity, TEntity> projection = null, Order order = null)
        {
            SortDefinition<TEntity> sortDefinition = null;

            sortDefinition = new BsonDocument(string.IsNullOrEmpty(order?.Property) ? "Id" : order.Property, order?.Crescent == true ? 1 : -1);

            FindOptions findOptions = new FindOptions();
            findOptions.Collation = new Collation(locale: "en", strength: CollationStrength.Primary, caseFirst: CollationCaseFirst.Off);

            var cursor = filter == null ? _collection.Find(t => true, findOptions).Sort(sortDefinition) 
                : _collection.Find(filter, findOptions).Sort(sortDefinition);

            if (projection != null)
            {
                cursor = cursor.Project(projection);
            }

            

            var result = await cursor.ToListAsync();

            if (!result.Any() && whenNoExists != null) return whenNoExists(filter);

            return result;
        }

        public virtual async Task<List<TEntity>> GetAllByCommandAsync(string mongoDbTextCommand)
        {
            var document = new BsonDocument()
            {
                 { "eval",  mongoDbTextCommand }
            };

            var command = new BsonDocumentCommand<BsonDocument>(document);
            var response = await Task.Run(() => _context.Database.RunCommand(command));

            return response.ToList() as List<TEntity>;
        }

        public async Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, TEntity> whenNoExists = null)
        {
            var cursor = await _collection.FindAsync(filter);

            var result = await cursor.FirstOrDefaultAsync();

            if (result == null && whenNoExists != null) return whenNoExists(filter);

            return result;
        }

        public async Task<TEntity> GetLastOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, TEntity> whenNoExists = null)
        {
            var sort = Builders<TEntity>.Sort.Descending(x => x.DataCriacao);
            var retorn = await _collection.FindAsync(filter);
            if (retorn.FirstOrDefault() != null)
            {
                var result = await _collection.Find(filter).Sort(sort).FirstAsync();
                if (result == null && whenNoExists != null) return whenNoExists(filter);

                return result;
            }
            return null;
        }

        public virtual Task AddAsync(TEntity newEntity)
        {
            return _collection.InsertOneAsync(newEntity);
        }

        public virtual Task UpdateAsync(string key, TEntity newEntity) => UpdateAsync(Guid.Parse(key), newEntity);

        public virtual Task UpdateAsync(Guid key, TEntity newEntity)
        {
            return _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Where(x => x.Id == key), newEntity);
        }

        public virtual Task RemoveAsync(string key) => RemoveAsync(Guid.Parse(key));

        public virtual Task RemoveAsync(Guid key)
        {
            return _collection.DeleteOneAsync(Builders<TEntity>.Filter.Where(x => x.Id == key));
        }

        public virtual Task<TEntity> GetByIdAsync(Guid id) => GetOneAsync(t => t.Id == id);

        public virtual async Task<bool> ExistsByExpressionAsync(Expression<Func<TEntity, bool>> expression)
        {
            var reg = await GetOneAsync(expression);

            return reg != null;
        }

        public virtual async Task<long> CountAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, List<TEntity>> whenNoExists = null,
            ProjectionDefinition<TEntity, TEntity> projection = null)
        {
            var cursor = filter == null ? _collection.Find(t => true) : _collection.Find(filter);

            if (projection != null)
            {
                cursor = cursor.Project(projection);
            }

            return await cursor.CountDocumentsAsync();
        }

        public virtual Task<IClientSessionHandle> StartTransactionAsync()
        {
            return _context.Client.StartSessionAsync();
        }

        public virtual Task AddManyAsync(IEnumerable<TEntity> entities)
        {
            return _collection.InsertManyAsync(entities);
        }
    }
}