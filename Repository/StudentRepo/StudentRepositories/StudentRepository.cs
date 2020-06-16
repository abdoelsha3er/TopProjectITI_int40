using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.StudentRepo.StudentRepositories
{
    public class StudentRepository : IStudentRepository
    {
        DBGProjectITI_Int40 _context;
        public StudentRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        public async Task AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task<Student> CheckStudentLogin(string name, string password, string email)
        {
            var parentobj = await _context.Parents.SingleAsync(a => a.Email == email);
            var studentobj = await _context.Students.SingleAsync(s => s.StudentName == name && s.Password == password);
            if (parentobj.ParentId == studentobj.ParentId)
            {
                return studentobj;
            }
            return null;

            //return await _context.Students.FirstOrDefaultAsync(s => s.ParentId == parentobj.ParentId && 
            //s.StudentName == name && s.Password == password);
        }

        public Task DeleteStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public async Task EditStudent(Student oldStudent, Student student)
        {
            //Student oldStudent = await GetStudentById(studentId);
            oldStudent.StudentName = student.StudentName;
            oldStudent.Password = student.Password;
            oldStudent.DateOfBirth = student.DateOfBirth;
            oldStudent.School = student.School;
            oldStudent.Education = student.Education;
            oldStudent.DegreeOfLastYear = student.DegreeOfLastYear;
            _context.Students.Update(oldStudent);
            // _context.Entry(oldParent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);
        }
        // get students of parent by ParentId
        public async Task<IEnumerable<Student>> GetStudents(int id)
        {
            return await _context.Students.Where(a => a.ParentId == id).ToListAsync();
        }

        public async Task<Student> studentDetails(int id)
        {
            return await GetStudentById(id);
        }
    }
}
