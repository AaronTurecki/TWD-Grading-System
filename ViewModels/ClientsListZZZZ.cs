using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.ViewModels {
    public class ClientsListZZZZ {

        public ClientsListZZZZ(string firstname, string lastname, string dob, string email) {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Dob = dob;
            this.Email = email;
        }

        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Dob { set; get; }
    }
}