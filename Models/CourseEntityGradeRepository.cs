using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.Models
{
    public class CourseEntityGradeRepository
    {
        public CourseEntityGrade ReturnGrade(int courseEntityId, int userId)
        {
            twd_projectEntities context = new twd_projectEntities();

            CourseEntityGrade courseEntityGrades = (from c in context.CourseEntityGrades
                                        where c.courseEntityID == courseEntityId
                                        && c.userId == userId
                                        select c).FirstOrDefault();

            return courseEntityGrades;
        }



        //******* DO NOT REMOVE ----- USED FOR MODAL IN SELECT STUDENT VIEW TO UPDATE GRADES *******//
        public string UpdateGrade(int entityId, int userId, int grade, int latePenalty, string comment)
        {
            const string CREATED = "Successfully updated ";
            const string NOT_INSERTED = "Error - Could not insert";
            string message = "";

            twd_projectEntities context = new twd_projectEntities();

            CourseEntityGrade existingCourseEntityGrade = (from c in context.CourseEntityGrades
                                                 where c.userId == userId
                                                    && c.courseEntityID == entityId
                                                 select c).FirstOrDefault();

            if (existingCourseEntityGrade == null)
            {
                try
                {
                    CourseEntityGrade newCourseEntityGrade = new CourseEntityGrade();
                    newCourseEntityGrade.grade = grade;
                    newCourseEntityGrade.latePenalty = latePenalty;
                    newCourseEntityGrade.comment = comment;
                    newCourseEntityGrade.userId = userId;
                    newCourseEntityGrade.courseEntityID = (short)entityId;

                    context.CourseEntityGrades.Add(newCourseEntityGrade);
                    context.SaveChanges();
                    message = CREATED;
                }
                catch
                {
                    message = NOT_INSERTED;
                }
            }
            else {
                try
                {
                    existingCourseEntityGrade.grade = grade;
                    existingCourseEntityGrade.latePenalty = latePenalty;
                    existingCourseEntityGrade.comment = comment;
                    existingCourseEntityGrade.userId = userId;
                    existingCourseEntityGrade.courseEntityID = (short)entityId;

                    context.SaveChanges();
                    message = CREATED;
                }
                catch
                {
                    message = NOT_INSERTED;
                }
            
            }
            return message;
        }
    }
}


//newCourseEntityGrade.grade = grade;
//                    newCourseEntityGrade.latePenalty = latePenalty;
//                    newCourseEntityGrade.comment = comment;
//                    newCourseEntityGrade.courseEntityID = (short)entityId;