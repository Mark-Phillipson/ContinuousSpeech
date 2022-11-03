namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class AdditionalCommand
    {
        public int ID { get; set; }

        public int CustomIntelliSenseID { get; set; }

        public decimal WaitBefore { get; set; }

        [Required]
        public string SendKeys_Value { get; set; } = null!;

        [StringLength(255)]
        public string? Remarks { get; set; }

        [Required]
        [StringLength(30)]
        public string DeliveryType { get; set; }= null !;

        public virtual CustomIntelliSense CustomIntelliSense { get; set; }
    }
}
