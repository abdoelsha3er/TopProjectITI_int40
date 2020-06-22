using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.StudentRepo.StudentsGroupsRepositories
{
    public class StudentsGroupsRepository :IStudentsGroupsRepository
    {
        DBGProjectITI_Int40 _context;
        public StudentsGroupsRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }
        // this used in studnet profile, and parent student profile
        // get all groups of selected Student); "selected student "may be login>> take form token or 
        //, parent have students so can see all groups of his students
        public async Task<IEnumerable<StudentGroup>> GetStudentGroups(int studentId)
        {
            return await _context.StudentsGroups.Include(g => g.EductionalCenterGroup)
                           .Where(s => s.StudentId == studentId)
                           .ToListAsync();
        }
        // get all Students of Selected Group, "selected group" , its come from token when eductional center make login with relation include
        public async Task<IEnumerable<StudentGroup>> GetGroupStudents(int groupId)
        {
            return await _context.StudentsGroups.Include(s => s.Student)
                           .Where(g => g.EductionalCenterGroupId== groupId)
                           .ToListAsync();
        }
        // get one studentgroup  for search as aone object , may use in delete or update
        public async Task<StudentGroup> GetStudentGroup(int groupId, int studentId)
        {
            return await _context.StudentsGroups.FirstOrDefaultAsync(sg => sg.EductionalCenterGroupId == groupId && sg.StudentId == studentId);
        }   

        // Add Stuedents to Group
        public async Task AddStudentToGroup(StudentGroup studentGroup)
        {
            _context.StudentsGroups.Add(studentGroup);
            await _context.SaveChangesAsync();
        }
        // delete StudentGroup > Remove student from group
        public async Task DeleteStudntFromGroup(StudentGroup studentGroup)
        {
            _context.StudentsGroups.Remove(studentGroup);
            await _context.SaveChangesAsync();

        }
    }
}
