namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ApplicationsToKill")]
    public partial class ApplicationsToKill
    {
        public int ID { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Required]
        [StringLength(50)]
        public string ProcessName { get; set; }

        [Required]
        [StringLength(255)]
        public string CommandName { get; set; }

        public bool Display { get; set; }
    }
}
