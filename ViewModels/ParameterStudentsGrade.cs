using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.Models;

namespace twd_project.ViewModels
{
    public class ParameterStudentsGrade {
        public Intake                IntakeO                { get; private set; }
        public Module                ModuleO                { get; private set; }
        public Course                CourseO                { get; private set; }
        public CourseEntity          CourseEntityO          { get; private set; }
        public EntityParameter       EntityParameterO       { get; private set; }
        public List<StudentGrades>   StudentParameterGrades { get; private set; }

        public ParameterStudentsGrade(int entityParameterId) {

            EntityParameterRepository entityParameterRepository = new EntityParameterRepository();
            EntityParameterO = entityParameterRepository.ReturnTheEntityParameter(entityParameterId);

            CourseEntityRepository courseEntityRepository = new CourseEntityRepository();
            CourseEntityO = courseEntityRepository.ReturnTheCourseEntity(Convert.ToInt32(EntityParameterO.courseEntityId));

            CourseRepository courseRepository = new CourseRepository();
            CourseO = courseRepository.ReturnTheCourse(CourseEntityO.courseId);

            ModuleRepository moduleRepository = new ModuleRepository();
            ModuleO = moduleRepository.ReturnTheModule(CourseO.moduleId);

            IntakeRepository intakeRepository = new IntakeRepository();
            IntakeO = intakeRepository.ReturnTheIntake(ModuleO.intakeId);

            TWD_UserRepository userRepository = new TWD_UserRepository();
            List<TWD_User> Students = userRepository.ReturnIntakeAllStudents(IntakeO.intakeId);

            List<StudentGrades> studentParameterGrades = new List<StudentGrades>();

            foreach (TWD_User student in Students) {

                ParameterGradeRepository parameterGradeRepository = new ParameterGradeRepository();
                EntityParameterGrade entityParameterGrade = parameterGradeRepository.ReturnStudentParameterGrade(student.userId, EntityParameterO.entityParameterId);

                StudentGrades studentGrades = new StudentGrades(student, entityParameterGrade);
                studentParameterGrades.Add(studentGrades);
            }
            StudentParameterGrades = studentParameterGrades;
        }
    }
}