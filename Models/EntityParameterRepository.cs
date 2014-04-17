using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.Models {

    public class EntityParameterRepository {

        public List<string> ReturnEntityParameters(int corseEntityId) {
            twd_projectEntities context = new twd_projectEntities();

            List<EntityParameter> allEntityParameters = (from ep in context.EntityParameters
                                                        where ep.courseEntityId == corseEntityId
                                                       select ep).ToList();

            List<string> newEntitieParameters = new List<string>();

            foreach (EntityParameter entityParameter in allEntityParameters) {
                string newCourseEntity = entityParameter.entityParameterWeight + "|" + entityParameter.entityParameter1;
                newEntitieParameters.Add(newCourseEntity);
            }
            return newEntitieParameters;
        }

        public int ReturnEntityParameterId(int corseEntityId, string entityParameter) {
            twd_projectEntities context = new twd_projectEntities();

            int entityParameterId = (from ep in context.EntityParameters
                                    where ep.entityParameter1 == entityParameter
                                   select ep.entityParameterId).FirstOrDefault();
            return entityParameterId;
        }

        //******************csw***************************************************************************

        public List<EntityParameter> ReturnAllEntityParameters(int corseEntityId) {
            twd_projectEntities context = new twd_projectEntities();

            List<EntityParameter> allEntityParameters = (from ep in context.EntityParameters
                                                        where ep.courseEntityId == corseEntityId
                                                       select ep).ToList();
            return allEntityParameters;
        }

        public EntityParameter HasEntityAParameter(int corseEntityId) {
            twd_projectEntities context = new twd_projectEntities();

            EntityParameter entityParameter = (from ep in context.EntityParameters
                                              where ep.courseEntityId == corseEntityId
                                             select ep).FirstOrDefault();
            return entityParameter;
        }

        public List<EntityParameter> ReturnCourseEntityParameters(int corseEntityId) {
            twd_projectEntities context = new twd_projectEntities();

            List<EntityParameter> courseEntityParameters = (from ce in context.CourseEntities
                                                            from ep in context.EntityParameters
                                                            where ce.courseId == 1
                                                               && ce.courseEntityID == ep.courseEntityId
                                                            select ep).ToList();
            return courseEntityParameters;
        }

        public string PublishParameterGrades(int entityParameterId) {
            const string PUBLISHED      = "Grades are now available to view and students have been notified.";
            const string NOT_PUBLISHED  = "Could not insert publish grades. Please try again.";
            string       message        = "";

            twd_projectEntities context = new twd_projectEntities();

            EntityParameter entityParameter = (from ep in context.EntityParameters
                                               where ep.entityParameterId == entityParameterId
                                               select ep).FirstOrDefault();

            if (entityParameter != null) {
                try {
                    entityParameter.showGrades = true;

                    context.SaveChanges();
                    message = PUBLISHED;
                } catch {
                    message = NOT_PUBLISHED;
                }
            }

            return message;
        }

        public EntityParameter ReturnTheEntityParameter(int entityParameterId) {
            twd_projectEntities context = new twd_projectEntities();

            EntityParameter entityParameter = (from ep in context.EntityParameters
                                              where ep.entityParameterId == entityParameterId
                                             select ep).FirstOrDefault();
            return entityParameter;
        }

        public string AddEntityParameter(string entityParameter, string entityParameterWeight, int courseEntityId) {
            const string CREATED      = "Successfully created ";
            const string NOT_INSERTED = "Could not insert ";
            const string EXISTS       = ". It already exists";
            string message            = "";

            twd_projectEntities context         = new twd_projectEntities();
            EntityParameter newEentityParameter = new EntityParameter();

            EntityParameter existingEntityParameter = (from e in context.EntityParameters
                                                      where e.entityParameter1 == entityParameter
                                                         && e.courseEntityId   == courseEntityId
                                                     select e).FirstOrDefault();

            if (existingEntityParameter == null) {
                try {
                    newEentityParameter.entityParameter1 = entityParameter;
                    newEentityParameter.entityParameterWeight = Convert.ToInt16(entityParameterWeight);
                    newEentityParameter.courseEntityId = (short)courseEntityId;
                    newEentityParameter.showGrades = false;

                    context.EntityParameters.Add(newEentityParameter);
                    context.SaveChanges();
                    message = CREATED + entityParameter;
                } catch {
                    message = NOT_INSERTED + entityParameter;
                }
            } else {
                message = "Could not insert " + entityParameter + EXISTS;
            }
            return message;
        }

        public string RemoveEntityParameter(int centityParameterId) {
            const string REMOVED = "Entity parameter has successfully been removed.";
            const string NOT_REMOVED = "Unable to remove entity parameter.";
            string message = "";

            twd_projectEntities context = new twd_projectEntities();

            EntityParameter existingEntityParameter = (from c in context.EntityParameters
                                                      where c.entityParameterId == centityParameterId
                                                     select c).FirstOrDefault();

            if (existingEntityParameter != null) {

                try {
                    context.EntityParameters.Remove(existingEntityParameter);
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