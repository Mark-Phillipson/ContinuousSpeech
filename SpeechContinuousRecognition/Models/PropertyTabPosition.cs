namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class PropertyTabPosition
    {
        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        public string ObjectName { get; set; } = null!;

        [Required]
        [StringLength(60)]
        public string? PropertyName { get; set; }

        public int NumberOfTabs { get; set; }
    }
}
