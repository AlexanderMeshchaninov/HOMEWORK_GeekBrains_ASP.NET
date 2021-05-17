using System.Collections.Generic;
using MetricsManager.Models;

namespace MetricsManager.DAL
{
    public interface IAgentsRepository<T> where T : class
    {
        void Create(T item);
        List<T> Read();
    }
}