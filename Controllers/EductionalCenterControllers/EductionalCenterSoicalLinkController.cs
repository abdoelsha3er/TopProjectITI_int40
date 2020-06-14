using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.EductionalCenter.EductionalCenterSoicalLinkRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherEductionRepositories;

namespace TopProjectITI_int40.Controllers.EductionalCenterControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EductionalCenterSoicalLinkController : ControllerBase
    {
        IEductionalCenterSoicalLinkRepository _eductionalCenterSoicalLinkRepository;
        public EductionalCenterSoicalLinkController(IEductionalCenterSoicalLinkRepository eductionalCenterSoicalLinkRepository)
        {
            _eductionalCenterSoicalLinkRepository = eductionalCenterSoicalLinkRepository;
        }
        ///////////////////////
        // Get AllEductionalCenterSoicalLink(EductionalCenterSoicalLinkId)
        [HttpGet]
        [Route("GetEductionalCenterSoicalLinks")]   // id here will get from teacher who is logined on system
        public async Task<QueryResult<EductionalCenterSoicalLink>> GetEductionalCenterSoicalLinks([FromForm] int eductionalCenterId)
        {
            var eductionalCenterSoicalLinks = await _eductionalCenterSoicalLinkRepository.GetEductionalCenterSoicalLinks(eductionalCenterId);
            if (eductionalCenterSoicalLinks != null)
            {
                return (eductionalCenterSoicalLinks);
            }
            else
            {
                return null;
            }
        }
        //////////////////////////////////////////////////
        // Add new EductionalCenterSoicalLink
        [HttpPost]
        [Route("AddEductionalCenterSoicalLink")]
        public async Task<ActionResult> AddEductionalCenterSoicalLink([FromForm] EductionalCenterSoicalLink eductionalCenterSoicalLink)
        {
            if (ModelState.IsValid)
            {

                if (eductionalCenterSoicalLink != null)
                {
                    await _eductionalCenterSoicalLinkRepository.AddEductionalCenterSoicalLink(eductionalCenterSoicalLink);
                    return Created("EductionalCenterSoicalLinkTable", eductionalCenterSoicalLink);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        ////////////////////
        //Delete
        [HttpDelete]
        [Route("DeleteEductionalCenterSoicalLink")]
        public async Task<IActionResult> DeleteEductionalCenterSoicalLink([FromForm] EductionalCenterSoicalLink eductionalCenterSoicalLink)
        {
            // check phoneteache is exist in table or no
            EductionalCenterSoicalLink eductionalCenterSoicalLinkById = await _eductionalCenterSoicalLinkRepository.GetEductionalCenterSoicalLinkById(eductionalCenterSoicalLink.EductionalCenterSoicalLinkId);
            if (eductionalCenterSoicalLinkById == null)
            {
                return Content("not found , please Check!...");
            }
            else
            {
                await _eductionalCenterSoicalLinkRepository.DeleteEductionalCenterSoicalLink(eductionalCenterSoicalLinkById);
                return Ok("Deleted Successfully");
            }
        }
    }
}