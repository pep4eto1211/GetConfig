//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GetConfig.Db.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProjectsUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public int AccessFlag { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}