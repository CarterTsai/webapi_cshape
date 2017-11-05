using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace checkAPI
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();
        private static async Task MainAsync()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5050");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

             // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            
            // string json = JsonConvert.SerializeObject("{}", Formatting.Indented);
            // var httpContent = new StringContent(json);

            var response = await client.GetAsync("http://localhost:5050/api/Values");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
