using Microsoft.Extensions.DependencyInjection;

namespace AppGemini
{
    public partial class App : Application
    {
        public static IServiceProvider Services =>
       Current?.Handler?.MauiContext?.Services
       ?? throw new InvalidOperationException("Services no disponible todavía");

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}