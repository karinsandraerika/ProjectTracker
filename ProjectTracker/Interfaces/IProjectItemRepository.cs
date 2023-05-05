using System;
using ProjectTracker.Models;

namespace ProjectTracker.Interfaces
{
	public interface IProjectItemRepository
	{
        ICollection<ProjectItem> GetProjectItems();
        ProjectItem GetProjectItem(int id);
        bool ProjectItemExists(int id);

        //ProjectItem GetProjectItem(string name);
        //bool CreateProjectItem(int ProjectId, int PersonId, ProjectItem item);
        //bool UpdateProjectItem(int ProjectId, int PersonId, ProjectItem item);
        //bool DeleteProjectItem(ProjectItem item);
        //bool Save();

    }
}

