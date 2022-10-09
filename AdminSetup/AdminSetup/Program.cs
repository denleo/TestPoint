using Autofac;

namespace AdminSetup
{
    internal static class Program
    {
        internal static IContainer? Container;

        [STAThread]
        static void Main()
        {
            Container = DiBuilder.Build();

            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }
    }
}