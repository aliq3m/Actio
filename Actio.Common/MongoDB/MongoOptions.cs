using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actio.Common.MongoDB
{
   public class MongoOptions
    {
        public string ConnectionString { get; set; }

        public string Database { get; set; }

        public bool Seed { get; set; }
    }
}
