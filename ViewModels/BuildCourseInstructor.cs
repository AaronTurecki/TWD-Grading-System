using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.Models;

namespace twd_project.ViewModels {

    public class BuildCourseInstructor {

        public int                IntakeId          { get; private set; }
        public string             IntakeName        { get; private set; }
        public int                ModuleId          { get; private set; }
        public string             ModuleName        { get; private set; }
        public int                CourseId          { get; private set; }
        public string             CourseName        { get; private set; }
        public List<CourseEntity> CourseEntities    { get; private set; }
        public List<List<string>> EntityParameters  { get; private set; }
        public List<Instructor>   CourseInstructors { get; private set; }
        public List<TWD_User>     AllMarkers        { get; private set; }

        public BuildCourseInstructor(int courseId) {
            CourseRepository courseRepository = new CourseRepository();
            Course course = courseRepository.ReturnTheCourse(courseId);

            ModuleRepository moduleRepository = new ModuleRepository();
            Module module = moduleRepository.ReturnTheModule(course.moduleId);

            IntakeRepository intakeRepository = new IntakeRepository();
            Intake intake = intakeRepository.ReturnTheIntake(module.intakeId);

            CourseEntityRepository courseEntityRepository = new CourseEntityRepository();
            List<CourseEntity> courseEntities = courseEntityRepository.ReturnAllCourseEntities(courseId);

            InstructorRepository instructorRepository = new InstructorRepository();
            CourseInstructors = instructorRepository.ReturnCourseInstructors(courseId);

            UserRoleRepository userRoleRepository = new UserRoleRepository();
            AllMarkers = userRoleRepository.ReturnAllMarkers();

            IntakeId   = intake.intakeId;
            IntakeName = intake.intake1;
            ModuleId   = module.moduleId;
            ModuleName = module.module1;
            CourseId   = courseId;
            CourseName = course.course1;
            List<CourseEntity> newCourseEntities = new List<CourseEntity>(); ;
            EntityParameterRepository entityParameterRepository = new EntityParameterRepository();
            List<EntityParameter> entityParameters = entityParameterRepository.ReturnCourseEntityParameters(courseId);

            foreach (CourseEntity courseEntity in courseEntities) {
                EntityParameter entityParameter = entityParameterRepository.HasEntityAParameter(courseEntity.courseEntityID);
                if (entityParameter == null) {
                    newCourseEntities.Add(courseEntity);
                }
            }

            CourseEntities = newCourseEntities;

            List<List<string>> parameterGroup = new List<List<string>>();

            if (entityParameters.Count() > 0) {
                for (int i = 0; i < entityParameters.Count(); i++) {
                    int oldCourseEntityId = Convert.ToInt32(entityParameters[i].courseEntityId);
                    CourseEntity courseEntity = courseEntityRepository.ReturnTheCourseEntity(Convert.ToInt32(entityParameters[i].courseEntityId));

                    List<string> courseEntityLine = new List<string>();
                    courseEntityLine.Add("E");
                    courseEntityLine.Add(courseEntity.courseEntityID.ToString());
                    courseEntityLine.Add(courseEntity.courseEntityWeight.ToString());
                    courseEntityLine.Add(courseEntity.courseEntity1);
                    courseEntityLine.Add(" ");

                    parameterGroup.Add(courseEntityLine);

                    while (oldCourseEntityId == entityParameters[i].courseEntityId) {
                        List<string> entityParameterLine = new List<string>();
                        entityParameterLine.Add("EP");
                        entityParameterLine.Add(entityParameters[i].entityParameterId.ToString());
                        entityParameterLine.Add(entityParameters[i].entityParameterWeight.ToString());
                        entityParameterLine.Add(entityParameters[i].entityParameter1);
                        i++;
                        parameterGroup.Add(entityParameterLine);

                        if (i == entityParameters.Count()) {
                            break;
                        }
                    }
                }
            }

            EntityParameters = parameterGroup;
        }
    }
}