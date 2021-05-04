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
            DbConnection = @"Data Source=metrics.db;Version=3;Pooling=True;Max Pool Size=100;";
        }
    }
}
