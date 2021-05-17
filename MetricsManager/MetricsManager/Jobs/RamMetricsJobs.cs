using System;
using MetricsManager.Client;
using MetricsManager.DAL;
using Quartz;
using System.Threading.Tasks;
using MetricsManager.Models;
using MetricsManager.Requests;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class RamMetricsJobs : IJob
    {
        public DateTimeOffset LastDateFromDb { get; set; }

        private readonly IRamMetricsAgentsRepository _ramMetricsAgentsRepository;

        private readonly IMetricsAgentClient _agentClient;

        private readonly IAgentsRepository _agentsRepository;

        private readonly ILogger<RamMetricsJobs> _logger;

        public RamMetricsJobs(
            IRamMetricsAgentsRepository ramMetricsAgentsRepository,
            IMetricsAgentClient agentClient,
            IAgentsRepository agentsRepository,
            ILogger<RamMetricsJobs> logger)
        {
            _ramMetricsAgentsRepository = ramMetricsAgentsRepository;

            _agentClient = agentClient;

            _agentsRepository = agentsRepository;

            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            //считываем адреса зарегистрированных агентов с бд менеджера
            var agents = _agentsRepository.Read();

            var lastDateFromDb = _ramMetricsAgentsRepository
                .GetByTimePeriod(DateTimeOffset.MinValue, DateTimeOffset.MaxValue);

            foreach (var lastDate in lastDateFromDb)
            {
                if (lastDate.Time <= DateTimeOffset.UtcNow)
                {
                    LastDateFromDb = lastDate.Time;
                }
            }

            if (agents != null)
            {
                foreach (var agent in agents)
                {
                    try
                    {
                        //cоздание запроса в Metrics Agent
                        var response = _agentClient.GetRamMetrics(new RamMetricsApiRequest
                        {
                            ClientBaseAddress = agent.AgentAddress,

                            FromTime = LastDateFromDb,

                            ToTime = DateTimeOffset.UtcNow,

                        }).Metrics;

                        foreach (var metrics in response)
                        {
                            //получение метрик и записывание в бд Metrics Manager
                            _ramMetricsAgentsRepository.Create(new RamMetrics()
                            {
                                //отметка о том, что ID именно запрошенного Агента
                                AgentId = agent.AgentId,
                                //Остальное получаю от Агента
                                Value = metrics.Value,
                                Time = metrics.Time,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                }

                return Task.CompletedTask;
            }

            return Task.Delay(1000);
        }
    }
}