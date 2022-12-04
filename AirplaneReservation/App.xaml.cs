using AirplaneReservation.Database;
using AirplaneReservation.Factories;
using AirplaneReservation.Models;
using AirplaneReservation.Providers;
using AirplaneReservation.Services;
using AirplaneReservation.Services.Interfaces;
using AirplaneReservation.Stores;
using AirplaneReservation.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Windows;

namespace AirplaneReservation
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        var databasePath = Path.Combine(_host.Services.GetRequiredService<IHostingEnvironment>().ContentRootPath, "database.sqlite3");
                        options.UseSqlite(string.Concat("Data Source=", databasePath));
                    });

                    ConfigureServices(services);
                })
                .Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //stores
            services.AddSingleton<NavigationStore>();

            //factories 
            services.AddTransient<IReservationFactory, ReservationFactory>();

            //services
            services.AddSingleton<IDatabaseAccessService, DatabaseAccessService>();

            //viewmodels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<TimetableViewModel>(tt =>
                new TimetableViewModel(TimetableToPassengerAmountNavigation()));

            services.AddTransient<PassengerAmountViewModel>(pa => 
                new PassengerAmountViewModel(
                    TimetableNavigation(), 
                    PassengerAmountToReservationNavigation()));

            services.AddTransient<ReservationViewModel>(r =>
                new ReservationViewModel(
                    TimetableNavigation(), 
                    ReservationToConfirmationNavigation(),
                    // WARNING: Always use GetRequiredService with dependency injection to prevent Null Services
                    _host.Services.GetRequiredService<IReservationFactory>(),
                    _host.Services.GetRequiredService<IDatabaseAccessService>(),
                    _host.Services.GetRequiredService<IDatabaseProvider>()));

            services.AddTransient<ConfirmationViewModel>(c =>
                new ConfirmationViewModel(TimetableNavigation()));

            //MainWindow
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
                (parameters) =>
                {       
                        // WARNING: Navigation parameter always has to be converted to their type
                        // reason: parameters are passed as an object for universal use
                        Tuple<Flight, int> tupleParameters = parameters as Tuple<Flight, int>;

                        var reservationViewModel = _host.Services.GetRequiredService<ReservationViewModel>();
                        reservationViewModel.SelectedFlight = tupleParameters.Item1;
                        reservationViewModel.AmountOfPassengers = tupleParameters.Item2;
                        return reservationViewModel;
                    });
        }

        private INavigationService ReservationToConfirmationNavigation()
        {
            return new NavigationService(_host.Services.GetRequiredService<NavigationStore>(),
                () => _host.Services.GetRequiredService<ConfirmationViewModel>());
        }
    }
}
