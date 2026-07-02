using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using PDVStore.Data;
using PDVStore.Forms;
using PDVStore.Integrations;
using PDVStore.Services;
using PDVStore.ViewModels;
using PDVStore.Helpers;

namespace PDVStore
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Build a generic host to configure DI and other services and then run the WinForms app.
            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Register EF Core DbContext using centralized connection helper
                    services.AddDbContext<PDVContext>(options =>
                        options.UseSqlServer(ConnectionHelper.GetConnectionString()));

                    // Application services
                    services.AddTransient<VendaService>();
                    services.AddTransient<EstoqueService>();
                    services.AddTransient<RelatorioService>();
                    services.AddTransient<PagamentoIntegrator>();
                    services.AddSingleton<DashboardViewModel>();

                    // Register WinForms so they can receive IServiceProvider in constructors
                    services.AddTransient<frmLogin>();
                    services.AddTransient<frmMenuPrincipal>();
                })
                .Build();

            // Resolve the initial form from DI and run the app
            var svc = host.Services;
            var loginForm = svc.GetRequiredService<frmLogin>();
            Application.Run(loginForm);

            // Dispose host when application exits
        }
    }
}
