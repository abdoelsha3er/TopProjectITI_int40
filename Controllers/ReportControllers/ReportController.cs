using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterGroupRepositories;
using TopProjectITI_int40.Repository.ReportRepo.ReportReporitories;
using TopProjectITI_int40.Repository.StudentRepo.StudentsGroupsRepositories;

namespace TopProjectITI_int40.Controllers.ReportControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        IReportRepository _reportRepository;
        IEductionalCenterGroupRepository _eductionalCenterGroupRepository;
        IStudentsGroupsRepository _studentsGroups;
        public ReportController(IReportRepository reportRepository, IEductionalCenterGroupRepository eductionalCenterGroupRepository,
           IStudentsGroupsRepository studentsGroups)
        {
            _reportRepository = reportRepository;
            _eductionalCenterGroupRepository = eductionalCenterGroupRepository;
            _studentsGroups = studentsGroups;
        }
        // Add Report To Parent
        [HttpPost]
        [Route("AddReportsToParent/{centerid}/{groupid}")]
        public async Task<ActionResult> AddReportsToParent([FromForm]  ReportStudentGroup[] reports,
                                                                        int centerid, int groupid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (reports == null)
            {
                return NotFound();
            }
            var group = await _eductionalCenterGroupRepository.GetEductionalCenterGroupById(groupid);
            if (group == null || centerid == 0 || group.EductionalCenterId != centerid)
            {
                return Content("not found , please Check!...");
            }
            await _reportRepository.AddAllReportsToParent(reports);
            return Created("TeacherTable", reports);
        }

        //  Get All Reports To Center in Selected Date
        [HttpGet]
        [Route("GetAllReportsToCeter/{centerid}/{groupid}/{date}")]
        public async Task<ActionResult> GetAllReportsToCeter(int centerid, int groupid, DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var group = await _eductionalCenterGroupRepository.GetEductionalCenterGroupById(groupid);
            if (centerid == 0 || groupid == 0 || group.EductionalCenterId != centerid)
            {
                return NotFound();
            }
            var getAllReports = await _reportRepository.GetAllReportsToCenter(groupid, date);
            return Created("TeacherTable", getAllReports);
        }

        // Get Reports of Student in Selected Group
        [HttpGet]
        [Route("GetAllReportsToParentInGroup/{studentid}/{groupid}")]
        public async Task<ActionResult> GetAllReportsToParentInGroup(int studentid, int groupid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (studentid == 0 || groupid == 0)
            {
                return NotFound();
            }
            var studentgroups = await _studentsGroups.GetStudentGroups(studentid);

            if (studentgroups == null)
                return NotFound();

            foreach (var item in studentgroups)
            {
                if (item.EductionalCenterGroupId == groupid)
                {
                    var getAllReports = await _reportRepository.GetAllReportsToParentInGroup(studentid, groupid);
                    return Ok(getAllReports);
                }
            }
            return NotFound();
        }

        // Get Report of Student in Group with Selected Date
        [HttpGet]
        [Route("GetReportToParent/{studentid}/{groupid}/{date}")]
        public async Task<ActionResult> GetReportToParent(int studentid, int groupid, DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (studentid == 0 || groupid == 0)
            {
                return NotFound();
            }
            var studentgroups = await _studentsGroups.GetStudentGroups(studentid);

            if (studentgroups == null)
                return NotFound();

            foreach (var item in studentgroups)
            {
                if (item.EductionalCenterGroupId == groupid)
                {
                    var getreportToParent = await _reportRepository.GetReportToParent(studentid, groupid, date);
                    return Ok(getreportToParent);
                }
            }
            return NotFound();
        }
    }
}