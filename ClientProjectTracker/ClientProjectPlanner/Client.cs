using System;
using Newtonsoft.Json;
using System.Net.Http;

namespace ClientProjectPlanner
{
	public class Client
	{
        HttpClient httpClient = new HttpClient();
        string url = "https://localhost:7029/api/ProjectItem/";
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        

        public void GetProjectItem()
        {
            Console.WriteLine("Id of the project item you want: ");
            string id = Console.ReadLine();

            Uri uri = new Uri(url + id);
            HttpResponseMessage response = httpClient.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(json);
                //ProjecItem item = JsonSerialoizer.Deserialize<ProjectItem>(json);
                //Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Error {response.ReasonPhrase}");
            }
        }

        public List<ProjectItem> GetProjectItems()
        {
            
            Uri uri = new Uri(url);

            HttpResponseMessage response = httpClient.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                string json = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine("Received json: " + json);
                return JsonConvert.DeserializeObject<List<ProjectItem>>(json, settings);
            }
            else
            {
                Console.WriteLine("Error. Status Code " + (int)response.StatusCode + ": " + response.StatusCode);
            }

            return new List<ProjectItem>();
        }
        /*
        private void Post(ProjectItem item)
        {
            
            /*
            Console.WriteLine("What's the model?");
            string model = Console.ReadLine();
            Console.WriteLine("What's the reg nr?");
            string regNr = Console.ReadLine();
            Car car = new Car() { Model = model, RegNr = regNr };

            HttpClient httpClient = new HttpClient();
            Uri uri = new Uri("https://localhost:7150/api/car/post");

            string json = JsonSerializer.Serialize(car);

            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = httpClient.PostAsync(uri, stringContent).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(car.Model + " successfully registered!");
            }
            else
            {
                Console.WriteLine("Post failed. Status Code " + (int)response.StatusCode + ": " + response.StatusCode);
            }
        }*/
    }
}

