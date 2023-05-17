using System;
using Newtonsoft.Json;

namespace ClientProjectPlanner
{
	public class ClientProject
	{
        HttpClient httpClient = new HttpClient();
        string url = "https://localhost:7029/api/Project/";
        public JsonSerializerSettings settings { get; set; } = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };



        public List<Project> GetProjects()
        {
            Uri uri = new Uri(url);
            HttpResponseMessage response = httpClient.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {string json = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine("Received json: " + json);
                return JsonConvert.DeserializeObject<List<Project>>(json, settings);
            }
            else
            {
                Console.WriteLine("Error. Status Code " + (int)response.StatusCode + ": " + response.StatusCode);
            }

            return new List<Project>();
        }
    }
}

