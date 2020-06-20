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
using TopProjectITI_int40.Repository.StudentRepo.StudentRepositories;

namespace TopProjectITI_int40.Controllers.StudentControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudentRepository _studentRepository;
        IWebHostEnvironment _host;
        PhotoSettings _photoSetting;
        public StudentController(IStudentRepository studentRepository, IOptionsSnapshot<PhotoSettings> options, IWebHostEnvironment host)
        {
            _studentRepository = studentRepository;
            _photoSetting = options.Value;
            _host = host;
        }
        // Get All Students in system
        [HttpGet]
        [Route("GetAllStudents")]
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Student> students = await _studentRepository.GetAllStudents();
                if (students != null)
                    return students;
                return null;
            }
            return null;
        }


        [HttpPost]
        [Route("AddStudent")]
        public async Task<ActionResult> AddStudent([FromForm] Student student, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (student == null)
            {
                return NotFound();
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
                var uploadsFolderPath = Path.Combine(_host.WebRootPath, "StudentImages");
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

                student.Picture = fileName;
            }
            await _studentRepository.AddStudent(student);
            return Created("Student Table", student);
        }
        [HttpPut]
        [Route("EditStudent/{StudentId}")]
        public async Task<ActionResult> editStudent([FromForm] Student student, int StudentId)
        {
            Student studentById = await _studentRepository.GetStudentById(StudentId);
            if (studentById != null)
            {
                await _studentRepository.EditStudent(studentById, student);
                return Ok(studentById);
            }
            return NotFound();
        }
        // Delete Parent by Id

        // Check login 
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> LoginStudent([FromForm] string UserName, [FromForm]string Password, [FromForm] string email)
        {
            if (ModelState.IsValid)
            {
                Student student = await _studentRepository.CheckStudentLogin(UserName, Password, email);
                if (student != null)
                {
                    return Created("Student Table", student);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        // get Details by id
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> selectStudentDetails(int id)
        {
            if (ModelState.IsValid)
            {
                Student student = await _studentRepository.studentDetails(id);
                if (student != null)
                {
                    return Ok(student);
                }

                return NotFound();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Students/{id}")]
        public async Task<IEnumerable<Student>> GetAllStudents(int id)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Student> students = await _studentRepository.GetStudents(id);
                if (students != null)
                    return students;
                return null;
            }
            return null;

        }
    }
}
