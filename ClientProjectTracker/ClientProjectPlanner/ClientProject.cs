using System;
using System.Runtime.ConstrainedExecution;
using System.Text;
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
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Project>>(json, settings);
            }
            else
            {
                Console.WriteLine($"Error: {(int)response.StatusCode} {response.StatusCode}");
                return new List<Project>();
            } 
        }

        public Project GetProject(int id)
        {
            Uri uri = new Uri(url + id);
            HttpResponseMessage response = httpClient.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Project>(json, settings);
            }
            else
            {
                Console.WriteLine($"Error: {(int)response.StatusCode} {response.StatusCode}");
                return null;
            } 
        }

        public void PostProject(Project project)
        {
            Uri uri = new Uri(url);
            string json = JsonConvert.SerializeObject(project);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync(uri, stringContent).Result;
            Console.WriteLine($"{(int) response.StatusCode} {response.StatusCode}");
        }
        
        public void UpdateProject(Project project)
        {
            string id = project.Id.ToString();
            Uri uri = new(url + id);
            string json = JsonConvert.SerializeObject(project);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PutAsync(uri, stringContent).Result;
            Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode}");
        }

        public void DeleteProject(int id)
        {
            Uri uri = new Uri(url + id);
            HttpResponseMessage response = httpClient.DeleteAsync(uri).Result;
            Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode}");
        }

    }
}

