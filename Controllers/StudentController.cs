using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using twd_project.BusinessLogic;
using twd_project.Models;

namespace twd_project.Controllers
{
    [Authorize(Roles = "Student, Administrator, Instructor")]
    public class StudentController : Controller
    {
        //[HttpGet]
        //public ActionResult StudentArea(int studentID = 0)
        //{
        //    int userId;
        //    //ViewBag.Message = User.Identity.Name + ". You are a " + role + " in the Student Area.";
        //    TWD_UserRepository studentGrades = new TWD_UserRepository();
           
        //    SessionHelper sessionHelper = new SessionHelper();
        //    ViewBag.Role = sessionHelper.ReturnUserType();

        //    if (studentID == 0)
        //    {
        //        userId = (int)Session["UserId"];
        //    }
        //    else {
        //        userId = studentID;
        //        ViewBag.studentId = userId; 
        //    }



           
        //    //return View(studentGrades.StudentGrades(userId));
        //}
        //[HttpGet]
        //public ActionResult StudentAreaParam(int courseEntityID, int studentID = 0)
        //{
        //    int userId;
        //    //ViewBag.Message = User.Identity.Name + ". You are a " + role + " in the Student Area.";
        //    TWD_UserRepository studentGrades = new TWD_UserRepository();

        //    SessionHelper sessionHelper = new SessionHelper();
        //    ViewBag.Role = sessionHelper.ReturnUserType();
        //    ViewBag.CourseId = courseEntityID;
        //    if (studentID == 0)
        //    {
        //        userId = (int)Session["UserId"];
        //    }
        //    else
        //    {
        //        userId = studentID;
        //        ViewBag.studentId = userId; 
        //    }


        //    return View(studentGrades.StudentGrades(userId));
        //}
        //[HttpGet]
        //public ActionResult StudentAreaEntity( int courseId, int studentID = 0)
        //{
        //    int userId;
        //    //ViewBag.Message = User.Identity.Name + ". You are a " + role + " in the Student Area.";
        //    TWD_UserRepository studentGrades = new TWD_UserRepository();

        //    SessionHelper sessionHelper = new SessionHelper();
        //    ViewBag.Role = sessionHelper.ReturnUserType();
        //    ViewBag.CourseId = courseId;
        //    if (studentID == 0)
        //    {
        //        userId = (int)Session["UserId"];
        //    }
        //    else
        //    {
        //        userId = studentID;
        //        ViewBag.studentId = userId; 
        //    }


        //    return View(studentGrades.StudentGrades(userId));
        //}
    }
}
