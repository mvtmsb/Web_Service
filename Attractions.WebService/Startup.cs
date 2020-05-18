using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Attractions.ApplicationServices.GetAttractionListUseCase;
using Attractions.ApplicationServices.Repositories;
using Attractions.DomainObjects.Ports;
using Attractions.DomainObjects;
using System.Collections.Generic;

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
            services.AddScoped<InMemoryAttractionRepository>(x => new InMemoryAttractionRepository(
                new List<Attraction> {
                    new Attraction() 
                    { 
                        Id = 1,
                       NameObject="DiscO", 
                       Location= "Парк Культуры и отдыха «Сокольники»",
                       Admission="срок действия истек, эксплуатация аттракциона не допускается",
                       PeriodA="04.2019-12.2019", 

    },
                    new Attraction()
                    {
                       Id = 2,
                         NameObject="Деревенский поезд",
                       Location= "Парк Культуры и отдыха «Измайловский»",
                       Admission="срок действия истек, эксплуатация аттракциона не допускается",
                       PeriodA="04.2019-12.2019",
                    }

            }));
            services.AddScoped<IReadOnlyAttractionRepository>(x => x.GetRequiredService<InMemoryAttractionRepository>());
            services.AddScoped<IAttractionRepository>(x => x.GetRequiredService<InMemoryAttractionRepository>());

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
