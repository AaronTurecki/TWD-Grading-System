using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.Models {

    public class CourseEntityRepository {

        public List<string> ReturnCourseEntities(int courseId) {
            twd_projectEntities context = new twd_projectEntities();

            List<CourseEntity> allCourseEntities = (from ce in context.CourseEntities
                                           where ce.courseId == courseId
                                         select ce).ToList();

            List<string> courseEntities = new List<string>();

            foreach (CourseEntity courseEntity in allCourseEntities) {
                string newCourseEntity = courseEntity.courseEntityWeight + "|" + courseEntity.courseEntity1;
                courseEntities.Add(newCourseEntity);
            }

            return courseEntities;
        }

        public int ReturnCourseEntityId(int courseId, string courseEntity) {
            twd_projectEntities context = new twd_projectEntities();

            int courseEntityId = (from ce in context.CourseEntities
                                 where ce.courseEntity1 == courseEntity
                                    && ce.courseId == courseId
                                select ce.courseEntityID).FirstOrDefault();
            return courseEntityId;
        }

        //******************csw***************************************************************************
        
        public List<CourseEntity> ReturnAllCourseEntities(int courseId) {
            twd_projectEntities context = new twd_projectEntities();

            List<CourseEntity> allCourseEntities = (from ce in context.CourseEntities
                                                   where ce.courseId == courseId
                                                  select ce).ToList();
            return allCourseEntities;
        }

        public CourseEntity ReturnTheCourseEntity(int courseEntityId) {
            twd_projectEntities context = new twd_projectEntities();

            CourseEntity courseEntity = (from ce in context.CourseEntities
                                        where ce.courseEntityID == courseEntityId
                                       select ce).FirstOrDefault();
            return courseEntity;
        }

        public string PublishEntityGrades(int courseEntityId) {
            const string PUBLISHED = "Grades are now available to view and students have been notified.";
            const string NOT_PUBLISHED = "Could not insert publish grades. Please try again.";
            string message = "";

            twd_projectEntities context = new twd_projectEntities();

            CourseEntity courseEntity = (from ep in context.CourseEntities
                                               where ep.courseEntityID == courseEntityId
                                               select ep).FirstOrDefault();

            if (courseEntity != null) {
                try {
                    courseEntity.showGrades = true;

                    context.SaveChanges();
                    message = PUBLISHED;
                } catch {
                    message = NOT_PUBLISHED;
                }
            }

            return message;
        }

        public string AddCourseEntity(string courseEntity, string courseEntityWeight, int courseId) {
            const string CREATED      = "Successfully created ";
            const string NOT_INSERTED = "Could not insert ";
            const string EXISTS       = ". It already exists";
            string message            = "";

            twd_projectEntities context  = new twd_projectEntities();
            CourseEntity newCourseEntity = new CourseEntity();

            CourseEntity existingCourseEntity = (from c in context.CourseEntities
                                                where c.courseEntity1 == courseEntity
                                                   && c.courseId      == courseId
                                               select c).FirstOrDefault();

            if (existingCourseEntity == null) {
                try {
                    newCourseEntity.courseEntity1 = courseEntity;
                    newCourseEntity.courseEntityWeight = Convert.ToInt16(courseEntityWeight);
                    newCourseEntity.courseId = (short)courseId;
                    newCourseEntity.showGrades = false;

                    context.CourseEntities.Add(newCourseEntity);
                    context.SaveChanges();
                    message = CREATED + courseEntity;
                }
                catch {
                    message = NOT_INSERTED + courseEntity;
                }
            }
            else {
                message = "Could not insert " + courseEntity + EXISTS;
            }
            return message;
        }

        public string RemoveCourseEntity(int courseEntityId) {
            const string REMOVED = "Course entity has successfully been removed.";
            const string NOT_REMOVED = "Unable to remove course entity.";
            string message = "";

            twd_projectEntities context = new twd_projectEntities();

            CourseEntity existingCourseEntity = (from c in context.CourseEntities
                                                where c.courseEntityID == courseEntityId
                                               select c).FirstOrDefault();

            if (existingCourseEntity != null) {

                try {
                    context.CourseEntities.Remove(existingCourseEntity);
                    context.SaveChanges();
                    message = REMOVED;
                }
                catch {
                    message = NOT_REMOVED;
                }
            }
            return message;
        }
    }
}