using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.AdminRepo.Category;

namespace TopProjectITI_int40.Controllers.AdminControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorySubjectController : ControllerBase
    {
        private readonly ICategorySubjectRepository _ics;

        public CategorySubjectController(ICategorySubjectRepository ics)
        {
            _ics = ics;
        }
        // list
        public IActionResult GetCategorySubject()
        {
            return Ok(_ics.GetCategorySubject());
        }
        [HttpGet("{id}")]
        //get by id
        public ActionResult<CategorySubject> getid(int id)
        {
            CategorySubject cs = _ics.GetCategorySubjectById(id);
            if (cs == null)
            {
                return NotFound();
            }
            else
            {
                return cs;
            }
        }

        [HttpPost]
        public IActionResult Add([FromForm] CategorySubject cs)
        {
            _ics.AddCategorySubject(cs);
            return Created("CategorySubjectTable", cs);
            //cs= _ics.GetCategorySubjectById(cs.CategorySubjectId);
            //return Ok(cs);
        }

        [HttpPut]
        public IActionResult Edit([FromForm]CategorySubject cs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CategorySubject cs2 = _ics.GetCategorySubjectById(cs.CategorySubjectId);
            if (cs2 == null)
            {
                return NotFound();
            }
            _ics.EditCategorySubject(cs);
            return Ok();
        }
    }
}