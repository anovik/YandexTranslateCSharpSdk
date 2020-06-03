using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YandexTranslateCoreSdkDemo.Models
{
    public class TranslateViewModel
    {
        public TranslateViewModel()
        {
            Languages = new Dictionary<string, string>();
        }

        [Required]
        public string Key { get; set; }        

        public Dictionary<string,string> Languages { get; set; }

        [Required]
        [Display(Name ="Output Language")]
        public string OutputLanguage { get; set; }

        [Required]
        [Display(Name = "Input Text")]
        public string InputText { get; set; }

        [Display(Name = "Output Text")]
        public string OutputText { get; set; }
    }
}
