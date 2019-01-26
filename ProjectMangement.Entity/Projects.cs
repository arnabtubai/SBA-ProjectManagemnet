using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Entity
{
    public class Projects
    {
        [Key]
        public int Project_ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Project { get; set; }

        public DateTime? StartDate { get; set; }
        [GreaterThan("StartDate",ErrorMessage ="Start Date cannot be greater than End date.")]
        public DateTime? EndDate { get; set; }

        public int Priority { get; set; }
        
        public int User_ID { get; set; }
        [Required]
        public bool? Completed { get; set; }
        public virtual Users Users { get; set; }
    }
}