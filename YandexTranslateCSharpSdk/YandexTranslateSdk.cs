using System.Collections.Generic;
using System.Threading.Tasks;

namespace YandexTranslateCSharpSdk
{
    /// <summary>
    /// Wrapper for Yandex Translate API
    /// https://tech.yandex.com/translate/
    /// </summary>
    public class YandexTranslateSdk
    {
        private DetectLanguageManager detectManager = new DetectLanguageManager();
        private LanguagesManager languagesManager = new LanguagesManager();
        private TranslateManager translateManager = new TranslateManager();

        public string ApiKey { get; set; }

        public bool IsJson { get; set; }

        public async Task<string> DetectLanguageAsync(string text)
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new YandexTranslateException("Empty API Key");
            }
            detectManager.ApiKey = ApiKey;
            if (IsJson)
            {
                return await detectManager.DetectLanguageJsonAsync(text);
            }
            else
            {
                return await detectManager.DetectLanguageXmlAsync(text);
            }
        }

        public async Task<List<string>> GetLanguagesAsync()
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new YandexTranslateException("Empty API Key");
            }
            languagesManager.ApiKey = ApiKey;
            if (IsJson)
            {
                return await languagesManager.GetLanguagesJsonAsync();
            }
            else
            {
                return await languagesManager.GetLanguagesXmlAsync();
            }
        }

        public async Task<string> TranslateTextAsync(string text, string direction)
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new YandexTranslateException("Empty API Key");
            }
            translateManager.ApiKey = ApiKey;
            if (IsJson)
            {
                return await translateManager.TranslateTextJsonAsync(text, direction);
            }
            else
            {
                return await translateManager.TranslateTextXmlAsync(text, direction);
            }
        }

    }
}
