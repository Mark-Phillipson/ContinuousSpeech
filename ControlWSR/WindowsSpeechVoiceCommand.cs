namespace ControlWSR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WindowsSpeechVoiceCommand")]
    public partial class WindowsSpeechVoiceCommand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WindowsSpeechVoiceCommand()
        {
            CustomWindowsSpeechCommands = new HashSet<CustomWindowsSpeechCommand>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string SpokenCommand { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomWindowsSpeechCommand> CustomWindowsSpeechCommands { get; set; }
    }
}
