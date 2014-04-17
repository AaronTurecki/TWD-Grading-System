using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.Models {

    public class ModuleRepository {
        private const string PROJECT = "PROJECT - final client project";
        private const string OK = "OK";

        public List<string> ReturnModules(int intakeId) {
            twd_projectEntities context = new twd_projectEntities();

            List<Module> allModules = (from m in context.Modules
                                      where m.intakeId == intakeId
                                     select m).ToList();

            List<string> modules = new List<string>();

            foreach (Module module in allModules) {
                string newModule = module.moduleWeight + "|" + module.module1;
                modules.Add(newModule);
            }
            return modules;
        }

        public int ReturnModuleId(int intakeId, string module) {
            twd_projectEntities context = new twd_projectEntities();

            int moduleId = (from m in context.Modules
                           where m.module1 == module
                              && m.intakeId == intakeId
                          select m.moduleId).FirstOrDefault();
            return moduleId;
        }
        //******************csw***************************************************************************
        public List<Module> ReturnIntakeModules(int intakeId) {
            twd_projectEntities context = new twd_projectEntities();

            List<Module> intakeModules = (from i in context.Modules
                                          where i.intakeId == intakeId
                                          orderby i.moduleId
                                          select i).ToList();
            return intakeModules;
        }

        public List<Module> ReturnAllModules(int intakeId) {
            twd_projectEntities context = new twd_projectEntities();

            List<Module> allModules = (from m in context.Modules
                                             where m.intakeId == intakeId
                                            select m).ToList();
            return allModules;
        }

        public Module ReturnTheModule(int moduleId) {
            twd_projectEntities context = new twd_projectEntities();

            Module module = (from m in context.Modules
                            where m.moduleId == moduleId
                           select m).FirstOrDefault();
            return module;
        }

        public string AddModules(int intakeId) {
            const string PLANNING         = "PLANNING & DESIGNING WEB SITES";
            const string CODING           = "CODING & DEVELOPING WEB SITES";
            const string TESTING          = "TESTING & MANAGING WEB SITES";
            const string SPECIALTY        = "SPECIALTY TOPICS";            
            const int    PLANNING_WEIGHT  =  8;
            const int    CODING_WEIGHT    = 10;
            const int    TESTING_WEIGHT   =  8;
            const int    SPECIALTY_WEIGHT =  6;
            const int    PROJECT_WEIGHT   = 8;
            const string OK               = "OK";
            string message = "";

            message = AddModule(intakeId, PLANNING, PLANNING_WEIGHT);
            if (message == OK) {
                message = AddModule(intakeId, CODING, CODING_WEIGHT);
                if (message == OK) {
                    message = AddModule(intakeId, TESTING, TESTING_WEIGHT);
                    if (message == OK) {
                        message = AddModule(intakeId, SPECIALTY, SPECIALTY_WEIGHT);
                        if (message == OK) {
                            message = AddModule(intakeId, PROJECT, PROJECT_WEIGHT);
                        }
                    }
                }
            }
            return message;
        }

        public string AddModule(int intakeId, string module, int moduleWeight) {
            const string NOT_INSERTED = "Could not insert ";
            const string EXISTS       = ". It already exists";
            string message = "";

            twd_projectEntities context = new twd_projectEntities();
            Module newModule = new Module();

            Module existingModule = (from m in context.Modules
                                    where m.module1  == module
                                       && m.intakeId == intakeId
                                   select m).FirstOrDefault();

            if (existingModule == null) {
                try {
                    newModule.module1      = module;
                    newModule.moduleWeight = (short)moduleWeight;
                    newModule.intakeId     = (short)intakeId;
                     
                    context.Modules.Add(newModule);
                    context.SaveChanges();
                    message = OK;
                }
                catch {
                    message = NOT_INSERTED + module;
                }
            }
            else {
                message = "Could not insert " + module + EXISTS;
            }
            return message;
        }

        public string RemoveModules(int intakeId) {
            const string NO = "NO";
            string message  =  OK;

            twd_projectEntities context = new twd_projectEntities();

            List<short> allModules = (from m in context.Modules
                                     where m.intakeId == intakeId
                                    select m.moduleId).ToList(); ;

            foreach (short moduleId in allModules) {

                Module deletingModule = (from m in context.Modules
                                        where m.moduleId == moduleId
                                       select m).FirstOrDefault();

                if (deletingModule != null) {

                    try {
                        context.Modules.Remove(deletingModule);
                        context.SaveChanges();
                    } catch {
                        message = NO;
                    }
                }
            }
            return message;
        }
    }
}