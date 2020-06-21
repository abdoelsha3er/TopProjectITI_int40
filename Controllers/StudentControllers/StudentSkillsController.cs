using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.StudentRepo.StudentSkillsRepositories;

namespace TopProjectITI_int40.Controllers.StudentControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSkillsController : ControllerBase
    {
        IStudentSillsRepository _studentSkillsRepository;
        public StudentSkillsController(IStudentSillsRepository studentSkillsRepository)
        {
            _studentSkillsRepository = studentSkillsRepository;
        }
        // Get AllStudentSkills(id)
        [HttpGet]
        [Route("GetStudentSkills/{studentId}")]
        public async Task<IEnumerable<StudentSkill>> GetStudentSkills(int studentId)
        {
            var studentSkill = await _studentSkillsRepository.GetStudentSkills(studentId);
            if (studentSkill != null)
            {
                return (studentSkill);
            }
            else
            {
                return null;
            }
        }
        // Add new Skill to Student
        [HttpPost]
        [Route("AddStudentSkills")]
        public async Task<ActionResult> addStudentSkills([FromForm] StudentSkill studentSkill)
        {
            if (ModelState.IsValid)
            {
                if (studentSkill != null)
                {
                    await _studentSkillsRepository.AddStudentSkill(studentSkill);
                    return Created("Student Skills Table", studentSkill);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
    }
}