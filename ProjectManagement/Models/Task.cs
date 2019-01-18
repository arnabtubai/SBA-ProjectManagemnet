namespace ProjectManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            Tasks1 = new HashSet<Task>();
        }

        [Key]
        public int Task_ID { get; set; }

        public int Parent_ID { get; set; }

        public int Project_ID { get; set; }

        [Column("Task")]
        [Required]
        [StringLength(150)]
        public string Task1 { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Priority { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        public virtual Project Project { get; set; }

        public virtual  List<Task> TaskList { get; set; }

        public virtual Tasks Task2 { get; set; }
    }
}
