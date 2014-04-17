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
    public class StuController : Controller {
        const string ROLE = "Role";

        [Authorize(Roles = "Administrator, Instructor, Student")]
        public ActionResult StudentArea(int studentUserId = 0) {
            ViewBag.Role = Session["Role"];
            int userId = (int)Session["UserID"];

            if (studentUserId == 0) {
                studentUserId = (int)Session["UserID"];
            }

            TWD_UserRepository twd_UserRepository = new TWD_UserRepository();
            TWD_User user = twd_UserRepository.ReturnUser(studentUserId);

            IntakeRepository intakeRepository = new IntakeRepository();
            Intake intake = intakeRepository.ReturnTheIntake(Convert.ToInt32(user.intakeId));

            ViewBag.UserId  = userId;
            ViewBag.Intake  = intake;
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();

            ViewGradeRepository viewGradeRepository = new ViewGradeRepository();

            return View(viewGradeRepository.ReturnCertificateGrade(intake.intakeId, studentUserId));
        }

        [Authorize(Roles = "Administrator, Instructor, Student")]
        public void ExportStudentIntakeReport(int intakeId = 0, int studentUserId = 0) {

            StringWriter sw = new StringWriter();

            ViewGradeRepository viewGradeRepository = new ViewGradeRepository();
            StudentCertificateGrade studentGradeModel = viewGradeRepository.ReturnCertificateGrade(intakeId, studentUserId);
            sw.WriteLine(studentGradeModel.UserDetail + ",,, Grade");

            GradeExportRepository gradeExportRepository = new GradeExportRepository();

            List<string> reportLine = gradeExportRepository.FormatArrayLines(studentGradeModel.ModuleGroup1);
            foreach (string item in reportLine) {
                sw.WriteLine(item);
            }

            reportLine = gradeExportRepository.FormatArrayLines(studentGradeModel.ModuleGroup2);
            foreach (string item in reportLine) {
                sw.WriteLine(item);
            }

            reportLine = gradeExportRepository.FormatArrayLines(studentGradeModel.ModuleGroup3);
            foreach (string item in reportLine) {
                sw.WriteLine(item);
            }

            reportLine = gradeExportRepository.FormatArrayLines(studentGradeModel.ModuleGroup4);
            foreach (string item in reportLine) {
                sw.WriteLine(item);
            }

            reportLine = gradeExportRepository.FormatArrayLines(studentGradeModel.ModuleGroup5);
            foreach (string item in reportLine) {
                sw.WriteLine(item);
            }

            sw.WriteLine(gradeExportRepository.FormatTotalLine(studentGradeModel.IntakeTotals));

            string fileName = "attachment;filename=TWD " + studentGradeModel.UserDetail + ".csv";

            Response.ClearContent();
            Response.AddHeader("content-disposition", fileName);
            Response.ContentType = "text/csv";
            Response.Write(sw.ToString());
            Response.End();
        }

        [Authorize(Roles = "Administrator, Instructor, Student")]
        public ActionResult ViewCourseGrade(int studentUserId, int courseId = 0) {
            ViewBag.Role = Session["Role"];
            int userId = (int)Session["UserID"];

            TWD_UserRepository twd_UserRepository = new TWD_UserRepository();
            TWD_User user = twd_UserRepository.ReturnUser(studentUserId);

            int intakeId = Convert.ToInt32(user.intakeId);

            IntakeRepository intakeRepository = new IntakeRepository();

            ViewBag.UserId  = userId;
            ViewBag.Intake  = intakeRepository.ReturnTheIntake(intakeId);
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();

            ViewGradeRepository viewGradeRepository = new ViewGradeRepository();
            return View(viewGradeRepository.ReturnStudentCourseDetail(intakeId, courseId, studentUserId));
        }

        [Authorize(Roles = "Administrator, Instructor, Student")]
        public void ExportStudentCourseReport(int intakeId = 0, int studentUserId = 0) {

            StringWriter sw = new StringWriter();

            ViewGradeRepository viewGradeRepository = new ViewGradeRepository();
            StudentCertificateGrade studentGradeModel = viewGradeRepository.ReturnCertificateGrade(intakeId, studentUserId);
            sw.WriteLine(studentGradeModel.UserDetail + ",,, Grade");

            GradeExportRepository gradeExportRepository = new GradeExportRepository();

            List<string> reportLine = gradeExportRepository.FormatArrayLines(studentGradeModel.ModuleGroup1);
            foreach (string item in reportLine) {
                sw.WriteLine(item);
            }

            reportLine = gradeExportRepository.FormatArrayLines(studentGradeModel.ModuleGroup2);
            foreach (string item in reportLine) {
                sw.WriteLine(item);
            }

            reportLine = gradeExportRepository.FormatArrayLines(studentGradeModel.ModuleGroup3);
            foreach (string item in reportLine) {
                sw.WriteLine(item);
            }

            reportLine = gradeExportRepository.FormatArrayLines(studentGradeModel.ModuleGroup4);
            foreach (string item in reportLine) {
                sw.WriteLine(item);
            }

            reportLine = gradeExportRepository.FormatArrayLines(studentGradeModel.ModuleGroup5);
            foreach (string item in reportLine) {
                sw.WriteLine(item);
            }

            sw.WriteLine(gradeExportRepository.FormatTotalLine(studentGradeModel.IntakeTotals));

            string fileName = "attachment;filename=TWD " + studentGradeModel.UserDetail + ".csv";

            Response.ClearContent();
            Response.AddHeader("content-disposition", fileName);
            Response.ContentType = "text/csv";
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}
