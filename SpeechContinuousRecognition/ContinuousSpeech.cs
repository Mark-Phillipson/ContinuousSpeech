using System.Configuration;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
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
        private readonly IEnumerable<VirtualKeyCode> all3Modifiers = new List<VirtualKeyCode>()
        { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT, VirtualKeyCode.MENU };
        private readonly IEnumerable<VirtualKeyCode> controlAndShift = new List<VirtualKeyCode>()
        { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT };
        private readonly IEnumerable<VirtualKeyCode> controlAndAlt = new List<VirtualKeyCode>()
        { VirtualKeyCode.CONTROL, VirtualKeyCode.MENU };

        private readonly IEnumerable<VirtualKeyCode> windowAndShift = new List<VirtualKeyCode>()
        { VirtualKeyCode.LWIN, VirtualKeyCode.SHIFT };
        private readonly IEnumerable<VirtualKeyCode> altAndShift = new List<VirtualKeyCode>()
        { VirtualKeyCode.MENU, VirtualKeyCode.SHIFT };
        
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        const string VOICE_LAUNCHER = @"C:\Users\MPhil\source\repos\SpeechRecognitionHelpers\VoiceLauncher\bin\Release\VoiceLauncher.exe";

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
    string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public Process currentProcess { get; set; }

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
            Screen[] screens = Screen.AllScreens;
            if (Screen.AllScreens.Count() > 1)
            {
                this.SetBounds(1680, 100, this.Width, this.Height);
            }
            UpdateTheCurrentProcess();

            Text = "Continuous Speech";
            await SpeechSetupAsync();
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
        }

        private void UpdateTheCurrentProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            currentProcess = Process.GetProcessById((int)pid);
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

            // Creates a speech recognizer from microphone.
            recognizer = new Microsoft.CognitiveServices.Speech.SpeechRecognizer(config);
            {
                // Subscribes to events.
                recognizer.Recognizing +=

                (s, e) =>
                {
                    try
                    {
                        TextBoxResults = $"RECOGNISING: Text= {e.Result.Text}{Environment.NewLine}{TextBoxResults}";
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
                        buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = false; }));
                        buttonStart.Invoke(new MethodInvoker(delegate { buttonStart.Enabled = true; }));
                    }
                    catch (Exception exception) { global::System.Console.WriteLine(exception.Message); }
                };

                // Before starting recognition, add a phrase list to help recognition.
                //Note does not work for single words has to be a phrase, Maybe put these in a database table later
                //Should be no more than 500 to be practical
                PhraseListGrammar phraseListGrammar = PhraseListGrammar.FromRecognizer(recognizer);
                phraseListGrammar.AddPhrase("Blazor Server");
                phraseListGrammar.AddPhrase("Blazor Client");
                phraseListGrammar.AddPhrase("Blazor WASM");
                phraseListGrammar.AddPhrase("24 Courtenay Road");
                phraseListGrammar.AddPhrase("Microsoft Azure");
                phraseListGrammar.AddPhrase("Double Home");
                phraseListGrammar.AddPhrase("Words Semi Colon");
                phraseListGrammar.AddPhrase("Save All");
                phraseListGrammar.AddPhrase("To Lower");
                phraseListGrammar.AddPhrase("Starts With");



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
            await StopContinuous().ConfigureAwait(false);
        }

        private async Task StopContinuous()
        {
            try
            {
                await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                Icon? icon = Icon.ExtractAssociatedIcon($"{Application.StartupPath}Mic-04.ico");
                if (icon!= null )
                {
                    this.Invoke(new MethodInvoker(delegate { this.Icon = icon; }));
                }
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
                Icon? icon = Icon.ExtractAssociatedIcon($"{Application.StartupPath}Mic-03.ico");
                if (icon != null)
                {
                    this.Invoke(new MethodInvoker(delegate { this.Icon = icon;  }));
                }

            }
            catch (Exception exception)
            {
                await global::System.Console.Out.WriteLineAsync(exception.Message);
            }
            buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = true; }));
        }
        private async void SpeechRecognizer_SpeechRecognised(object? sender, SpeechRecognitionEventArgs e)
        {
            {
                SpeechRecognitionResult result = e.Result;
                //form.LabelStatus = ($"Reason: {result.Reason.ToString()}");
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    //textBoxResultsLocal.Text=($"Final result: Text: {result.Text}.");
                    TextBoxResults = $"Final Recognised result: {result.Text}{Environment.NewLine}{TextBoxResults}";
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
                    //resultRaw = SpeechCommandsHelper.ConvertNatoPhoneticAlphabetToLetter(resultRaw);
                    if (this.ConvertWordsToSymbols)
                    {
                        UpdateTheCurrentProcess();
                        resultRaw = _speechCommandsHelper.ConvertWordsToSymbols(resultRaw, this, result,currentProcess);
                    }

                    resultRaw = SpeechCommandsHelper.PerformCodeFunctions(resultRaw);

                    resultMain = resultRaw;
                    if (resultRaw.Trim().ToLower() == "use dragon")
                    {
                        await StopContinuous().ConfigureAwait(false);
                        buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = false; }));
                        buttonStart.Invoke(new MethodInvoker(delegate { buttonStart.Enabled = true; }));
                        _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.ADD);
                        resultMain = "{Use Dragon}";
                    }


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
                        TextBoxResults = $"Text Entry Value: {resultMain}{Environment.NewLine}{TextBoxResults}";
                        if (resultMain.StartsWith("{") && resultMain.EndsWith("}"))
                        {
                            return;
                        }
                        if (!resultMain.ToLower().Contains("stop continuous"))
                        {
                            _inputSimulator.Keyboard.TextEntry($"{resultMain}".Trim());
                        }
                        else
                        {
                            await StopContinuous().ConfigureAwait(false);
                            buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = false;  }));
                            buttonStart.Invoke(new MethodInvoker(delegate { buttonStart.Enabled = true;  }));
                        }
                    }
                }
            }
        }
        private void UpdateCurrentProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            currentProcess = Process.GetProcessById((int)pid);
        }

        private void buttonCloseApplication_Click(object sender, EventArgs e)
        {
            Text = " Disposing of recogniser please wait! ";
            recognizer.Dispose();
            Application.Exit();
        }

    
}
}