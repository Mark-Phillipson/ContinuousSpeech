namespace ControlWSR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GeneralLookup
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Item_Value { get; set; }

        [Required]
        [StringLength(255)]
        public string Category { get; set; }

        public int? SortOrder { get; set; }

        [StringLength(255)]
        public string DisplayValue { get; set; }
    }
}
