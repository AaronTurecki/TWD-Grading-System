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

    [Authorize(Roles = "Administrator, Instructor")]
    public class InstructorController : Controller {

        public ActionResult InstructorArea(int intakeId = 0, string message = " ") {
            ViewBag.Role    = Session["Role"];
            int userId      = (int)Session["UserID"];
            ViewBag.Message = message;

            IntakeRepository intakeRepository = new IntakeRepository();

            if (intakeId == 0) {
                intakeId = intakeRepository.ReturnLatestIntake();
            }

            ViewBag.UserId  = userId;
            ViewBag.Intake  = intakeRepository.ReturnTheIntake(intakeId);
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();

            ViewGradeRepository viewGradeRepository = new ViewGradeRepository();
            return View(viewGradeRepository.ReturnInstructorReport(intakeId, userId));
        }

        [Authorize(Roles = "Administrator, Instructor")]
        public void ExportCSVReport(int intakeId, string intake) {
            int userID = (int)Session["UserID"];
            StringWriter sw = new StringWriter();

            ViewGradeRepository viewGradeRepository = new ViewGradeRepository();
            CourseGradeModel courseGradeModel = viewGradeRepository.ReturnInstructorReport(intakeId, userID);

            GradeExportRepository gradeExportRepository = new GradeExportRepository();
            sw.WriteLine(gradeExportRepository.FormatInstructorHeading(courseGradeModel.Students
                                                                     , courseGradeModel.IntakeO));

            List<string> reportLines = gradeExportRepository.FormatArrayLines(courseGradeModel.CourseGroup);
            foreach (string item in reportLines) {
                sw.WriteLine(item);
            }

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

            return RedirectToAction("InstructorArea", "Instructor", new { intakeId = intakeId, message = message });
        }

        [Authorize(Roles = "Administrator, Instructor")]
        public ActionResult SelectEntity(int intakeId, int courseId = 0) {
            ViewBag.Role = Session["Role"];
            int userId = (int)Session["UserID"];

            IntakeRepository intakeRepository = new IntakeRepository();

            ViewBag.UserId  = userId;
            ViewBag.Intake  = intakeRepository.ReturnTheIntake(intakeId);
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();

            BuildCourseInstructor buildCourse = new BuildCourseInstructor(courseId);

            return View(buildCourse);
        }

        [Authorize(Roles = "Administrator, Instructor")]
        public ActionResult SelectStudent(int intakeId, int courseEntityId, string message = "")
        {
            ViewBag.Message = message;
            ViewBag.Role = Session["Role"];
            int userId = (int)Session["UserID"];

            IntakeRepository intakeRepository = new IntakeRepository();

            ViewBag.UserId = userId;
            ViewBag.Intake = intakeRepository.ReturnTheIntake(intakeId);
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();

            BuildCourseEntityGrade buildCourseEntityGrade = new BuildCourseEntityGrade(courseEntityId);

            return View(buildCourseEntityGrade);
        }


        [Authorize(Roles = "Administrator, Instructor")]
        public ActionResult ParameterStudentGrades(int intakeId, int entityParameterId, string message = "") {
            ViewBag.Message = message;
            ViewBag.Role = Session["Role"];
            int userId = (int)Session["UserID"];

            IntakeRepository intakeRepository = new IntakeRepository();

            ViewBag.UserId = userId;
            ViewBag.Intake = intakeRepository.ReturnTheIntake(intakeId);
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();

            ParameterStudentsGrade parameterStudentsGrade = new ParameterStudentsGrade(entityParameterId);

            return View(parameterStudentsGrade);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Instructor")]
        public ActionResult UpdateParameterGrade(int intakeId, int entityParameterId, string studentUserId, string grade, string latePenalty, string comment) {

            int gradeInt = 0;

            if (grade != " ") {
                gradeInt = Convert.ToInt32(grade);
            }

            int latePenaltyInt = 0;

            if (latePenalty != " ") {
                latePenaltyInt = Convert.ToInt32(latePenalty);
            }

            int userIdInt = Convert.ToInt32(studentUserId);

            ParameterGradeRepository parameterGradeRepository = new ParameterGradeRepository();
            string message = parameterGradeRepository.UpdateParameterGrade(entityParameterId, userIdInt, gradeInt, latePenaltyInt, comment);

            return RedirectToAction("ParameterStudentGrades", "Instructor", new { intakeId = intakeId, entityParameterId = entityParameterId, message = message });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Instructor")]
        public ActionResult PublishParameterGrade(int intakeId, int entityParameterId) {

            EntityParameterRepository entityParameterRepository = new EntityParameterRepository();
            string message = entityParameterRepository.PublishParameterGrades(entityParameterId);

            return RedirectToAction("ParameterStudentGrades", "Instructor", new { intakeId = intakeId, entityParameterId = entityParameterId, message = message });
        }


        [HttpPost]
        [Authorize(Roles = "Administrator, Instructor")]
        public ActionResult UpdateGrade(int intakeId, int entityId, string grade, string latePenalty, string comment, string studentUserId) {

            int gradeInt = 0;

            if (grade == "")
            {
                grade = "0";
                gradeInt = Convert.ToInt32(grade);
            }

            if (grade != " ") {
                gradeInt = Convert.ToInt32(grade);
            }

            int latePenaltyInt = 0;

            if (latePenalty == "")
            {
                latePenalty = "0";
                latePenaltyInt = Convert.ToInt32(latePenalty);
            }

            if (latePenalty != " ") {
                latePenaltyInt = Convert.ToInt32(latePenalty);
            }

            int userIdInt = Convert.ToInt32(studentUserId);

            CourseEntityGradeRepository courseEntityGradeRepository = new CourseEntityGradeRepository();
            string message = courseEntityGradeRepository.UpdateGrade(entityId, userIdInt, gradeInt, latePenaltyInt, comment);

            return RedirectToAction("SelectStudent", "Instructor", new { intakeId = intakeId, courseEntityId = entityId, message = message });

        }
        [HttpPost]
        [Authorize(Roles = "Administrator, Instructor")]
        public ActionResult PublishEntityGrade(int intakeId, int courseEntityId) {

            CourseEntityRepository courseEntityRepository = new CourseEntityRepository();
            string message = courseEntityRepository.PublishEntityGrades(courseEntityId);

            return RedirectToAction("SelectStudent", "Instructor", new { intakeId = intakeId, courseEntityId = courseEntityId, message = message });
        }
    }
}
