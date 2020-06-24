using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
        // get all teacher in system
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
            Teacher teacherById= await _teacherRegisterRepository.GetTeacherById(teacherId);
            var fileName = teacherById.Picture;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (teacherId != 0)
            {
                //teacherById = await _teacherRegisterRepository.GetTeacherById(teacherId);  //GetTeacherPhoneById search
                if (teacherById == null)
                {
                    return Content("not found , please Check!...");
                }
                if (file != null)
                {
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
                    var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolderPath))
                    {
                        Directory.CreateDirectory(uploadsFolderPath);
                    }
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadsFolderPath, fileName);  // filepath

                    if (teacherById.Picture != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", teacherById.Picture);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream); // picture saved to the path (folder)
                            }
                            //teacher.Picture = fileName;
                        }
                    }
                    else
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream); // picture saved to the path (folder)
                            //teacher.Picture = fileName;
                        }
                    }
                    
                }
            }
            // for hashing password
            string password = teacher.Password;
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: password,
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA1,
               iterationCount: 10000,
               numBytesRequested: 256 / 8));
            teacher.Password = hashed;
            await _teacherRegisterRepository.EditTeacherProfile(teacher, teacherId, fileName);
            return Created("TeacherTable", teacherById);
        }
    }
}