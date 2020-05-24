using Microsoft.Extensions.DependencyInjection;
using Attractions.ApplicationServices.GetAttractionListUseCase;
using Attractions.ApplicationServices.Ports.Cache;
using Attractions.ApplicationServices.Repositories;
using Attractions.DesktopClient.InfrastructureServices.ViewModels;
using Attractions.DomainObjects;
using Attractions.DomainObjects.Ports;
using Attractions.InfrastructureServices.Cache;
using Attractions.InfrastructureServices.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Attractions.DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDomainObjectsCache<Attraction>, DomainObjectsMemoryCache<Attraction>>();
            services.AddSingleton<NetworkAttractionRepository>(
                x => new NetworkAttractionRepository("localhost", 80, useTls: false, x.GetRequiredService<IDomainObjectsCache<Attraction>>())
            );
            services.AddSingleton<CachedReadOnlyAttractionRepository>(
                x => new CachedReadOnlyAttractionRepository(
                    x.GetRequiredService<NetworkAttractionRepository>(), 
                    x.GetRequiredService<IDomainObjectsCache<Attraction>>()
                )
            );
            services.AddSingleton<IReadOnlyAttractionRepository>(x => x.GetRequiredService<CachedReadOnlyAttractionRepository>());
            services.AddSingleton<IGetAttractionListUseCase, GetAttractionListUseCase>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs args)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
