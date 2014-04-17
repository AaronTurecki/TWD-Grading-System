using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.Models;

namespace twd_project.ViewModels
{
    public class BuildCourseEntityGrade
    {
        public int                   IntakeId         { get; private set; }
        public string                IntakeName       { get; private set; }
        public int                   ModuleId         { get; private set; }
        public string                ModuleName       { get; private set; }
        public int                   CourseId         { get; private set; }
        public string                CourseName       { get; private set; }
        public int                   CourseEntityId   { get; private set; }
        public string                CourseEntityName { get; private set; }
        public List<EntityParameter> EntityParameters { get; private set; }
        public List<TWD_User>        Students         { get; private set; }
        public List<string>          A                { get; private set; }

        public BuildCourseEntityGrade() {
        }
        
        public BuildCourseEntityGrade(int courseEntityId) {
            CourseEntityGradeRepository courseEntityGradeRepository = new CourseEntityGradeRepository();
            List<string> a = new List<string>();
            
            CourseEntityRepository courseEntityRepository = new CourseEntityRepository();
            CourseEntity courseEntity = courseEntityRepository.ReturnTheCourseEntity(courseEntityId);

            CourseRepository courseRepository = new CourseRepository();
            Course course = courseRepository.ReturnTheCourse(courseEntity.courseId);

            ModuleRepository moduleRepository = new ModuleRepository();
            Module module = moduleRepository.ReturnTheModule(course.moduleId);

            IntakeRepository intakeRepository = new IntakeRepository();
            Intake intake = intakeRepository.ReturnTheIntake(module.intakeId);

            EntityParameterRepository entityParameterRepository = new EntityParameterRepository();
            EntityParameters = entityParameterRepository.ReturnAllEntityParameters(courseEntityId);

            TWD_UserRepository userRepository = new TWD_UserRepository();
            Students = userRepository.ReturnIntakeAllStudents(module.intakeId);

            List<string> students = new List<string>();

            foreach (TWD_User user in Students) {
               
                string student = user.userName + "|" + user.firstName + " " + user.lastName;
                students.Add(student);

                string[] spStudent = student.Split('|');
                int userId = userRepository.ReturnUserId(spStudent[0]);
                CourseEntityGrade grade = courseEntityGradeRepository.ReturnGrade(courseEntityId, userId);

                if (grade == null)
                {
                    a.Add(student + "| | | |" + userId);
                }
                else
                {
                    if (grade.latePenalty == null)
                    {
                        a.Add(student + "|" + grade.grade.ToString() + "| |" + grade.comment.ToString() + "|" + grade.userId);
                    }
                    else
                    {
                        a.Add(student + "|" + grade.grade.ToString() + "|" + grade.latePenalty.ToString() + "|" + grade.comment.ToString() + "|" + grade.userId);
                    }
                }
            }



            IntakeId         = intake.intakeId;
            IntakeName       = intake.intake1;
            ModuleId         = module.moduleId;
            ModuleName       = module.module1;
            CourseId         = course.courseId;
            CourseName       = course.course1;
            CourseEntityId   = courseEntityId;
            CourseEntityName = courseEntity.courseEntity1;
            A = a;
        }
    }
}