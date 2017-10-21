using Microsoft.AspNetCore.Mvc;
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
                string inputLanguage = await wrapper.DetectLanguage(model.InputText);
                string outputLanguage = model.OutputLanguage;
                string direction = inputLanguage + "-" + outputLanguage;
                model.OutputText = await wrapper.TranslateText(model.InputText, direction);

                ModelState.Clear();

                return View("Index", model);
            }
            return View("Index", model);
        }

        [HttpPost]      
        public async Task<ActionResult> GetLanguages(TranslateViewModel model)
        {
            YandexTranslateSdk wrapper = new YandexTranslateSdk();
            wrapper.ApiKey = model.Key;
            model.Languages = await wrapper.GetLanguages();
            return View("Index", model);
        }
    }
}