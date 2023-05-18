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
            {string json = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine("Received json: " + json);
                return JsonConvert.DeserializeObject<List<Project>>(json, settings);
            }
            else
            {
                Console.WriteLine("Error. Status Code " + (int)response.StatusCode + ": " + response.StatusCode);
            }
            //Return an empty list in case no result?
            return new List<Project>();
        }

        public Project GetProject(int id)
        {
            Uri uri = new Uri(url + id);
            HttpResponseMessage response = httpClient.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Project>(json, settings);
                //Console.WriteLine(json);
                //ProjecItem item = JsonSerialoizer.Deserialize<ProjectItem>(json);
                //Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Error {response.ReasonPhrase}");
            }
            // What to return
            return new Project();
        }

        public bool PostProject(Project project)
        {
            Uri uri = new Uri(url);
            string json = JsonConvert.SerializeObject(project);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync(uri, stringContent).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool UpdateProject(Project project)
        {
            Uri uri = new Uri(url + project.Id);

            string json = JsonConvert.SerializeObject(args);
            Console.WriteLine("sending json: " + json);

            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = httpClient.PutAsync(uri, stringContent).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(args.Title + " successfully changed author to " + args.NewAuthor);
                return true;
            }
            Console.WriteLine("Error. Status Code " + (int)response.StatusCode + ": " + response.StatusCode);
            return false;
        }

        public bool DeleteProject(int id)
        {
            Uri uri = new Uri(url + id);

            HttpResponseMessage response = httpClient.DeleteAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            Console.WriteLine("Error. Status Code " + (int)response.StatusCode + ": " + response.StatusCode);
            return false;
        }

    }
}

