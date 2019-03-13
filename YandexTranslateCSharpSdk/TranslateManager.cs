using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
#if !NETCORE
using System.Web.Script.Serialization;
#else
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#endif
using System.Xml;

namespace YandexTranslateCSharpSdk
{
    /// <summary>
    /// Wrapper for Translate a text methods
    /// https://tech.yandex.com/translate/doc/dg/reference/translate-docpage/
    /// </summary>
    internal class TranslateManager
    {
        internal string ApiKey { get; set; }

        internal async Task<string> TranslateTextXml(string text, string direction)
        {
            string response = await PostData(text, direction,
                "https://translate.yandex.net/api/v1.5/tr/translate?", "application/xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);
            XmlNodeList list = xmlDoc.GetElementsByTagName("text");
            if (list.Count > 0)
            {
                return list[0].InnerText;
            }
            return null;
        }
        internal async Task<string> TranslateTextJson(string text, string direction)
        {
            string response = await PostData(text, direction,
             "https://translate.yandex.net/api/v1.5/tr.json/translate?", "application/json");
#if NETCORE
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(response); 
#else
            var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(response);
#endif 
            var outputText = dict["text"];
            if (outputText == null)
            {
                return null; 
            }
            else
            {
#if NETCORE
                JArray list = outputText as JArray;
#else
                ArrayList list = outputText as ArrayList;               
#endif
                if (list.Count > 0)
                {
                    return list[0].ToString();
                }

            }
            return null;
        }

        private async Task<string> PostData(string text, string direction, string url, string mediaType)
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
                    postData.Add(new KeyValuePair<string, string>("lang", direction));

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
