using AirplaneReservation.Models;
using AirplaneReservation.Services;
using AirplaneReservation.Stores;
using AirplaneReservation.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.DirectoryServices;
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

            //viewmodels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<TimetableViewModel>(tt =>
                new TimetableViewModel(TimetableToPassengerAmountNavigation())
                );
            services.AddSingleton<PassengerAmountViewModel>(pa => 
                new PassengerAmountViewModel(TimetableNavigation(), PassengerAmountToReservationNavigation()));

            //views
            services.AddSingleton<MainWindow>(m => new MainWindow()
            {
                DataContext = m.GetRequiredService<MainViewModel>()
            });
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            INavigationService homeNavigationService = TimetableNavigation();
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

        private INavigationService TimetableNavigation()
        {
            return new NavigationService(_host.Services.GetRequiredService<NavigationStore>(),
                () => _host.Services.GetRequiredService<TimetableViewModel>());
        }

        private IParameterNavigationService TimetableToPassengerAmountNavigation()
        {
            return new ParameterNavigationService(_host.Services.GetRequiredService<NavigationStore>(),
                (parameter) =>
                    {
                        var passengerAmountViewModel = _host.Services.GetRequiredService<PassengerAmountViewModel>();
                        passengerAmountViewModel.SelectedFlight = parameter as Flight;
                        return passengerAmountViewModel;
                    });
        }
        private IParameterNavigationService PassengerAmountToReservationNavigation()
        {
            return new ParameterNavigationService(_host.Services.GetRequiredService<NavigationStore>(),
                (parameter) => 
                    {
                        var reservationViewModel = _host.Services.GetRequiredService<ReservationViewModel>();
                        reservationViewModel.SelectedFlight = parameter as Flight;
                        return reservationViewModel;
                    });
        }
    }
}
