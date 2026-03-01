
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppGemini.Services;


public partial class SettingsViewModel : ObservableObject
{
    private readonly SettingsService _settings;

    [ObservableProperty]
    private string apiKey = "";

    [ObservableProperty]
    private string model = "gemini-1.5-flash";

    public SettingsViewModel(SettingsService settings)
    {
        _settings = settings;
        ApiKey = _settings.ApiKey;
        Model = _settings.Model;
    }

    [RelayCommand]
    private void Save()
    {
        _settings.ApiKey = ApiKey?.Trim() ?? "";
        _settings.Model = Model?.Trim() ?? "gemini-1.5-flash";
    }
}