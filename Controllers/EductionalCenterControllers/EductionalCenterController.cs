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
using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterRepositories;
using TopProjectITI_int40.ViewModels;

namespace TopProjectITI_int40.Controllers.EductionalCenterControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EductionalCenterController : ControllerBase
    {
        IEductionalCenterRepository _eductionalCenterRepository;
        PhotoSettings _photoSetting;
        IWebHostEnvironment _host;
        IConfiguration _configuration;
        public EductionalCenterController(IEductionalCenterRepository eductionalCenterRepository, 
                                          IOptionsSnapshot<PhotoSettings> options, 
                                          IWebHostEnvironment host,
                                          IConfiguration configuration)
        {
            _eductionalCenterRepository = eductionalCenterRepository;
            this._photoSetting = options.Value; //  
            _host = host;
            _configuration = configuration;
        }

        // Get All Eductional Centers
        [HttpGet]
        [Route("GetEductionalCenters")]
        public async Task<IActionResult> GetEductionalCenters()
        {
            IEnumerable<EductionalCenter> eductionalCenters = await _eductionalCenterRepository.GetEductionalCenters();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(eductionalCenters);
            }
        }

        //Get AllTeacherEduction(id)
        [Authorize]
        [HttpGet]
        [Route("GetEductionalCenterById")]   // id here will get from teacher who is logined on system
        public async Task<IActionResult> GetEductionalCenterById()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error Message :", "You unAuthorize to Get Data of this Teacher");
                return BadRequest(ModelState);
            }
            else
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var eductionalCenterId = claims.Where(p => p.Type == "EductionalCenterId").FirstOrDefault()?.Value;
                    var eductionalCenter = await _eductionalCenterRepository.GetEductionalCenterById(int.Parse(eductionalCenterId));

                    if (eductionalCenter == null)
                    {
                        ModelState.AddModelError("Erroe Message : ", "You unAuthorize to Get Data of this Teacher");
                        return BadRequest(ModelState);
                    }
                    return Ok(new { EductionalCenter = eductionalCenter });
                }
            }
            return BadRequest();
        }

        //Get AllTeacherEduction(id)
        [HttpGet]
        [Route("GetEductionalCenterById/{eductionalCenterId}")]   // id here will get from teacher who is logined on system
        public async Task<IActionResult> GetEductionalCenterById(int eductionalCenterId)
        {
            var eductionalcenter = await _eductionalCenterRepository.GetEductionalCenterById(eductionalCenterId);
            if (eductionalcenter != null)
            {
                return Ok(eductionalcenter);  //status 200
            }
            else
            {
                return null;
            }
        }

        // Register new EductionalCenter 
        [HttpPost]
        [Route("AddEductionalCenter")]
        public async Task<ActionResult> AddEductionalCenter([FromForm] EductionalCenter eductionalCenter, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (eductionalCenter == null)
            {
                return NotFound();
            }
            if (file!=null)
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
                eductionalCenter.Picture = fileName;
            }
            else
            {
                eductionalCenter.Picture = "";  // can make default picture
            }

            await _eductionalCenterRepository.AddEductionalCenter(eductionalCenter);
            return Created("TeacherTable", eductionalCenter);
        }

        //Edit
        [Authorize]
        [HttpPut]
        [Route("EditEductionalCenter")]
        public async Task<IActionResult> EditEductionalCenter([FromForm] EductionalCenter eductionalCenter,  IFormFile file)
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
            var eductionalCenterId = claims.Where(p => p.Type == "EductionalCenterId").FirstOrDefault()?.Value;
            if (eductionalCenterId == null)   // check teacher is exists or no
            {
                ModelState.AddModelError("ErrorMessage:", "You are not Authanticated");
                return BadRequest(ModelState);
            }

            var editEductionalCenterById = await _eductionalCenterRepository.GetEductionalCenterById(int.Parse(eductionalCenterId));
            var fileName = editEductionalCenterById.Picture;

            if (int.Parse(eductionalCenterId) != 0)
            {
                if (editEductionalCenterById == null)
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
                    var EductionalCenterFolderPath = Path.Combine(_host.WebRootPath, "EductionalCenterPictures");
                    if (!Directory.Exists(EductionalCenterFolderPath))
                    {
                        Directory.CreateDirectory(EductionalCenterFolderPath);
                    }
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(EductionalCenterFolderPath, fileName);  // filepath

                    if (editEductionalCenterById.Picture != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\EductionalCenterPictures", editEductionalCenterById.Picture);
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
            string password = eductionalCenter.Password;
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
            eductionalCenter.Password = hashed;
            await _eductionalCenterRepository.EditEductionalCenter(eductionalCenter, editEductionalCenterById.EductionalCenterId, fileName);
            return Created("EductionalCenterTable", editEductionalCenterById);
        }

        // Login Eductional Center 
        [HttpPost]
        [Route("LoginEductionalCenter")]
        public async Task<IActionResult> LoginEductionalCenter([FromForm] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var check = await _eductionalCenterRepository.LoginEductionalCenter(loginViewModel);
            if (check == null)
            {
                ModelState.AddModelError("", "Invalid User Name");
                return BadRequest(ModelState);
            }
            if (check.Password != loginViewModel.Password)
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
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, check.EductionalCenterId.ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("EductionalCenterId", check.EductionalCenterId.ToString()));
            permClaims.Add(new Claim("UserName", check.UserName));
            permClaims.Add(new Claim("Email", check.Email));
            permClaims.Add(new Claim("Table", "EductionalCenters"));

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