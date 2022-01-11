using System;
using System.Threading.Tasks;
using Actio.Api.Models;
using Actio.Api.Repositories;
using Actio.Common.Events;

namespace Actio.Api.Handlers
{
    public class ActivityCreateHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _repository;
        public ActivityCreateHandler(IActivityRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(ActivityCreated @event)
        {
            await _repository.AddAsync(new Activity() {
                Id = @event.Id,
                UserId=@event.UserId,
                Category=@event.Category,
                Name=@event.Name,
                CreateAt=@event.CreatedAt,
                Description=@event.Description
            });
            Console.WriteLine($"Activity Created : {@event.Name}");
        }

    }
}