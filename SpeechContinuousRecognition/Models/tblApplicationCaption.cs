namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class tblApplicationCaption
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string Caption_Contains { get; set; }

        [Required]
        [StringLength(255)]
        public string Application_Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description_Filter { get; set; }

        public bool? Show_Commands { get; set; }
    }
}
