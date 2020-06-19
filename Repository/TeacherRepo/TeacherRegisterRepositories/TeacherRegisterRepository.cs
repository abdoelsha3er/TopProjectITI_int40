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
        public async Task EditTeacherProfile(Teacher newTeacher, int id)
        {
            Teacher oldTeacher = await GetTeacherById(id);
            oldTeacher.FirstName = newTeacher.FirstName;
            oldTeacher.LastName = newTeacher.LastName;
            oldTeacher.UserName = newTeacher.UserName;
            oldTeacher.Password = newTeacher.Password;
            oldTeacher.Picture = newTeacher.Picture;
            oldTeacher.CityId = newTeacher.CityId;
            oldTeacher.AddressDetails = newTeacher.AddressDetails;
            oldTeacher.Gender = newTeacher.Gender;
            oldTeacher.DateOfBirth = newTeacher.DateOfBirth;
            _context.Entry(oldTeacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
