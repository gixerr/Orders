using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Orders.Core.Domain;
using Orders.Core.Repositories;

namespace Orders.Infrastructure.Repositories
{
    public class MongoCategoryRepository : ICategoryRepository, IMongoRepository
    {
        private readonly IMongoDatabase _mongoDb;
        private IMongoCollection<Category> Categories => _mongoDb.GetCollection<Category>("Categories");

        public MongoCategoryRepository(IMongoDatabase mongoDb)
        {
            _mongoDb = mongoDb;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
            => await Categories.AsQueryable().ToListAsync();


        public async Task<Category> GetAsync(Guid id)
            => await Categories.AsQueryable().SingleOrDefaultAsync(c => c.Id == id);

        public async Task<Category> GetAsync(string name)
            => await Categories.AsQueryable().SingleOrDefaultAsync(c => c.Name.ToLowerInvariant() == name.ToLowerInvariant());
        
        public async Task AddAsync(Category category)
            => await Categories.InsertOneAsync(category);

        public async Task UpdateAsync(Category category)
            => await Categories.ReplaceOneAsync(c => c.Id == category.Id, category);

        public async Task RemoveAsync(Guid id)
            => await Categories.DeleteOneAsync(c => c.Id == id);
    }
}