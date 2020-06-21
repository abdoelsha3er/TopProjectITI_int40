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
using TopProjectITI_int40.Repository.ParentRepo.ParentRepositories;

namespace TopProjectITI_int40.Controllers.ParentControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        IParentRepository _parentRepository;
        PhotoSettings _photoSetting;
        IWebHostEnvironment _host;
        public ParentController(IParentRepository parentRepository, IOptionsSnapshot<PhotoSettings> options, IWebHostEnvironment host)
        {
            _parentRepository = parentRepository;
            this._photoSetting = options.Value; //  
            _host = host;
        }
        // Registeer 
        [HttpPost]
        [Route("AddParent")]
        public async Task<ActionResult> AddParent([FromForm] Parent parent, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (parent == null)
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
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
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

            parent.Picture = fileName;
            await _parentRepository.AddParent(parent);
            return Created("Parent Table", parent);
        }
        [HttpPut]
        [Route("EditParent/{id}")]
        public async Task<ActionResult> EditParent([FromForm] Parent parent, int ParentId)
        {
            if (ModelState.IsValid)
            {
                Parent parentById = await _parentRepository.GetParentById(ParentId);
                if (parentById != null)
                {
                    await _parentRepository.EditParent(parentById, ParentId);
                    return Ok(parentById);
                }
                return NotFound();
            }
            return BadRequest();
        }
        // Delete Parent by Id
        [HttpDelete]
        [Route("DeleteParent/{id}")]
        public async Task<ActionResult> DeleteParent(int parentId)
        {
            if (ModelState.IsValid)
            {
                Parent parentById = await _parentRepository.GetParentById(parentId);

                if (parentById != null)
                {
                    await _parentRepository.DeleteParent(parentId);
                    return Ok("ok");
                }
                return NotFound();
            }
            return BadRequest();
        }
        // Check login 
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> LoginParent([FromForm] string UserName, [FromForm]string Password)
        {
            if (ModelState.IsValid)
            {
                Parent parent = await _parentRepository.CheckParentLogin(UserName, Password);
                if (parent != null)
                {
                    return Created("Parent Table", parent);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<ActionResult> selectParentDetails(int id)
        {
            if (ModelState.IsValid)
            {
                Parent parent = await _parentRepository.parentDetails(id);
                if (parent != null)
                    return Created("Parent Table", parent);
                return NotFound();
            }
            return BadRequest();
        }
    }
}