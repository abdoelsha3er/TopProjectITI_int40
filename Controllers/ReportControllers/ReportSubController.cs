using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Repository.ReportRepo.ReportReporitories.ReportSupRepositories;

namespace TopProjectITI_int40.Controllers.ReportControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportSubController : ControllerBase
    {
        IReportSupRepository _reportSupRepository;
        public ReportSubController(IReportSupRepository reportSupRepository)
        {
            _reportSupRepository = reportSupRepository;
        }
        [HttpGet]
        [Route("GetAllReportsDates/{centerid}/{groupid}")]
        public async Task<ActionResult> GetAllReportsDates(int centerid, int groupid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (centerid == 0 || groupid == 0)
            {
                return NotFound();
            }
            var group = await _reportSupRepository.GetAllReportToCenter(groupid);
            return Created("TeacherTable", group);
        }
    }
}