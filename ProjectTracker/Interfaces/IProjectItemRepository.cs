using System;
using ProjectTracker.Dto;
using ProjectTracker.Models;

namespace ProjectTracker.Interfaces
{
	public interface IProjectItemRepository
	{
        ICollection<ProjectItemDto> GetProjectItems();
        ProjectItemDto GetProjectItem(int id);
        bool ProjectItemExists(int id);
        bool CreateProjectItem(ProjectItemDto projectItem);
        bool UpdateProjectItem(ProjectItemDto projectItem);
        bool DeleteProjectItem(ProjectItemDto projectItem);
        bool Save();

    }
}

