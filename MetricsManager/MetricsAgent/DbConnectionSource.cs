using System;
using System.Collections.Generic;

namespace MetricsAgent
{
    public class DbConnectionSource
    {
        public string DbConnection {get; set;}

        public DbConnectionSource()
        {
            DbConnection = "Data Source =:memory:";
        }
    }
}
