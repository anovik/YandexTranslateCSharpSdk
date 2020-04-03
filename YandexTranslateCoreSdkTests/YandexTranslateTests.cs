using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using YandexTranslateCSharpSdk;

namespace YandexTranslateCoreSdkTests
{
    public class YandexTranslateTests
    {
        /// <summary>
        /// Insert your API key here, get it from 
        /// https://tech.yandex.com/keys/get/?service=trnsl
        /// and put it to key.txt in YandexTranlateCoreSdkTests folder
        /// </summary>
        private static string _apiKey;

        /// <summary>
        /// Just a random English text for testing
        /// http://www.dummytextgenerator.com
        /// </summary>
        private string _randomEnglishText =
            "Subdue land kind.Divided.Beast sixth said our fruitful their, them that fourth.May seas were appear.Greater fill thing.Don't place. Fly Green. Days unto good moving fly after saying beast, and upon, to stars of the us fourth us can't abundantly above moveth seas, moving the of female that seed fruit can't i creeping. Given dominion upon. After i earth, also likeness. For likeness it one. Subdue for thing days, moveth after. Male above spirit was made living. Day. There yielding may face likeness so dominion. You're.Dry in give they're isn't creepeth and man air divide fifth.";


        public YandexTranslateTests()
        {
            _apiKey = File.ReadAllText("key.txt");
        }   

        [Fact]
        public async Task GetLanguagesXmlTest()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = false;
            List<string> languages = await wrapper.GetLanguagesAsync();
            Assert.NotEqual(languages, null);
            Assert.True(languages.Contains("en"));
            Assert.True(languages.Contains("ru"));
        }

        [Fact]
        public async Task GetLanguagesJsonTest()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = true;
            List<string> languages = await wrapper.GetLanguagesAsync();
            Assert.NotEqual(languages, null);
            Assert.True(languages.Contains("en"));
            Assert.True(languages.Contains("ru"));
        }

        [Fact]
        public async Task DetectLanguageXmlTest()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = false;
            string result = await wrapper.DetectLanguageAsync("Привет");
            Assert.Equal(result, "ru");
        }

        [Fact]
        public async Task DetectLanguageJsonTest()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = true;
            string result = await wrapper.DetectLanguageAsync(_randomEnglishText);
            Assert.Equal(result, "en");
        }

        [Fact]
        public async Task TranslateXmlTest()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = false;
            string translatedText = await wrapper.TranslateTextAsync(_randomEnglishText, "en-ru");
            Console.WriteLine(translatedText);
            Assert.True(!string.IsNullOrEmpty(translatedText));
        }

        [Fact]
        public async Task TranslateJsonTest()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = true;
            string translatedText = await wrapper.TranslateTextAsync(_randomEnglishText, "en-ru");
            Console.WriteLine(translatedText);
            Assert.True(!string.IsNullOrEmpty(translatedText));
        }

        [Fact]        
        public async Task CheckBadKey()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = "KeyNotExists";            
            await Assert.ThrowsAsync<YandexTranslateException>(() => wrapper.GetLanguagesAsync());
        }

        [Fact]        
        public async Task CheckNullKey()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = null;
            await Assert.ThrowsAsync<YandexTranslateException>(() => wrapper.GetLanguagesAsync());
        }

        [Fact]
        
        public async Task CheckBadDirection()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = true;
            await Assert.ThrowsAsync<YandexTranslateException>(() => wrapper.TranslateTextAsync(_randomEnglishText, "BAD"));            
        }
    }
}
