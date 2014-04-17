using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.ViewModels;

namespace twd_project.Models {

    public class TWD_UserRepository {

        //public StudentMarks StudentGrades(int userId)
        //{
            //twd_projectEntities context = new twd_projectEntities();
            //TWD_User student = (from s in context.TWD_User
            //                    where s.userId == userId
            //                    select s).FirstOrDefault();
            //StudentMarks studentmarks = new StudentMarks();
            //studentmarks.firstName = student.firstName;
            //studentmarks.lastName = student.lastName;
            //studentmarks.intakeId = student.intakeId;
            //studentmarks.userId = student.userId;
           

            
            //studentmarks.spModuleList_Result = (from m in context.spModuleList(studentmarks.intakeId)
            //                                    select m).ToList();
            //// foreach ( Module a in  ){}
            ////CourseRepository a = new CourseRepository();
            ////List<Course> q = a.ReturnModuleCourses(moduleId);
            //studentmarks.spCourseMark_Result = (from c in context.spCourseMark(userId)
            //                                    select c).ToList();
            //studentmarks.spEntityParam_Result = (from e in context.spEntityParam(userId)
            //                                     select e).ToList();
            //return studentmarks;
        //}

        public int ReturnUserId(string userName) {

            twd_projectEntities context = new twd_projectEntities();

            int userId = (from u in context.TWD_User
                         where u.userName == userName
                        select u.userId).FirstOrDefault();

            return userId;
        }

        public List<int> ReturnUserIdForGrades(int intakeId) {

            twd_projectEntities context = new twd_projectEntities();

            List<int> userId = (from u in context.TWD_User
                          where u.intakeId == intakeId
                          select u.userId).ToList();

            return userId;
        }

        public List<string> ReturnIntakeStudents(int intakeId) {

            twd_projectEntities context = new twd_projectEntities();

            List<TWD_User> users = (from u in context.TWD_User
                                    where u.intakeId == intakeId
                                    select u).ToList();

            List<string> students = new List<string>();

            foreach (TWD_User user in users)
            {
                string student = user.userName + "|" + user.firstName + " " + user.lastName;
                students.Add(student);
            }

            return students;
        }

        //***********csw***************************************************************************************************

        public TWD_User ReturnUser(int userId) {

            twd_projectEntities context = new twd_projectEntities();

            TWD_User user = (from u in context.TWD_User
                            where u.userId == userId
                           select u).FirstOrDefault();
            return user;
        }

        public List<TWD_User> ReturnIntakeAllStudents(int intakeId) {

            twd_projectEntities context = new twd_projectEntities();
            List<TWD_User> students = (from u in context.webpages_Roles
                                       from n in u.TWD_User
                                       where u.RoleId == 3
                                          && n.intakeId == intakeId
                                       orderby n.userId
                                       select n).ToList();
            return students;
        }
    }
}