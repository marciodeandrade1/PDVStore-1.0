using PDVStore.Forms;
//using PowerArgs;

namespace PDVStore
{
    internal static class Program
    {
        public static object WinFormsApp { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Program.cs (.NET 8 WinForms com Host)
         //   var builder = WinFormsApp. .CreateBuilder(Args);

            // Connection string centralizada
            string conn = @"Server=(localdb)\mssqllocaldb;Database=PDV_LojaDB;Trusted_Connection=True;";

            //builder.Services.AddDbContext<PdvContext>(opt => opt.UseSqlServer(conn));
            //builder.Services.AddTransient<VendaService>();
            //builder.Services.AddTransient<EstoqueService>();
            //builder.Services.AddTransient<RelatorioService>();
            //builder.Services.AddTransient<PagamentoIntegrator>();
            //builder.Services.AddSingleton<DashboardViewModel>();

            //var app = builder.Build();
           // app.Run<frmLogin>();
            Application.Run(new frmLogin());
        }
    }
}