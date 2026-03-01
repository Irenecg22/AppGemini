using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppGemini.Models;
using AppGemini.Services;
using System.Collections.ObjectModel;

namespace AppGemini.ViewModels;

public partial class ChatViewModel : ObservableObject
{
    private readonly GeminiService _gemini;
    private readonly SpeechService _speech;
    private readonly SpeechToTextService _stt;   // ✅ AÑADIR

    private CancellationTokenSource? _requestCts;

    public ObservableCollection<Chat> Messages { get; } = new();

    [ObservableProperty]
    private string inputText = "";

    [ObservableProperty]
    private bool isBusy;

    public IRelayCommand SendCommand { get; }
    public IRelayCommand CancelCommand { get; }
    public IRelayCommand SpeakLastCommand { get; }
    public IRelayCommand ListenCommand { get; } // ✅ AÑADIR

    public ChatViewModel(GeminiService gemini, SpeechService speech, SpeechToTextService stt) // ✅ AÑADIR stt
    {
        _gemini = gemini;
        _speech = speech;
        _stt = stt;

        SendCommand = new AsyncRelayCommand(SendAsync, () => !IsBusy);
        CancelCommand = new RelayCommand(Cancel);
        SpeakLastCommand = new AsyncRelayCommand(SpeakLastAsync);

        ListenCommand = new AsyncRelayCommand(ListenAsync, () => !IsBusy); // ✅ AÑADIR
    }

    private async Task SendAsync()
    {
        var text = InputText?.Trim();
        if (string.IsNullOrWhiteSpace(text)) return;

        Messages.Add(new Chat { Role = "user", Text = text });
        InputText = "";

        _requestCts?.Cancel();
        _requestCts = new CancellationTokenSource();

        try
        {
            IsBusy = true;

            var reply = await _gemini.GenerateAsync(text, _requestCts.Token);

            Messages.Add(new Chat { Role = "model", Text = reply });

            await _speech.SpeakAsync(reply);
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            Messages.Add(new Chat { Role = "model", Text = $"Error: {ex.Message}" });
        }
        finally
        {
            IsBusy = false;
            (SendCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
            (ListenCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged(); // ✅
        }
    }

    private async Task ListenAsync() // ✅ AÑADIR
    {
        _requestCts?.Cancel();
        _requestCts = new CancellationTokenSource();

        try
        {
            IsBusy = true;
            var text = await _stt.ListenAsync(_requestCts.Token);

            if (!string.IsNullOrWhiteSpace(text))
                InputText = text;
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            Messages.Add(new Chat { Role = "model", Text = $"STT error: {ex.Message}" });
        }
        finally
        {
            IsBusy = false;
            (SendCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
            (ListenCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
        }
    }

    private void Cancel()
    {
        _requestCts?.Cancel();
        _speech.Stop();
    }

    private async Task SpeakLastAsync()
    {
        var last = Messages.LastOrDefault(m => m.Role == "model")?.Text ?? "";
        await _speech.SpeakAsync(last);
    }
}