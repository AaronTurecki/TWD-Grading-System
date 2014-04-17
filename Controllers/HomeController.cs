using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using twd_project.BusinessLogic;
using twd_project.Models;
using twd_project.ViewModels;
using WebMatrix.WebData;
using Recaptcha;

namespace twd_project.Controllers {
    [Authorize]
    public class HomeController : Controller {

        public const string ROLE = "Role";
        public const string USER_ID = "UserID";

        [AllowAnonymous]
        public ActionResult Index(string message = "") {
            ViewBag.Role = "None";
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LoginModel model) {
            const int MAX_ATTEMPTS = 5;
            const int DAYS = 0;
            const int HOURS = 0;
            const int MINS = 2;
            const int SECONDS = 0;
            TimeSpan ts = new TimeSpan(DAYS, HOURS, MINS, SECONDS);
            const string WAIT_MESSAGE = "Please wait two minutes before trying again.";
            if (ModelState.IsValid && WebSecurity.IsAccountLockedOut(model.UserName,
                                                            MAX_ATTEMPTS, ts))
            {
                string message = "Your account has been locked.  " + WAIT_MESSAGE;
                ModelState.AddModelError("", message);
                return View(model);
            }

            else if (ModelState.IsValid
                && WebSecurity.Login(model.UserName, model.Password)) {

                System.Threading.Thread.Sleep(2000); // Add two second delay

                SessionHelper sessionHelper = new SessionHelper();
                string role = sessionHelper.SetUserRole(model.UserName);

                if (role == "Administrator") {
                    return RedirectToAction("AdminArea", "Admin");
                } else if (role == "Instructor") {
                    return RedirectToAction("InstructorArea", "Instructor");
                } else {
                    return RedirectToAction("StudentArea", "Stu");
                }
            }
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            
            return View(model);
        }

        public ActionResult LogOff() {
            // Log the user out.
            WebSecurity.Logout();
            SessionHelper sessionHelper = new SessionHelper();
            sessionHelper.EndSession();
            string message = User.Identity.Name + " you have successfully logged out.";
            return RedirectToAction("Index", "Home", new { message = message });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CreateIntake(string message = "") {
            ViewBag.Role = Session["Role"];
            ViewBag.Message = message;

            CreateIntakeZZZZ createIntake = new CreateIntakeZZZZ();

            return View(createIntake);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string message = "") {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [RecaptchaControlMvc.CaptchaValidator]
        public ActionResult ResetPassword(FormCollection collection, bool captchaValid)
        {
            if (!captchaValid)
            {
                return RedirectToAction("Index", "Home",
                                        new { message = "Verification failed.  Please try again." });
            }
            else
            {
                string email = collection["txtEmail"];
                const int MINUTES = 40;
                string token = WebSecurity.GeneratePasswordResetToken(email, MINUTES);

                //TEMP FOR TESTING LOCALLY
                //const string FROM = "michael@twd.whiterabbitdev.com";
                //const string SUBJECT = "Reset Password Request";
                //string confirmLink = "http://localhost:59384/Home/ChangePassword?token=" + token;
                //string message = "We have received your request to reset your password. To do so, please click the following link within the next 40 minutes: " + confirmLink;
                //Email confirmation = new Email();
                //confirmation.Recipient = email;
                //confirmation.Sender = FROM;
                //confirmation.Subject = SUBJECT;
                //confirmation.Body = message;
                //END TEMP

                EmailHelper emailHlp = new EmailHelper();
                emailHlp.SendResetRequest(email, token);
              //emailHlp.SendMessage(confirmation);

                return RedirectToAction("Index", "Home", new { message = "Please check for a message from us which provides instructions on how to reset your password." });
            }
        }

        [AllowAnonymous]
        public ActionResult ConfirmPasswordRequest() {
            ViewBag.Role = Session["Role"];
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePassword(string token) {
            PasswordResetModel prm = new PasswordResetModel();
            ViewBag.ResetToken = token;
            ViewBag.Role = Session["Role"];
            ViewBag.Message = "";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult UpdatePassword(int intakeId = 0, string message = "")  // change the layout
        {
            int userId = (int)Session[USER_ID];
            ViewBag.Message = message;
            ViewBag.Role = Session[ROLE];
            ViewBag.UserId = userId;

            IntakeRepository intakeRepository = new IntakeRepository();
            if (intakeId == 0)
            {
                intakeId = intakeRepository.ReturnLatestIntake();
            }
            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();
            ViewBag.Intake = intakeRepository.ReturnTheIntake(intakeId);
            ViewBag.UserIntakeId = intakeId;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult UpdatePassword(UpdatePasswordModel model, int intakeId = 0)
        {
            IntakeRepository intakeRepository = new IntakeRepository();
            
            if (intakeId == 0)
            {
                intakeId = intakeRepository.ReturnLatestIntake();
            }

            ViewBag.Intakes = intakeRepository.ReturnAllIntakes();
            ViewBag.UserIntakeId = intakeId;
            ViewBag.Role = Session[ROLE];

            MembershipUser u = Membership.GetUser(User.Identity.Name);

            try 
            {
                if (u.ChangePassword(model.OldPassword, model.NewPassword))
                {
                    ViewBag.Success = true;
                }
                else
                {
                    ViewBag.Fail = true;
                }
            }
            catch (Exception e)
            {
                ViewBag.Fail = true;
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChangePassword(PasswordResetModel model) {
            if (ModelState.IsValid)
            {
                bool success = WebSecurity.ResetPassword(model.ResetToken,
                                                         model.NewPassword);
                if (success)
                {
                    ViewBag.Success = true;
                    ViewBag.Role = Session["Role"];
                }
                else
                {
                    ViewBag.Fail = true;
                    ViewBag.Token = model.ResetToken;
                    ViewBag.Role = Session["Role"];
                }
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Warning() {
            ViewBag.Role = Session["Role"];
            return View();
        }

        [AllowAnonymous]
        public ActionResult Requirements() {
            return View();
        }
    }
}
