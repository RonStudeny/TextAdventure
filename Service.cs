using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.DataStructure;

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

    public static class Helpers
    {
        /// <summary>
        /// Generic function for creating basic conversation loops, prints a message and options with which to respond
        /// </summary>
        /// <param name="message">The message that begins the conversation</param>
        /// <param name="options">An array of options, that will be given to the user to choose from</param>
        /// <param name="res">A number coresponding to the chosen option, starting from 0</param>
        public static void Conversation(string message, string[] options, out int res)
        {
            bool loop = true;
            res = -1;
            ;
            while (loop)
            {
                Console.Clear();
                Console.WriteLine(message + "\n");
                for (int i = 0; i < options.Length; i++)
                    Console.WriteLine($"{i + 1}. {options[i]}");

                if (int.TryParse(Console.ReadLine(), out res) && res > 0 && res <= options.Length)
                {
                    res--;
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Invalid input, type a corresponding number...");
                    Console.ReadLine();
                }
            }
        }

        public static List<itemType> GetItemsOfType<itemType>(List<Item> items) where itemType : Item
        {
            List<itemType> pickedItems = new List<itemType>();
            foreach (var item in items)
            {
                if (item is itemType itemOfType)
                    pickedItems.Add(itemOfType);
            }
            return pickedItems;
        }

        public static string[] GetNames<itemType>(List<itemType> items) where itemType : Item
        {
            string[] res = new string[items.Count];
            for (int i = 0; i < items.Count; i++)
                res[i] = items[i].ToString();
            return res;
        }
    }
}
