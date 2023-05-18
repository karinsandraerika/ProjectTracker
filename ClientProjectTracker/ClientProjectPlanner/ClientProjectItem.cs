using System;
using Newtonsoft.Json;

namespace ClientProjectPlanner
{
	public class ClientProjectItem
	{
        HttpClient httpClient = new HttpClient();
        string url = "https://localhost:7029/api/ProjectItem/";
        public JsonSerializerSettings settings { get; set; } = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public List<ProjectItem> GetProjectItems()
        {
            Uri uri = new Uri(url);
            HttpResponseMessage response = httpClient.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<ProjectItem>>(json, settings);
            }
            else
            {
                Console.WriteLine($"Error: {(int)response.StatusCode} {response.StatusCode}");
                return new List<ProjectItem>();
            }
        }

        public ProjectItem GetProjectItem(int id)
        {
            Uri uri = new Uri(url + id);
            HttpResponseMessage response = httpClient.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ProjectItem>(json, settings);
            }
            else
            {
                Console.WriteLine($"Error: {(int)response.StatusCode} {response.StatusCode}");
                return null;
            }
        }
    }
}

