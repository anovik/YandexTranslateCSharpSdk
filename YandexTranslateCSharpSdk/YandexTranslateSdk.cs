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

        private readonly Dictionary<string, string> supportedLanguages = 
            new Dictionary<string, string>()
        {
            { "az", "Azerbaijan"},
            { "sq", "Albanian"},
            { "am", "Amharic"},
            { "en", "English"},
            { "ar", "Arabic"},
            { "hy", "Armenian"},
            { "af", "Afrikaans"},
            { "eu", "Basque"},
            { "ba", "Bashkir"},
            { "be", "Belarusian"},
            { "bn", "Bengali"},
            { "my", "Burmese"},
            { "bg", "Bulgarian"},
            { "bs", "Bosnian"},
            { "cy", "Welsh"},
            { "hu", "Hungarian"},
            { "vi", "Vietnamese"},
            { "ht", "Haitian (Creole)"},
            { "gl", "Galician"},
            { "nl", "Dutch"},
            { "mrj", "Hill Mari"},
            { "el", "Greek"},
            { "ka", "Georgian"},
            { "gu", "Gujarati"},
            { "da", "Danish"},
            { "he", "Hebrew"},
            { "yi", "Yiddish"},
            { "id", "Indonesian"},
            { "ga", "Irish"},
            { "it", "Italian"},
            { "is", "Icelandic"},
            { "es", "Spanish"},
            { "kk", "Kazakh"},
            { "kn", "Kannada"},
            { "ca", "Catalan"},
            { "ky", "Kyrgyz"},
            { "zh", "Chinese"},
            { "ko", "Korean"},
            { "xh", "Xhosa"},
            { "km", "Khmer"},
            { "lo", "Laotian"},
            { "la", "Latin"},
            { "lv", "Latvian"},
            { "lt", "Lithuanian"},
            { "lb", "Luxembourgish"},
            { "mg", "Malagasy"},
            { "ms", "Malay"},
            { "ml", "Malayalam"},
            { "mt", "Maltese"},
            { "mk", "Macedonian"},
            { "mi", "Maori"},
            { "mr", "Marathi"},
            { "mhr", "Mari"},
            { "mn", "Mongolian"},
            { "de", "German"},
            { "ne", "Nepali"},
            { "no", "Norwegian"},
            { "pa", "Punjabi"},
            { "pap", "Papiamento"},
            { "fa", "Persian"},
            { "pl", "Polish"},
            { "pt", "Portuguese"},
            { "ro", "Romanian"},
            { "ru", "Russian"},
            { "ceb", "Cebuano"},
            { "sr", "Serbian"},
            { "si", "Sinhala"},
            { "sk", "Slovakian"},
            { "sl", "Slovenian"},
            { "sw", "Swahili"},
            { "su", "Sundanese"},
            { "tg", "Tajik"},
            { "th", "Thai"},
            { "tl", "Tagalog"},
            { "ta", "Tamil"},
            { "tt", "Tatar"},
            { "te", "Telugu"},
            { "tr", "Turkish"},
            { "udm", "Udmurt"},
            { "uz", "Uzbek"},
            { "uk", "Ukrainian"},
            { "ur", "Urdu"},
            { "fi", "Finnish"},
            { "fr", "French"},
            { "hi", "Hindi"},
            { "hr", "Croatian"},
            { "cs", "Czech"},
            { "sv", "Swedish"},
            { "gd", "Scottish"},
            { "et", "Estonian"},
            { "eo", "Esperanto"},
            { "jv", "Javanese"},
            { "ja", "Japanese"},
            { "cv", "Chuvash"},
            { "sah", "Yakut"}
        };

        /// <summary>
        /// API key. You need to set it before calling methods of SDK 
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// JSON or XML 
        /// </summary>
        public bool IsJson { get; set; }

        /// <summary>
        /// Automatically detect language by piece of text        
        /// </summary>
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

        /// <summary>
        /// Get all supported languages (both codes and descriptions)
        /// </summary>
        public async Task<Dictionary<string,string>> GetLanguagesAsync()
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new YandexTranslateException("Empty API Key");
            }
            languagesManager.ApiKey = ApiKey;
            var languageCodes = new List<string>();
            var languages = new Dictionary<string, string>();
            if (IsJson)
            {
                languageCodes = await languagesManager.GetLanguagesJsonAsync();
            }
            else
            {
                languageCodes = await languagesManager.GetLanguagesXmlAsync();
            }
            foreach(var code in languageCodes)
            {
                if (supportedLanguages.ContainsKey(code))
                {
                    languages.Add(code, supportedLanguages[code]);
                }
                else
                {
                    languages.Add(code, "");
                }
            }
            return languages;
        }

        /// <summary>
        /// Translate given text in the given direction
        /// </summary>
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
