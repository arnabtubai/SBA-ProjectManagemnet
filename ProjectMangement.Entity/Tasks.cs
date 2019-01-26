using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManagement.Entity
{
    [Table("Tasks")]
    public class Tasks
    {
        [Key]
        public int Task_ID { get; set; }

        public int Parent_ID { get; set; }

        [Required]
        public int Project_ID { get; set; }

      
        public int User_ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Task { get; set; }

        public DateTime? StartDate { get; set; }
        [GreaterThan("StartDate", ErrorMessage = "Start Date cannot be greater than End date.")]
        public DateTime? EndDate { get; set; }

        public int Priority { get; set; }

        [Required]
        [StringLength(10)]
        public string  Status { get; set; }

        public virtual Projects Projects { get; set; }


      
    }
}