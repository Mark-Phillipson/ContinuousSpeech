namespace ControlWSR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Todos")]
    public partial class Todo
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public bool Completed { get; set; }

        [StringLength(255)]
        public string Project { get; set; }

        public bool Archived { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        public int SortPriority { get; set; }
    }
}
