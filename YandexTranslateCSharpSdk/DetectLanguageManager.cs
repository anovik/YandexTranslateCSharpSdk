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
    /// Wrapper for Detect the language methods
    /// https://tech.yandex.com/translate/doc/dg/reference/detect-docpage/
    /// </summary>
    internal class DetectLanguageManager
    {
        internal string ApiKey { get; set; }

        internal async Task<string> DetectLanguageXml(string text)
        {
            string response = await PostData(text, "https://translate.yandex.net/api/v1.5/tr/detect?",
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
        internal async Task<string> DetectLanguageJson(string text)
        {
            string response = await PostData(text, "https://translate.yandex.net/api/v1.5/tr.json/detect?",
                "application/json");
            
            var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(response);
            var lang = dict["lang"];
            return lang == null ? null : lang.ToString();
        }

        private async Task<string> PostData(string text, string url, string mediaType)
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
            catch(Exception)
            {
                throw new Exception();
            }
        }
    }
}
