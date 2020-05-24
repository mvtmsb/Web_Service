using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Attractions.InfrastructureServices.Gateways.Database;
using Microsoft.EntityFrameworkCore;
using Attractions.ApplicationServices.GetAttractionListUseCase;
using Attractions.ApplicationServices.Ports.Gateways.Database;
using Attractions.ApplicationServices.Repositories;
using Attractions.DomainObjects.Ports;

namespace Attractions.WebService
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
            services.AddDbContext<AttractionContext>(opts => 
                opts.UseSqlite($"Filename={System.IO.Path.Combine(System.Environment.CurrentDirectory, "Attractions.db")}")
            );

            services.AddScoped<IAttractionDatabaseGateway, AttractionEFSqliteGateway>();

            services.AddScoped<DbAttractionRepository>();
            services.AddScoped<IReadOnlyAttractionRepository>(x => x.GetRequiredService<DbAttractionRepository>());
            services.AddScoped<IAttractionRepository>(x => x.GetRequiredService<DbAttractionRepository>());

            services.AddScoped<IGetAttractionListUseCase, GetAttractionListUseCase>();

            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
