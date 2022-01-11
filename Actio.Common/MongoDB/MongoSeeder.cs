using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Common.MongoDB
{
   public class MongoSeeder: IDatabaseSeeder
    {
        protected readonly IMongoDatabase Database;

        public MongoSeeder(IMongoDatabase database)
        {
            Database = database;
        }
        public async Task SeedAsynd()
        {
            var collectikonsCursor = await Database.ListCollectionsAsync();
            var collections =await collectikonsCursor.ToListAsync();

            if (collections.Any())
            {
                return;
            }

            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}
