using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherSubjectRepositories;

namespace TopProjectITI_int40.Controllers.TeacherControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSubjectController : ControllerBase
    {
        ITeacherSubjectRepository _teacherSubjectRepository;
        public TeacherSubjectController(ITeacherSubjectRepository teacherSubjectRepository)
        {
            _teacherSubjectRepository = teacherSubjectRepository;
        }
        // Get AllTeacherSubjectsNotAssign(id)
        [HttpGet]
        [Route("GetTeacherSubjectsNotAssign")]   // id here will get from teacher who is logined on system
        public async Task<QueryResult<TeacherSubjects>> GetTeacherSubjectsNotAssign([FromForm] int id, [FromForm] int csid)
        {
            var teacherSubectsNotAssign = await _teacherSubjectRepository.GetTeacherSubjectsNotAssign(id, csid);
            if (teacherSubectsNotAssign != null)
            {
                return (teacherSubectsNotAssign);
            }
            else
            {
                //return (teacherSubectsNotAssign);
                return null;
            }
        }
        // Get GetTeacherSubjectstById(id)
        [Authorize]
        [HttpGet]
        [Route("GetTeacherSubjectst")]    // TeacherId will Selected by teacher login (token) 
        public async Task<IActionResult> GetTeacherSubjects()
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
            var teachersubects = await _teacherSubjectRepository.GetTeacherSubjects(int.Parse(teacherId));
            if (teachersubects == null)
            {
                ModelState.AddModelError("Erroe Message : ", "You unAuthorize to Get Data of this Teacher");
                return BadRequest(ModelState);
            }
            return Ok(teachersubects);
        }
        // Add new TeacherSubjec
        [Authorize]
        [HttpPost]
        [Route("AddTeacherSubject")]
        public async Task<ActionResult> AddTeacherSubject([FromForm] TeacherSubjects teacherSubject)
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
            await _teacherSubjectRepository.AddTeacherSubject(teacherSubject);
            return Created("TeacherSubjectTable", teacherSubject);
               
        }
        //Delete TeacherSubjec
        [Authorize]
        [HttpDelete]
        [Route("DeleteTeacherSubject/{subjectId}")]
        public async Task<IActionResult> DeleteTeacherSubject(int subjectId)
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
            var teacherSubjectById = await _teacherSubjectRepository.GetTeacherSubject(int.Parse(teacherId), subjectId);
            if (teacherSubjectById == null)
            {
                ModelState.AddModelError("Erroe Message : ", "You unAuthorize to Get Data of this Teacher");
                return BadRequest(ModelState);
            }
            await _teacherSubjectRepository.DeleteTeacherSubject(teacherSubjectById);
            return Ok("Deleted Successfully");        
        }
    }
}