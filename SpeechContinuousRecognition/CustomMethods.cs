using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WindowsInput;

namespace SpeechContinuousRecognition
{
     public  class CustomMethods
    {
        IInputSimulator _inputSimulator= new  InputSimulator();
        public CustomMethods()
        {

        }
        public  string  EnterTimestamp( string? dictation= null )
        {
            var value = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            _inputSimulator.Keyboard.TextEntry(value);
            return $"{{Entered timestamp}}";
        }
    }
}
