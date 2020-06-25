using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterGroupRepositories;
using TopProjectITI_int40.Repository.ReportRepo.ReportReporitories;
using TopProjectITI_int40.Repository.ReportRepo.ReportReporitories.ReportSupRepositories;
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
        PhotoSettings _photoSetting;
        IWebHostEnvironment _host;
        IReportSupRepository _reportSupRepository;
        public ReportController(IReportRepository reportRepository, IEductionalCenterGroupRepository eductionalCenterGroupRepository,
           IStudentsGroupsRepository studentsGroups, IOptionsSnapshot<PhotoSettings> options, IWebHostEnvironment host,
                                 IReportSupRepository reportSupRepository)
        {
            _reportRepository = reportRepository;
            _eductionalCenterGroupRepository = eductionalCenterGroupRepository;
            _studentsGroups = studentsGroups;
            this._photoSetting = options.Value; //  
            _host = host;
            _reportSupRepository = reportSupRepository;
        }
        // Add Report To Parent

        [HttpPost]
        [Route("AddReportsToParent/{centerid}/{groupid}")]
        public async Task<ActionResult> AddReportsToParent([FromForm]  ReportStudentGroup[] reports,
                                                         int centerid, int groupid, IFormFile NextLecture, 
                                                         IFormFile Homework)
        {
            string NextLectureName = "";
            string HomeworkName = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (reports == null)
            {
                return NotFound();
            }
            if (NextLecture != null && Homework != null)
            {
                if (NextLecture.Length != 0 || Homework.Length != 0)
                {
                    return NotFound();
                }
                if (NextLecture.Length > _photoSetting.MaxBytes || Homework.Length > _photoSetting.MaxBytes)
                {
                    return BadRequest("Max file size exceeded");
                }
                var uploadsFolderPath = Path.Combine(_host.WebRootPath, "ReportsFiles");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                NextLectureName = Guid.NewGuid().ToString() + Path.GetExtension(NextLecture.FileName);
                var NextLecturePath = Path.Combine(uploadsFolderPath, NextLectureName);  // filepath
                HomeworkName = Guid.NewGuid().ToString() + Path.GetExtension(Homework.FileName);
                var HomeworkPath = Path.Combine(uploadsFolderPath, HomeworkName);  // filepath

                using (var stream = new FileStream(NextLecturePath, FileMode.Create))
                {
                    await NextLecture.CopyToAsync(stream); // picture saved to the path (folder)
                }
                using (var stream = new FileStream(HomeworkPath, FileMode.Create))
                {
                    await Homework.CopyToAsync(stream); // picture saved to the path (folder)
                }

                var group = await _eductionalCenterGroupRepository.GetEductionalCenterGroupById(groupid);
                if (group == null || centerid == 0 || group.EductionalCenterId != centerid)
                {
                    return Content("not found , please Check!...");
                }

            }
            int reportId = await _reportSupRepository.AddReport(NextLectureName, HomeworkName);

            await _reportRepository.AddAllReportsToParent(reports, reportId);
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