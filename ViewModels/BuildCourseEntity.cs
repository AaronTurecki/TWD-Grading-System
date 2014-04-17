using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.Models;

namespace twd_project.ViewModels {

    public class BuildCourseEntity {
        public int                   IntakeId         { get; private set; }
        public string                IntakeName       { get; private set; }
        public int                   ModuleId         { get; private set; }
        public string                ModuleName       { get; private set; }
        public int                   CourseId         { get; private set; }
        public string                CourseName       { get; private set; }
        public int                   CourseEntityId   { get; private set; }
        public string                CourseEntityName { get; private set; }
        public List<EntityParameter> EntityParameters { get; private set; }

        public BuildCourseEntity() {
        }
        
        public BuildCourseEntity(int courseEntityId) {
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

            IntakeId         = intake.intakeId;
            IntakeName       = intake.intake1;
            ModuleId         = module.moduleId;
            ModuleName       = module.module1;
            CourseId         = course.courseId;
            CourseName       = course.course1;
            CourseEntityId   = courseEntityId;
            CourseEntityName = courseEntity.courseEntity1;
        }
    }
}