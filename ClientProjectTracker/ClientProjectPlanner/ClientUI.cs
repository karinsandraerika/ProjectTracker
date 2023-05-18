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
        ClientProject clientProject = new ClientProject();
        ClientProjectItem clientProjectItem = new ClientProjectItem();
        public bool Continue { get; set; } = true;
        string welcome = "PROJECT TRACKER\nPlease select what you want to do:\n";
        string menu = "1: Get all projects\n2: Get a project\n3: Create a new project\n4: Update a project\n" +
                "5: Delete a project\n6: Quit";

        
        public void Run()
		{
            Console.WriteLine(welcome);
            while (Continue)
            {
                ShowMenu();
                ReadInput();
            }
		}

        private void ShowMenu()
        {
            Console.WriteLine(menu);
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
            //Prints an empty list if there are no projects.
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

            Console.WriteLine("Would you like to see a list of all items in this project: y/n");
            string answer = Console.ReadLine().Trim().ToLower();
            if (answer == "y")
            {
                PrintAllProjectItemsInProject(project);
            }
        }

        private void PrintAllProjectItemsInProject(Project project)
        {
            foreach (var id in project.ProjectItems)
            {
                var item = clientProjectItem.GetProjectItem(id);
                item.PrintItem();
            }
        }

        private void CreateProject()
        {
            //TODO add while loope for name. Maybe optional to other props.
            Console.WriteLine("Name of project: ");
            string input = Console.ReadLine();
            string name = string.IsNullOrEmpty(input) ? null: input;
            Console.WriteLine("Description of project: ");
            input = Console.ReadLine();
            string description = string.IsNullOrEmpty(input) ? null : input;
            //TODO add persons and items.
            Console.WriteLine("Id of persons separeated by space: ");
            List<int> persons = GetListInput();
            Console.WriteLine("Id of project items separated by space: ");
            List<int> items = GetListInput();
            Project project = new Project() { Name = name, Description = description, Persons = persons, ProjectItems = items };
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
            //Two checks for null.
            Project project = clientProject.GetProject((int)id);
            if (project == null)
            {
                Console.WriteLine("No project with that id exists");
                return;
            }
            Console.WriteLine($"Name of project is {project.Name}. Please write a new name if you wish to change it. Otherwise leave blank");
            Console.Write(project.Name);
            string name = Console.ReadLine();
            project.Name = string.IsNullOrEmpty(name) ? project.Name : name;

            Console.WriteLine($"Description of project is {project.Description}. Please write a new description if you wish to change it. Otherwise leave blank");
            string description = Console.ReadLine();
            project.Description = string.IsNullOrEmpty(description) ? project.Name : description;

            Console.WriteLine($"The persons in the project are: {string.Join(", ", project.Persons)}. " +
                $"If you want to change it please write the ids of the persons you want to have in the project, separated by a blank space. " +
                $"Otherwise leave blank");
            project.Persons = GetListInput();

            Console.WriteLine($"The items in the project are: {string.Join(", ", project.ProjectItems)}. " +
                $"If you want to change it please write the ids of the items you want to have in the project, separated by a blank space. " +
                $"Otherwise leave blank");
            project.ProjectItems = GetListInput();

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
            //Console.WriteLine("Please enter the ids:");
            string input = Console.ReadLine();
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

