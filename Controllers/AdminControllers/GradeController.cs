using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopProjectITI_int40.AppDBContext;

namespace TopProjectITI_int40.Controllers.AdminControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        DBGProjectITI_Int40 _context;
        public GradeController(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        // GetGovernments
        [HttpGet]
        [Route("GetGrades")]   
        public async Task<IActionResult> GetGovernments()
        {
            var grades = await _context.Grades.ToListAsync();
            return Ok(grades);
        }
    }
}