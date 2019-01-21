using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class TaskViewModel
    {
        [Key]
        public int Task_ID { get; set; }

        public int Parent_ID { get; set; }

        [Required]
        public int Project_ID { get; set; }

        [Required]
        public int User_ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Task { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int Priority { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }
        public string ParentTask { get; set; }
    }
}