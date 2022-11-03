using System.Configuration;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;
using System.Xml.Linq;

using Microsoft.CognitiveServices.Speech;

using WindowsInput;
using WindowsInput.Native;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SpeechContinuousRecognition
{
    public partial class ContinuousSpeech : Form
    {
        private IInputSimulator _inputSimulator = new InputSimulator();
        SpeechRecognizer recognizer;
        private string resultMain = "";
        public ContinuousSpeech()
        {
            InitializeComponent();
        }
        private SpeechCommandsHelper _speechCommandsHelper = new SpeechCommandsHelper();
        public string TextBoxResults
        {
            get => textBoxResultsLocal.Text; set
            {
                textBoxResultsLocal.Invoke(new MethodInvoker(delegate { textBoxResultsLocal.Text = value; }));
            }
        }
        public string LabelStatus
        {
            get => labelStatus.Text;
            set
            {
                try
                {

                    labelStatus.Invoke(new MethodInvoker(delegate { labelStatus.Text = value; }));
                }
                catch (Exception exception)
                {
                    global::System.Console.WriteLine(exception.Message);
                }
            }
        }
        public bool OutputUppercase
        {
            get => checkBoxUppercase.Checked;
            set
            {
                try
                {
                    checkBoxUppercase.Invoke(new MethodInvoker(delegate { checkBoxUppercase.Checked = value; }));

                }
                catch (Exception exception)
                {
                    global::System.Console.WriteLine(exception.Message);
                }
                if (value)
                {
                    checkBoxLowercase.Checked = false;
                }
            }
        }
        public bool OutputLowercase
        {
            get => checkBoxLowercase.Checked;
            set
            {
                try
                {

                    checkBoxLowercase.Invoke(new MethodInvoker(delegate { checkBoxLowercase.Checked = value; }));
                }
                catch (Exception exception) { global::System.Console.WriteLine(exception.Message); }
                if (value)
                {
                    checkBoxUppercase.Invoke(new MethodInvoker(delegate { checkBoxUppercase.Checked = value; }));
                }
            }
        }
        public bool TreatAsCommand
        {
            get => checkBoxTreatAsCommand.Checked;
            set => checkBoxTreatAsCommand.Checked = value;
        }
        public bool ConvertWordsToSymbols
        {
            get => checkBoxConvertWordsToSymbols.Checked;
            set
            {
                checkBoxConvertWordsToSymbols.Invoke(new MethodInvoker(delegate { checkBoxConvertWordsToSymbols.Checked = value; }));
            }
        }
        public bool RemovePunctuation
        {
            get => checkBoxRemovePunctuation.Checked;
            set
            {
                checkBoxRemovePunctuation.Invoke(new MethodInvoker(delegate { checkBoxRemovePunctuation.Checked = value; }));
            }
        }
        private async void ContinuousSpeech_Load(object sender, EventArgs e)
        {
            buttonStop.Enabled = false;
            Text = "Continuous Speech ";
            await SpeechSetupAsync();

        }

        private async Task SpeechSetupAsync()
        {
            // Creates an instance of a speech config with specified subscription key and region.
            // Replace with your own subscription key and service region (e.g., "westus").
            string? SPEECH__SERVICE__KEY;
            string? SPEECH__SERVICE__REGION;
            SPEECH__SERVICE__KEY = ConfigurationManager.AppSettings.Get("SpeechAzureKey");
            SPEECH__SERVICE__REGION = ConfigurationManager.AppSettings.Get("SpeechAzureRegion");
            var config = SpeechConfig.FromSubscription(SPEECH__SERVICE__KEY, SPEECH__SERVICE__REGION);
            string resultMain = "";

            // Creates a speech recognizer from microphone.
            recognizer = new Microsoft.CognitiveServices.Speech.SpeechRecognizer(config);
            {
                // Subscribes to events.
                recognizer.Recognizing +=

                (s, e) =>
                {
                    try
                    {
                        Console.WriteLine($"RECOGNIZING: Text={e.Result.Text}");
                        TextBoxResults = $" RECOGNISING: Text= {e.Result.Text}";
                    }
                    catch (Exception exception)
                    {
                        global::System.Console.WriteLine(exception.Message);
                    }
                };

                recognizer.Recognized += new EventHandler<SpeechRecognitionEventArgs>(SpeechRecognizer_SpeechRecognised);

                recognizer.Canceled += (s, e) =>
                {
                    try
                    {
                        TextBoxResults= $"\n    Canceled. Reason: {e.Reason.ToString()}, CanceledReason: {e.ErrorDetails}";
                        
                    }
                    catch (Exception exception)
                    {
                        global::System.Console.WriteLine(exception.Message);
                    }
                };

                recognizer.SessionStarted += (s, e) =>
                {
                    try
                    {
                        LabelStatus = "Continuous Dictation STARTED";
                    }
                    catch (Exception exception)
                    {
                        global::System.Console.WriteLine(exception.Message);
                    }
                };

                recognizer.SessionStopped += (s, e) =>
                {
                    try
                    {
                        LabelStatus = $"STOPPED {recognizer.Properties.GetProperty("Status")}";
                    }
                    catch (Exception exception) { global::System.Console.WriteLine(exception.Message); }
                };

                await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
                //do
                //{
                //    LabelStatus = " Say stop continuous to stop ";

                //    //Console.WriteLine("Press Enter to stop");

                //} while ( !resultMain!.ToLower().Contains("stop continuous"));

                // Stops recognition.


            }

        }

        private async void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                await global::System.Console.Out.WriteLineAsync(exception.Message);
            }
        }

        private void ContinuousSpeech_FormClosing(object sender, FormClosingEventArgs e)
        {
            Text = " Closing please wait ..";
            recognizer.Dispose();
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            // Starts continuous recognition. 
            // Uses StopContinuousRecognitionAsync() to stop recognition.
            try
            {
                await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                await global::System.Console.Out.WriteLineAsync(exception.Message);
            }
            buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = true; }));
        }
        private void SpeechRecognizer_SpeechRecognised(object sender, SpeechRecognitionEventArgs e)
        {
            {
                SpeechRecognitionResult result = e.Result;
                //form.LabelStatus = ($"Reason: {result.Reason.ToString()}");
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    //textBoxResultsLocal.Text=($"Final result: Text: {result.Text}.");
                    string resultRaw = result.Text;//.Replace("Stop continuous.", "").Trim();
                    if (this.OutputUppercase)
                    {
                        resultRaw = resultRaw.ToUpper();
                    }
                    if (this.OutputLowercase)
                    {
                        resultRaw = resultRaw.ToLower();
                    }
                    if (this.RemovePunctuation)
                    {
                        resultRaw = SpeechCommandsHelper.RemovePunctuation(resultRaw);
                    }
                    if (this.ConvertWordsToSymbols)
                    {
                        resultRaw = _speechCommandsHelper.ConvertWordsToSymbols(resultRaw, this, result);
                    }

                    resultRaw = SpeechCommandsHelper.PerformCodeFunctions(resultRaw);

                    resultMain = resultRaw;
                    //form.TextBoxResults = resultMain;
                    if (resultMain.Trim().ToLower().StartsWith("command"))
                    {
                        //resultMain = resultMain.Replace("Command", "").Trim();
                        //resultMain = SpeechCommandsHelper.ConvertToTitle(resultMain);
                        //_inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                        //_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LWIN);
                        //_inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                        //TextBoxResults = resultMain;
                        //_inputSimulator.Keyboard.Sleep(400);
                        //var resultEmulated = speechRecognizer.EmulateRecognize(resultMain);
                        //_inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                        //_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LWIN);
                        //_inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                    }
                    else if (resultMain != null && resultMain.Length > 0)
                    {
                        TextBoxResults = $"Text Entry Value: {resultMain}";
                        if (resultMain.StartsWith("{") && resultMain.EndsWith("}"))
                        {
                            return;
                        }
                        if (!resultMain.ToLower().Contains("stop continuous"))
                        {
                            _inputSimulator.Keyboard.TextEntry($"{resultMain}");
                        }
                    }
                }
            };

        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            SpeechRecognitionResult speechRecognitionResult=  null ;
            var result = _speechCommandsHelper.PerformDatabaseCommands(speechRecognitionResult, "Testing", _inputSimulator, this);
        }
    }
}