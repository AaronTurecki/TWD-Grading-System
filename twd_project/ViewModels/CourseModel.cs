using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.ViewModels {
    public class CourseModel {
        public int    CourseId { get; set; }
        public string Course   { get; set; }
        public int?   Hours    { get; set; }

        public CourseModel(int courseId, string course, int? hours) {
            CourseId = courseId;
            Course   = course;
            Hours    = hours;
        }
    }
}