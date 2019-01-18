using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Projects
    {
        [Key]
        public int Project_ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Project { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        [Required]
        public int Priority { get; set; }
        
        public int User_ID { get; set; }

        public virtual Users Users { get; set; }
    }
}