﻿using System;
using ProjectTracker.Models;

namespace ProjectTracker.Interfaces
{
	public interface IProjectRepository
	{
        ICollection<Project> GetProjects();
        Project GetProject(int id);
        bool ProjectExists(int id);
    }
}

