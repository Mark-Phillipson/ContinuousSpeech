namespace SpeechContinuousRecognition.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LauncherMultipleLauncherBridge")]
    public partial class LauncherMultipleLauncherBridge
    {
        public int ID { get; set; }

        public int LauncherID { get; set; }

        public int MultipleLauncherID { get; set; }

        public virtual Launcher Launcher { get; set; }

        public virtual MultipleLauncher MultipleLauncher { get; set; }
    }
}
