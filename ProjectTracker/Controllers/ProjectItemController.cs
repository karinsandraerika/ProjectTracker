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
        private readonly IProjectRepository _projectRepository;  
        private readonly IMapper _mapper;

        public ProjectItemController(IProjectItemRepository projectItemRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _projectItemRepository = projectItemRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        //[ProducesResponseType(200, Type =typeof(IEnumerable<ProjectItem>))]
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

            var projectItem = _mapper.Map<ProjectItemDto>(_projectItemRepository.GetProjectItem(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(projectItem);
        }

        [HttpPost]
        public IActionResult PostProjectItem([FromQuery] int projectId, [FromBody] ProjectItemDto newProjectItem)
        {
            if (newProjectItem == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectItemMap = _mapper.Map<ProjectItem>(newProjectItem);
            projectItemMap.Project = _projectRepository.GetProject(projectId);

            if (!_projectItemRepository.CreateProjectItem(projectItemMap))
            {
                ModelState.AddModelError("", "Something went wrong hile saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Sucessfully added");
        }

        [HttpPut("{projectItemId}")]
        public IActionResult UpdateProject(int projectItemId, [FromBody] ProjectItemDto projectItemInfo)
        {
            if (projectItemInfo == null)
            {
                return BadRequest(ModelState);
            }

            if (projectItemId != projectItemInfo.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_projectItemRepository.ProjectItemExists(projectItemId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var projectItemMap = _mapper.Map<ProjectItem>(projectItemInfo);

            if (!_projectItemRepository.UpdateProjectItem(projectItemMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            //return NoContent();
            return Ok("Succesfully updated");
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
