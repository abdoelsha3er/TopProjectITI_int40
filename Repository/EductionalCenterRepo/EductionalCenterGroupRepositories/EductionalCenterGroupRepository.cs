﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterGroupRepositories
{
    public class EductionalCenterGroupRepository : IEductionalCenterGroupRepository
    {

        DBGProjectITI_Int40 _context;
        public EductionalCenterGroupRepository(DBGProjectITI_Int40 context)
        {
            _context = context;
        }

        // Add EductionalCenterGroup
        public async Task AddEductionalCenterGroup(EductionalCenterGroup eductionalCenterGroup)
        {
            await _context.EductionalCenterGroups.AddAsync(eductionalCenterGroup);
            await _context.SaveChangesAsync();
        }
        // get all groups of eductional center by it's id       //  when login , we gets id for center by token
        public async Task<IEnumerable<EductionalCenterGroup>> GetEductionalCenterGroups(int eductionalCenterId)
        {
            return await _context.EductionalCenterGroups
                        .Include(t=>t.Teacher)
                        .Include(s=>s.Subject)
                        .Include(g=>g.Grade)
                        .Where(a => a.EductionalCenterId == eductionalCenterId).ToListAsync();
        }

        // Search By eductionalCenterGroupId
        public async Task<EductionalCenterGroup> GetEductionalCenterGroupById(int eductionalCenterGroupId)
        {
            return await _context.EductionalCenterGroups
                        .Include(t => t.Teacher)
                        .Include(s => s.Subject)
                        .Include(g => g.Grade)
                        .Where(a=> a.EductionalCenterGroupId== eductionalCenterGroupId).FirstOrDefaultAsync();
        }

        // gar all groups centerId, gradeid , subjectId
        public async Task<IEnumerable<EductionalCenterGroup>> GetGroupsByCenterIDGradeIdSubjectID(int eductionalCenterId, int gradeId, int subjectid)
        {
            return await _context.EductionalCenterGroups
                        .Include(c => c.EductionalCenter)
                        .Include(t => t.Teacher)
                        .Include(s => s.Subject)
                        .Include(gr => gr.Grade)
                        .Where(g => g.EductionalCenterId == eductionalCenterId && g.GradeId == gradeId && g.SubjectId == subjectid)
                        .ToListAsync();
        }
        // Edit
        public async Task EditEductionalCenterGroup(EductionalCenterGroup newEductionalCenterGroup, int eductionalCenterGroupId)  // for editing profile
        {
            EductionalCenterGroup oldEductionalCenterGroup = await GetEductionalCenterGroupById(eductionalCenterGroupId);
            oldEductionalCenterGroup.Name = newEductionalCenterGroup.Name;
            oldEductionalCenterGroup.TeacherId = newEductionalCenterGroup.TeacherId;
            oldEductionalCenterGroup.SubjectId = newEductionalCenterGroup.SubjectId;
            oldEductionalCenterGroup.GradeId = newEductionalCenterGroup.GradeId;
            oldEductionalCenterGroup.Description = newEductionalCenterGroup.Description;
            oldEductionalCenterGroup.TotleStudents = newEductionalCenterGroup.TotleStudents;
            oldEductionalCenterGroup.DateFrom = newEductionalCenterGroup.DateFrom;
            oldEductionalCenterGroup.DateTo = newEductionalCenterGroup.DateTo;
            oldEductionalCenterGroup.PriceInMonth = newEductionalCenterGroup.PriceInMonth;
            oldEductionalCenterGroup.Status = newEductionalCenterGroup.Status;
            oldEductionalCenterGroup.ArchivedReason = newEductionalCenterGroup.ArchivedReason;
            //_context.Parents.Update(oldEductionalCenterGroup);
            _context.Entry(oldEductionalCenterGroup).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // delete by id
        public async Task DeleteEductionalCenterGroup(int eductionalCenterGroupId)
        {
            var eductionalCenterGroup = await GetEductionalCenterGroupById(eductionalCenterGroupId);
            _context.EductionalCenterGroups.Remove(eductionalCenterGroup);
            await _context.SaveChangesAsync();
        }

    }
}
