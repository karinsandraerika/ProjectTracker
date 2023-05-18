using System;
using ProjectTracker.Dto;
using ProjectTracker.Models;

namespace ProjectTracker.Interfaces
{
	public interface IProjectRepository
	{
        ICollection<ProjectDto> GetProjects();
        ProjectDto GetProject(int id);
        bool ProjectExists(int id);
        bool CreateProject(ProjectDto project);
        bool UpdateProject(ProjectDto project);
        bool DeleteProject(ProjectDto project);
        bool Save();
    }
}

