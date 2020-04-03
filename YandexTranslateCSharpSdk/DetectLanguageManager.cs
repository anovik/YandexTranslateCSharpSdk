
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
#if !NETCORE
using System.Web.Script.Serialization;
#else
using Newtonsoft.Json;
#endif
using System.Xml;

namespace YandexTranslateCSharpSdk
{
    /// <summary>
    /// Wrapper for Detect the language methods
    /// https://tech.yandex.com/translate/doc/dg/reference/detect-docpage/
    /// </summary>
    internal class DetectLanguageManager
    {
        internal string ApiKey { get; set; }

        internal async Task<string> DetectLanguageXmlAsync(string text)
        {
            string response = await PostDataAsync(text, "https://translate.yandex.net/api/v1.5/tr/detect?",
               "application/xml");
            XmlDocument xmlDoc = new XmlDocument();            
            xmlDoc.LoadXml(response);
            XmlNodeList list = xmlDoc.GetElementsByTagName("DetectedLang");
            if (list.Count > 0)
            {
                return list[0].Attributes["lang"].Value;
            }
            return null;
            
        }
        internal async Task<string> DetectLanguageJsonAsync(string text)
        {
            string response = await PostDataAsync(text, "https://translate.yandex.net/api/v1.5/tr.json/detect?",
                "application/json");
#if NETCORE
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(response); 
#else
            var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(response);
#endif
            var lang = dict["lang"];
            return lang == null ? null : lang.ToString();
        }

        private async Task<string> PostDataAsync(string text, string url, string mediaType)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(url);

                    httpClient.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue(mediaType));

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                        "");

                    var postData = new List<KeyValuePair<string, string>>();
                    postData.Add(new KeyValuePair<string, string>("key", ApiKey));
                    postData.Add(new KeyValuePair<string, string>("text", text));

                    HttpContent content = new FormUrlEncodedContent(postData);
                    request.Content = content;
                    HttpResponseMessage response = await httpClient.SendAsync(request)
                           .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException)
            {
                throw new YandexTranslateException(
                    "Bad parameters or other problem communicating Yandex.Translate API");
            }
        }
    }
}
