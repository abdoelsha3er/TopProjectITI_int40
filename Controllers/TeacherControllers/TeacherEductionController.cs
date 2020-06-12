using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        [Route("GetTeacherEductions")]   // id here will get from teacher who is logined on system
        public async Task<QueryResult<TeacherEduction>> GetTeacherEductions([FromForm] int teacherId)
        {
            var teacherEductions = await _teacherEductionRepository.GetTeacherEductions(teacherId);
            if (teacherEductions != null)
            {
                return (teacherEductions);
            }
            else
            {
                return null;
            }
        }
        //////////////////////////////////////////////////
        // Add new TeacherEduction to Teacher 
        [HttpPost]
        [Route("AddTeacherEduction")]
        public async Task<ActionResult> AddTeacherEduction([FromForm] TeacherEduction teacherEduction)
        {
            if (ModelState.IsValid)
            {

                if (teacherEduction != null)
                {
                    await _teacherEductionRepository.AddTeacherEduction(teacherEduction);
                    return Created("TeacherEductionTable", teacherEduction);
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
        [Route("DeleteTeacherEduction")]
        public async Task<IActionResult> DeleteTeacherEduction([FromForm] TeacherEduction teacherEduction)
        {
            TeacherEduction teacherEductionById = await _teacherEductionRepository.GetTeacherEductionById(teacherEduction.TeacherEductionId);
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