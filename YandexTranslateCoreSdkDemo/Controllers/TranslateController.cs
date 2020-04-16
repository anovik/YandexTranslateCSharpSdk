using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YandexTranslateCoreSdkDemo.Models;
using YandexTranslateCSharpSdk;

namespace YandexTranslateCoreSdkDemo.Controllers
{
    public class TranslateController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            TranslateViewModel model = new TranslateViewModel();            
            return View(model);
        }

        [HttpPost]       
        public async Task<IActionResult> Translate(TranslateViewModel model)
        {
            if (ModelState.IsValid)
            {
                YandexTranslateSdk wrapper = new YandexTranslateSdk();
                wrapper.ApiKey = model.Key;
                string inputLanguage = await wrapper.DetectLanguageAsync(model.InputText);
                string outputLanguage = model.OutputLanguage;
                string direction = inputLanguage + "-" + outputLanguage;
                model.OutputText = await wrapper.TranslateTextAsync(model.InputText, direction);
                model.Languages = new List<string>(TempData["Languages"] as string[]);

                ModelState.Clear();

                return View("Index", model);
            }
            return View("Index", model);
        }

        [HttpPost]      
        public async Task<ActionResult> GetLanguages(TranslateViewModel model)
        {
            if (string.IsNullOrEmpty(model.Key))
            {
                throw new Exception("Empty API Key");
            }
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = model.Key;
            model.Languages = await wrapper.GetLanguagesAsync();
            TempData["Languages"] = model.Languages;
            return View("Index", model);
        }
    }
}