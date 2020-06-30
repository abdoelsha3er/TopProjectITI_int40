using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        [Route("GetTeacherExperience")]   // id here will get from teacher who is logined on system
        public async Task<IActionResult> GetTeacherExperience()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ErrorMessage:", "You unAuthorize to Get Data of this Teacher");
                return BadRequest(ModelState);
            }
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                ModelState.AddModelError("ErrorMessage:", "You are not Authanticated");
                return BadRequest(ModelState);
            }
            IEnumerable<Claim> claims = identity.Claims;
            var teacherId = claims.Where(p => p.Type == "TeacherId").FirstOrDefault()?.Value;
            var teachrExperiences = await _teacherExperienceRepository.GetTeacherExperience(int.Parse(teacherId));
            if (teachrExperiences == null)
            {
                ModelState.AddModelError("Erroe Message : ", "You unAuthorize to Get Data of this Teacher");
                return BadRequest(ModelState);
            }
            return Ok(teachrExperiences);
        }

        // Add new Experience to Teacher 
        [Authorize]
        [HttpPost]
        [Route("AddTeacherExperience")]
        public async Task<ActionResult> AddTeacherExperience([FromForm] TeacherExperience teacherExperience)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ErrorMessage:", "You unAuthorize to Get Data of this Teacher");
                return BadRequest(ModelState);
            }
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                ModelState.AddModelError("ErrorMessage:", "You are not Authanticated");
                return BadRequest(ModelState);
            }
            IEnumerable<Claim> claims = identity.Claims;
            var teacherId = claims.Where(p => p.Type == "TeacherId").FirstOrDefault()?.Value;
            if (teacherId == null)
            {
                ModelState.AddModelError("ErrorMessage:", "You are not Authanticated");
                return BadRequest(ModelState);
            }
            await _teacherExperienceRepository.AddTeacherExperience(teacherExperience);
            return Created("TeacherExperienceTable", teacherExperience);
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
        [Route("DeleteTeacherExperience/{teacherExperienceId}")]
        public async Task<IActionResult> DeleteTeacherExperience(int teacherExperienceId)
        {
            // check phoneteache is exist in table or no
            TeacherExperience teacherExperienceById = await _teacherExperienceRepository.GetTeacherExperienceById(teacherExperienceId);
            if (teacherExperienceById == null)
            {
                return Content("not found , please Check!...");
            }
            else
            {
                await _teacherExperienceRepository.DeleteTeacherExperience(teacherExperienceById);
                return Ok("Deleted Successfully");
            }
        }
    }
}