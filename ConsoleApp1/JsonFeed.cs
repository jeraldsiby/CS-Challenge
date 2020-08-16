using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// core API functions
    /// </summary>
    class JsonFeed
    {
        static string _url = ""; 

        public JsonFeed(string endpoint)
        {
            _url = endpoint;
        }
        
        /// <summary>
        /// Gets Random jokes
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="category"></param>
        /// <returns>The list of jokes</returns>
		public static string[] GetRandomJokes(string firstname, string lastname, string category)
		{
            string joke = "";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                string url = "jokes/random";
                if (category != null)
                {
                    if (url.Contains('?'))
                        url += "&";
                    else url += "?";
                    url += "category=";
                    url += category;
                }
                try
                {
                    joke = Task.FromResult(client.GetStringAsync(url).Result).Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return GetCustomizedJokes(firstname, lastname, joke);
        }
        /// <summary>
        /// returns the customized joke with random name appened
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="joke"></param>
        /// <returns>array of string - customized joke</returns>
        private static string[] GetCustomizedJokes(string firstname, string lastname, string joke)
        {
            if (firstname != null && lastname != null)
            {
                int index = joke.IndexOf("Chuck Norris");
                string firstPart = joke.Substring(0, index);
                string secondPart = joke.Substring(0 + index + "Chuck Norris".Length, joke.Length - (index + "Chuck Norris".Length));
                joke = firstPart + " " + firstname + " " + lastname + secondPart;
            }
            return new string[] { JsonConvert.DeserializeObject<dynamic>(joke).value };
        }

        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <returns></returns>
        public static dynamic Getnames()
		{
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                try 
                {
                    var result = client.GetStringAsync("").Result;
                    return JsonConvert.DeserializeObject<dynamic>(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
			
		}

        /// <summary>
        /// Returns joke categories
        /// </summary>
        /// <returns>the list of categories</returns>
		public static string[] GetCategories()
		{
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                try
                {
                    return new string[] { Task.FromResult(client.GetStringAsync("jokes/categories").Result).Result };
                }
                catch (Exception)
                {
                    throw;
                }
            }
		}
    }
}
