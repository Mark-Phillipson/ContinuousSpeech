namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class Login
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string? Name { get; set; }

        [Required]
        [StringLength(255)]
        public string? Username { get; set; }

        [StringLength(255)]
        public string? Password { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public DateTimeOffset? Created { get; set; }
    }
}
