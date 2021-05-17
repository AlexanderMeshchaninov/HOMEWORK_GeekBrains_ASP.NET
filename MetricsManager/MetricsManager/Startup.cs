using System;
using AutoMapper;
using FluentMigrator.Runner;
using MetricsManager.Client;
using MetricsManager.DAL;
using MetricsManager.Factory;
using MetricsManager.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsManager
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
            services.AddSingleton<IAgentsRepository, AgentsRepository>();
            services.AddSingleton<ICpuMetricsAgentsRepository, CpuMetricsAgentsRepository>();
            services.AddSingleton<IDotNetMetricsAgentsRepository, DotNetMetricsAgentsRepository>();
            services.AddSingleton<IHddMetricsAgentsRepository, HddMetricsAgentsRepository>();
            services.AddSingleton<INetworkMetricsAgentsRepository, NetworkMetricsAgentsRepository>();
            services.AddSingleton<IRamMetricsAgentsRepository, RamMetricsAgentsRepository>();
            services.AddSingleton<IDbConnection, DbConnectionSource>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();

            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p
                    .WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            //cpu
            services.AddSingleton<CpuMetricsJobs>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(CpuMetricsJobs),
                cronExpression: "0/5 * * * * ?")); //запуск каждые 5 сек.
            //dotnet
            services.AddSingleton<DotNetMetricsJobs>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DotNetMetricsJobs),
                cronExpression: "0/5 * * * * ?")); //запуск каждые 5 сек.
            //hdd
            services.AddSingleton<HddMetricsJobs>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HddMetricsJobs),
                cronExpression: "0/5 * * * * ?")); //запуск каждые 5 сек.
            //network
            services.AddSingleton<NetworkMetricsJobs>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(NetworkMetricsJobs),
                cronExpression: "0/5 * * * * ?")); //запуск каждые 5 сек.
            //ram
            services.AddSingleton<RamMetricsJobs>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RamMetricsJobs),
                cronExpression: "0/5 * * * * ?")); //запуск каждые 5 сек.

            var mapperConfiguration = new MapperConfiguration(mp => mp
                .AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            var dbConnectionSource = new DbConnectionSource();
            var connection = dbConnectionSource.AddConnectionDb(100, true);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // добавляем поддержку SQLite 
                    .AddSQLite()
                    // устанавливаем строку подключения
                    .WithGlobalConnectionString(connection)
                    // подсказываем где искать классы с миграциями
                    .ScanIn(typeof(Startup).Assembly).For.Migrations())
                    .AddLogging(lb => lb
                    .AddFluentMigratorConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();
        }
    }
}
