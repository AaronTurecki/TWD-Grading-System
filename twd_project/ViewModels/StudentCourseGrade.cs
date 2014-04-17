using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.Models;

namespace twd_project.ViewModels {

    public class StudentCourseGrade {
        public int                IntakeId     { get; private set; }
        public int                UserId       { get; private set; }
        public string             UserDetail   { get; private set; }
        public string             CourseName   { get; private set; }
        public List<List<string>> CourseGroup  { get; private set; }
        public List<string>       CourseTotals { get; private set; }

        public void SetStudentDetails(int userId, string intake, int courseId) {
            TWD_UserRepository twd_UserRepository = new TWD_UserRepository();
            TWD_User user = twd_UserRepository.ReturnUser(userId);

            CourseRepository courseRepository = new CourseRepository();
            Course course = courseRepository.ReturnTheCourse(courseId);

            IntakeId   = Convert.ToInt32(user.intakeId);
            UserId     = userId;
            UserDetail = intake + ":    " + user.firstName + " " + user.lastName + "  -  " + user.bcitId;
            CourseName = course.course1;
        }

        public void SetCourseGroup(List<List<string>> courseGroup) {
            CourseGroup = courseGroup;
        }

        public void SetCourseTotals(List<string> courseTotals) {
            CourseTotals = courseTotals;
        }
    }
}