﻿using System;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace ClientProjectPlanner
{
	public class ClientUI
	{
        Client client = new Client();
        ClientProject clientProject = new ClientProject();

        public bool Continue { get; set; } = true;
        public void Run()
		{
            while (Continue)
            {
                ShowMenu();
                ReadInput();
            }
		}

        private void ShowMenu()
        {
            Console.WriteLine("1: Get all projects\n2: Get a project\n3: Create a new project\n4: Update a project\n5: Delete a project\n" +
                "6: Quit");
        }

        private void ReadInput()
        {
            Dictionary<string, Action> menuOptions = new Dictionary<string, Action>()
            {
                {"1", PrintAllProjects},
                {"2", PrintOneProject},
                {"3", CreateProject},
                {"4", PrintAllProjects},
                {"5", DeleteProject},
                {"6",  () => { Continue = false;} }
            };
            string choice = Console.ReadLine().Trim();
            if (menuOptions.TryGetValue(choice, out Action action))
                action();
            else
                Console.WriteLine("invalid choice\n");
            Console.WriteLine();
        }

        private void PrintAllProjects()
        {
            List<Project> projects = clientProject.GetProjects();
            projects.ForEach(p => p.PrintProject());
            //Console.WriteLine();
            
        }

        private void PrintOneProject()
        {
            Console.WriteLine("Id of the project you want: ");
            //TODO check if null, check if number.
            int id = int.Parse(Console.ReadLine());
            Project project = clientProject.GetProject(id);
            project.PrintProject();
        }

        private void CreateProject()
        {
            //TODO add while loope for name. Maybe optional to other props.
            Console.WriteLine("Name of project: ");
            string name = Console.ReadLine();
            Console.WriteLine("Description of project: ");
            string description = Console.ReadLine();
            Project project = new Project() { Name = name, Description = description };
            if (clientProject.PostProject(project))
            {
                Console.WriteLine("Successfully added");
            }
            else
            {
                Console.WriteLine("Failed to add");
            }
        }

        private void UpdateProject()
        {

        }

        private void DeleteProject()
        {
            Console.WriteLine("Id to delete: ");
            int id = int.Parse(Console.ReadLine());
            if (clientProject.DeleteProject(id))
            {
                Console.WriteLine("Succesfully deleted");
            }
            else
            {
                Console.WriteLine("Could not delete");
            }
        }
    }
}

