using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.Models  {

    public class ParameterGradeRepository {

        public EntityParameterGrade ReturnStudentParameterGrade(int userId, int entityParameterId) {
            twd_projectEntities context = new twd_projectEntities();

            EntityParameterGrade entityParameterGrade = (from ep in context.EntityParameterGrades
                                                        where ep.entityParameterId == entityParameterId
                                                           && ep.userId == userId
                                                       select ep).FirstOrDefault();
            return entityParameterGrade;
        }

        public string UpdateParameterGrade(int entityParameterId, int userId, int grade, int latePenalty, string comment) {

            const string CREATED     = "Successfully added grades.";
            const string UPDATED     = "Successfully updated grades.";
            const string NOT_ADDED   = "Error - Could not add student grades";
            const string NOT_UPDATED = "Error - Could not update student grades";
            string message = "";

            twd_projectEntities context = new twd_projectEntities();
            EntityParameterGrade existingEntityParameterGrade = (from c in context.EntityParameterGrades
                                                                where c.userId == userId
                                                                   && c.entityParameterId == entityParameterId
                                                               select c).FirstOrDefault();
            if (existingEntityParameterGrade == null) {
                try {
                    EntityParameterGrade newEntityParameterGrade = new EntityParameterGrade();
                    newEntityParameterGrade.grade = grade;
                    newEntityParameterGrade.latePenalty = latePenalty;
                    newEntityParameterGrade.comment = comment;
                    newEntityParameterGrade.userId = userId;
                    newEntityParameterGrade.entityParameterId = (Int16)(entityParameterId);

                    context.EntityParameterGrades.Add(newEntityParameterGrade);
                    context.SaveChanges();
                    message = CREATED;
                } catch {
                    message = NOT_ADDED;
                }
            } else {
                try {
                    existingEntityParameterGrade.grade = grade;
                    existingEntityParameterGrade.latePenalty = latePenalty;
                    existingEntityParameterGrade.comment = comment;
                    existingEntityParameterGrade.userId = userId;
                    existingEntityParameterGrade.entityParameterId = (Int16)(entityParameterId);

                    context.SaveChanges();
                    message = UPDATED;
                } catch {
                    message = NOT_UPDATED;
                }
            }
            return message;
        }
    }
}
