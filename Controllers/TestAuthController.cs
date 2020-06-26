using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.ViewModels;

namespace TopProjectITI_int40.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAuthController : ControllerBase
    {
        DBGProjectITI_Int40 _context;
        IConfiguration _configuration;
        public TestAuthController(DBGProjectITI_Int40 context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult LoginTeacher([FromForm] TeacherViewModel teacherViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var check = _context.Teachers.SingleOrDefault(a => a.UserName == teacherViewModel.UserName);
            // in null
            if (check==null|| check.Password != teacherViewModel.Password)
            {
                ModelState.AddModelError("try Loign", "Invalid UserName & Password");
                return BadRequest(ModelState);
            }
            // if found

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
            //return Unauthorized(new { error = "you can't access this action" });
            return Ok(new { token = jwt_token });

        }
        [Authorize]
        [HttpGet]
        [Route("getTeacher")]
        public IActionResult getTeacher()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var teacherId = claims.Where(p => p.Type == "TeacherId").FirstOrDefault()?.Value;
                var teacher = _context.Teachers.SingleOrDefault(x => x.TeacherId == int.Parse(teacherId));
                if (teacher == null)
                {
                    return NotFound();
                }
                return Ok(new { teacher = teacher });

            }
            return BadRequest();
        }
    }
}