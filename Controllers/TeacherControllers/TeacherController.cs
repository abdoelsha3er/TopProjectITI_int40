using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherRegisterRepositories;
using TopProjectITI_int40.ViewModels;

namespace TopProjectITI_int40.Controllers.TeacherControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        ITeacherRegisterRepository _teacherRegisterRepository;
        PhotoSettings _photoSetting;
        IWebHostEnvironment _host;
        IConfiguration _configuration;
        public TeacherController(ITeacherRegisterRepository teacherRegisterRepository, 
                                 IOptionsSnapshot<PhotoSettings> options, 
                                 IWebHostEnvironment host,
                                 IConfiguration configuration)
        {
            _teacherRegisterRepository = teacherRegisterRepository;
            _photoSetting = options.Value; //  
            _host = host;
            _configuration = configuration;
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
        [Authorize]
        // get by id
        [HttpGet]
        [Route("GetTeacherById")]
        public async Task<IActionResult> GetTeacherById()
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
            var teacher = await _teacherRegisterRepository.GetTeacherById(int.Parse(teacherId));

            if (teacher == null)
            {
                ModelState.AddModelError("Erroe Message : ", "You unAuthorize to Get Data of this Teacher");
                return BadRequest(ModelState);
            }
            return Ok(new { teacher = teacher });
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
        [Route("EditTeacherProfile")]
        public async Task<IActionResult> EditTeacherProfile([FromForm] Teacher teacher, IFormFile file)
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
            if (teacherId==null)   // check teacher is exists or no
            {
                ModelState.AddModelError("ErrorMessage:", "You are not Authanticated");
                return BadRequest(ModelState);
            }
            // teacher in DB
            var teacherById = await _teacherRegisterRepository.GetTeacherById(int.Parse(teacherId));
            var fileName = teacherById.Picture;  // old picture

            if (int.Parse(teacherId) != 0)
            {
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
            await _teacherRegisterRepository.EditTeacherProfile(teacher, teacherById.TeacherId, fileName);
            return Created("TeacherTable", teacherById);
        }

        //Login Teacher
        [HttpPost]
        [Route("LoginTeacher")]
        public async Task<IActionResult> LoginTeacher([FromForm] LoginViewModel teacherViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var check = await _teacherRegisterRepository.LoginTeacher(teacherViewModel);
            if (check==null)
            {
                ModelState.AddModelError("", "Invalid User Name");
                return BadRequest(ModelState);
            }
            if (check.Password != teacherViewModel.Password)
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return BadRequest(ModelState);
            }
            // If Found 
            // create JWT Token
            string key = _configuration.GetSection("JwtConfig").GetSection("secret").Value; //Secret key which will be used later during validation   
            var issuer = "http://localhost:6853";  //normally this will be your site URL   

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short   
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, check.TeacherId.ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("TeacherId", check.TeacherId.ToString()));
            permClaims.Add(new Claim("UserName", check.UserName));
            permClaims.Add(new Claim("Email", check.Email));
            permClaims.Add(new Claim("Table", "Teachers"));

            //Create Security Token object by giving required parameters   
            var token = new JwtSecurityToken(issuer, //Issure   
                            issuer,  //Audience   
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = jwt_token });
        }
    }
}