using Microsoft.AspNetCore.Mvc;

namespace YandexTranslateCoreSdkDemo.Controllers
{
    public class TranslateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}