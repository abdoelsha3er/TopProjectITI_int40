using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string Homework { get; set; }
        public string NextLecture { get; set; }
        public DateTime reportDate { get; set; }
        public List<ReportStudentGroup> reportStudentGroup { get; set; }
    }
}
