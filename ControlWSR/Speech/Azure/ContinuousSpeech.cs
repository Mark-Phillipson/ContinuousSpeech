
using Microsoft.CognitiveServices.Speech;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;

using WindowsInput;
using WindowsInput.Native;

namespace ControlWSR.Speech.Azure
{
    public class ContinuousSpeech
    {
        SpeechCommandsHelper speechCommandsHelper = new SpeechCommandsHelper();
        public ContinuousSpeech()
        {
        }
        public async Task<string> SpeechContinuousRecognitionAsync(AvailableCommandsForm form, InputSimulator inputSimulator, System.Speech.Recognition.SpeechRecognizer speechRecognizer)
        {
            // Creates an instance of a speech config with specified subscription key and region.
            // Replace with your own subscription key and service region (e.g., "westus").
            string SPEECH__SERVICE__KEY;
            string SPEECH__SERVICE__REGION;
            SPEECH__SERVICE__KEY = ConfigurationManager.AppSettings.Get("SpeechAzureKey");
            SPEECH__SERVICE__REGION = ConfigurationManager.AppSettings.Get("SpeechAzureRegion");
            var config = SpeechConfig.FromSubscription(SPEECH__SERVICE__KEY, SPEECH__SERVICE__REGION);
            string resultMain = null;

            // Creates a speech recognizer from microphone.
            using (var recognizer = new Microsoft.CognitiveServices.Speech.SpeechRecognizer(config))
            {
                // Subscribes to events.
                recognizer.Recognizing += (s, e) =>
                {
                    //Console.WriteLine($"RECOGNIZING: Text={e.Result.Text}");
                    // form.TextBoxResults = $" RECOGNISING: Text= {e.Result.Text}";
                };

                recognizer.Recognized += (s, e) =>
               {
                   SpeechRecognitionResult result = e.Result;
                   //form.LabelStatus = ($"Reason: {result.Reason.ToString()}");
                   if (result.Reason == ResultReason.RecognizedSpeech)
                   {
                       Console.WriteLine($"Final result: Text: {result.Text}.");
                       string resultRaw = result.Text.Replace("Stop continuous.", "").Trim();
                       if (form.OutputUppercase)
                       {
                           resultRaw = resultRaw.ToUpper();
                       }
                       if (form.OutputLowercase)
                       {
                           resultRaw = resultRaw.ToLower();
                       }
                       if (form.RemovePunctuation)
                       {
                           resultRaw = SpeechCommandsHelper.RemovePunctuation(resultRaw);
                       }
                       resultRaw = SpeechCommandsHelper.PerformCodeFunctions(resultRaw);

                       resultMain = resultRaw;
                       //form.TextBoxResults = resultMain;
                       if (resultMain.Trim().ToLower().StartsWith("command"))
                       {
                           resultMain = SpeechCommandsHelper.RemovePunctuation(resultMain);
                           resultMain = resultMain.Replace("Command", "").Trim();
                           inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                           inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LWIN);
                           inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                           inputSimulator.Keyboard.Sleep(200);
                           inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);

                           var resultEmulated = speechRecognizer.EmulateRecognize("Brackets In" );
                           inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                           inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LWIN);
                           inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                           inputSimulator.Keyboard.Sleep(200);
                           inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);

                       }
                       else
                       {
                           inputSimulator.Keyboard.TextEntry($"{resultMain}");
                       }
                   }
               };

                recognizer.Canceled += (s, e) =>
                {
                    //form.LabelStatus = $"\n    Canceled. Reason: {e.Reason.ToString()}, CanceledReason: {e.Reason}";
                };

                recognizer.SessionStarted += (s, e) =>
                {
                    //form.LabelStatus = "Continuous Dictation STARTED";
                };

                recognizer.SessionStopped += (s, e) =>
                {
                    //form.LabelStatus = "STOPPED";
                };

                // Starts continuous recognition. 
                // Uses StopContinuousRecognitionAsync() to stop recognition.
                await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                do
                {
                    //Console.WriteLine("Press Enter to stop");

                } while (resultMain == null || !resultMain.ToLower().Contains("stop continuous"));

                // Stops recognition.
                await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                return resultMain;
            }
        }
    }
}
