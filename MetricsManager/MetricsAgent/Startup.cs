using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SQLite;
using MetricsAgent.DAL;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        //������������ ������ ����������� � ��
        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            var dbConnectionSource = new DbConnectionSource();
            
            var connection = new SQLiteConnection(dbConnectionSource.DbConnection);
            connection.Open();
            PrepareSchema(connection, "cpumetrics");
            PrepareSchema(connection, "dotnetmetrics");
            PrepareSchema(connection, "hddmetrics");
            PrepareSchema(connection, "networkmetrics");
            PrepareSchema(connection, "rammetrics");
            services.AddSingleton(connection);
        }

        //������ ���� ������
        private void PrepareSchema(SQLiteConnection connection, string tableName)
        {
            using (var command = new SQLiteCommand(connection))
            {
                //������ ����� ����� ������� ��� ����������
                //������� ������� � ��������� ���� ��� ���������� � ���� ������
                command.CommandText = $"DROP TABLE IF EXISTS {tableName}";
                command.ExecuteNonQuery();

                command.CommandText = @$"CREATE TABLE {tableName}(id INTEGER PRIMARY KEY, value INT, time INT64)";
                command.ExecuteNonQuery();

                //������ ������ � ��������� ������� ��
                command.CommandText = $"INSERT INTO {tableName}(value, time) VALUES(49, 1617224400000)";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO {tableName}(value, time) VALUES(55, 1617570000000)";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO {tableName}(value, time) VALUES(101, 1618002000000)";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO {tableName}(value, time) VALUES(18, 1618434000000)";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO {tableName}(value, time) VALUES(94, 1618866000000)";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO {tableName}(value, time) VALUES(94, 1619298000000)";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO {tableName}(value, time) VALUES(94, 1619730000000)";
                command.ExecuteNonQuery();
            }
        }
    }
}
