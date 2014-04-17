using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.ViewModels;

namespace twd_project.Models {

    public class InstructorRepository {

        public bool isInstructorOfCourse(int userId, int courseId) {
            twd_projectEntities context = new twd_projectEntities();
            bool linkCourse = false;

            var courseInstructor = from u in context.TWD_User
                                   from c in u.Courses
                                  where c.courseId == courseId
                                     && u.userId == userId
                                    select new {
                                          Name = u.firstName
                                    };

            foreach (var row in courseInstructor) {
                linkCourse = true;
            }
            return linkCourse;
        }


        public List<Instructor> ReturnCourseInstructors(int courseId) {

            twd_projectEntities context = new twd_projectEntities();

            var courseInstructors = from u in context.TWD_User
                                    from c in u.Courses
                                   where c.courseId == courseId        
                                  select new {Name = u.firstName + " " + 
                                                     u.lastName,
                                            UserId = u.userId};

            List<Instructor> instructors = new List<Instructor>();

            foreach (var row in courseInstructors) {
                string name = row.Name;
                int userId = row.UserId;
                Instructor instructor = new Instructor(name, userId);
                instructors.Add(instructor);
            } 
            return instructors;
        }

        public List<CourseModel> ReturnInstructorsCourses(int userId) {

            twd_projectEntities context = new twd_projectEntities();

            var ids = from u in context.Courses
                      from c in u.TWD_User
                     where c.userId == userId
                    select new { CourseId    = u.courseId,
                                 CourseName  = u.course1,
                                 CourseHours = u.courseHours
                               };

            List<CourseModel> courseIds = new List<CourseModel>();

            foreach (var id in ids) {
                int    courseId    = id.CourseId;
                string course      = id.CourseName;
                int?   courseHours = id.CourseHours;
                CourseModel courseModel = new CourseModel(courseId, course, courseHours);
                courseIds.Add(courseModel);
            }
            return courseIds;
        }

        //public TWD_User ReturnInstructorCourses(int userID) {

        //    twd_projectEntities context = new twd_projectEntities();
        //    TWD_User user = (from i in context.TWD_User
        //                     where i.userId == userID
        //                     select i).FirstOrDefault();
        //    return user;
        //}

        public string AddInstructor(int userId, int courseId) {
            const string CREATED = "Successfully added instructor.";
            const string NOT_INSERTED = "Could not add instructor.";
            string message = "";

            twd_projectEntities context = new twd_projectEntities();
            TWD_User marker = (from u in context.TWD_User
                               where u.userId == userId
                               select u).FirstOrDefault();

            Course course = (from c in context.Courses
                             where c.courseId == courseId
                             select c).FirstOrDefault();
            try {
                marker.Courses.Add(course);
                context.SaveChanges();

                message = CREATED;
            } catch {
                message = NOT_INSERTED;
            }
            return message;
        }

        public string RemoveInstructor(int userId, int courseId) {
            const string REMOVED = "Successfully removed instructor.";
            const string NOT_REMOVED = "Could not remove instructor.";
            string message = "";

            twd_projectEntities context = new twd_projectEntities();
            TWD_User marker = (from u in context.TWD_User
                               where u.userId == userId
                               select u).FirstOrDefault();

            Course course = (from c in context.Courses
                             where c.courseId == courseId
                             select c).FirstOrDefault();
            try {
                if (marker.Courses.Contains(course)) {
                    marker.Courses.Remove(course);
                    context.SaveChanges();

                    message = REMOVED;
                } else {
                    message = NOT_REMOVED;
                }
            } catch {
                message = NOT_REMOVED;
            }
            return message;
        }
    }
}