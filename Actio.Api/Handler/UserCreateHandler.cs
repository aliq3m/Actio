using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Events;

namespace Actio.Api.Handler
{
    public class UserCreateHandler:IEventHandler<UserCreated> 
    {
        public Task HandleAsync(UserCreated @event)
        {
           
            Console.WriteLine("hiiiii");
           return Task.CompletedTask;
        }
    }
}
