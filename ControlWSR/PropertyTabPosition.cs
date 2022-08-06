namespace ControlWSR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PropertyTabPosition
    {
        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        public string ObjectName { get; set; }

        [Required]
        [StringLength(60)]
        public string PropertyName { get; set; }

        public int NumberOfTabs { get; set; }
    }
}
