using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.ViewModels
{
    public class StudentMarks
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Nullable<short> intakeId { get; set; }
        public List<studentCourseGrade_Result> CourseGrade { get; set; }
       
    }
}