using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
    //маскировочный интерфейс
    //необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface INetworkMetricsRepository : IRepository<NetworkMetrics>
    {
    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        //наше соединение с бд
        private SQLiteConnection connection;

        //инжектируем соединение с бд в наш репозиторий через конструктор
        public NetworkMetricsRepository(SQLiteConnection connection)
        {
            this.connection = connection;
        }

        public void Create(NetworkMetrics item)
        {
            //создаем команду
            using var cmd = new SQLiteCommand(connection);

            //прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = "INSERT INTO dotNetmetrics(value, time) VALUES(@value, @time)";

            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);
            cmd.Prepare();

            //выполнение команды
            cmd.ExecuteNonQuery();
        }

        public IList<NetworkMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            //создаем команду
            using var cmd = new SQLiteCommand(connection);

            //прописываем в команду SQL запрос на выдачу данных
            cmd.CommandText = "SELECT * FROM dotnetmetrics WHERE time>=@fromTime AND time<=@toTime";

            cmd.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeMilliseconds());
            cmd.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeMilliseconds());
            cmd.Prepare();

            var returnList = new List<NetworkMetrics>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                //пока есть что читать - читаем
                while (reader.Read())
                {
                    //добавляем объект в список возврата
                    returnList.Add(new NetworkMetrics()
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt64(2),
                    });
                }
            }
            return returnList;
        }
    }
}
