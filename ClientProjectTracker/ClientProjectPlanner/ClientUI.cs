using System;
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
                {"2", PrintAllProjects},
                {"3", PrintAllProjects},
                {"4", PrintAllProjects},
                {"5", PrintAllProjects},
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


    }
}

