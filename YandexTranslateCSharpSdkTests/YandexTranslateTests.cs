using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YandexTranslateCSharpSdk;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace YandexTranslateCSharpSdkTests
{
    /// <summary>
    /// Visual Studio Tests for YandexTranslateCSharpSdk
    /// </summary>    
    [TestClass]
    public class YandexTranslateTests
    {
        /// <summary>
        /// Insert your API key here, get it from 
        /// https://tech.yandex.com/keys/get/?service=trnsl
        /// and put it to key.txt in YandexTranslateCSharpSdkTests folder
        /// </summary>
        private static string _apiKey;
        
        /// <summary>
        /// Just a random English text for testing
        /// http://www.dummytextgenerator.com
        /// </summary>
        private string _randomEnglishText = 
            "Subdue land kind.Divided.Beast sixth said our fruitful their, them that fourth.May seas were appear.Greater fill thing.Don't place. Fly Green. Days unto good moving fly after saying beast, and upon, to stars of the us fourth us can't abundantly above moveth seas, moving the of female that seed fruit can't i creeping. Given dominion upon. After i earth, also likeness. For likeness it one. Subdue for thing days, moveth after. Male above spirit was made living. Day. There yielding may face likeness so dominion. You're.Dry in give they're isn't creepeth and man air divide fifth.";

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            _apiKey = File.ReadAllText("key.txt");
        }

        [TestMethod]
        public async Task GetLanguagesXmlTest()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = false;
            List<string> languages = await wrapper.GetLanguages();            
            Assert.AreNotEqual(languages, null);
            Assert.IsTrue(languages.Contains("en"));
            Assert.IsTrue(languages.Contains("ru"));
        }

        [TestMethod]
        public async Task GetLanguagesJsonTest()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = true;
            List<string> languages = await wrapper.GetLanguages();
            Assert.AreNotEqual(languages, null);
            Assert.IsTrue(languages.Contains("en"));
            Assert.IsTrue(languages.Contains("ru"));
        }

        [TestMethod]
        public async Task DetectLanguageXmlTest()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = false;
            string result = await wrapper.DetectLanguage("Привет");
            Assert.AreEqual(result, "ru");
        }

        [TestMethod]
        public async Task DetectLanguageJsonTest()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = true;
            string result = await wrapper.DetectLanguage(_randomEnglishText);
            Assert.AreEqual(result, "en");
        }

        [TestMethod]
        public async Task TranslateXmlTest()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = false;
            string translatedText = await wrapper.TranslateText(_randomEnglishText, "en-ru");
            Console.WriteLine(translatedText);
            Assert.IsTrue(!string.IsNullOrEmpty(translatedText));
        }

        [TestMethod]
        public async Task TranslateJsonTest()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = true;
            string translatedText = await wrapper.TranslateText(_randomEnglishText, "en-ru");
            Console.WriteLine(translatedText);
            Assert.IsTrue(!string.IsNullOrEmpty(translatedText));
        }

        [TestMethod]
        public async Task CheckBadKey()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = "KeyNotExists";
            List<string> languages = await wrapper.GetLanguages();
        }

        [TestMethod]
        public async Task CheckNullKey()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = null;
            List<string> languages = await wrapper.GetLanguages();
        }

        [TestMethod]
        public async Task CheckBadDirection()
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = _apiKey;
            wrapper.IsJson = true;
            string translatedText = await wrapper.TranslateText(_randomEnglishText, "BAD");            
        }
    }
}
