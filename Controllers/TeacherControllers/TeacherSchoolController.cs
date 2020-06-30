using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherSchoolRepositories;

namespace TopProjectITI_int40.Controllers.TeacherControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSchoolController : ControllerBase
    {
        ITeacherSchoolRepository _teacherSchoolRepository;
        public TeacherSchoolController(ITeacherSchoolRepository teacherSchoolRepository)
        {
            _teacherSchoolRepository = teacherSchoolRepository;
        }
        ///////////////////////
        // Get AllTeacherSchool(id)
        [Authorize]
        [HttpGet]
        [Route("GetTeacherSchools")]   // id here will get from teacher who is logined on system
        public async Task<IActionResult> GetTeacherSchools()
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
            var teacherSchools = await _teacherSchoolRepository.GetTeacherSchools(int.Parse(teacherId));
            if (teacherSchools == null)
            {
                ModelState.AddModelError("Erroe Message : ", "You unAuthorize to Get Data of this Teacher");
                return BadRequest(ModelState);
            }
            return Ok(teacherSchools);
        }
        //////////////////////////////////////////////////
        // Add new SocialLinks to Teacher 
        [Authorize]
        [HttpPost]
        [Route("AddTeacherSchool")]
        public async Task<ActionResult> AddTeacherSchool([FromForm] TeacherSchool teacherSchool)
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
            await _teacherSchoolRepository.AddTeacherSchool(teacherSchool);
            return Created("TeacherSchoolTable", teacherSchool);
        }
        ////////////////////
        //Delete
        [HttpDelete]
        [Route("DeleteTeacherSchool/{teacherSchoolId}")]
        public async Task<IActionResult> DeleteTeacherSchool(int teacherSchoolId)
        {
            TeacherSchool teacherSchoolById = await _teacherSchoolRepository.GetTeacherSchoolById(teacherSchoolId);
            if (teacherSchoolById == null)
            {
                return Content("not found , please Check!...");
            }
            await _teacherSchoolRepository.DeleteTeacherSchool(teacherSchoolById);
            return Ok("Deleted Successfully");
        }
    }
}