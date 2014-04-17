using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.ViewModels;
using twd_project.BusinessLogic;

namespace twd_project.Models {

    public class ViewGradeRepository {

        public IntakeGradeModel ReturnAdminReport(int intakeId, int userId) {
            const int TOTALCREDITS = 40;

            IntakeRepository intakeRepository = new IntakeRepository();
            Intake intake = intakeRepository.ReturnTheIntake(intakeId);

            IntakeGradeModel intakeGradeModel = new IntakeGradeModel();
            intakeGradeModel.SetIntakeHeading(intake, intakeRepository.ReturnAllIntakes());

            TWD_UserRepository students = new TWD_UserRepository();
            List<TWD_User> intakeStudents = students.ReturnIntakeAllStudents(intakeId);

            intakeGradeModel.SetStudentHeadings(intakeStudents);

            int moduleNo = 1;

            List<string> certificateGradeLine = new List<string>();
            certificateGradeLine.Add("CG");
            certificateGradeLine.Add("Certificate Grade");
            certificateGradeLine.Add("");
            certificateGradeLine.Add("");

            ModuleRepository moduleRepository = new ModuleRepository();
            List<Module> intakeModules = moduleRepository.ReturnIntakeModules(intakeId);

            foreach (Module module in intakeModules) {
                List<List<string>> moduleGroup = new List<List<string>>();

                List<string> moduleLine = new List<string>();

                int gradePosition = 0;
                double? intakeGradeTotal = 0;

                moduleLine.Add("M");
                moduleLine.Add(module.module1);
                moduleLine.Add("Hours");
                moduleLine.Add(module.moduleWeight.ToString() + " Credits");

                CourseRepository courseRepository = new CourseRepository();
                List<Course> moduleCourses = courseRepository.ReturnModuleCourses(module.moduleId);

                if (moduleCourses.Count() == 0) {
                    foreach (TWD_User student in intakeStudents) {
                        moduleLine.Add("0");
                    }
                } else {
                    foreach (Course course in moduleCourses) {

                        List<string> courseLine = new List<string>();

                        gradePosition = 4;

                        InstructorRepository instructorRepository = new InstructorRepository();
                        bool linkCourse = instructorRepository.isInstructorOfCourse(userId, course.courseId);
                        if (linkCourse == true) {
                            courseLine.Add(course.courseId.ToString());
                        } else {
                            courseLine.Add(" ");
                        }

                        courseLine.Add(course.course1);
                        courseLine.Add(course.courseHours.ToString());
                        courseLine.Add(course.courseWeight.ToString() + "%");

                        StudentGradeRepository studentGradeRepository = new StudentGradeRepository();
                        List<allStudentsCourseGrade_Result> studentGrades = studentGradeRepository.returnAllStudentsCourseGrade(intakeId, course.courseId);

                        foreach (allStudentsCourseGrade_Result student in studentGrades) {
                            List<string> reportRow = new List<string>();

                            double moduleGradeTotal = 0;

                            if (student.courseGrade != null) {
                                courseLine.Add(student.courseGrade.ToString());
                                moduleGradeTotal = Math.Round((float)student.courseGrade * (float)course.courseWeight / 100, 1);
                                try {
                                    moduleGradeTotal += Math.Round(Convert.ToSingle(moduleLine[gradePosition]), 1);
                                    moduleLine[gradePosition] = moduleGradeTotal.ToString();
                                } catch {
                                    moduleLine.Add(moduleGradeTotal.ToString());
                                }
                            } else {
                                courseLine.Add(" ");
                                try {
                                    moduleGradeTotal += Math.Round(Convert.ToSingle(moduleLine[gradePosition]), 1);
                                    moduleLine[gradePosition] = moduleGradeTotal.ToString();
                                } catch {
                                    moduleLine.Add(moduleGradeTotal.ToString());
                                }
                            }
                            gradePosition++;
                        }
                        moduleGroup.Add(courseLine);
                    }
                }

                List<List<string>> sortGroup = new List<List<string>>();

                //Move the module heading to the top of the module group.
                sortGroup.Add(moduleLine);
                int courseCount = moduleGroup.Count();
                foreach (List<string> item in moduleGroup) {
                    sortGroup.Add(item);
                }

                switch (moduleNo) {
                    case 1:
                        intakeGradeModel.SetModuleGroup1(sortGroup);
                        break;
                    case 2:
                        intakeGradeModel.SetModuleGroup2(sortGroup);
                        break;
                    case 3:
                        intakeGradeModel.SetModuleGroup3(sortGroup);
                        break;
                    case 4:
                        intakeGradeModel.SetModuleGroup4(sortGroup);
                        break;
                    case 5:
                        intakeGradeModel.SetModuleGroup5(sortGroup);
                        break;
                    default:
                        break;
                }
                moduleNo++;

                for (int i = 4; i < gradePosition; i++) {
                    intakeGradeTotal = Math.Round(Convert.ToSingle(moduleLine[i]) * (Convert.ToSingle(module.moduleWeight) / TOTALCREDITS), 1);

                    try {
                        intakeGradeTotal += Math.Round(Convert.ToSingle(certificateGradeLine[i]), 1);
                        certificateGradeLine[i] = intakeGradeTotal.ToString();
                    } catch {
                        certificateGradeLine.Add(intakeGradeTotal.ToString());
                    }
                }
            }
            intakeGradeModel.SetIntakeTotals(certificateGradeLine);

            return intakeGradeModel;
        }

        public CourseGradeModel ReturnInstructorReport(int intakeId, int userId) {

            IntakeRepository intakeRepository = new IntakeRepository();
            Intake intake = intakeRepository.ReturnTheIntake(intakeId);

            CourseGradeModel courseGradeModel = new CourseGradeModel();
            courseGradeModel.SetIntakes(intake, intakeRepository.ReturnAllIntakes());

            List<string> firstNames = new List<string>();
            List<string> lastNames  = new List<string>();
            List<string> bcitIds    = new List<string>();
            List<int>    userIds    = new List<int>();

            firstNames.Add(" ");
            firstNames.Add(" ");
            firstNames.Add(" ");
            lastNames.Add(" ");
            lastNames.Add(" ");
            lastNames.Add(" ");
            bcitIds.Add(" ");
            bcitIds.Add(" ");
            bcitIds.Add(" ");
            userIds.Add(0);
            userIds.Add(0);
            userIds.Add(0);

            TWD_UserRepository twd_UserRepository = new TWD_UserRepository();
            List<TWD_User> intakeStudents = twd_UserRepository.ReturnIntakeAllStudents(intakeId);

            courseGradeModel.SetStudentHeadings(intakeStudents);

            InstructorRepository instructorRepository = new InstructorRepository();
            List<CourseModel> courseIds = instructorRepository.ReturnInstructorsCourses(userId);

            List<List<string>> courseGroup = new List<List<string>>();

            foreach (CourseModel course in courseIds) {

                List<string> courseLine = new List<string>();

                courseLine.Add(course.CourseId.ToString());
                courseLine.Add(course.Course);
                courseLine.Add(course.Hours.ToString() + " hours");

                StudentGradeRepository studentGradeRepository = new StudentGradeRepository();
                List<allStudentsCourseGrade_Result> studentGrades = studentGradeRepository.returnAllStudentsCourseGrade(intakeId, course.CourseId);

                if (studentGrades.Count() == 0) {
                    foreach (TWD_User student in intakeStudents) {
                        courseLine.Add(" ");
                    }
                } else {
                    foreach (allStudentsCourseGrade_Result student in studentGrades) {

                        if (student.courseGrade != null) {
                            courseLine.Add(student.courseGrade.ToString());
                        } else {
                            courseLine.Add(" ");
                        }
                    }
                }
                courseGroup.Add(courseLine);
            }
            courseGradeModel.SetCourseGroup(courseGroup);
            return courseGradeModel;
        }

        public StudentCertificateGrade ReturnCertificateGrade(int intakeId, int userId) {
            const int TOTALCREDITS = 40;

            IntakeRepository intakeRepository = new IntakeRepository();
            Intake intake = intakeRepository.ReturnTheIntake(intakeId);

            StudentCertificateGrade studentGradeModel = new StudentCertificateGrade();
            studentGradeModel.SetStudentDetails(userId, intake.intake1);

            int moduleNo = 1;

            List<string> certificateGradeLine = new List<string>();
            certificateGradeLine.Add("CG");
            certificateGradeLine.Add("Certificate Grade");
            certificateGradeLine.Add("");
            certificateGradeLine.Add("");

            ModuleRepository moduleRepository = new ModuleRepository();
            List<Module> intakeModules = moduleRepository.ReturnIntakeModules(intakeId);

            foreach (Module module in intakeModules) {
                List<List<string>> moduleGroup = new List<List<string>>();

                List<string> moduleLine = new List<string>();

                int gradePosition = 0;
                double? certificateGradeTotal = 0;

                moduleLine.Add("M");
                moduleLine.Add(module.module1);
                moduleLine.Add("Hours");
                moduleLine.Add(module.moduleWeight.ToString() + " Credits");

                CourseRepository courseRepository = new CourseRepository();
                List<Course> moduleCourses = courseRepository.ReturnModuleCourses(module.moduleId);

                if (moduleCourses.Count() == 0) {
                        moduleLine.Add("0");
                } else {
                    foreach (Course course in moduleCourses) {

                        List<string> courseLine = new List<string>();

                        gradePosition = 4;

                        courseLine.Add(course.courseId.ToString());
                        courseLine.Add(course.course1);
                        courseLine.Add(course.courseHours.ToString());
                        courseLine.Add(course.courseWeight.ToString() + "%");
                        
                        StudentGradeRepository studentGradeRepository = new StudentGradeRepository();
                        aStudentCourseGrade_Result studentGrade = studentGradeRepository.returnAStudentCourseGrade(intakeId, course.courseId, userId);

                        List<string> reportRow = new List<string>();

                        double moduleGradeTotal = 0;

                        if (studentGrade != null) {
                            courseLine.Add(studentGrade.courseGrade.ToString());
                            moduleGradeTotal = Math.Round((float)studentGrade.courseGrade * (float)course.courseWeight / 100, 1);
                            try {
                                moduleGradeTotal += Math.Round(Convert.ToSingle(moduleLine[gradePosition]), 1);
                                moduleLine[gradePosition] = moduleGradeTotal.ToString();
                            } catch {
                                moduleLine.Add(moduleGradeTotal.ToString());
                            }
                        } else {
                            courseLine.Add(" ");
                            try {
                                moduleGradeTotal += Math.Round(Convert.ToSingle(moduleLine[gradePosition]), 1);
                                moduleLine[gradePosition] = moduleGradeTotal.ToString();
                            } catch {
                                moduleLine.Add(moduleGradeTotal.ToString());
                            }
                        }
                        moduleGroup.Add(courseLine);
                    }
                }

                List<List<string>> sortGroup = new List<List<string>>();

                //Move the module heading to the top of the module group.
                sortGroup.Add(moduleLine);
                int courseCount = moduleGroup.Count();
                foreach (List<string> item in moduleGroup) {
                    sortGroup.Add(item);
                }

                switch (moduleNo) {
                    case 1:
                        studentGradeModel.SetModuleGroup1(sortGroup);
                        break;
                    case 2:
                        studentGradeModel.SetModuleGroup2(sortGroup);
                        break;
                    case 3:
                        studentGradeModel.SetModuleGroup3(sortGroup);
                        break;
                    case 4:
                        studentGradeModel.SetModuleGroup4(sortGroup);
                        break;
                    case 5:
                        studentGradeModel.SetModuleGroup5(sortGroup);
                        break;
                    default:
                        break;
                }
                moduleNo++;

                if (Convert.ToSingle(moduleLine[4]) != 0) {
                    certificateGradeTotal = Math.Round(Convert.ToSingle(moduleLine[4]) * (Convert.ToSingle(module.moduleWeight) / TOTALCREDITS), 1);

                    try {
                        certificateGradeTotal += Math.Round(Convert.ToSingle(certificateGradeLine[4]), 1);
                        certificateGradeLine[4] = certificateGradeTotal.ToString();
                    } catch {
                        certificateGradeLine.Add(certificateGradeTotal.ToString());
                    }
                }
            }
            studentGradeModel.SetIntakeTotals(certificateGradeLine);

            return studentGradeModel;
        }

        public StudentCourseGrade ReturnStudentCourseDetail(int intakeId, int courseId, int userId) {

            IntakeRepository intakeRepository = new IntakeRepository();
            Intake intake = intakeRepository.ReturnTheIntake(intakeId);

            StudentCourseGrade studentGradeDetailModel = new StudentCourseGrade();
            studentGradeDetailModel.SetStudentDetails(userId, intake.intake1, courseId);

            StudentGradeRepository studentGradeRepository = new StudentGradeRepository();
            List<studentEntityGrade_Result> studentEntityGrades = studentGradeRepository.returnStudentEntityGrades(courseId, userId);

            List<List<string>> courseGroup = new List<List<string>>();
            List<string> courseTotals = new List<string>();
            double courseTotal = 0;

            foreach (studentEntityGrade_Result studentEntityGrade in studentEntityGrades) {
                List<string> entityLine = new List<string>();
                entityLine.Add("E");
                entityLine.Add(studentEntityGrade.courseEntity);
                entityLine.Add(studentEntityGrade.courseEntityWeight.ToString());
                entityLine.Add(studentEntityGrade.grade.ToString());
                entityLine.Add(studentEntityGrade.latePenalty.ToString());
                entityLine.Add(studentEntityGrade.comment);
                courseGroup.Add(entityLine);
                courseTotal += Convert.ToSingle(studentEntityGrade.courseEntityWeight * (studentEntityGrade.grade - (studentEntityGrade.grade * studentEntityGrade.latePenalty / 100)) / 100);
            }

            List<studentParameterGrade_Result> studentParameterGrades = studentGradeRepository.returnStudentParameterGrades(courseId, userId);
            CourseEntityRepository courseEntityRepository = new CourseEntityRepository();

            double entityTotal = 0;
            
            if (studentParameterGrades.Count() > 0) {
                for (int i=0; i < studentParameterGrades.Count(); i++) {
                    int oldEntityId = studentParameterGrades[i].courseEntityId;
                    List<List<string>> tempGroup = new List<List<string>>();
                    while (oldEntityId == studentParameterGrades[i].courseEntityId) {
                            List<string> parameterLine = new List<string>();
                            parameterLine.Add("P");
                            parameterLine.Add(studentParameterGrades[i].entityParameter);
                            parameterLine.Add(studentParameterGrades[i].entityParameterWeight.ToString());
                            parameterLine.Add(studentParameterGrades[i].grade.ToString());
                            parameterLine.Add(studentParameterGrades[i].latePenalty.ToString());
                            parameterLine.Add(studentParameterGrades[i].comment);
                            tempGroup.Add(parameterLine);
                            entityTotal += Convert.ToSingle(studentParameterGrades[i].entityParameterWeight * (studentParameterGrades[i].grade - (studentParameterGrades[i].grade * studentParameterGrades[i].latePenalty / 100)) / 100);
                            i++;
                            if (i == studentParameterGrades.Count()) {
                                break;
                            }
                        }
                    
                    if (studentParameterGrades.Count() > 0) {
                        List<string> entityLine = new List<string>();
                        CourseEntity courseEntity = courseEntityRepository.ReturnTheCourseEntity(oldEntityId);
                        entityLine.Add("EP");
                        entityLine.Add(courseEntity.courseEntity1);
                        entityLine.Add(courseEntity.courseEntityWeight.ToString());
                        entityLine.Add(entityTotal.ToString());
                        entityLine.Add(" ");
                        //entityLine.Add(" ");
                        courseGroup.Add(entityLine);
                        courseTotal += Math.Round(Convert.ToSingle(courseEntity.courseEntityWeight * entityTotal / 100),2);
                    }
                    //Move the module heading to the top of the module group.
                    foreach (List<string> item in tempGroup) {
                        courseGroup.Add(item);
                    }
                }
            }

            courseTotals.Add("Course Total");
            courseTotals.Add(" ");
            courseTotals.Add(courseTotal.ToString());
            courseTotals.Add(" ");
            courseTotals.Add(" ");
            courseTotals.Add(" ");

            studentGradeDetailModel.SetCourseGroup(courseGroup);
            studentGradeDetailModel.SetCourseTotals(courseTotals);

            return studentGradeDetailModel;
        }
    }
}