namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class Example
    {
        public int ID { get; set; }

        public int NumberValue { get; set; }

        [Required][StringLength(255)] public string Text { get; set; } = null!;

        [Required]
        public string LargeText { get; set; }=null!;

        public bool Boolean { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateValue { get; set; }
    }
}
