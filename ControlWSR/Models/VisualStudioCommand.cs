namespace ControlWSR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VisualStudioCommand
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Caption { get; set; }

        [Required]
        [StringLength(255)]
        public string Command { get; set; }
    }
}
