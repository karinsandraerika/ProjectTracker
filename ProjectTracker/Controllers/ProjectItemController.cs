using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Models;

namespace ProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectItemController : ControllerBase
	{
            DatabaseContext DatabaseContext;

            public ProjectItemController(DatabaseContext databaseContext)
            {
                DatabaseContext = databaseContext;
            }

        [HttpGet]
        public  List<ProjectItem> GetProjectItems()
        {
            return DatabaseContext.ProjectItem.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ProjectItem> GetProjectItem(int id)
        {
            ProjectItem item = DatabaseContext.ProjectItem.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item; ;
        }

        [HttpPut("{id}")]
        public IActionResult PutProjectItem(int id, ProjectItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            DatabaseContext.Entry(item).State = EntityState.Modified;

      
            DatabaseContext.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<ProjectItem> PostProjectItem(ProjectItem item)
        {
            
            DatabaseContext.ProjectItem.Add(item);
            DatabaseContext.SaveChanges();

            return CreatedAtAction(nameof(GetProjectItem), new { id = item.Id }, item);
        }
    }
 }
