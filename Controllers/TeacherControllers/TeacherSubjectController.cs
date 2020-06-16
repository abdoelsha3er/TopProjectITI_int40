using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        [Route("GetTeacherSubjectst/{teacherId}")]    // TeacherId will Selected by teacher login (token) 
        public async Task<QueryResult<TeacherSubjects>> GetTeacherSubjectst(int teacherId)
        {
            var teachersubects = await _teacherSubjectRepository.GetTeacherSubjectst(teacherId);
            if (teachersubects != null)
            {
                return (teachersubects);
            }
            else
            {
                return null;
            }
        }
        // Add new TeacherSubjec
        [HttpPost]
        [Route("AddTeacherSubject")]
        public async Task<ActionResult> AddTeacherSubject([FromForm] TeacherSubjects teacherSubject)
        {
            if (ModelState.IsValid)
            {

                if (teacherSubject != null)
                {
                    await _teacherSubjectRepository.AddTeacherSubject(teacherSubject);
                    return Created("TeacherSubjectTable", teacherSubject);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        //Delete TeacherSubjec
        [HttpDelete]
        [Route("DeleteTeacherSubject")]
        public async Task<IActionResult> DeleteTeacherSubject([FromForm] TeacherSubjects teacherSubject)
        {
            if (ModelState.IsValid)
            {

                if (teacherSubject != null)
                {
                    await _teacherSubjectRepository.DeleteTeacherSubject(teacherSubject);
                    return Ok("Deleted Successfully");
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