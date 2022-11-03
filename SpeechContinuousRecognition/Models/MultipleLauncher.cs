namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("MultipleLauncher")]
    public partial class MultipleLauncher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MultipleLauncher()
        {
            LauncherMultipleLauncherBridges = new HashSet<LauncherMultipleLauncherBridge>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(70)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LauncherMultipleLauncherBridge> LauncherMultipleLauncherBridges { get; set; }
    }
}
