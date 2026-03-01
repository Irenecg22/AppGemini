using System;
using System.Collections.Generic;
using System.Text;

namespace AppGemini.Services
{
    public class SettingsService
    {
        private const string ApiKeyKey = "gemini_api_key";
        private const string ModelKey = "gemini_model";

        public string ApiKey
        {
            get => Preferences.Get(ApiKeyKey, string.Empty);
            set => Preferences.Set(ApiKeyKey, value ?? string.Empty);
        }

        public string Model
        {
            get => Preferences.Get(ModelKey, "gemini-1.5-flash");
            set => Preferences.Set(ModelKey, value ?? "gemini-1.5-flash");
        }
    }
}
