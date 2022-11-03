namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("SavedMousePosition")]
    public partial class SavedMousePosition
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string NamedLocation { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public DateTime? Created { get; set; }
    }
}
