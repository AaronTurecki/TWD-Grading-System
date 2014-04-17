using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.Models;

namespace twd_project.ViewModels {

    public class CourseGradeModel {
        public Intake             IntakeO      { get; private set; }
        public List<Intake>       Intakes      { get; private set; }
        public List<TWD_User>     Students     { get; private set; }
        public List<List<string>> CourseGroup  { get; private set; }

        public void SetIntakes(Intake intake, List<Intake> intakes) {
            IntakeO = intake;
            Intakes = intakes;
        }

        public void SetStudentHeadings(List<TWD_User> students) {
            Students = students;
        }

        public void SetCourseGroup(List<List<string>> courseGroup) {
            CourseGroup = courseGroup;
        }
    }
}