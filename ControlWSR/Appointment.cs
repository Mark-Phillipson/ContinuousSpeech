namespace ControlWSR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Appointment
    {
        public int ID { get; set; }

        public int AppointmentType { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        [StringLength(255)]
        public string Caption { get; set; }

        public bool AllDay { get; set; }

        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int Label { get; set; }

        public int Status { get; set; }

        [StringLength(255)]
        public string Recurrence { get; set; }
    }
}
