using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.ViewModels
{
    public class StudentGrades {
        public TWD_User Student          { get; set; }
        public string   ParameterGrade   { get; set; }
        public string   ParameterPenalty { get; set; }
        public string   ParameterComment { get; set; }

        public StudentGrades(TWD_User student, EntityParameterGrade parameterGrade) {
            Student = student;
            if (parameterGrade == null) {
                ParameterGrade   = " ";
                ParameterPenalty = " ";
                ParameterComment = " ";
            } else {
                ParameterGrade   = parameterGrade.grade.ToString();
                ParameterPenalty = parameterGrade.latePenalty.ToString();
                ParameterComment = parameterGrade.comment;
            }
        } 
    }
}