namespace ControlWSR
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using WindowsInput.Native;

    public partial class CustomWindowsSpeechCommand
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string TextToEnter { get; set; }

        public VirtualKeyCode KeyDownValue { get; set; }

        public VirtualKeyCode ModifierKey { get; set; }

        public VirtualKeyCode KeyPressValue { get; set; }

        [StringLength(100)]
        public string MouseCommand { get; set; }

        [StringLength(100)]
        public string ProcessStart { get; set; }

        [StringLength(100)]
        public string CommandLineArguments { get; set; }

        public int WindowsSpeechVoiceCommandId { get; set; }

        public virtual WindowsSpeechVoiceCommand WindowsSpeechVoiceCommand { get; set; }
    }
}
