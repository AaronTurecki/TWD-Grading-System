using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.Models {

    public class BuildModule {

        public int          IntakeId   { get; private set; }
        public string       IntakeName { get; private set; }
        public int          ModuleId   { get; private set; }
        public string       ModuleName { get; private set; }
        public List<Course> Courses    { get; private set; }

        public BuildModule() {
        }

        public BuildModule(int moduleId) {

            ModuleRepository moduleRepository = new ModuleRepository();
            Module module = moduleRepository.ReturnTheModule(moduleId);

            IntakeRepository intakeRepository = new IntakeRepository();
            Intake intake = intakeRepository.ReturnTheIntake(module.intakeId);

            CourseRepository courseRepository = new CourseRepository();
            Courses = courseRepository.ReturnModuleCourses(moduleId);

            IntakeId   = module.intakeId;
            IntakeName = intake.intake1;
            ModuleName = module.module1;
            ModuleId   = moduleId;
        }
    }
}