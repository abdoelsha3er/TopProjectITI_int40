using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        [Route("GetTeacherSchools")]   // id here will get from teacher who is logined on system
        public async Task<QueryResult<TeacherSchool>> GetTeacherSchools([FromForm] int teacherId)
        {
            var teacherSchools = await _teacherSchoolRepository.GetTeacherSchools(teacherId);
            if (teacherSchools != null)
            {
                return (teacherSchools);
            }
            else
            {
                return null;
            }
        }
        //////////////////////////////////////////////////
        // Add new SocialLinks to Teacher 
        [HttpPost]
        [Route("AddTeacherSchool")]
        public async Task<ActionResult> AddTeacherSchool([FromForm] TeacherSchool teacherSchool)
        {
            if (ModelState.IsValid)
            {

                if (teacherSchool != null)
                {
                    await _teacherSchoolRepository.AddTeacherSchool(teacherSchool);
                    return Created("TeacherSchoolTable", teacherSchool);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        ////////////////////
        //Delete
        [HttpDelete]
        [Route("DeleteTeacherSchool")]
        public async Task<IActionResult> DeleteTeacherSchool([FromForm] TeacherSchool teacherSchool)
        {
            TeacherSchool teacherSchoolById = await _teacherSchoolRepository.GetTeacherSchoolById(teacherSchool.TeacherSchoolId);
            if (teacherSchoolById == null)
            {
                return Content("not found , please Check!...");
            }
            else
            {
                await _teacherSchoolRepository.DeleteTeacherSchool(teacherSchoolById);
                return Ok("Deleted Successfully");
            }
        }
    }
}