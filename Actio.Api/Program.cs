using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Services;

namespace Actio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
           ServiceHost.Create<Startup>(args)
               .UseRabbitMq()
               .SubscribeToEvent<ActivityCreated>()
               .SubscribeToEvent<UserCreated>()
               .Build()
               .Run();
        }

       
    }
}
