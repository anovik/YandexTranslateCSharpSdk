using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;

namespace YandexTranslateCSharpSdk
{
    /// <summary>
    /// Wrapper for Get the list of supported languages methods
    /// https://tech.yandex.com/translate/doc/dg/reference/getLangs-docpage/
    /// </summary>
    public class LanguagesManager
    {
        public string ApiKey { get; set; }

        public bool IsJson { get; set; }

        public async Task<List<string>> GetLanguages()
        {
           if (IsJson)
           {
               return await GetLanguagesJson();
           }
           else
           {
               return await GetLanguagesXml();
           }
        }

        private async Task<List<string>> GetLanguagesXml()
        {
            List<string> languages = new List<string>();
            string response = await PostData("https://translate.yandex.net/api/v1.5/tr/getLangs?", "application/xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);            
            XmlNodeList list = xmlDoc.GetElementsByTagName("Item");
            foreach(XmlNode node in list)
            {
                languages.Add(node.Attributes["key"].Value);
            }
            return languages;
        }

        private async Task<List<string>> GetLanguagesJson()
        {
            List<string> languages = new List<string>();
            string response = await PostData("https://translate.yandex.net/api/v1.5/tr.json/getLangs?", "application/json");
            var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(response);
            var lang = dict["langs"];
            return languages;           
        }

        private async Task<string> PostData(string url, string mediaType)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://translate.yandex.net/api/v1.5/tr/getLangs?");

                httpClient.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                    "");

                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("key", ApiKey));
                postData.Add(new KeyValuePair<string, string>("ui", "en"));

                HttpContent content = new FormUrlEncodedContent(postData);
                request.Content = content;
                HttpResponseMessage response = await httpClient.SendAsync(request)
                       .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
                return await response.Content.ReadAsStringAsync();                
            }
        }
    }
}
