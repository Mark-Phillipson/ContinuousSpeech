namespace ControlWSR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Launcher")]
    public partial class Launcher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Launcher()
        {
            LauncherMultipleLauncherBridges = new HashSet<LauncherMultipleLauncherBridge>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string CommandLine { get; set; }

        public int CategoryID { get; set; }

        public int? ComputerID { get; set; }

        public virtual Category Category { get; set; }

        public virtual Computer Computer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LauncherMultipleLauncherBridge> LauncherMultipleLauncherBridges { get; set; }
    }
}
