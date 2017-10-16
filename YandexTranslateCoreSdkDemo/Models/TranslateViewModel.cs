using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YandexTranslateCoreSdkDemo.Models
{
    public class TranslateViewModel
    {
        [Required]
        public string Key { get; set; }

        public List<string> Languages { get; set; }

        public string InputLanguage { get; set; }

        public string OutputLanguage { get; set; }

        public string InputText { get; set; }

        public string OutputText { get; set; }
    }
}
