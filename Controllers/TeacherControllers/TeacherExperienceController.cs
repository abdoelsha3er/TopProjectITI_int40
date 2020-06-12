using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherExperienceRepositories;

namespace TopProjectITI_int40.Controllers.TeacherControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherExperienceController : ControllerBase
    {
        ITeacherExperienceRepository _teacherExperienceRepository;
        public TeacherExperienceController(ITeacherExperienceRepository teacherExperienceRepository)
        {
            _teacherExperienceRepository = teacherExperienceRepository;
        }
        // Get AllTeacherSubjectsNotAssign(id)
        [HttpGet]
        [Route("GetTeacherExperience")]   // id here will get from teacher who is logined on system
        public async Task<QueryResult<TeacherExperience>> GetTeacherExperience([FromForm] int teacherId)
        {
            var teachrExperiences = await _teacherExperienceRepository.GetTeacherExperience(teacherId);
            if (teachrExperiences != null)
            {
                return (teachrExperiences);
            }
            else
            {
                return null;
            }
        }

        // Add new Experience to Teacher 
        [HttpPost]
        [Route("AddTeacherExperience")]
        public async Task<ActionResult> AddTeacherExperience([FromForm] TeacherExperience teacherExperience)
        {
            if (ModelState.IsValid)
            {

                if (teacherExperience != null)
                {
                    await _teacherExperienceRepository.AddTeacherExperience(teacherExperience);
                    return Created("TeacherExperienceTable", teacherExperience);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        //Edit
        [HttpPut]
        [Route("EditTeacherPhone")]
        public async Task<IActionResult> EditTeacherExperience([FromForm] TeacherExperience teacherExperience)
        {
            TeacherExperience teacherExperienceById = await _teacherExperienceRepository.GetTeacherExperienceById(teacherExperience.TeacherExperienceId);
            if (teacherExperienceById == null)
            {
                return Content("not found , please Check!...");
            }
            else
            {
                await _teacherExperienceRepository.EditTeacherExperience(teacherExperience);
                return Created("TeacherExperienceTable", teacherExperience);
            }
        }

        //Delete
        [HttpDelete]
        [Route("DeleteTeacherExperience")]
        public async Task<IActionResult> DeleteTeacherExperience([FromForm] TeacherExperience teacherExperience)
        {
            // check phoneteache is exist in table or no
            TeacherExperience teacherExperienceById = await _teacherExperienceRepository.GetTeacherExperienceById(teacherExperience.TeacherExperienceId);
            if (teacherExperienceById == null)
            {
                return Content("not found , please Check!...");
            }
            else
            {
                await _teacherExperienceRepository.DeleteTeacherExperience(teacherExperience);
                return Ok("Deleted Successfully");
            }
        }
    }
}