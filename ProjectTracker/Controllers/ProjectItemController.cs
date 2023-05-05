using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Dto;
using ProjectTracker.Interfaces;
using ProjectTracker.Models;
using ProjectTracker.Repository;

namespace ProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectItemController : ControllerBase
	{
        private readonly IProjectItemRepository _projectItemRepository;
        //DatabaseContext DatabaseContext; //Can this be removed. db in repository
        private readonly IMapper _mapper;

        public ProjectItemController(IProjectItemRepository projectItemRepository,
            IMapper mapper)
        {
            _projectItemRepository = projectItemRepository;
            //DatabaseContext = databaseContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type =typeof(IEnumerable<ProjectItem>))]
        public IActionResult GetProjectItems()
        {
            var projectItems = _mapper.Map<List<ProjectItemDto>>(_projectItemRepository.GetProjectItems());
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(projectItems);
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectItem(int id)
        {
            if (!_projectItemRepository.ProjectItemExists(id))
            {
                return NotFound();
            }

            var projectItem = _projectItemRepository.GetProjectItem(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(projectItem);
        }

        /*
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
        */
    }
}
