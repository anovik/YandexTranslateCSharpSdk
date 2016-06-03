using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace YandexTranslateCSharpSdk
{
    /// <summary>
    /// Wrapper for Translate a text methods
    /// https://tech.yandex.com/translate/doc/dg/reference/translate-docpage/
    /// </summary>
    public class TranslateManager
    {
        public string ApiKey { get; set; }

        public bool IsJson { get; set; }

        public async Task<string> TranslateText(string text, string direction)
        {
            if (IsJson)
            {
                return await TranslateTextJson(text, direction);
            }
            else
            {
                return await TranslateTextXml(text, direction);
            }
        }

        private async Task<string> TranslateTextXml(string text, string direction)
        {
            string response = await PostData(text, direction,
                "https://translate.yandex.net/api/v1.5/tr/translate?", "application/xml");
            return response;                 
        }
        private async Task<string> TranslateTextJson(string text, string direction)
        {
            string response = await PostData(text, direction,
             "https://translate.yandex.net/api/v1.5/tr.json/translate?", "application/json");
            return response;
        }

        private async Task<string> PostData(string text, string direction, string url, string mediaType)
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
    }
}
