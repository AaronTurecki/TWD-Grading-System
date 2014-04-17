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

namespace twd_project.BusinessLogic {

    public class SecurityHelper {

        public string AddUser(TWD_User user) {
            const string STUDENT = "Student";
            string password = Convert.ToString(DateTime.Now);
            string message = "";

            try {
                WebSecurity.CreateUserAndAccount(user.userName, password, new
                {
                    firstName = user.firstName,
                    lastName = user.lastName,
                    intakeId = user.intakeId,
                    bcitId = user.bcitId
                });
                if (!Roles.IsUserInRole(user.userName, STUDENT))
                {
                    Roles.AddUserToRole(user.userName, STUDENT);
                }
                message = "Successfully added " + STUDENT + " " + user.userName;
            } catch {
                message = STUDENT + " " + user.userName + " was not successfully added. Please try again.";
            }
            return message;
        }

        public string RemoveUser(TWD_User user) {

            if (Roles.GetRolesForUser(user.userName).Count() > 0) {
                Roles.RemoveUserFromRoles(user.userName, Roles.GetRolesForUser(user.userName));
            }

            ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(user.userName);
            ((SimpleMembershipProvider)Membership.Provider).DeleteUser(user.userName, true);
            string message = "Successfully removed " + user.userName;

            return message;
        }
    }
}