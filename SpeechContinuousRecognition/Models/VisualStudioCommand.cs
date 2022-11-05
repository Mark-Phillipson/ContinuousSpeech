namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class VisualStudioCommand
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Caption { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Command { get; set; } = null!;
    }
}
