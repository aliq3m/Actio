using System;

namespace Actio.Common.Events {
    public class ActivityCreated : IAuthenticatedEvent {
        public Guid Id { get; set; }
        public Guid UserId { get; }
        public string Category { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime CreatedAt { get; }

        protected ActivityCreated () {

        }

        public ActivityCreated (Guid id, Guid userId,
            string categoty, string name,
            string description, DateTime createAt
        ) {
            this.Id = id;
            this.Category = categoty;
            this.CreatedAt = createAt;
            this.Description = description;
            this.UserId = userId;
            this.Name = name;
        }
    }
}