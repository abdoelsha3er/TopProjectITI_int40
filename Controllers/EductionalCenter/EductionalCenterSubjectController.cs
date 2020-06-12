using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.Admin.SubjectRepositories;
using TopProjectITI_int40.Repository.EductionalCenter.EductionalCenterSubjectsRepositories;

namespace TopProjectITI_int40.Controllers.EductionalCenter
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
        [Route("GetEductionalCenterSubjecsAssign")]   // id here will get from EductionalCenter who is logined on system
        public async Task<QueryResult<EductionalCenterSubjects>> GetEductionalCenterSubjecsAssign([FromForm] int eductionalCenterId)
        {
            var eductionalCenterSubjectsNotAssign = await _eductionalCenterSubjectRepository.GetEductionalCenterSubjecsAssign(eductionalCenterId);  // all subjects with eductionalcenter
            if (eductionalCenterSubjectsNotAssign != null)
            {
                return (eductionalCenterSubjectsNotAssign);
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
        //Delete EductionalCenterSubject
        [HttpDelete]
        [Route("DeleteEductionalCenterSubject")]
        public async Task<IActionResult> DeleteEductionalCenterSubject([FromForm] EductionalCenterSubjects eductionalCenterSubject)
        {
            if (ModelState.IsValid)
            {

                if (eductionalCenterSubject != null)
                {
                    await _eductionalCenterSubjectRepository.DeleteEductionalCenterSubject(eductionalCenterSubject);
                    return Ok("Deleted Successfully");
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }






        //// Get AllEductionalCenterSubjectsNotAssign(id)
        //[HttpGet]
        //[Route("GetEductionalCenterSubjecsNotAssign")]   // id here will get from teacher who is logined on system
        //public async Task<QueryResult<EductionalCenterSubjects>> GetEductionalCenterSubjecsNotAssign([FromForm] int eductionalCenterId, [FromForm] int categorySubjectId)
        //{
        //    var eductionalCenterSubjectsNotAssign = await _eductionalCenterSubjectRepository.GetEductionalCenterSubjecsNotAssign(eductionalCenterId, categorySubjectId);
        //    if (eductionalCenterSubjectsNotAssign != null)
        //    {
        //        return (eductionalCenterSubjectsNotAssign);
        //    }
        //    else
        //    {
        //        //return (teacherSubectsNotAssign);
        //        return null;
        //    }
        //}

        //[HttpGet]
        //[Route("GetTeacherSubjectsNotAssign2")]   // id here will get from teacher who is logined on system
        //public async Task<QueryResult<EductionalCenterSubjects>> GetTeacherSubjectsNotAssign2([FromForm] int id, [FromForm] int csid)
        //{
        //    var eductionalCenterSubjectsNotAssign = await _eductionalCenterSubjectRepository.GetEductionalCenterSubjecsAssign(id);  // all subjects with eductionalcenter
        //    var allSubjects = await _subjectRepository.GetByCategoryId(csid);   // all subjecs
        //    if (eductionalCenterSubjectsNotAssign != null && allSubjects !=null)
        //    {
        //        foreach (var item in allSubjects)
        //        {
        //            return item;
        //        }
        //        return (eductionalCenterSubjectsNotAssign);
        //    }
        //    else
        //    {
        //        //return (teacherSubectsNotAssign);
        //        return null;
        //    }
        //}


    }
}