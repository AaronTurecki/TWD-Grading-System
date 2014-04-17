using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.Models {

    public class CourseRepository {

        public List<string> ReturnCourses(int moduleId) {
            twd_projectEntities context = new twd_projectEntities();

            List<Course> allCourses = (from c in context.Courses
                                      where c.moduleId == moduleId
                                     select c).ToList();

            List<string> courses = new List<string>();

            foreach (Course course in allCourses) {
                string newCourse = course.courseWeight + "|" + course.course1;
                courses.Add(newCourse);
            }
            return courses;
        }

        public int ReturnCourseId(int moduleId, string course) {
            twd_projectEntities context = new twd_projectEntities();

            int courseId = (from c in context.Courses
                            from m in context.Modules
                           where c.course1  == course
                              && c.moduleId == moduleId
                              && c.moduleId == m.moduleId
                          select c.courseId).FirstOrDefault();
            return courseId;
        }

        //******************csw***************************************************************************

        public List<Course> ReturnModuleCourses(int moduleId) {
            twd_projectEntities context = new twd_projectEntities();

            List<Course> moduleCourses = (from i in context.Courses
                                          where i.moduleId == moduleId
                                          orderby i.courseId
                                          select i).ToList();
            return moduleCourses;
        }

        public Course ReturnTheCourse(int courseId) {
            twd_projectEntities context = new twd_projectEntities();

            Course course = (from m in context.Courses
                             where m.courseId == courseId
                             select m).FirstOrDefault();
            return course;
        }

        public string AddCourse(string course, string courseWeight, string courseHours, int moduleId) {
            const string CREATED      = "Successfully created ";
            const string NOT_INSERTED = "Could not insert ";
            const string EXISTS       = ". It already exists";
            string message            = "";

            twd_projectEntities context = new twd_projectEntities();
            Course newCourse = new Course();

            Course existingCourse = (from c in context.Courses
                                    where c.course1  == course
                                       && c.moduleId == moduleId
                                   select c).FirstOrDefault();

            if (existingCourse == null) {
                try {
                    newCourse.course1      = course;
                    newCourse.courseWeight = Convert.ToInt16(courseWeight);
                    newCourse.courseHours  = Convert.ToInt16(courseHours);
                    newCourse.moduleId     = (short)moduleId;

                    context.Courses.Add(newCourse);
                    context.SaveChanges();
                    message = CREATED + course;
                } catch {
                    message = NOT_INSERTED + course;
                }
            } else {
                message = "Could not insert " + course + EXISTS;
            }
            return message;
        }

        public string RemoveCourse(int courseId) {
            const string REMOVED     = "The course has successfully been removed.";
            const string NOT_REMOVED = "Unable to remove the course.";
            string message           = "";

            twd_projectEntities context = new twd_projectEntities();

            Course existingCourse = (from c in context.Courses
                                    where c.courseId == courseId
                                   select c).FirstOrDefault();

            if (existingCourse != null) {

                try {
                    context.Courses.Remove(existingCourse);
                    context.SaveChanges();
                    message = REMOVED;
                } catch {
                    message = NOT_REMOVED;
                }
            }
            return message;
        }
    }
}