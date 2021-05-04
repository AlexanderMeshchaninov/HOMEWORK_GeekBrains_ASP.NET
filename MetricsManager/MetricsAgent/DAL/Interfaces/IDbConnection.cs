using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public interface IDbConnection
    {
        string AddConnectionDb(int poolSize = 100, bool pooling = true);
    }
}
