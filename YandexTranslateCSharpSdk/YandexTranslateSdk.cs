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

        public async Task<string> DetectLanguage(string text)
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new YandexTranslateException("Empty API Key");
            }
            detectManager.ApiKey = ApiKey;
            if (IsJson)
            {
                return await detectManager.DetectLanguageJson(text);
            }
            else
            {
                return await detectManager.DetectLanguageXml(text);
            }
        }

        public async Task<List<string>> GetLanguages()
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new YandexTranslateException("Empty API Key");
            }
            languagesManager.ApiKey = ApiKey;
            if (IsJson)
            {
                return await languagesManager.GetLanguagesJson();
            }
            else
            {
                return await languagesManager.GetLanguagesXml();
            }
        }

        public async Task<string> TranslateText(string text, string direction)
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new YandexTranslateException("Empty API Key");
            }
            translateManager.ApiKey = ApiKey;
            if (IsJson)
            {
                return await translateManager.TranslateTextJson(text, direction);
            }
            else
            {
                return await translateManager.TranslateTextXml(text, direction);
            }
        }

    }
}
