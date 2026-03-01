namespace AppGemini.Views;

public partial class SettingsView : ContentPage
{
    public SettingsView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext == null)
        {
            BindingContext = App.Current?.Handler?.MauiContext?.Services
                ?.GetRequiredService<SettingsViewModel>();
        }
    }
}