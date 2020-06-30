using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherPhonesRepositories;

namespace TopProjectITI_int40.Controllers.TeacherControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherPhoneController : ControllerBase
    {
        ITeacherPhonesRepository _teacherPhonesRepository;
        public TeacherPhoneController(ITeacherPhonesRepository teacherPhonesRepository)
        {
            _teacherPhonesRepository = teacherPhonesRepository;
        }
        // Get AllTeacherPhones(id)
        [Authorize]
        [HttpGet]
        [Route("GetTeacherPhones")]   // id here will get from teacher who is logined on system
        public async Task<IActionResult> GetTeacherPhones()
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
            var teachrPhones = await _teacherPhonesRepository.GetTeacherPhones(int.Parse(teacherId));
            if (teachrPhones == null)
            {
                ModelState.AddModelError("Erroe Message : ", "You unAuthorize to Get Data of this Teacher");
                return BadRequest(ModelState);
            }
            return Ok(teachrPhones);
        }
        // Add new Phone to Teacher 
        [Authorize]
        [HttpPost]
        [Route("AddTeacherPhone")]
        public async Task<ActionResult> AddTeacherPhone([FromForm] TeacherPhone teacherPhone)
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
            await _teacherPhonesRepository.AddTeacherPhone(teacherPhone);
            return Created("TeacherPhoneTable", teacherPhone);
        }

        //Edit
        [HttpPut]
        [Route("EditTeacherPhone")]
        public async Task<IActionResult> EditTeacherPhone([FromForm] TeacherPhone teacherPhone)
        {
            TeacherPhone teacherPhoneById = await _teacherPhonesRepository.GetTeacherPhoneById(teacherPhone.TeacherPhoneId); //GetTeacherPhoneById search
            if (teacherPhoneById==null)
            {
                return Content("not found , please Check!...");
            }
            else
            {
                await _teacherPhonesRepository.EditTeacherPhone(teacherPhone);
                return Created("TeacherPhoneTable", teacherPhone);
            }
        }
        //Delete
        [HttpDelete]
        [Route("DeleteTeacherPhone/{teacherPhoneId}")]
        public async Task<IActionResult> DeleteTeacherPhone(int teacherPhoneId)
        {
            // check phoneteache is exist in table or no
            TeacherPhone teacherPhoneById = await _teacherPhonesRepository.GetTeacherPhoneById(teacherPhoneId);
            if (teacherPhoneById == null)
            {
                return Content("not found , please Check!...");
            }
            else
            {
                await _teacherPhonesRepository.DeleteTeacherPhone(teacherPhoneById);
                return Ok("Deleted Successfully");
            }
        }
    }
}