using System;
using ProjectTracker.Models;

namespace ProjectTracker.Interfaces
{
	public interface IProjectItemRepository
	{
        ICollection<ProjectItem> GetProjectItems();
        ProjectItem GetProjectItem(int id);
        bool ProjectItemExists(int id);
        bool CreateProjectItem(ProjectItem projectItem);
        bool Save();

    }
}

