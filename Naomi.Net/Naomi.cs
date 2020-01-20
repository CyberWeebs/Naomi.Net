using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Naomi.Net
{
    public class Naomi
    {
        private const string BaseUri = "https://saucenao.com";

        private string ApiKey { get; }

        private HttpClient Http { get; }

        public Naomi(string apiKey, HttpClient httpClient = null)
        {
            ApiKey = apiKey;
            Http = httpClient ?? new HttpClient();

            Http.BaseAddress = new Uri(BaseUri);
        }

        public async Task<NaomiResult> GetSearchResults(string file, NaomiOptions options = null)
        {
            if (!Uri.IsWellFormedUriString(file, UriKind.Absolute))
            {
                throw new ArgumentException("File URI is not well-formed.");
            }

            options = options ?? NaomiOptions.Default;

            var qDict = options.BuildContent(file, ApiKey);
            string query;

            using (var content = new FormUrlEncodedContent(qDict.ToDictionary(k => k.Key, k => k.Value.ToString())))
            {
                query = content.ReadAsStringAsync().Result;
                string response = await Http.GetStringAsync("search.php?" + query);

                return JsonConvert.DeserializeObject<NaomiResult>(response);
            }
        }
    }
}