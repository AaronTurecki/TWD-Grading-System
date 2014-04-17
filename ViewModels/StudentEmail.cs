using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twd_project.ViewModels {

    public class StudentEmail {

        public int    IntakeId { get; private set; }
        public int    UserId   { get; private set; }
        public string All      { get; private set; }
        public string Subject  { get; private set; }
        public string Body     { get; private set; }
    }
}