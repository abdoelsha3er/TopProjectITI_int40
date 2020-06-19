using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterPhonesRepositories;

namespace TopProjectITI_int40.Controllers.EductionalCenterControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EductionalCenterPhoneController : ControllerBase
    {
        IEductionalCenterPhoneRepository _eductionalCenterPhoneRepository;
        public EductionalCenterPhoneController(IEductionalCenterPhoneRepository eductionalCenterPhoneRepository)
        {
            _eductionalCenterPhoneRepository = eductionalCenterPhoneRepository;
        }
        // Get AllEductionalCenterPhone(eductionalCenterPhoneId)
        [HttpGet]
        [Route("GetEductionalCenterPhones/{eductionalCenterPhoneId}")]   // id here will get from teacher who is logined on system
        public async Task<QueryResult<EductionalCenterPhone>> GetEductionalCenterPhones(int eductionalCenterPhoneId)
        {
            var eductionalCenterPhones = await _eductionalCenterPhoneRepository.GetEductionalCenterPhones(eductionalCenterPhoneId);
            if (eductionalCenterPhones != null)
            {
                return (eductionalCenterPhones);
            }
            else
            {
                return null;
            }
        }
        // Add new Phone to <EductionalCenter 
        [HttpPost]
        [Route("AddEductionalCenterPhone")]
        public async Task<ActionResult> AddEductionalCenterPhone([FromForm] EductionalCenterPhone eductionalCenterPhone)
        {
            // check phone number is there exists in database 
            var isExistNumber = _eductionalCenterPhoneRepository.CheckPhoneExists(eductionalCenterPhone.EductionalCenterPhoneNumber);
            if (isExistNumber.Result != null)
            {
                return Content("this phone number exists as before!!!. Please Write Another Phone");
            }
            await _eductionalCenterPhoneRepository.AddEductionalCenterPhone(eductionalCenterPhone);
            return Created("EductionalCenterTable", eductionalCenterPhone);
        }
        //Edit
        [HttpPut]
        [Route("EditEductionalCenterPhone")]
        public async Task<IActionResult> EditGetEductionalCenterPhone([FromForm] EductionalCenterPhone eductionalCenterPhone)
        {
            EductionalCenterPhone eductionalCenterPhoneById = await _eductionalCenterPhoneRepository.GetEductionalCenterPhoneById(eductionalCenterPhone.EductionalCenterPhoneId); //GetTeacherPhoneById search
            if (eductionalCenterPhoneById == null)
            {
                return Content("not found , please Check!...");
            }
            else if(eductionalCenterPhone.EductionalCenterPhoneNumber != eductionalCenterPhoneById.EductionalCenterPhoneNumber)
            {
                var isExistNumber = _eductionalCenterPhoneRepository.CheckPhoneExists(eductionalCenterPhone.EductionalCenterPhoneNumber);
                if (isExistNumber.Result != null)
                {
                    return Content("this phone number exists as before!!!. Please Write Another Phone");
                }
                else
                {
                    await _eductionalCenterPhoneRepository.EditEductionalCenterPhone(eductionalCenterPhone);
                    return Created("EductionalCenterTable", eductionalCenterPhone);
                }
            }
            else
            {
                await _eductionalCenterPhoneRepository.EditEductionalCenterPhone(eductionalCenterPhone);
                return Created("EductionalCenterTable", eductionalCenterPhone);
            }
        }
        //Delete
        [HttpDelete]
        [Route("DeleteEductionalCenterPhone/{eductionalCenterPhoneId}")]
        public async Task<IActionResult> DeleteEductionalCenterPhone(int eductionalCenterPhoneId)
        {
            // check phoneteache is exist in table or no
            EductionalCenterPhone eductionalCenterPhoneById = await _eductionalCenterPhoneRepository.GetEductionalCenterPhoneById(eductionalCenterPhoneId);
            if (eductionalCenterPhoneById == null)
            {
                return Content("not found , please Check!...");
            }
            else
            {
                await _eductionalCenterPhoneRepository.DeleteEductionalCenterPhone(eductionalCenterPhoneById);
                return Ok("Deleted Successfully");
            }
        }
    }
}