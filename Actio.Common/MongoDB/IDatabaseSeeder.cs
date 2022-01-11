using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Actio.Common.MongoDB
{
    public interface IDatabaseSeeder
    {
        Task SeedAsynd();
    }
}
