﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.StudentRepo.StudentRepositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents(int id);
        Task<Student> GetStudentById(int studentId);
        Task<IEnumerable<Student>> GetAllStudents();
        Task<IEnumerable<Student>> GetStudentsByGradeId(int gradeId);   // get all by gradeId
        Task AddStudent(Student student);
        Task EditStudent(Student newStudent, int studentId);
        Task DeleteStudent(int studentId);
        Task<Student> CheckStudentLogin(string userName, string password, string email);
        Task<Student> studentDetails(int id);
    }
}
