namespace ControlWSR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdditionalCommand
    {
        public int ID { get; set; }

        public int CustomIntelliSenseID { get; set; }

        public decimal WaitBefore { get; set; }

        [Required]
        public string SendKeys_Value { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        [Required]
        [StringLength(30)]
        public string DeliveryType { get; set; }

        public virtual CustomIntelliSense CustomIntelliSense { get; set; }
    }
}
