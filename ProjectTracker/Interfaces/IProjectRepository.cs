using System;
using ProjectTracker.Models;

namespace ProjectTracker.Interfaces
{
	public interface IProjectRepository
	{
        ICollection<Project> GetProjects();
    }
}

