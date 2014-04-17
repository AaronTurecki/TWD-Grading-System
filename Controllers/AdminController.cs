using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using twd_project.BusinessLogic;
using twd_project.Models;
using twd_project.ViewModels;
using WebMatrix.WebData;

namespace twd_project.Controllers {
    [Authorize]
    public class AdminController : Controller {

        public const string ROLE = "Role";
        public const string USER_ID = "UserID";

        [Authorize(Roles = "Administrator")]
        public ActionResult AdminArea(int intakeId = 0, string message="") {
            ViewBag.Role = Session[ROLE];
            int userId = (int)Session[USER_ID];

            IntakeRepository intakeRepository = new IntakeRepository();

            if (intakeId == 0) {
                intakeId = intakeRepository.ReturnLatestIntake();
            }

            ViewBag.UserId  = userId;
            ViewBag.Intake  = intakeRepository.ReturnTheIntake(intakeId);
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();
            ViewBag.Message = message;

            ViewGradeRepository viewGradeRepository = new ViewGradeRepository();
            return View(viewGradeRepository.ReturnAdminReport(intakeId, userId));
        }

        [Authorize(Roles = "Administrator")]
        public void ExportCSVReport(int intakeId, string intake) {
            int userId = (int)Session[USER_ID];

            StringWriter sw = new StringWriter();

            ViewGradeRepository viewGradeRepository = new ViewGradeRepository();
            IntakeGradeModel intakeGradeModel = viewGradeRepository.ReturnAdminReport(intakeId, userId);

            GradeExportRepository gradeExportRepository = new GradeExportRepository();
            sw.WriteLine(gradeExportRepository.FormatAdminHeading(intakeGradeModel.Students, intake));

            List<string> reportLines = gradeExportRepository.FormatArrayLines(intakeGradeModel.ModuleGroup1);
            foreach (string item in reportLines) {
                sw.WriteLine(item);
            }

            reportLines = gradeExportRepository.FormatArrayLines(intakeGradeModel.ModuleGroup2);
            foreach (string item in reportLines) {
                sw.WriteLine(item);
            }

            reportLines = gradeExportRepository.FormatArrayLines(intakeGradeModel.ModuleGroup3);
            foreach (string item in reportLines) {
                sw.WriteLine(item);
            }

            reportLines = gradeExportRepository.FormatArrayLines(intakeGradeModel.ModuleGroup4);
            foreach (string item in reportLines) {
                sw.WriteLine(item);
            }

            reportLines = gradeExportRepository.FormatArrayLines(intakeGradeModel.ModuleGroup5);
            foreach (string item in reportLines) {
                sw.WriteLine(item);
            }

            sw.WriteLine(gradeExportRepository.FormatTotalLine(intakeGradeModel.IntakeTotals));

            string fileName = "attachment;filename=" + intake + ".csv";
            
            Response.ClearContent();
            Response.AddHeader("content-disposition", fileName);
            Response.ContentType = "text/csv";
            Response.Write(sw.ToString());
            Response.End();
        }

        [Authorize(Roles = "Administrator, Instructor")]
        public ActionResult EmailStudent(int intakeId, string studentName = " ") {
            string message = " ";

            if (studentName == " ") {
                message = "Email has successfully been sent.";
            } else {
                message = "An email has been sent to " + studentName;
            }

            return RedirectToAction("AdminArea", "Admin", new { intakeId = intakeId, message = message });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult BuildIntake(int intakeId, string message = "") {
            ViewBag.Message = message;
            ViewBag.Role = Session[ROLE];
            int userId = (int)Session[USER_ID];

            IntakeRepository intakeRepository = new IntakeRepository();

            if (intakeId == 0) {
                intakeId = intakeRepository.ReturnLatestIntake();
            }

            ViewBag.UserId = userId;
            ViewBag.Intake = intakeRepository.ReturnTheIntake(intakeId);
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();

            BuildIntake buildIntake = new BuildIntake(intakeId);

            return View(buildIntake);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddIntake(Intake intake) {
            IntakeRepository intakeRepository = new IntakeRepository();
            string message = intakeRepository.AddIntake(intake);

            return RedirectToAction("BuildIntake", "Admin", new { intakeId = intake.intakeId, message = message });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult RemoveIntake(int intakeId, int removeIntake) {
            IntakeRepository intakeRepository = new IntakeRepository();
            string message = intakeRepository.RemoveIntake(removeIntake);
            if (intakeId == removeIntake) {
                intakeId = 0;
            }

            return RedirectToAction("BuildIntake", "Admin", new { intakeId = intakeId, message = message });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddStudent(TWD_User student) {
            string message = "";

            if (ModelState.IsValid) {
                SecurityHelper securityHelper = new SecurityHelper();
                message = securityHelper.AddUser(student);
            }
            return RedirectToAction("BuildIntake", "Admin", new { intakeId = student.intakeId, message = message });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult RemoveStudent(TWD_User student) {

            SecurityHelper securityHelper = new SecurityHelper();
            string message = securityHelper.RemoveUser(student);

            return RedirectToAction("BuildIntake", "Admin", new { intakeId = student.intakeId, message = message });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult StudentEmail(int intakeId, string subject, string body, string userName = "") {
            EmailHelper emailHelper = new EmailHelper();
            string message = emailHelper.SendStudentEmail(userName, subject, body);

            return RedirectToAction("BuildIntake", "Admin", new { intakeId = intakeId, message = message });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult BuildModule(int intakeId, int moduleId, string message = "") {
            ViewBag.Message = message;
            ViewBag.Role = Session[ROLE];
            int userId = (int)Session[USER_ID];

            IntakeRepository intakeRepository = new IntakeRepository();

            ViewBag.UserId = userId;
            ViewBag.Intake = intakeRepository.ReturnTheIntake(intakeId);
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();

            BuildModule buildModule = new BuildModule(moduleId);

            return View(buildModule);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddCourse(int intakeId, string course, string courseWeight, string courseHours, int moduleId) {
            CourseRepository courseRepository = new CourseRepository();
            string message = courseRepository.AddCourse(course, courseWeight, courseHours, moduleId);

            return RedirectToAction("BuildModule", "Admin", new { intakeId = intakeId, moduleId = moduleId, message = message });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult RemoveCourse(int intakeId, int courseId, int moduleId) {
            CourseRepository courseRepository = new CourseRepository();
            string message = courseRepository.RemoveCourse(courseId);

            return RedirectToAction("BuildModule", "Admin", new { intakeId = intakeId, moduleId = moduleId, message = message });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult BuildCourse(int intakeId, int courseId, string message = "") {
            ViewBag.Message = message;
            ViewBag.Role = Session[ROLE];
            int userId = (int)Session[USER_ID];

            IntakeRepository intakeRepository = new IntakeRepository();

            ViewBag.UserId = userId;
            ViewBag.Intake = intakeRepository.ReturnTheIntake(intakeId);
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();

            BuildCourse buildCourse = new BuildCourse(courseId);

            return View(buildCourse);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddCourseEntity(int intakeId, string courseEntity, string courseEntityWeight, int courseId) {
            CourseEntityRepository courseEntityRepository = new CourseEntityRepository();
            string message = courseEntityRepository.AddCourseEntity(courseEntity, courseEntityWeight, courseId);

            return RedirectToAction("BuildCourse", "Admin", new { intakeId = intakeId, courseId = courseId, message = message });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult RemoveCourseEntity(int intakeId, int courseEntityId, int courseId) {
            CourseEntityRepository courseEntityRepository = new CourseEntityRepository();
            string message = courseEntityRepository.RemoveCourseEntity(courseEntityId);

            return RedirectToAction("BuildCourse", "Admin", new { intakeId = intakeId, courseId = courseId, message = message });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddInstructor(int intakeId, int courseId, int userId) {
            InstructorRepository instructorRepository = new InstructorRepository();
            string message = instructorRepository.AddInstructor(userId, courseId);

            return RedirectToAction("BuildCourse", "Admin", new { intakeId = intakeId, courseId = courseId, message = message });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult RemoveInstructor(int intakeId, int courseId, int userId) {
            InstructorRepository instructorRepository = new InstructorRepository();
            string message = instructorRepository.RemoveInstructor(userId, courseId);

            return RedirectToAction("BuildCourse", "Admin", new { intakeId = intakeId, courseId = courseId, message = message });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult BuildCourseEntity(int intakeId, int courseEntityId, string message = "") {
            ViewBag.Message = message;
            ViewBag.Role = Session[ROLE];
            int userId = (int)Session[USER_ID];

            IntakeRepository intakeRepository = new IntakeRepository();

            ViewBag.UserId = userId;
            ViewBag.Intake = intakeRepository.ReturnTheIntake(intakeId);
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();

            BuildCourseEntity buildCourseEntity = new BuildCourseEntity(courseEntityId);

            return View(buildCourseEntity);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddEntityParameter(int intakeId, string entityParameter, string entityParameterWeight, int courseEntityId) {
            EntityParameterRepository entityParameterRepository = new EntityParameterRepository();
            string message = entityParameterRepository.AddEntityParameter(entityParameter, entityParameterWeight, courseEntityId);

            return RedirectToAction("BuildCourseEntity", "Admin", new { intakeId = intakeId, courseEntityId = courseEntityId, message = message });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult RemoveEntityParameter(int intakeId, int entityParameterId, int courseEntityId) {
            EntityParameterRepository entityParameterRepository = new EntityParameterRepository();
            string message = entityParameterRepository.RemoveEntityParameter(entityParameterId);

            return RedirectToAction("BuildCourseEntity", "Admin", new { intakeId = intakeId, courseEntityId = courseEntityId, message = message });
        }

        [Authorize(Roles = "Administrator, Instructor")]
        public ActionResult ViewStudents(int intakeID = 0) {
            if (intakeID == 0) {
                intakeID = 6;
            }

            IntakeRepository intakeRepo = new IntakeRepository();
            Intake intake = intakeRepo.ReturnIntake(intakeID);
            ViewBag.Role = Session[ROLE];
            return View(intake);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult RegisterUser(int intakeId=0, string message = "") {
            int userId = (int)Session[USER_ID];
            ViewBag.Message = message;
            ViewBag.Role = Session[ROLE];
            ViewBag.UserId = userId;

            IntakeRepository intakeRepository = new IntakeRepository();
            if (intakeId == 0) {
                intakeId = intakeRepository.ReturnLatestIntake();
            }
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();
            ViewBag.Intake = intakeRepository.ReturnTheIntake(intakeId);
            ViewBag.intakeObjs = intakeRepository.GetIntakes();
            ViewBag.UserIntakeId = intakeId;

            return View();
        }
        
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult RegisterUser(RegisterModel model, int intakeId=0) {
            // Ensure model is valid just in case user has disabled javascript.

            IntakeRepository intakeRepository = new IntakeRepository();
            if (intakeId == 0)
            {
                intakeId = intakeRepository.ReturnLatestIntake();
            }
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();
            ViewBag.UserIntakeId = intakeId;

            if (ModelState.IsValid) {
                if (model.UserName != "" && model.FirstName != "" && model.LastName != "" && model.BCITId != "" && model.Password != "")
                {
                    try
                    {
                        // Create the user.
                        int? intake = null;
                        if (model.Role == "Student")
                        {
                            //IntakeRepository intakeRepository = new IntakeRepository();
                            intake = intakeRepository.ReturnIntakeId(model.Intake);
                        }
                        WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new
                        {
                            firstName = model.FirstName,
                            lastName = model.LastName,
                            intakeId = intake,
                            bcitId = model.BCITId
                        });
                        if (!Roles.IsUserInRole(model.UserName, model.Role))
                        {
                            Roles.AddUserToRole(model.UserName, model.Role);
                        }
                        string messageSuccess = "Successfully added " + model.Role + " " + model.UserName + ".";
                        return RedirectToAction("AdminArea", "Admin", new { message = messageSuccess });
                    }
                    catch
                    {
                        //Bug on reload of Register view when catch error!!!!
                        ViewBag.Role = Session[ROLE];
                        ModelState.AddModelError("", "An error occurred.  The user may already exist.");
                        return RedirectToAction("AdminArea", "Admin");
                    }
                }
                else {
                    string messageError = "An error occurred. Please complete each form section before submitting.";
                    return RedirectToAction("AdminArea", "Admin", new { message = messageError });
                }

                
            }
            else
            {
                string messageError = "An error occurred. Please complete each form section before submitting.";
                return RedirectToAction("AdminArea", "Admin", new { message = messageError });
            }
        }
    }
}
