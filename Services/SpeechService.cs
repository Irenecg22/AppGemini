using System;
using System.Collections.Generic;
using System.Text;

namespace AppGemini.Services
{
    public class SpeechService
    {
        CancellationTokenSource? _ttsCts;

        public async Task SpeakAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            _ttsCts?.Cancel();
            _ttsCts = new CancellationTokenSource();

            await TextToSpeech.Default.SpeakAsync(text, cancelToken: _ttsCts.Token);
        }

        public void Stop()
        {
            _ttsCts?.Cancel();
        }
    }
}
