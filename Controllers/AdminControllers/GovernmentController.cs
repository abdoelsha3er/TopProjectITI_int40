using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Controllers.AdminControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernmentController : ControllerBase
    {
        DBGProjectITI_Int40 _context;
        public GovernmentController(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        // Get AllTeacherEduction(id)
        [HttpGet]
        [Route("GetGovernments")]   // id here will get from teacher who is logined on system
        public async Task<IActionResult> GetGovernments()
        {
            var governments = await _context.Governments.ToListAsync();
            return Ok(governments);
        }
    }
}