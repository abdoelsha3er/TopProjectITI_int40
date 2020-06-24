using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.StudentRepo.StudentsGroupsRepositories
{
    public interface IStudentsGroupsRepository  
    {
        // get all groups of selected Student); "selected student "may be login>> take form token or 
                            //, parent have students so can see all groups of his students
        Task<IEnumerable<StudentGroup>> GetStudentGroups(int studentId);
        // get all Students of Selected Group, "selected group" , its come from token when eductional center make login with relation include
        Task<IEnumerable<StudentGroup>> GetGroupStudentsJoined(int groupId);  // join group
        Task<IEnumerable<StudentGroup>> GetGroupStudentsWaitJoine(int groupId);  // not join group
                                                                                 //Task<StudentGroup> GetStudentGroup(int groupId, int studentId);
        StudentGroup GetStudentGroup(int groupId, int studentId);
        void EditStudentGroup(StudentGroup newStudentGroup, int groupId, int studentId);

        Task AddStudentToGroup(StudentGroup studentGroup);
        Task DeleteStudntFromGroup(StudentGroup studentGroup);
    }
}
