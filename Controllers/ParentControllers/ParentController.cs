using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.ParentRepo.ParentRepositories;
using TopProjectITI_int40.ViewModels;

namespace TopProjectITI_int40.Controllers.ParentControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        IParentRepository _parentRepository;
        PhotoSettings _photoSetting;
        IWebHostEnvironment _host;
        IConfiguration _configuration;
        public ParentController(IParentRepository parentRepository,
                                IOptionsSnapshot<PhotoSettings> options,
                                IWebHostEnvironment host,
                                IConfiguration configuration)
        {
            _parentRepository = parentRepository;
            this._photoSetting = options.Value; //  
            _host = host;
            _configuration = configuration;
        }
        // Registeer 
        [HttpPost]
        [Route("AddParent")]
        public async Task<ActionResult> AddParent([FromForm] Parent parent, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (parent == null)
            {
                return NotFound();
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
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
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

            parent.Picture = fileName;
            await _parentRepository.AddParent(parent);
            return Created("Parent Table", parent);
        }
        [HttpPut]
        [Route("EditParent/{id}")]
        public async Task<ActionResult> EditParent([FromForm] Parent parent, int ParentId)
        {
            if (ModelState.IsValid)
            {
                Parent parentById = await _parentRepository.GetParentById(ParentId);
                if (parentById != null)
                {
                    await _parentRepository.EditParent(parentById, ParentId);
                    return Ok(parentById);
                }
                return NotFound();
            }
            return BadRequest();
        }
        // Delete Parent by Id
        [HttpDelete]
        [Route("DeleteParent/{id}")]
        public async Task<ActionResult> DeleteParent(int parentId)
        {
            if (ModelState.IsValid)
            {
                Parent parentById = await _parentRepository.GetParentById(parentId);

                if (parentById != null)
                {
                    await _parentRepository.DeleteParent(parentId);
                    return Ok("ok");
                }
                return NotFound();
            }
            return BadRequest();
        }
        // Check login 
        /*[HttpPost]
        [Route("Login")]
        public async Task<ActionResult> LoginParent([FromForm] string UserName, [FromForm]string Password)
        {
            if (ModelState.IsValid)
            {
                Parent parent = await _parentRepository.CheckParentLogin(UserName, Password);
                if (parent != null)
                {
                    return Created("Parent Table", parent);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }*/

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<ActionResult> selectParentDetails(int id)
        {
            if (ModelState.IsValid)
            {
                Parent parent = await _parentRepository.parentDetails(id);
                if (parent != null)
                    return Created("Parent Table", parent);
                return NotFound();
            }
            return BadRequest();
        }

        //Login Teacher
        [HttpPost]
        [Route("LoginParent")]
        public async Task<IActionResult> LoginParent([FromForm] LoginViewModel parentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var check = await _parentRepository.LoginParent(parentViewModel);
            if (check == null)
            {
                ModelState.AddModelError("", "Invalid User Name");
                return BadRequest(ModelState);
            }
            if (check.Password != parentViewModel.Password)
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
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, check.ParentId.ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("ParentId", check.ParentId.ToString()));
            permClaims.Add(new Claim("UserName", check.UserName));
            permClaims.Add(new Claim("Email", check.Email));
            permClaims.Add(new Claim("Table", "Parents"));

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