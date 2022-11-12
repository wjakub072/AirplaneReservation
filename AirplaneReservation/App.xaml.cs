using AirplaneReservation.Services;
using AirplaneReservation.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace AirplaneReservation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    //services.AddDbContext<ApplicationDatabaseContext>(options =>
                    //{
                    //    options.UseSqlServer(context.Configuration.GetConnectionString("DatabaseServer"));
                    //});
                    ConfigureServices(services);
                })
                .Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //stores
            services.AddSingleton<NavigationStore>();
            //services.AddSingleton<CustomerStore>();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            INavigationService homeNavigationService = StartupNavigation();
            homeNavigationService.Navigate();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }
            base.OnExit(e);
        }

        private INavigationService StartupNavigation()
        {
            return new NavigationService(_host.Services.GetRequiredService<NavigationStore>(),
                () => _host.Services.GetRequiredService<LoginViewModel>());
        }
    }
}
