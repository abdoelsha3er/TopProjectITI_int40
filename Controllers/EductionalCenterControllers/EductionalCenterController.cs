using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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

        // Get All Eductional Centers
        [HttpGet]
        [Route("GetEductionalCenters")]
        public async Task<IActionResult> GetEductionalCenters()
        {
            IEnumerable<EductionalCenter> eductionalCenters = await _eductionalCenterRepository.GetEductionalCenters();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(eductionalCenters);
            }
        }
        // Get AllTeacherEduction(id)




        [HttpGet]
        [Route("Profile/{userName}")]   // id here will get from teacher who is logined on system
        public async Task<IActionResult> GetEductionalCenterByUserName(string userName)
        {
            var eductionalcenter = await _eductionalCenterRepository.GetEductionalCenterByUserName(userName);
            if (eductionalcenter != null)
            {
                return Ok(eductionalcenter);  //status 200
            }
            else
            {
                return null;
            }
        }

        //Get AllTeacherEduction(id)
        [HttpGet]
        [Route("GetEductionalCenterById/{eductionalCenterId}")]   // id here will get from teacher who is logined on system
        public async Task<IActionResult> GetEductionalCenterById(int eductionalCenterId)
        {
            var eductionalcenter = await _eductionalCenterRepository.GetEductionalCenterById(eductionalCenterId);
            if (eductionalcenter != null)
            {
                return Ok(eductionalcenter);  //status 200
            }
            else
            {
                return null;
            }
        }

        // Register new EductionalCenter 
        [HttpPost]
        [Route("AddEductionalCenter")]
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
            if (file!=null)
            {
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
            }
            else
            {
                eductionalCenter.Picture = "";  // can make default picture
            }

            await _eductionalCenterRepository.AddEductionalCenter(eductionalCenter);
            return Created("TeacherTable", eductionalCenter);
        }

        //Edit
        [HttpPut]
        [Route("EditEductionalCenter/{eductionalCenterId}")]
        public async Task<IActionResult> EditEductionalCenter([FromForm] EductionalCenter eductionalCenter, int eductionalCenterId, IFormFile file)
        {
            EductionalCenter editEductionalCenterById = await _eductionalCenterRepository.GetEductionalCenterById(eductionalCenterId);
            var fileName = editEductionalCenterById.Picture;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (eductionalCenterId != 0)
            {
                if (editEductionalCenterById == null)
                {
                    return Content("not found , please Check!...");
                }
                if (file != null)
                {
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
                    var EductionalCenterFolderPath = Path.Combine(_host.WebRootPath, "EductionalCenterPictures");
                    if (!Directory.Exists(EductionalCenterFolderPath))
                    {
                        Directory.CreateDirectory(EductionalCenterFolderPath);
                    }
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(EductionalCenterFolderPath, fileName);  // filepath

                    if (editEductionalCenterById.Picture != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\EductionalCenterPictures", editEductionalCenterById.Picture);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream); // picture saved to the path (folder)
                            }
                            //teacher.Picture = fileName;
                        }
                    }
                    else
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream); // picture saved to the path (folder)
                            //teacher.Picture = fileName;
                        }
                    }

                }
            }
            // for hashing password
            string password = eductionalCenter.Password;
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: password,
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA1,
               iterationCount: 10000,
               numBytesRequested: 256 / 8));
            eductionalCenter.Password = hashed;
            await _eductionalCenterRepository.EditEductionalCenter(eductionalCenter, eductionalCenterId, fileName);
            return Created("EductionalCenterTable", editEductionalCenterById);
        }
    }
}