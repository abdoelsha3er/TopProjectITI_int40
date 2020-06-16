using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.AdminRepo.SubjectRepositories;

namespace TopProjectITI_int40.Controllers.AdminControllers
{
    [Route("api/[controller]")]     /// here i used async , and routes of methods
    [ApiController]
    public class SubjectController : ControllerBase
    {
        ISubjectRepository _subjectRepository;
        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        // GetAll
        //[HttpGet("GetSubjects")]
        [HttpGet]
        [Route("GetSubjects")]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _subjectRepository.GetSubjects();
            if (subjects == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(subjects);
            }
        }
        // GetBy id
        [HttpGet]
        [Route("GetSubjectById/{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var subject = await _subjectRepository.GetSubjectById(id);
            if (subject != null)
            {
                return Ok(subject);
            }
            else
            {
                return Content("Sorry !, no subject found, please check you data input");
            }
        }
        // GetBy id
        [HttpGet]
        [Route("GetSubjectsByCategoryId/{catid}")]   // GetSubjectsByCategoryId
        public async Task<IActionResult> GetSubjectsByCategoryId( int catid)
        {
            var subjects = await _subjectRepository.GetSubjectsByCategoryId(catid);
            if (subjects != null)
            {
                return Ok(subjects);
            }
            else
            {
                return Content("Sorry !, no subject found, please check you data input");
            }
        }
        [HttpPost]
        [Route("AddSubject")]
        public async Task<ActionResult> AddSubject([FromForm] Subject subject)
        {
            if (ModelState.IsValid)
            {
                
                if (subject !=null)
                {
                    await _subjectRepository.AddSubject(subject);
                    return Created("SubjectTable", subject);
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
        [Route("EditSubject/{id}")]
        public async Task<IActionResult> EditSubject([FromForm] Subject subject)
        {
            if (ModelState.IsValid)
            {
                Subject subj = await _subjectRepository.GetSubjectById(subject.SubjectId);
                if (subj != null)
                {
                    await _subjectRepository.EditSubject(subject);
                    return Ok(subject);
                }
                return NotFound();
            }
            return BadRequest();
        }
        //Delete
        [HttpDelete]
        [Route("DeleteSubject")]
        public async Task<IActionResult> DeleteSubject([FromForm] Subject subject)
        {
            if (ModelState.IsValid)
            {

                if (subject != null)
                {
                    await _subjectRepository.DeleteSubject(subject);
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