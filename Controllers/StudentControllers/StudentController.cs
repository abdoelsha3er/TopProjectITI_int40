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

        // get all students by gradeid
        [HttpGet]
        [Route("GetStudentsByGradeId/{gradeId}")]
        public async Task<IEnumerable<Student>> GetStudentsByGradeId(int gradeId)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Student> students = await _studentRepository.GetStudentsByGradeId(gradeId);
                if (students != null)
                    return students;
                return null;
            }
            return null;

        }

        // Register New Studet
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


        // Edit
        [HttpPut]
        [Route("EditStudent/{ParentId}/{StudentId}")]
        public async Task<IActionResult> EditStudentProfile([FromForm] Student student, int ParentId, int StudentId, IFormFile file)
        {
            Student studentById;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (StudentId == 0 && ParentId == 0)
            {
                return NotFound();
            }
            studentById = await _studentRepository.GetStudentById(StudentId);  //GetTeacherPhoneById search
            if (studentById == null && studentById.ParentId != ParentId)
            {
                return Content("not found , please Check!...");
            }
            else if (studentById.Picture != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\StudentImages", studentById.Picture);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            if (student == null)
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
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "StudentImages");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//af08e9df-721d-4020-96cb-5c5c705e3db3.jpg
            var filePath = Path.Combine(uploadsFolderPath, fileName);  // filepath

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream); // picture saved to the path (folder)
            }
            student.Picture = fileName;
            await _studentRepository.EditStudent(student, StudentId);
            return Ok(studentById);

        }



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
        [Route("Details/{parentId}/{studentId}")]
        public async Task<ActionResult> selectStudentDetails(int parentId, int studentId)
        {
            if (ModelState.IsValid)
            {
                Student student = await _studentRepository.studentDetails(studentId);
                if (student != null && student.ParentId == parentId)
                {
                    int i = GetAge(DateTime.Now, student.DateOfBirth);
                    if (i == 0)
                        i = 1;
                    student.DateOfBirth = new DateTime(1, 1, i);
                    return Created("Student Table", student);
                }
                return NotFound();
            }

            return BadRequest();
        }

        // Get Age 
        public static int GetAge(DateTime reference, DateTime birthday)
        {
            int age = reference.Year - birthday.Year;
            if (reference < birthday.AddYears(age)) age--;

            return age;
        }

    }
}
