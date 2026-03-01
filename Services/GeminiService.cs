using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AppGemini.Services
{
    public class GeminiService
    {
        private readonly HttpClient _http = new();
        private readonly SettingsService _settings;

        public GeminiService(SettingsService settings)
        {
            _settings = settings;
        }

        public async Task<string> GenerateAsync(string prompt, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(_settings.ApiKey))
                throw new InvalidOperationException("Falta la API Key. Ve a Settings.");

            var model = _settings.Model; // ej: "gemini-1.5-flash"
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={_settings.ApiKey}";

            var body = new
            {
                contents = new[]
                {
                new {
                    role = "user",
                    parts = new[] { new { text = prompt } }
                }
            }
            };

            using var resp = await _http.PostAsJsonAsync(url, body, cancellationToken: ct);
            resp.EnsureSuccessStatusCode();

            using var stream = await resp.Content.ReadAsStreamAsync(ct);
            using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: ct);

            // response.candidates[0].content.parts[0].text
            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text ?? "";
        }
    }
}
