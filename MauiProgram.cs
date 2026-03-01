using AppGemini.Services;
using AppGemini.ViewModels;
using AppGemini.Views;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Media;
using Microsoft.Extensions.Logging;


namespace AppGemini;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<SettingsService>();
        builder.Services.AddSingleton<GeminiService>();
        builder.Services.AddSingleton<SpeechService>();
        builder.Services.AddSingleton<ISpeechToText>(SpeechToText.Default);
        builder.Services.AddSingleton<SpeechToTextService>();
        builder.Services.AddSingleton<ChatViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<ChatView>();
        builder.Services.AddSingleton<SettingsView>();

        return builder.Build();
    }
}