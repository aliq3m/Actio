using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Api.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;
        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Activity activity)
        {
            await Collection.InsertOneAsync(activity);
        }

        public async Task<IEnumerable<Activity>> BrowseAsync(Guid userId)
        {
            return await Collection.AsQueryable()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<Activity> GetAsync(Guid Id)
        {
            return await Collection.AsQueryable()
                  .FirstOrDefaultAsync(x => x.Id == Id);
        }


        private IMongoCollection<Activity> Collection =>
            _database.GetCollection<Activity>("Activities");
    }
}
