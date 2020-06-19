using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.AdminRepo.SubjectRepositories;
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
        [HttpGet]
        [Route("GetTeacherPhones/{teacherId}")]   // id here will get from teacher who is logined on system
        public async Task<QueryResult<TeacherPhone>> GetTeacherPhones(int teacherId)
        {
            var teachrPhoness = await _teacherPhonesRepository.GetTeacherPhones(teacherId);
            if (teachrPhoness != null)
            {
                return (teachrPhoness);
            }
            else
            {
                //return (teacherSubectsNotAssign);
                return null;
            }
        }
        // Add new Phone to Teacher 
        [HttpPost]
        [Route("AddTeacherPhone")]
        public async Task<ActionResult> AddTeacherPhone([FromForm] TeacherPhone teacherPhone)
        {
            // check phone number is there exists in database 
            var isExistNumber = _teacherPhonesRepository.CheckPhoneExists(teacherPhone.TeacherPhoneNumber);
            if (isExistNumber.Result != null)
            {
                return Content("this phone number exists as before!!!. Please Write Another Phone");
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