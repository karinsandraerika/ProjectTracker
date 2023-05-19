using System;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;
using System.Collections.Generic;
namespace ClientProjectPlanner
{
	public class ClientUI
	{
        private readonly ClientProject clientProject = new ClientProject();
        private readonly ClientProjectItem clientProjectItem = new ClientProjectItem();
        public bool Continue { get; set; } = true;
        public string Welcome { get; set; } = "PROJECT TRACKER\nPlease select what you want to do:\n";
        public string Menu { get; set; } = "1: Get all projects\n2: Get a project\n3: Create a new project\n4: Update a project\n" +
                "5: Delete a project\n6: Quit";

        public ClientUI()
        {
            clientProject = new ClientProject();
            clientProjectItem = new ClientProjectItem();
        }

        public void Run()
		{
            Console.WriteLine(Welcome);
            while (Continue)
            {
                ShowMenu();
                ReadInput();
            }
		}

        private void ShowMenu()
        {
            Console.WriteLine(Menu);
        }

        private void ReadInput()
        {
            Dictionary<string, Action> menuOptions = new Dictionary<string, Action>()
            {
                {"1", PrintAllProjects},
                {"2", PrintOneProject},
                {"3", CreateProject},
                {"4", UpdateProject},
                {"5", DeleteProject},
                {"6",  () => { Continue = false;} }
            };
            string choice = Console.ReadLine().Trim();
            Console.WriteLine();
            if (menuOptions.TryGetValue(choice, out Action action))
                action();
            else
                Console.WriteLine("invalid choice\n");
            Console.WriteLine();
        }

        private void PrintAllProjects()
        {
            List<Project>? projects = clientProject.GetProjects();
            projects.ForEach(p => p.PrintProject());
        }

        private void PrintOneProject()
        {
            Console.WriteLine("Id of the project you want: ");
            int? id = CheckId();
            if (id == null)
            {
                return;
            }
            Project project = clientProject.GetProject((int)id);
            project?.PrintProject();

            if (project != null)
            {
                Console.WriteLine("Would you like to see a list of all items in this project: y/n");
                string answer = Console.ReadLine().Trim().ToLower();
                if (answer == "y")
                {
                    PrintAllProjectItemsInProject(project);
                }
            }
            
        }

        private void PrintAllProjectItemsInProject(Project project)
        {
            foreach (var id in project.ProjectItems)
            {
                var item = clientProjectItem.GetProjectItem(id);
                item?.PrintItem();
            }
        }

        private void CreateProject()
        {
            Console.WriteLine("Name of project (required): ");
            string name = Console.ReadLine();
            Console.WriteLine("Description of project: ");
            string description = Console.ReadLine();
            Console.WriteLine("Id of persons separated by space: ");
            List<int> persons = GetListInput();
            Console.WriteLine("Id of project items separated by space: ");
            List<int> items = GetListInput();
            Project project = new Project() {
                Name = !string.IsNullOrEmpty(name) ? name : null,
                Description = !string.IsNullOrEmpty(description) ? description : null,
                Persons =  persons.Any() ? persons : null,
                ProjectItems = items.Any() ? items : null };
            clientProject.PostProject(project);
        }

        private void UpdateProject()
        {
            Console.WriteLine("Please write the id of the project you want to update: ");
            int? id = CheckId();
            if (id == null)
            {
                return;
            }
            Project project = clientProject.GetProject((int)id);
            if (project == null)
            {
                return;
            }
            Console.WriteLine("Name of project (required): ");
            string name = Console.ReadLine();
            Console.WriteLine("Description of project: ");
            string description = Console.ReadLine();
            Console.WriteLine("Id of persons separated by space: ");
            List<int> persons = GetListInput();
            Console.WriteLine("Id of project items separated by space: ");
            List<int> items = GetListInput();

            project.Name = !string.IsNullOrEmpty(name) ? name : null;
            project.Description = !string.IsNullOrEmpty(description) ? description : null;
            project.Persons = persons.Any() ? persons : null;
            project.ProjectItems = items.Any() ? items : null;
            
            clientProject.UpdateProject(project);
        }

        private void DeleteProject()
        {
            Console.WriteLine("Id to delete: ");
            int? id = CheckId();
            if (id == null)
            {
                return;
            }
            clientProject.DeleteProject((int)id);
        }

        private int? CheckId()
        {
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("You need to write a number for id.");
                return null;
            }
            return id;
        }
 
        private List<int> GetListInput()
        {
            string input = Console.ReadLine().Trim();
            string[] inputList = input.Split(" ");
            List<int> ints = new List<int>();
            foreach (var item in inputList)
            {
                if (int.TryParse(item, out int number))
                {
                    ints.Add(number);
                }
            }
            return ints;
        }
    }
}

