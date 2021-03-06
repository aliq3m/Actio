using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Services.Identity.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoDatabase mongoDatabase)
        {
            _database = mongoDatabase;
        }

        public async Task<User> GetAsync(Guid id)
            => await collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email)
            => await collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Email == email.ToLowerInvariant());

        public async Task AddAsync(User user)
            => await collection.InsertOneAsync(user);

        private IMongoCollection<User> collection
            => _database.GetCollection<User>("Users");
    }
}
