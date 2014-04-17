using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.ViewModels;
using twd_project.BusinessLogic;

namespace twd_project.Models {

    public class ViewGradesRepository {

        public IntakeGradeModel ReturnAdminReport(int intakeId) {
            const int TOTALCREDITS = 40;

            IntakeRepository intakeRepository = new IntakeRepository();
            Intake intake = intakeRepository.ReturnTheIntake(intakeId);

            IntakeGradeModel intakeGradeModel = new IntakeGradeModel();
            intakeGradeModel.SetIntakeHeading(intakeId, intake.intake1,
                                               String.Format("{0:d}", intake.startDate),
                                               String.Format("{0:d}", intake.endDate),
                                               intakeRepository.ReturnAllIntakes());

            List<string> fName = new List<string>();
            List<string> lName = new List<string>();
            List<string> uName = new List<string>();

            fName.Add(" ");
            fName.Add(" ");
            fName.Add(" ");
            lName.Add(" ");
            lName.Add(" ");
            lName.Add(" ");
            uName.Add(" ");
            uName.Add(" ");
            uName.Add(" ");

            TWD_UserRepository students = new TWD_UserRepository();
            List<TWD_User> intakeStudents = students.ReturnIntakeAllStudents(intakeId);
            foreach (TWD_User student in intakeStudents) {
                fName.Add(student.firstName);
                lName.Add(student.lastName);
                uName.Add(student.bcitId);
            }

            intakeGradeModel.SetStudentHeadings(fName, lName, uName);

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
                }

                foreach (Course course in moduleCourses) {

                    List<string> courseLine = new List<string>();

                    gradePosition = 4;

                    courseLine.Add("C");
                    courseLine.Add(course.course1);
                    courseLine.Add(course.courseHours.ToString());
                    courseLine.Add(course.courseWeight.ToString() + "%");

                    StudentGradeRepository studentGradeRepository = new StudentGradeRepository();
                    List<studentCourseGrade_Result> studentGrades = studentGradeRepository.returnStudentCourseGrade(intakeId, course.courseId);

                    foreach (studentCourseGrade_Result student in studentGrades) {
                        List<string> reportRow = new List<string>();

                        double moduleGradeTotal = 0;

                        //double after1 = Math.Round(123.45, 1, MidpointRounding.AwayFromZero);

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
                    //intakeGradeTotal = (Convert.ToSingle(moduleLine[i]) * (Convert.ToSingle(module.moduleWeight) / TOTALCREDITS));

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
            courseGradeModel.SetIntakeHeading(intakeId, intake.intake1,
                                               String.Format("{0:d}", intake.startDate),
                                               String.Format("{0:d}", intake.endDate),
                                               intakeRepository.ReturnAllIntakes());

            List<string> fName = new List<string>();
            List<string> lName = new List<string>();
            List<string> uName = new List<string>();

            fName.Add(" ");
            fName.Add(" ");
            fName.Add(" ");
            lName.Add(" ");
            lName.Add(" ");
            lName.Add(" ");
            uName.Add(" ");
            uName.Add(" ");
            uName.Add(" ");

            TWD_UserRepository students = new TWD_UserRepository();
            List<TWD_User> intakeStudents = students.ReturnIntakeAllStudents(intakeId);
            foreach (TWD_User student in intakeStudents) {
                fName.Add(student.firstName);
                lName.Add(student.lastName);
                uName.Add(student.bcitId);
            }

            courseGradeModel.SetStudentHeadings(fName, lName, uName);

            InstructorRepository instructorRepository = new InstructorRepository();
            List<CourseModel> courseIds = instructorRepository.ReturnInstructorsCourses(userId);

            List<List<string>> courseGroup = new List<List<string>>();

            foreach (CourseModel course in courseIds) {

                List<string> courseLine = new List<string>();

                courseLine.Add(course.CourseId.ToString());
                courseLine.Add(course.Course);
                courseLine.Add(course.Hours.ToString() + " hours");

                StudentGradeRepository studentGradeRepository = new StudentGradeRepository();
                List<studentCourseGrade_Result> studentGrades = studentGradeRepository.returnStudentCourseGrade(intakeId, course.CourseId);

                if (studentGrades.Count() == 0) {
                    foreach (TWD_User student in intakeStudents) {
                        courseLine.Add(" ");
                    }
                } else {
                    foreach (studentCourseGrade_Result student in studentGrades) {

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
    }
}