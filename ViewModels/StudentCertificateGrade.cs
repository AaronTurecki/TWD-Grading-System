using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.Models;

namespace twd_project.ViewModels {

    public class StudentCertificateGrade {
        public int                IntakeId     { get; private set; }
        //public string             IntakeDetail { get; private set; }
        public int                UserId       { get; private set; }
        public string             UserDetail   { get; private set; }
        public List<List<string>> ModuleGroup1 { get; private set; }
        public List<List<string>> ModuleGroup2 { get; private set; }
        public List<List<string>> ModuleGroup3 { get; private set; }
        public List<List<string>> ModuleGroup4 { get; private set; }
        public List<List<string>> ModuleGroup5 { get; private set; }
        public List<string>       IntakeTotals { get; private set; }

        public void SetStudentDetails(int userId, string intake) {
            TWD_UserRepository twd_UserRepository = new TWD_UserRepository();
            TWD_User user = twd_UserRepository.ReturnUser(userId);

            IntakeId     = Convert.ToInt32(user.intakeId);
            //IntakeDetail = intake;
            UserId       = userId;
            UserDetail = intake + ":    " + user.firstName + " " + user.lastName + "  -  " + user.bcitId;
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