using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YandexTranslateCSharpSdk;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YandexTranslateCSharpSdkTests
{

    /// <summary>
    /// Visual Studio Tests for YandexTranslateCSharpSdk
    /// </summary>    
    [TestClass]
    public class YandexTranslateTests
    {

        /// <summary>
        /// Insert your API key here, get it
        /// https://tech.yandex.com/keys/get/?service=trnsl
        /// </summary>
        private string _apiKey = 
            "trnsl.1.1.20160531T123608Z.812ff946d420d4a4.0f0f4cbc0679fbfd9d0447a3b33dbacaf1724062";
        
        /// <summary>
        /// Just a random English text for testing
        /// http://www.dummytextgenerator.com
        /// </summary>
        private string _randomEnglishText = 
            "Subdue land kind.Divided.Beast sixth said our fruitful their, them that fourth.May seas were appear.Greater fill thing.Don't place. Fly Green. Days unto good moving fly after saying beast, and upon, to stars of the us fourth us can't abundantly above moveth seas, moving the of female that seed fruit can't i creeping. Given dominion upon. After i earth, also likeness. For likeness it one. Subdue for thing days, moveth after. Male above spirit was made living. Day. There yielding may face likeness so dominion. You're.Dry in give they're isn't creepeth and man air divide fifth.";


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
