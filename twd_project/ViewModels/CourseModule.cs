using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.ViewModels
{
    public class CourseModule
    {
        public short courseId { get; set; }
        public string course { get; set; }
        public Nullable<short> courseWeight { get; set; }
        public Nullable<bool> showResults { get; set; }
        public short moduleId { get; set; }
        public Nullable<int> courseHours { get; set; }
       
    }
}