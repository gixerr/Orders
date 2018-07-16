using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Orders.Core.Domain;
using Orders.Core.Repositories;

namespace Orders.Infrastructure.Repositories
{
    public class MongoItemRepository : IItemRepository, IMongoRepository
    {
        private readonly IMongoDatabase _mongoDb;
        public MongoItemRepository(IMongoDatabase mongoDb)
        {
            _mongoDb = mongoDb;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
            => await Items.AsQueryable().ToListAsync();

        public async Task<Item> GetAsync(Guid id)
            => await Items.AsQueryable().FirstOrDefaultAsync(i => i.Id == id);

        public async Task<IEnumerable<Item>> GetAsync(string name)
            => await Items.AsQueryable().Where(i 
                => string.Equals(i.Name, name, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();

        public async Task AddAsync(Item item)
            => await Items.InsertOneAsync(item);

        public async Task UpdateAsync(Item item)
            => await Items.ReplaceOneAsync(i => i.Id == item.Id, item);

        public async Task RemoveAsync(Guid id)
            => await Items.DeleteOneAsync(i => i.Id == id);

        private IMongoCollection<Item> Items => _mongoDb.GetCollection<Item>("Items");
    }
}