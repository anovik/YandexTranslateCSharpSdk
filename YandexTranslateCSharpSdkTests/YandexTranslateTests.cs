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
            LanguagesManager langManager = new LanguagesManager();
            langManager.ApiKey = _apiKey;
            langManager.IsJson = false;             
            List<string> languages = await langManager.GetLanguages();
            Assert.AreNotEqual(languages, null);
            Assert.IsTrue(languages.Contains("en"));
            Assert.IsTrue(languages.Contains("ru"));
        }

        [TestMethod]
        public async Task GetLanguagesJsonTest()
        {
            LanguagesManager langManager = new LanguagesManager();
            langManager.ApiKey = _apiKey;
            langManager.IsJson = true;
            List<string> languages = await langManager.GetLanguages();
            Assert.AreNotEqual(languages, null);
            Assert.IsTrue(languages.Contains("en"));
            Assert.IsTrue(languages.Contains("ru"));
        }

        [TestMethod]
        public async Task DetectLanguageXmlTest()
        {
            DetectLanguageManager detectManager = new DetectLanguageManager();
            detectManager.ApiKey = _apiKey;
            detectManager.IsJson = false;
            string result = await detectManager.DetectLanguage("Привет");
            Assert.AreEqual(result, "ru");
        }

        [TestMethod]
        public async Task DetectLanguageJsonTest()
        {
            DetectLanguageManager detectManager = new DetectLanguageManager();
            detectManager.ApiKey = _apiKey;
            detectManager.IsJson = true;
            string result = await detectManager.DetectLanguage(_randomEnglishText);
            Assert.AreEqual(result, "en");
        }

        [TestMethod]
        public async Task TranslateXmlTest()
        {
            TranslateManager translateManager = new TranslateManager();
            translateManager.ApiKey = _apiKey;
            translateManager.IsJson = false;
            string translatedText = await translateManager.TranslateText(_randomEnglishText, "en-ru");
            Console.WriteLine(translatedText);
        }

        [TestMethod]
        public async Task TranslateJsonTest()
        {
            TranslateManager translateManager = new TranslateManager();
            translateManager.ApiKey = _apiKey;
            translateManager.IsJson = true;
            string translatedText = await translateManager.TranslateText(_randomEnglishText, "en-ru");
            Console.WriteLine(translatedText);
        }
    }
}
