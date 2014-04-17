using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.Models {

    public class StudentGradeRepository {

        public List<allStudentsCourseGrade_Result> returnAllStudentsCourseGrade(int intakeId, int courseId) {

            twd_projectEntities context = new twd_projectEntities();
            List<allStudentsCourseGrade_Result> studentsGrades = (from i in context.allStudentsCourseGrade(intakeId, courseId)
                                                                select i).ToList();
            return studentsGrades;
        }

        public aStudentCourseGrade_Result returnAStudentCourseGrade(int intakeId, int courseId, int userId) {

            twd_projectEntities context = new twd_projectEntities();
            aStudentCourseGrade_Result studentGrade = (from i in context.aStudentCourseGrade(intakeId, courseId, userId)
                                                     select i).FirstOrDefault();
            return studentGrade;
        }

        public List<studentEntityGrade_Result> returnStudentEntityGrades(int courseId, int userId) {

            twd_projectEntities context = new twd_projectEntities();
            List<studentEntityGrade_Result> studentEntityGrades = (from i in context.studentEntityGrade(courseId, userId)
                                                                   select i).ToList();
            return studentEntityGrades;
        }

        public List<studentParameterGrade_Result> returnStudentParameterGrades(int courseId, int userId) {

            twd_projectEntities context = new twd_projectEntities();
            List<studentParameterGrade_Result> studentParameterGrades = (from i in context.studentParameterGrade(courseId, userId)
                                                                       select i).ToList();
            return studentParameterGrades;
        }
    }
}