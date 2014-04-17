using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.Models {

    public class IntakeRepository {

        public Intake ReturnIntake(int intakeID) {
            twd_projectEntities context = new twd_projectEntities();

            Intake intake = (from i in context.Intakes
                             where i.intakeId == intakeID
                             select i).FirstOrDefault();
            return intake;
        }

        public List<Intake> GetIntakes() {
            twd_projectEntities context = new twd_projectEntities();
            List<Intake> intakes = context.Intakes.ToList();
            return intakes;
        }

        public int ReturnIntakeId(string strIntake) {
            twd_projectEntities context = new twd_projectEntities();

            int intakeId = (from i in context.Intakes
                           where i.intake1 == strIntake
                          select i.intakeId).FirstOrDefault();
            return intakeId;
        }

        public List<string> ReturnIntakes() {
            twd_projectEntities context = new twd_projectEntities();

            List<string> intakes = (from i in context.Intakes
                                 orderby i.intakeId descending
                                  select i.intake1).ToList();
            return intakes;
        }
        //************CSW************************************************************************************
        public int ReturnLatestIntake() {
            twd_projectEntities context = new twd_projectEntities();

            var intakes = (from i in context.Intakes
                         select i.intakeId);
            int intake = intakes.Max();
           
            return intake;
        }
       
        public List<Intake> ReturnAllIntakes() {
            twd_projectEntities context = new twd_projectEntities();

            List<Intake> intakes = (from i in context.Intakes
                                        orderby i.intakeId descending
                                         select i).ToList();
            return intakes;
        }

        public Intake ReturnTheIntake(int intakeId) {
            twd_projectEntities context = new twd_projectEntities();

            Intake intake = (from i in context.Intakes
                                where i.intakeId == intakeId
                               select i).FirstOrDefault();
            return intake;
        }

        public string AddIntake(Intake intake) {
            string message = "";

            if (intake.startDate == null || intake.endDate == null) {
                message = "Both start and end dates are required to create an intake";
            } else {
                if (intake.intake1 == "") {
                    message = "Intake cannot be left blank";
                } else {
                    CreateIntake(intake);
                }
            }
            return message;
        }

        public string CreateIntake(Intake intake) {
            const string CREATED = "Successfully created ";
            const string NOT_INSERTED = "Could not insert ";
            const string EXISTS = ". It already exists";
            const string OK = "OK";
            string message = "";

            twd_projectEntities context = new twd_projectEntities();
            Intake newIntake = new Intake();

            Intake existingIntake = (from i in context.Intakes
                                    where i.intake1 == intake.intake1
                                   select i).FirstOrDefault();

            if (existingIntake == null) {
                try {
                    newIntake.intake1   = intake.intake1;
                    newIntake.startDate = Convert.ToDateTime(intake.startDate);
                    newIntake.endDate   = Convert.ToDateTime(intake.endDate);

                    context.Intakes.Add(newIntake);
                    context.SaveChanges();

                    ModuleRepository module = new ModuleRepository();
                    message = module.AddModules(newIntake.intakeId);

                    if (message == OK) {
                        message = CREATED + intake.intake1;
                    }
                } catch {
                    message = NOT_INSERTED + intake.intake1;
                }
            } else {
                message = NOT_INSERTED + intake.intake1 + EXISTS;
            }
            return message;
        }

        public string RemoveIntake(int intakeId) {
            const string REMOVED     = " has successfully been removed.";
            const string NOT_REMOVED = "Unable to remove ";
            string message           = "";

            twd_projectEntities context = new twd_projectEntities();

            Intake existingIntake = (from i in context.Intakes
                                    where i.intakeId == intakeId
                                   select i).FirstOrDefault();

            if (existingIntake != null) {
                try {
                    ModuleRepository moduleRepository = new ModuleRepository();
                    moduleRepository.RemoveModules(intakeId);

                    context.Intakes.Remove(existingIntake);
                    context.SaveChanges();
                    message = existingIntake.intake1 + REMOVED;
                }
                catch {
                    message = NOT_REMOVED + existingIntake.intake1 + ". All " + existingIntake.intake1 
                            + "'s courses and students need to be removed first";
                }
            }
            return message;
        }
    }
}