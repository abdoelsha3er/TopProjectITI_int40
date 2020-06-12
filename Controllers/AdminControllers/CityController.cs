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
    public class CityController : ControllerBase
    {
        DBGProjectITI_Int40 _context;
        public CityController(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        // Get AllTeacherEduction(id)
        [HttpGet]
        [Route("GetCitiesByGovernmentId/{governmentId}")]   // id here will get from teacher who is logined on system
        public async Task<IActionResult> GetCitiesByGovernmentId(int governmentId)
        {
            var cities = await _context.Cities.Where(c => c.GovernmentId == governmentId).ToListAsync();
            return Ok(cities);
        }
    }
}