using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.Models {

    public class UserRoleRepository {

        public string ReturnUserRole(string userName) {

            twd_projectEntities context = new twd_projectEntities();

            string userType = (from u in context.webpages_Roles
                               from n in u.TWD_User
                              where n.userName == userName
                             select u.RoleName).FirstOrDefault();

            return userType;
        }

        //public List<string> ReturnAllMarkers() {
        //    const int ADMINISTRATOR = 1;
        //    const int INSTRUCTOR    = 2;

        //    twd_projectEntities context = new twd_projectEntities();

        //    List<TWD_User> users = (from u in context.webpages_Roles
        //                            from n in u.TWD_User
        //                           where (u.RoleId == INSTRUCTOR 
        //                              ||  u.RoleId == ADMINISTRATOR)
        //                          select n).ToList();

        //    List<string> markers = new List<string>();

        //    foreach (TWD_User user in users) {
        //        string marker = user.userName + "|" + user.firstName + " " + user.lastName;
        //        markers.Add(marker);
        //    }

        //    return markers;
        //}

        //******************csw***************************************************************************
        public List<TWD_User> ReturnAllMarkers() {
            const int ADMINISTRATOR = 1;
            const int INSTRUCTOR = 2;

            twd_projectEntities context = new twd_projectEntities();

            List<TWD_User> markers = (from  u in context.webpages_Roles
                                      from  n in u.TWD_User
                                     where (u.RoleId == INSTRUCTOR ||
                                            u.RoleId == ADMINISTRATOR)
                                    select  n).ToList();
            return markers;
        }
    }
}