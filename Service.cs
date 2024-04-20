using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Service
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static string apiKey = "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";
        public static string endPoint = "https://api.openai.com/v1/completions";
        public static string modelType = "gpt-3.5-turbo-instruct";
        public static int maxTokens = 256;
        public static double temp = 1.0f;

        public static async Task<string> OpenAIComplete(string _apikey, string _endpoint, string _model, int _maxTokens, double _temp)
        {
            var requestBody = new
            {
                model = modelType,
                prompt = "Hello world",
                //max_tokens = maxTokens,
                temperature = temp
            };

            string jsonPayload = JsonConvert.SerializeObject(requestBody);

            var request = new HttpRequestMessage(HttpMethod.Post, endPoint);
            request.Headers.Add("Authorization", $"Bearer {apiKey}");
            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.SendAsync(request);
            string responseContent = await httpResponse.Content.ReadAsStringAsync();

            return responseContent;
        }

        public static void SaveToFile()
        {
            throw new NotImplementedException();
        }

        public static bool LoadFromFile()
        {
            throw new NotImplementedException();
        }

    }
}
