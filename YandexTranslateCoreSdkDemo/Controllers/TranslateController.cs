using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YandexTranslateCoreSdkDemo.Models;
using YandexTranslateCSharpSdk;

namespace YandexTranslateCoreSdkDemo.Controllers
{
    public class TranslateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(TranslateViewModel model)
        {
            if (ModelState.IsValid)
            {
                YandexTranslateSdk wrapper = new YandexTranslateSdk();
                wrapper.ApiKey = model.Key;
                model.OutputText = await wrapper.TranslateText(model.InputText, "en-ru");

                ModelState.Clear();                

                return View(model);
            }
            return View();
        }
    }
}