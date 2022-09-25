using Microsoft.Extensions.Configuration;

namespace AdminSetup
{
    internal static class Program
    {
        public static IConfiguration Configuration;

            /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json", false, true);
            Configuration = builder.Build();

            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }
    }
}