namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CurrentWindow")]
    public partial class CurrentWindow
    {
        public int ID { get; set; }

        public int Handle { get; set; }
    }
}
