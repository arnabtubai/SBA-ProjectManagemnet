﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManagement.Entity
{
    public class ProjectViewModel
    {
        [Key]
        public int Project_ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Project { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int Priority { get; set; }

        public int User_ID { get; set; }
        [Required]
        public bool? Completed { get; set; }

        public int NumberOfTasks { get; set; }

    }
}