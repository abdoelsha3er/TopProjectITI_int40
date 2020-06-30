using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherEductionRepositories;

namespace TopProjectITI_int40.Controllers.TeacherControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherEductionController : ControllerBase
    {
        ITeacherEductionRepository _teacherEductionRepository;
        public TeacherEductionController(ITeacherEductionRepository teacherEductionRepository)
        {
            _teacherEductionRepository = teacherEductionRepository;
        }
        ///////////////////////
        // Get AllTeacherEduction(id)
        [Authorize]
        [HttpGet]
        [Route("GetTeacherEductions")]   // id here will get from teacher who is logined on system
        public async Task<IActionResult> GetTeacherEductions()
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
            var teacherEductions = await _teacherEductionRepository.GetTeacherEductions(int.Parse(teacherId));
            if (teacherEductions == null)
            {
                ModelState.AddModelError("Erroe Message : ", "You unAuthorize to Get Data of this Teacher");
                return BadRequest(ModelState);
            }
            return Ok(teacherEductions);
        }
        //////////////////////////////////////////////////
        // Add new TeacherEduction to Teacher 
        [Authorize]
        [HttpPost]
        [Route("AddTeacherEduction")]
        public async Task<ActionResult> AddTeacherEduction([FromForm] TeacherEduction teacherEduction)
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
            if (teacherId==null)
            {
                ModelState.AddModelError("ErrorMessage:", "You are not Authanticated");
                return BadRequest(ModelState);
            }
            await _teacherEductionRepository.AddTeacherEduction(teacherEduction);
            return Created("TeacherEductionTable", teacherEduction);
        }
        ////////////////////
        //Delete
        [HttpDelete]
        [Route("DeleteTeacherEduction/{teacherEducationId}")]
        public async Task<IActionResult> DeleteTeacherEduction( int teacherEducationId)
        {
            TeacherEduction teacherEductionById = await _teacherEductionRepository.GetTeacherEductionById(teacherEducationId);
            if (teacherEductionById == null)
            {
                return Content("not found , please Check!...");
            }
            else
            {
                await _teacherEductionRepository.DeleteTeacherEduction(teacherEductionById);
                return Ok("Deleted Successfully");
            }
        }
    }
}