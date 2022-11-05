namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CustomIntelliSense")]
    public partial class CustomIntelliSense
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomIntelliSense()
        {
            AdditionalCommands = new HashSet<AdditionalCommand>();
        }

        public int ID { get; set; }

        public int LanguageID { get; set; }

        [Required]
        [StringLength(255)]
        public string Display_Value { get; set; }

        public string SendKeys_Value { get; set; }

        [StringLength(255)]
        public string? Command_Type { get; set; }

        public int CategoryID { get; set; }

        [StringLength(255)]
        public string? Remarks { get; set; }

        public string? Search { get; set; }

        public int? ComputerID { get; set; }

        [Required]
        [StringLength(30)]
        public string DeliveryType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdditionalCommand>? AdditionalCommands { get; set; }

        public virtual Category Category { get; set; }

        public virtual Computer Computer { get; set; }

        public virtual Language Language { get; set; }
    }
}
