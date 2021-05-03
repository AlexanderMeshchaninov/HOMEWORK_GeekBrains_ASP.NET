using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class DbConnectionSource
    {
        public string DbConnection { get; set; }

        public DbConnectionSource()
        {
            DbConnection = "Data Source =:memory:";
        }
    }
}
