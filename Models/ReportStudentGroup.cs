using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class ReportStudentGroup
    {
        public int ReportStudentGroupId { get; set; }

        [ForeignKey("EductionalCenterGroupId, StudentId")]
        public StudentGroup studentGroup { get; set; }
        public int EductionalCenterGroupId { get; set; }
        public int StudentId { get; set; }
        public int ReportId { get; set; }
        public bool Attendance { get; set; }
        public string CommunicationSkills { get; set; }
        public string NotesExam { get; set; }
        public bool StatusExam { get; set; }
        public int DegreeExam { get; set; }
        public string NotesLastHomework { get; set; }
        public bool StatusLastHomework { get; set; }
        public int DegreeLastHomework { get; set; }
        public Report Report { get; set; }
    }
}
