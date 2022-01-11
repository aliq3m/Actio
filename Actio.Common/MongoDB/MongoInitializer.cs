using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Actio.Common.MongoDB
{
   public class MongoInitializer:IDatabaseInitializer
   {
       private bool _initialized;
       private readonly bool _seed;
       private readonly IMongoDatabase _database;
       private readonly IDatabaseSeeder _seeder;

       public MongoInitializer( IMongoDatabase database, IOptions<MongoOptions> options,IDatabaseSeeder seeder)
       {
           _database = database;
           _seed = options.Value.Seed;
           _seeder = seeder;
       }
        public async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }

            RegisterConventions();
            _initialized = true;
            if (!_seed)
            {
                return;
            }

            await _seeder.SeedAsynd();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("ActionConventions",new MongoConventions() ,x=>true);
        }
        private class MongoConventions : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
