using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.Models;

namespace twd_project.ViewModels {

    public class BuildIntake {

        public int            IntakeId   { get; private set; }
        public string         IntakeName { get; private set; }
        public string         StartDate  { get; private set; }
        public string         EndDate    { get; private set; }
        public List<Intake>   Intakes    { get; private set; }
        public List<Module>   Modules    { get; private set; }
        public List<TWD_User> Students   { get; private set; }

        public BuildIntake() {
        }

        public BuildIntake(int intakeId) {
            IntakeId = intakeId;

            IntakeRepository intakeRepository = new IntakeRepository();
            Intake intake = intakeRepository.ReturnTheIntake(intakeId);
            Intakes       = intakeRepository.ReturnAllIntakes();
            IntakeName    = intake.intake1;
            StartDate     = String.Format("{0:d}", intake.startDate);
            EndDate       = String.Format("{0:d}", intake.endDate);

            ModuleRepository moduleRepository = new ModuleRepository();
            Modules = moduleRepository.ReturnAllModules(intakeId);

            TWD_UserRepository userRepository = new TWD_UserRepository();
            Students = userRepository.ReturnIntakeAllStudents(intakeId);
        }
    }
}