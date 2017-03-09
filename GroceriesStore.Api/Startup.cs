using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GroceriesStore.Shared;
using GroceriesStore.Infra.XmlRepository;
using GroceriesStore.Domain.Repositories;
using Microsoft.AspNetCore.Mvc.Formatters;
using GroceriesStore.Domain.Commands.Handlers;

namespace GroceriesStore.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                options.RespectBrowserAcceptHeader = true;
            });

            services.AddCors();

            services.AddTransient<IGroceriesRepository, GroceriesRepository>();
            services.AddTransient<RegisterGroceriesCommandHandler, RegisterGroceriesCommandHandler>();
            services.AddTransient<UpdateGroceriesCommandHandler, UpdateGroceriesCommandHandler>();
            services.AddTransient<DeleteGroceriesCommandHandler, DeleteGroceriesCommandHandler>();
            services.AddTransient<SearchGroceriesCommandHandler, SearchGroceriesCommandHandler>();
            services.AddTransient<GetGroceriesCommandHandler, GetGroceriesCommandHandler>();
            services.AddTransient<UpdateGroceriesPositionCommandHandler, UpdateGroceriesPositionCommandHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseMvc();

            Runtime.GroceriesPath = $"{ env.ContentRootPath}/{Configuration["AppSettings:BasePaths:Groceries"]}";
        }
    }
}