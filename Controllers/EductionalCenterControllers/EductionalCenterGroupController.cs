using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterGroupRepositories;

namespace TopProjectITI_int40.Controllers.EductionalCenterControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EductionalCenterGroupController : ControllerBase
    {
        IEductionalCenterGroupRepository _eductionalCenterGroupRepository;
        PhotoSettings _photoSetting;
        IWebHostEnvironment _host;
        public EductionalCenterGroupController(IEductionalCenterGroupRepository eductionalCenterGroupRepository,
                                               IOptionsSnapshot<PhotoSettings> options,
                                               IWebHostEnvironment host)
        {
            _eductionalCenterGroupRepository = eductionalCenterGroupRepository;
            this._photoSetting = options.Value; //  
            _host = host;
        }
        //
        // get all groups of eductional center by edcutionalbyid
        [HttpGet]
        [Route("GetEductionalCenterGroups/{eductionalCenterId}")]
        public async Task<IEnumerable<EductionalCenterGroup>> GetEductionalCenterGroups(int eductionalCenterId)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<EductionalCenterGroup> groups = await _eductionalCenterGroupRepository.GetEductionalCenterGroups(eductionalCenterId);
                if (groups != null)
                    return groups;
                return null;
            }
            return null;
        }
        // get by eductionalcenterGroupById
        [HttpGet]
        [Route("GetEductionalCenterGroupById/{eductionalCenterGroupId}")]
        public async Task<IActionResult> GetEductionalCenterGroupById(int eductionalCenterGroupId)
        {
            if (ModelState.IsValid)
            {
                var group = await _eductionalCenterGroupRepository.GetEductionalCenterGroupById(eductionalCenterGroupId);
                if (group != null)
                    return Ok(group);
                return null;
            }
            return null;

        }


        // Search by id;

        // Add New EductionalCenterGroup
        [HttpPost]
        [Route("AddEductionalCenterGroup")]
        public async Task<ActionResult> AddEductionalCenterGroup([FromForm] EductionalCenterGroup eductionalCenterGroup, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (eductionalCenterGroup == null)
            {
                return NotFound();
            }
            if (file.Length == 0)
            {
                return BadRequest("Empty file");
            }
            if (file.Length > _photoSetting.MaxBytes)
            {
                return BadRequest("Max file size exceeded");
            }
            if (!_photoSetting.IsSupported(file.FileName))
            {
                return BadRequest("Invalid file type");
            }
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "GroupsPictures");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);  // filepath

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream); // picture saved to the path (folder)
            }

            eductionalCenterGroup.Logo = fileName;

            await _eductionalCenterGroupRepository.AddEductionalCenterGroup(eductionalCenterGroup);
            return Created("EductionalCenterGroupTable", eductionalCenterGroup);
        }

        // Editing
        [HttpPut]
        [Route("EditEductionalCenterGroup/{eductionalCenterGroupId}")]
        public async Task<IActionResult> EditEductionalCenterGroup([FromForm]EductionalCenterGroup eductionalCenterGroup, int eductionalCenterGroupId, IFormFile file)
        {
            EductionalCenterGroup editEductionalCenterGroup;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (eductionalCenterGroupId != 0)
            {
                editEductionalCenterGroup = await _eductionalCenterGroupRepository.GetEductionalCenterGroupById(eductionalCenterGroupId); //GetTeacherPhoneById search
                if (editEductionalCenterGroup == null)
                {
                    return Content("not found , please Check!...");
                }
                else if (eductionalCenterGroup.Logo != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\EductionalCenterPictures", eductionalCenterGroup.Logo);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
            }
            if (eductionalCenterGroup == null)
            {
                return NotFound();
            }
            if (file.Length == 0)
            {
                return BadRequest("Empty file");
            }
            if (file.Length > _photoSetting.MaxBytes)
            {
                return BadRequest("Max file size exceeded");
            }
            if (!_photoSetting.IsSupported(file.FileName))
            {
                return BadRequest("Invalid file type");
            }
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "GroupsPictures");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);  // filepath

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream); // picture saved to the path (folder)
            }
            eductionalCenterGroup.Logo= fileName;
            await _eductionalCenterGroupRepository.EditEductionalCenterGroup(eductionalCenterGroup, eductionalCenterGroupId);
            return Created("EductionalCenterTable", eductionalCenterGroup);
        }


        // Delete by id
        // Deleting 
        [HttpDelete]
        [Route("DeleteEductionalCenterGroup/{eductionalCenterGroupId}")]
        public async Task<ActionResult> DeleteEductionalCenterGroup(int eductionalCenterGroupId)
        {
            if (ModelState.IsValid)   // will make check on this group have students or no
            {
                EductionalCenterGroup eductionalCenterGroup = await _eductionalCenterGroupRepository.GetEductionalCenterGroupById(eductionalCenterGroupId);

                if (eductionalCenterGroup != null)
                {
                    await _eductionalCenterGroupRepository.DeleteEductionalCenterGroup(eductionalCenterGroupId);
                    return Ok("Deleted Successfully !!!.");
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}