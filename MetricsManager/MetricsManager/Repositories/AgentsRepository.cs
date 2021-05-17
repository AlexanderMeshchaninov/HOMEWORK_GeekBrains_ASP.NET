using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.Models;

namespace MetricsManager.DAL
{
    public interface IAgentsRepository : IAgentsRepository<AgentInfo>
    {
    }

    public class AgentsRepository : IAgentsRepository
    {
        private readonly IDbConnection _dbConnection;

        public AgentsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Create(AgentInfo item)
        {
            using (var connection = new SQLiteConnection(_dbConnection.AddConnectionDb()))
            {
                connection.Execute("INSERT INTO agents(AgentId, AgentAddress) VALUES(@AgentId, @AgentAddress)",
                    new
                    {
                        AgentId = item.AgentId,
                        AgentAddress = item.AgentAddress
                    });
            }
        }

        public List<AgentInfo> Read()
        {
            using (var connection = new SQLiteConnection(_dbConnection.AddConnectionDb()))
            {
                return connection.Query<AgentInfo>("SELECT * FROM agents").ToList();
            }
        }
    }
}