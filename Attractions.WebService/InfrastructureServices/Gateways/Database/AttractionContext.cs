using Microsoft.EntityFrameworkCore;
using Attractions.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attractions.InfrastructureServices.Gateways.Database
{
    public class AttractionContext : DbContext
    {
        public DbSet<Attraction> Attractions { get; set; }

        public AttractionContext(DbContextOptions options)
            : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            FillTestData(modelBuilder);
        }
        private void FillTestData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Attraction>().HasData(
                new
  {Id=1L,
                    NameObject = "DiscO",
                    Location = "Парк Культуры и отдыха «Сокольники»",
                    Admission = "срок действия истек, эксплуатация аттракциона не допускается",
                    PeriodA = "04.2019-12.2019"

                },
                new
                {Id=2L,
                    NameObject = "Деревенский поезд",
                    Location = "Парк Культуры и отдыха «Измайловский»",
                    Admission = "срок действия истек, эксплуатация аттракциона не допускается",
                    PeriodA = "04.2019-12.2019"
                }
                
            );
        }
    }
}
