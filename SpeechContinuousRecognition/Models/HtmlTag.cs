namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class HtmlTag
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string? Tag { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [StringLength(255)]
        public string? ListValue { get; set; }

        public bool Include { get; set; }

        [StringLength(255)]
        public string? SpokenForm { get; set; }
    }
}
