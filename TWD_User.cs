//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace twd_project
{
    using System;
    using System.Collections.Generic;
    
    public partial class TWD_User
    {
        public TWD_User()
        {
            this.CourseEntityGrades = new HashSet<CourseEntityGrade>();
            this.EntityParameterGrades = new HashSet<EntityParameterGrade>();
            this.webpages_Roles = new HashSet<webpages_Roles>();
            this.Courses = new HashSet<Course>();
        }
    
        public int userId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Nullable<short> intakeId { get; set; }
        public string bcitId { get; set; }
    
        public virtual ICollection<CourseEntityGrade> CourseEntityGrades { get; set; }
        public virtual ICollection<EntityParameterGrade> EntityParameterGrades { get; set; }
        public virtual Intake Intake { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}