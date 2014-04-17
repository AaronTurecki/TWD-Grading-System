using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.Models;

namespace twd_project.BusinessLogic {
 
    public class SessionHelper {
        const string ROLE = "Role";
        const string USER_ID = "UserId";

        public string SetUserRole(string userName) {
            UserRoleRepository userRoleRepository = new UserRoleRepository();
            string role = userRoleRepository.ReturnUserRole(userName);

            HttpContext.Current.Session[ROLE]    = role;

            TWD_UserRepository user = new TWD_UserRepository();
            int userId = user.ReturnUserId(userName);

            HttpContext.Current.Session[USER_ID] = userId;

            return role;
        }

        public string ReturnUserType() {
            if (HttpContext.Current.Session[ROLE] != null) {
                return (string)HttpContext.Current.Session[ROLE];
            }
            else {
                return "None";
            }
        }

        public void EndSession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

    }
}