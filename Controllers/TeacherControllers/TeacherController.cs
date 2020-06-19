using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherRegisterRepositories;

namespace TopProjectITI_int40.Controllers.TeacherControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        ITeacherRegisterRepository _teacherRegisterRepository;
        PhotoSettings _photoSetting;
        IWebHostEnvironment _host;
        public TeacherController(ITeacherRegisterRepository teacherRegisterRepository, IOptionsSnapshot<PhotoSettings> options, IWebHostEnvironment host)
        {
            _teacherRegisterRepository = teacherRegisterRepository;
            this._photoSetting = options.Value; //  
            _host = host;
        }

        [HttpGet]
        [Route("GetTeachers")]
        public async Task<IEnumerable<Teacher>> GetTeachers()
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Teacher> teachers = await _teacherRegisterRepository.GetTeachers();
                if (teachers != null)
                    return teachers;
                return null;
            }
            return null;
        }

        // get by id
        [HttpGet]
        [Route("GetTeacherById/{teacherId}")]
        public async Task<IActionResult> GetTeacherById(int teacherId)
        {
            if (ModelState.IsValid)
            {
                var teacher = await _teacherRegisterRepository.GetTeacherById(teacherId);
                if (teacher != null)
                    return Ok(teacher);
                return null;
            }
            return null;
        }


        // Register to Teacher 
        [HttpPost]
        [Route("TeacherRegister")]
        public async Task<ActionResult> TeacherRegister([FromForm] Teacher teacher, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (teacher == null)
            {
                return NotFound();
            }
            if (file.Length == 0)
            {
                return BadRequest("Empty file");    // mandatory
            }
            if (file.Length > _photoSetting.MaxBytes)
            {
                return BadRequest("Max file size exceeded");
            }
            if (!_photoSetting.IsSupported(file.FileName))
            {
                return BadRequest("Invalid file type");
            }
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads"); //wwwroot
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            //datetimenow numbber in node da
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);  // filepath

            using (var stream = new FileStream(filePath, FileMode.Create))  // create  pricture
            {
                await file.CopyToAsync(stream); // picture saved to the path (folder)
            }
            teacher.Picture = fileName;
            await _teacherRegisterRepository.TeacherRegister(teacher);
            return Created("TeacherTable", teacher);
        }
        //Edit
        [HttpPut]
        [Route("EditTeacherProfile/{teacherId}")]
        public async Task<IActionResult> EditTeacherProfile([FromForm] Teacher teacher, int teacherId, IFormFile file)
        {
            Teacher teacherById;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (teacherId != 0)
            {
                teacherById = await _teacherRegisterRepository.GetTeacherById(teacherId);  //GetTeacherPhoneById search
                if (teacherById == null)
                {
                    return Content("not found , please Check!...");
                }
                else if (teacherById.Picture != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\EductionalCenterPictures", teacherById.Picture);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
            }
            if (teacher == null)
            {
                return Content("not found , please Check!...");
            }
            if (file.Length == 0)
            {
                return BadRequest("Empty file");
            }
            if (file.Length > _photoSetting.MaxBytes)
            {
                return BadRequest("Max file size exceeded");
            }
            if (!_photoSetting.IsSupported(file.FileName))
            {
                return BadRequest("Invalid file type");
            }
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "EductionalCenterPictures");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);  // filepath

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream); // picture saved to the path (folder)
            }
            teacher.Picture = fileName;
            await _teacherRegisterRepository.EditTeacherProfile(teacher, teacherId);
            return Created("TeacherTable", teacher);
        }
    }
}