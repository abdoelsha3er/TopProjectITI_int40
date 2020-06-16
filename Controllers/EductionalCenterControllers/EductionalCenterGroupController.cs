using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterGroupRepositories;

namespace TopProjectITI_int40.Controllers.EductionalCenterControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EductionalCenterGroupController : ControllerBase
    {
        IEductionalCenterGroupRepository _eductionalCenterGroupRepository;
        public EductionalCenterGroupController(IEductionalCenterGroupRepository eductionalCenterGroupRepository)
        {
            _eductionalCenterGroupRepository = eductionalCenterGroupRepository;
        }

        // Search by id;

        // Add New EductionalCenterGroup
        [HttpPost]
        [Route("AddEductionalCenterGroup")]
        public async Task<ActionResult> AddEductionalCenterGroup([FromForm] EductionalCenterGroup eductionalCenterGroup)
        {
            if (ModelState.IsValid)
            {
                if (eductionalCenterGroup != null)
                {
                    await _eductionalCenterGroupRepository.AddEductionalCenterGroup(eductionalCenterGroup);
                    return Created("EductionalCenterGroupTable", eductionalCenterGroup);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        //// Editing
        //[HttpPut]
        //[Route("DeleteEductionalCenterGroup/{eductionalCenterGroupId}")]
        //public async Task EditEductionalCenterGroup(EductionalCenterGroup eductionalCenterGroup, int eductionalCenterGroupId)
        //{

        //}


        // Delete by id
        // Deleting 
        [HttpDelete]
        [Route("DeleteEductionalCenterGroup/{eductionalCenterGroupId}")]
        public async Task<ActionResult> DeleteEductionalCenterGroup(int eductionalCenterGroupId)
        {
            if (ModelState.IsValid)
            {
                EductionalCenterGroup eductionalCenterGroup = await _eductionalCenterGroupRepository.GetEductionalCenterGroupById(eductionalCenterGroupId);

                if (eductionalCenterGroup != null)
                {
                    await _eductionalCenterGroupRepository.DeleteEductionalCenterGroup(eductionalCenterGroupId);
                    return Ok("Archived Successfully !!!.");
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}