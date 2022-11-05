namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ValuesToInsert")]
    public partial class ValuesToInsert
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string ValueToInsert { get; set; } = null!;
        [Required]
        [StringLength(255)]
        public string Lookup { get; set; } = null!;

        [StringLength(255)]
        public string? Description { get; set; }
    }
}
