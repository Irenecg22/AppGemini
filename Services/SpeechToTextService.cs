using CommunityToolkit.Maui.Media;
using System.Globalization;

namespace AppGemini.Services;

public class SpeechToTextService
{
    private readonly ISpeechToText _speechToText;

    public SpeechToTextService(ISpeechToText speechToText)
    {
        _speechToText = speechToText;
    }

    public async Task<string> ListenAsync(CancellationToken ct)
    {
        var granted = await _speechToText.RequestPermissions(ct);
        if (!granted)
            return "";

        var result = await _speechToText.ListenAsync(
            culture: new CultureInfo("es-ES"),
            recognitionResult: null,
            cancellationToken: ct);

        return result?.Text ?? "";
    }
}