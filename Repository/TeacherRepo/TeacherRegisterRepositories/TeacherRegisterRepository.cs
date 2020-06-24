using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherRegisterRepositories
{
    public class TeacherRegisterRepository : ITeacherRegisterRepository
    {
        DBGProjectITI_Int40 _context;
        public TeacherRegisterRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }

        // get all teachers
        public async Task<IEnumerable<Teacher>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }
        // Add new TeacherEduction
        public async Task TeacherRegister(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }
        // get by teacher id   // will get from token when logined
        public async Task<Teacher> GetTeacherById(int teacherId)
        {
            return await _context.Teachers.FindAsync(teacherId);
        }
        // Edit profile of Teacher
        public async Task EditTeacherProfile(Teacher newTeacher, int id, string file)
        {
            Teacher oldTeacher = await GetTeacherById(id);
            oldTeacher.FirstName = newTeacher.FirstName;
            oldTeacher.LastName = newTeacher.LastName;
            oldTeacher.UserName = newTeacher.UserName;
            oldTeacher.Password = newTeacher.Password;
            if (file!=null)
            {
                oldTeacher.Picture = file;
            }
            //
            oldTeacher.CityId = newTeacher.CityId;
            oldTeacher.AddressDetails = newTeacher.AddressDetails;
            oldTeacher.Gender = newTeacher.Gender;
            oldTeacher.DateOfBirth = newTeacher.DateOfBirth;
            _context.Entry(oldTeacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

//EductionalCenter editEductionalCenterById;
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            if (eductionalCenterId!=0)
//            {
//                editEductionalCenterById = await _eductionalCenterRepository.GetEductionalCenterById(eductionalCenterId); //GetTeacherPhoneById search
//                if (editEductionalCenterById == null)
//                {
//                    return Content("not found , please Check!...");
//                }
//                else if(editEductionalCenterById.Picture!=null)
//                {
//                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\EductionalCenterPictures", editEductionalCenterById.Picture);
//                    if (System.IO.File.Exists(path))
//                    {
//                        System.IO.File.Delete(path);
//                    }
//                }
//            }
//            if (eductionalCenter == null)
//            {
//                return NotFound();
//            }
//            if (file.Length == 0)
//            {
//                return BadRequest("Empty file");
//            }
//            if (file.Length > _photoSetting.MaxBytes)
//            {
//                return BadRequest("Max file size exceeded");
//            }
//            if (!_photoSetting.IsSupported(file.FileName))
//            {
//                return BadRequest("Invalid file type");
//            }
//            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "EductionalCenterPictures");
//            if (!Directory.Exists(uploadsFolderPath))
//            {
//                Directory.CreateDirectory(uploadsFolderPath);
//            }
//            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
//var filePath = Path.Combine(uploadsFolderPath, fileName);  // filepath

//            using (var stream = new FileStream(filePath, FileMode.Create))
//            {
//                await file.CopyToAsync(stream); // picture saved to the path (folder)
//            }
//            eductionalCenter.Picture = fileName;
//            await _eductionalCenterRepository.EditEductionalCenter(eductionalCenter, eductionalCenterId);
//            return Created("EductionalCenterTable", eductionalCenter);
