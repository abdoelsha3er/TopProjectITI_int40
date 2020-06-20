using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.StudentRepo.StudentsGroupsRepositories;

namespace TopProjectITI_int40.Controllers.StudentControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGroupController : ControllerBase
    {
        IStudentsGroupsRepository _studentsGroupsRepository;
        public StudentGroupController(IStudentsGroupsRepository studentsGroupsRepository)
        {
            _studentsGroupsRepository = studentsGroupsRepository;
        }
        // get all studnt groups
        [HttpGet]
        [Route("GetStudentGroups/{studentId}")]
        public async Task<IActionResult> GetStudentGroups(int studentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var studentGroups = await _studentsGroupsRepository.GetStudentGroups(studentId);
                if (studentGroups!=null)
                {
                    return Ok(studentGroups);
                }
                else
                {
                    return NoContent();
                }
            }
        }

        // get Group all students
        [HttpGet]
        [Route("GetGroupStudents/{groupId}")]
        public async Task<IActionResult> GetGroupStudents(int groupId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var groupstudents = await _studentsGroupsRepository.GetGroupStudents(groupId);
                if (groupstudents != null)
                {
                    return Ok(groupstudents);
                }
                else
                {
                    return NoContent();
                }
            }
        }

        // Add Student into group
        [HttpPost]
        [Route("AddStudentToGroup")]
        public async Task<ActionResult> AddStudentToGroup([FromForm] StudentGroup studentGroup)
        {
            if (ModelState.IsValid)
            {
                if (studentGroup != null)
                {
                    await _studentsGroupsRepository.AddStudentToGroup(studentGroup);
                    return Created("StudentGroupctTable", studentGroup);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest(ModelState);
        }

        // Delete Student from Group
        [HttpDelete]
        [Route("DeleteStudntFromGroup/{studentId}")]
        public async Task<IActionResult> DeleteStudntFromGroup(int groupId, int studentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var studentGroup = await _studentsGroupsRepository.GetStudentGroup(groupId,studentId);
                if (studentGroup != null)
                {
                    await _studentsGroupsRepository.DeleteStudntFromGroup(studentGroup);
                    return Ok("Deleted Successfully");
                }
                else
                {
                    return NoContent();
                }
            }
        }
    }
}