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
    
    public partial class EntityParameter
    {
        public EntityParameter()
        {
            this.EntityParameterGrades = new HashSet<EntityParameterGrade>();
        }
    
        public short entityParameterId { get; set; }
        public string entityParameter1 { get; set; }
        public Nullable<short> entityParameterWeight { get; set; }
        public Nullable<short> courseEntityId { get; set; }
        public Nullable<bool> showGrades { get; set; }
    
        public virtual CourseEntity CourseEntity { get; set; }
        public virtual ICollection<EntityParameterGrade> EntityParameterGrades { get; set; }
    }
}
