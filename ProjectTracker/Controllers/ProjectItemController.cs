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
        private readonly IMapper _mapper;

        public ProjectItemController(IProjectItemRepository projectItemRepository, IMapper mapper)
        {
            _projectItemRepository = projectItemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProjectItems()
        {
            var projectItems = _projectItemRepository.GetProjectItems();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

        [HttpPost]
        public IActionResult PostProjectItem([FromBody] ProjectItemDto newProjectItem)
        {
            if (newProjectItem == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_projectItemRepository.CreateProjectItem(newProjectItem))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully added" });
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
                return BadRequest(ModelState);
            }

            if (!_projectItemRepository.UpdateProjectItem(projectItemInfo))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProjectItem(int id)
        {
            if (!_projectItemRepository.ProjectItemExists(id))
            {
                return NotFound();
            }
            
            var projectItemDelete = _projectItemRepository.GetProjectItem(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_projectItemRepository.DeleteProjectItem(projectItemDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully deleted" });
        }

    }
}
