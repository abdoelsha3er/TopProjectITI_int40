﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.TeacherRepo.TeacherSubjectRepositories
{
    public interface ITeacherSubjectRepository
    {
        // Get All Subjects that not assign to this teacher
        Task<QueryResult<TeacherSubjects>> GetTeacherSubjectsNotAssign(int teacherId, int categorySubjectId);
        //Task<QueryResult<TeacherSubjects>> GetTeacherSubjectsNotAssign(int teacherId);
        //Subject GetSubjectByIdd(int id);
        Task<QueryResult<TeacherSubjects>> GetTeacherSubjects(int teacherId);
        Task AddTeacherSubject(TeacherSubjects teacherSubject);
        Task<TeacherSubjects> GetTeacherSubject(int teacherId, int subjectId);
        //Task DeleteTeacherSubject(int teacherId, int subjectId);
        Task DeleteTeacherSubject(TeacherSubjects teacherSubject);

    }
}
