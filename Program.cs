using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PDVStore.Data;
using PDVStore.Forms;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            using IHost host = CreateHostBuilder(Array.Empty<string>()).Build();

            // Resolve the initial form from DI and run the app
            var svc = host.Services;
            var loginForm = svc.GetRequiredService<frmLogin>();
            Application.Run(loginForm);

            // Dispose host when application exits
        }

        // Exposed so EF Core tools can build the application's host and resolve services at design time
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // Add DbContext and forms
                    // Use the same ConnectionHelper used by the design-time factory to keep
                    // runtime and design-time connection strings consistent.
                    services.AddDbContext<PDVContext>(options =>
                        options
                            .UseSqlServer(Helpers.ConnectionHelper.GetConnectionString())
                            // Suppress the PendingModelChangesWarning if any dynamic seed values remain.
                            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
                    );

                    services.AddSingleton<frmLogin>();
                    services.AddTransient<frmCadastroUsuario>();
                    services.AddTransient<frmGerenciarUsuarios>();
                    services.AddSingleton<frmMenuPrincipal>();
                });
    }
}
