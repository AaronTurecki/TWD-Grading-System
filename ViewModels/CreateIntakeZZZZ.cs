using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.Models;

namespace twd_project.ViewModels {

    public class CreateIntakeZZZZ {
        public TWD_User Instructor  { get; private set; }
        public List<Intake> Intakes { get; private set; }

        public CreateIntakeZZZZ(int instructor = 0)  {

            //if (instructor != 0) {
            //    InstructorRepository instructorRepository = new InstructorRepository();
            //    Instructor = instructorRepository.ReturnInstructorCourses(instructor);
            //} 

            IntakeRepository intakeRepository = new IntakeRepository();
            Intakes = intakeRepository.ReturnAllIntakes();
        }
    }
}