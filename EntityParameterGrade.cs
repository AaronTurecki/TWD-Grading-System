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
    
    public partial class EntityParameterGrade
    {
        public int userId { get; set; }
        public short entityParameterId { get; set; }
        public Nullable<int> grade { get; set; }
        public Nullable<int> latePenalty { get; set; }
        public string comment { get; set; }
    
        public virtual EntityParameter EntityParameter { get; set; }
        public virtual TWD_User TWD_User { get; set; }
    }
}
