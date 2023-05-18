using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Data;
using ProjectTracker.Dto;
using ProjectTracker.Interfaces;
using ProjectTracker.Models;
using ProjectTracker.Repository;


namespace ProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectController(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            //var projects = _mapper.Map<List<ProjectDto>>(_projectRepository.GetProjects());
            var projects = _projectRepository.GetProjects();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult GetProject(int id)
        {
            if (!_projectRepository.ProjectExists(id))
            {
                return NotFound();
            }

            //var project = _mapper.Map<ProjectDto>(_projectRepository.GetProject(id));
            var project = _projectRepository.GetProject(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(project);
        }

        [HttpPost]
        public IActionResult PostProject([FromBody] ProjectDto newProject)
        {
            if (newProject == null)
            {
                return BadRequest(ModelState);
            }
            //TODO project name
            var project = _projectRepository.GetProjects().Where(p => p.Name.Trim().ToUpper() == newProject.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (project != null)
            {
                ModelState.AddModelError("", "Project already exists");
                return StatusCode(422, ModelState);
            }
         
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           // var projectMap = _mapper.Map<Project>(newProject);
       
            if (!_projectRepository.CreateProject(newProject))
            {
                ModelState.AddModelError("", "Something went wrong hile saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Sucessfully added");
        }

        [HttpPut("{projectId}")]
        public IActionResult UpdateProject(int projectId, [FromBody] ProjectDto projectInfo)
        {
            if (projectInfo == null)
            {
                return BadRequest(ModelState);
            }

            if (projectId != projectInfo.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_projectRepository.ProjectExists(projectId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var projectMap = _mapper.Map<Project>(projectInfo);

            if (!_projectRepository.UpdateProject(projectInfo))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            //return NoContent();
            return Ok("Succesfully updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            if (!_projectRepository.ProjectExists(id))
            {
                return NotFound();
            }

            var projectDelete = _projectRepository.GetProject(id);
            //var personDelete = _mapper.Map<Person>(_personRepository.GetPerson(id));
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_projectRepository.DeleteProject(projectDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully deleted");
        }

    }
}

