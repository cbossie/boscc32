using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetServerless.Controllers;
using DotNetServerless.Filters;
using DotNetServerless.Models;
using DotNetServerless.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace DotNetServerless
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

            // Code for Boston Code Camp
            services.AddTransient<ISensorService, SensorService>();

            // Use plain Stackexchance.Redis
            var redisConfig = Configuration.GetSection("Redis").Get<RedisConfiguration>();
            services.AddSingleton(redisConfig);
            var configOptions = new ConfigurationOptions();
            foreach (var host in redisConfig.Hosts)
            {
                configOptions.EndPoints.Add(host.Host, host.Port);
            }
            var multiplexer = ConnectionMultiplexer.Connect(configOptions);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            // Adding StackExchange.Redis.Extensions
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(multiplexer);

            // Add Instance Information (for demo purposes only)
            services.AddTransient<IInstanceService, InstanceService>();
            services.AddSingleton(new InstanceInformation());
            services.AddScoped<IncrementInstanceCountFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

        }
    }
}
