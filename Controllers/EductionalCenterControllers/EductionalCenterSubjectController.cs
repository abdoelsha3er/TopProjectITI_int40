using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.AdminRepo.SubjectRepositories;
using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterSubjectsRepositories;

namespace TopProjectITI_int40.Controllers.EductionalCenterControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EductionalCenterSubjectController : ControllerBase
    {
        IEductionalCenterSubjectRepository _eductionalCenterSubjectRepository;
        ISubjectRepository _subjectRepository;
        public EductionalCenterSubjectController(IEductionalCenterSubjectRepository eductionalCenterSubjectRepository, ISubjectRepository subjectRepository)
        {
            _eductionalCenterSubjectRepository = eductionalCenterSubjectRepository;
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        [Route("GetEductionalCenterSubjecsAssign/{eductionalCenterId}")]   // id here will get from EductionalCenter who is logined on system
        public async Task<IEnumerable<EductionalCenterSubjects>> GetEductionalCenterSubjecsAssign(int eductionalCenterId)
        {
            var eductionalCenterSubjectsAssigned = await _eductionalCenterSubjectRepository.GetEductionalCenterSubjecsAssign(eductionalCenterId);  // all subjects with eductionalcenter
            if (eductionalCenterSubjectsAssigned != null)
            {
                return (eductionalCenterSubjectsAssigned);
            }
            else
            {
                return null;
            }
        }

        // Add new EductionalCenterSubject
        [HttpPost]
        [Route("AddEductionalCenterSubject")]
        public async Task<ActionResult> AddEductionalCenterSubject([FromForm] EductionalCenterSubjects eductionalCenterSubject)
        {
            if (ModelState.IsValid)
            {

                if (eductionalCenterSubject != null)
                {
                    await _eductionalCenterSubjectRepository.AddEductionalCenterSubject(eductionalCenterSubject);
                    return Created("EductionalCenterTable", eductionalCenterSubject);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        ////Delete EductionalCenterSubject
        //[HttpDelete]
        //[Route("DeleteEductionalCenterSubject/{eductionalCenterSubjectId}")]
        //public async Task<IActionResult> DeleteEductionalCenterSubject(int eductionalCenterSubjectId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //var eductionalCenterSubject = _eductionalCenterSubjectRepository.GetEductionalCenterSubjecsAssign
        //        if (eductionalCenterSubject != null)
        //        {
        //            await _eductionalCenterSubjectRepository.DeleteEductionalCenterSubject(eductionalCenterSubject);
        //            return Ok("Deleted Successfully");
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return BadRequest();
        //}


        // Delete Student from Group
        [HttpDelete]
        [Route("DeleteEductionalCenterSubject/{subjectId}")]
        public async Task<IActionResult> DeleteEductionalCenterSubject(int subjectId, [FromForm] int eductionalCenterId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var eductionalCenterSubject = await _eductionalCenterSubjectRepository.GetEductionalCenterSubject(subjectId, eductionalCenterId);
                if (eductionalCenterSubject != null)
                {
                    await _eductionalCenterSubjectRepository.DeleteEductionalCenterSubject(eductionalCenterSubject);
                    return Ok("Deleted Successfully");
                }
                else
                {
                    return NoContent();
                }
            }
        }



    }
}