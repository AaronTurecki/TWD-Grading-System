using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.Models;

namespace twd_project.ViewModels {

    public class IntakeGradeModel {
        public Intake             IntakeO      { get; private set; }
        public List<Intake>       Intakes      { get; private set; }
        public List<TWD_User>     Students     { get; private set; }
        public List<List<string>> ModuleGroup1 { get; private set; }
        public List<List<string>> ModuleGroup2 { get; private set; }
        public List<List<string>> ModuleGroup3 { get; private set; }
        public List<List<string>> ModuleGroup4 { get; private set; }
        public List<List<string>> ModuleGroup5 { get; private set; }
        public List<string>       IntakeTotals { get; private set; }
        
        public void SetIntakeHeading(Intake intake, List<Intake> intakes) {
            IntakeO   = intake;
            Intakes    = intakes;
        }

        public void SetStudentHeadings(List<TWD_User> students) {
            Students = students;
        }

        public void SetModuleGroup1(List<List<string>> moduleGroup) {
            ModuleGroup1 = moduleGroup;
        }

        public void SetModuleGroup2(List<List<string>> moduleGroup) {
            ModuleGroup2 = moduleGroup;
        }

        public void SetModuleGroup3(List<List<string>> moduleGroup) {
            ModuleGroup3 = moduleGroup;
        }

        public void SetModuleGroup4(List<List<string>> moduleGroup) {
            ModuleGroup4 = moduleGroup;
        }

        public void SetModuleGroup5(List<List<string>> moduleGroup) {
            ModuleGroup5 = moduleGroup;
        }

        public void SetIntakeTotals(List<string> intakeTotals) {
            IntakeTotals = intakeTotals;
        }
    }
}