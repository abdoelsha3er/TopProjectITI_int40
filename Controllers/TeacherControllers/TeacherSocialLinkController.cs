using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherSocialLinkRepositories;

namespace TopProjectITI_int40.Controllers.TeacherControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSocialLinkController : ControllerBase
    {
        ITeacherSocialLinkRepository _teacherSocialLinkRepository;
        public TeacherSocialLinkController(ITeacherSocialLinkRepository teacherSocialLinkRepository)
        {
            _teacherSocialLinkRepository = teacherSocialLinkRepository;
        }
        ///////////////////////
        // Get AllTeacherSubjectsNotAssign(id)
        [HttpGet]
        [Route("GetTeacherSocialLinks")]   // id here will get from teacher who is logined on system
        public async Task<QueryResult<TeacherSocialLink>> GetTeacherSocialLinks([FromForm] int teacherId)
        {
            var teacherSocialLinks = await _teacherSocialLinkRepository.GetTeacherSocialLinks(teacherId);
            if (teacherSocialLinks != null)
            {
                return (teacherSocialLinks);
            }
            else
            {
                return null;
            }
        }
        //////////////////////////////////////////////////
        // Add new SocialLinks to Teacher 
        [HttpPost]
        [Route("AddTeacherSocialLink")]
        public async Task<ActionResult> AddTeacherSocialLink([FromForm] TeacherSocialLink teacherSocialLink)
        {
            if (ModelState.IsValid)
            {

                if (teacherSocialLink != null)
                {
                    await _teacherSocialLinkRepository.AddTeacherSocialLink(teacherSocialLink);
                    return Created("TeacherSocialLinkTable", teacherSocialLink);
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
        [Route("DeleteTeacherSocialLink/{teacherSocialLinkId}")]
        public async Task<IActionResult> DeleteTeacherSocialLink(int teacherSocialLinkId)
        {
            // check phoneteache is exist in table or no
            TeacherSocialLink teacherSocialLinkById = await _teacherSocialLinkRepository.GetTeacherSocialLinkById(teacherSocialLinkId);
            if (teacherSocialLinkById == null)
            {
                return Content("not found , please Check!...");
            }
            else
            {
                await _teacherSocialLinkRepository.DeleteTeacherSocialLink(teacherSocialLinkById);
                return Ok("Deleted Successfully");
            }
        }
    }
}