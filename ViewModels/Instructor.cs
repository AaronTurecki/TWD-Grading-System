using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.ViewModels {
    public class Instructor {
        public string Name   { get; set; }
        public int    UserId { get; set; }

        public Instructor(string name, int userId){
            Name   = name;
            UserId = userId;
        }
    }
}