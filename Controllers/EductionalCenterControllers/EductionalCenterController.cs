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
using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterRepositories;

namespace TopProjectITI_int40.Controllers.EductionalCenterControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EductionalCenterController : ControllerBase
    {
        IEductionalCenterRepository _eductionalCenterRepository;
        PhotoSettings _photoSetting;
        IWebHostEnvironment _host;
        public EductionalCenterController(IEductionalCenterRepository eductionalCenterRepository, 
                                          IOptionsSnapshot<PhotoSettings> options, 
                                          IWebHostEnvironment host)
        {
            _eductionalCenterRepository = eductionalCenterRepository;
            this._photoSetting = options.Value; //  
            _host = host;
        }
        // Register new EductionalCenter 
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> AddEductionalCenter([FromForm] EductionalCenter eductionalCenter, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (eductionalCenter == null)
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
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "EductionalCenterPictures");
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

            eductionalCenter.Picture = fileName;
            await _eductionalCenterRepository.AddEductionalCenter(eductionalCenter);
            return Created("TeacherTable", eductionalCenter);
        }

        //Edit
        [HttpPut]
        [Route("Edit/{eductionalCenterId}")]
        public async Task<IActionResult> EditEductionalCenter([FromForm] EductionalCenter eductionalCenter, int eductionalCenterId, IFormFile file)
        {
            EductionalCenter editEductionalCenterById;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (eductionalCenterId!=0)
            {
                editEductionalCenterById = await _eductionalCenterRepository.GetEductionalCenterById(eductionalCenterId); //GetTeacherPhoneById search
                if (editEductionalCenterById == null)
                {
                    return Content("not found , please Check!...");
                }
                else if(editEductionalCenterById.Picture!=null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\EductionalCenterPictures", editEductionalCenterById.Picture);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
            }
            if (eductionalCenter == null)
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
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "EductionalCenterPictures");
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
            eductionalCenter.Picture = fileName;
            await _eductionalCenterRepository.EditEductionalCenter(eductionalCenter, eductionalCenterId);
            return Created("EductionalCenterTable", eductionalCenter);
        }
    }
}