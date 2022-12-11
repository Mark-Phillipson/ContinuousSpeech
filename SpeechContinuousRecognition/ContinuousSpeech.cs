using System.Configuration;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;

using DataAccessLibrary.Models;

using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Intent;

using SpeechContinuousRecognition.Repositories;

using WindowsInput;
using WindowsInput.Native;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SpeechContinuousRecognition
{
    public partial class ContinuousSpeech : Form
    {


        StatusDisplayForm statusDisplay = new StatusDisplayForm();
        private int _counter = 0;
        private string? lastCommand = null;
        WindowsVoiceCommand _windowsVoiceCommand = new WindowsVoiceCommand();
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

        static uint MOUSEEVENTF_WHEEL = 0x800;


        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);
        static void PerformMouseWheelDirection(string direction, int increment = 1)
        {
            unchecked
            {
                if (direction == "Up")
                {
                    var incrementUInt = (uint)(120 * increment);
                    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, incrementUInt, 0);
                }
                else if (direction == "Down")
                {
                    var incrementUInt = (uint)-(120 * increment);
                    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, incrementUInt, 0);
                }
            }
        }

        public Process? currentProcess { get; set; }

        private IInputSimulator _inputSimulator = new InputSimulator();
        SpeechRecognizer? recognizer = null;
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
                    OutputLowercase = false;
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
                    OutputUppercase = false;
                }
            }
        }
        public bool TreatAsCommand
        {
            get => checkBoxTreatAsCommand.Checked;
            set => checkBoxTreatAsCommand.Checked = value;
        }
        public bool TreatAsCommandFirst
        {
            get => checkBoxTreatAsCommandFirst.Checked;
            set
            {
                checkBoxTreatAsCommandFirst.Invoke(new MethodInvoker(delegate { checkBoxTreatAsCommandFirst.Checked = value; }));
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

            Text = "Azure Cognitive Services - Continuous Speech Code by Voice in Visual Studio";
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
            labelCurrentProcess.Invoke(new MethodInvoker(delegate { labelCurrentProcess.Text = currentProcess.ProcessName; }));
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
                //recognizer.Recognizing +=

                //(s, e) =>
                //{
                //    try
                //    {
                //        TextBoxResults = $"RECOGNISING: Text= {e.Result.Text}{Environment.NewLine}{TextBoxResults}";
                //    }
                //    catch (Exception exception)
                //    {
                //        global::System.Console.WriteLine(exception.Message);
                //    }
                //};

                recognizer.Recognized += new EventHandler<SpeechRecognitionEventArgs>(SpeechRecognizer_SpeechRecognised);

                recognizer.Canceled += (s, e) =>
                {
                    try
                    {
                        TextBoxResults = $"\n    Canceled. Reason: {e.Reason.ToString()}, CanceledReason: {e.ErrorDetails}";

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
                var results = _windowsVoiceCommand.GetPhraseListGrammars();
                PhraseListGrammar phraseListGrammar = PhraseListGrammar.FromRecognizer(recognizer);
                if (results != null)
                {
                    foreach (PhraseListGrammarStorage item in results)
                    {
                        phraseListGrammar.AddPhrase(item.PhraseListGrammarValue);
                    }
                }

                await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
                //do
                //{
                //    LabelStatus = " Say stop continuous to stop ";

                //    //Console.WriteLine("Press Enter to stop");

                //} while ( !resultMain!.ToLower().Contains("stop continuous"));

                // Stops recognition.
                labelStatus.Invoke(new MethodInvoker(delegate { labelStatus.ForeColor = Color.Green; }));

            }

        }

        private async void buttonStop_Click(object sender, EventArgs e)
        {
            await StopContinuous().ConfigureAwait(false);
            // This should Toggle DRAGON Microphone
            _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ADD);
        }

        private async Task StopContinuous()
        {
            if (recognizer == null) { return; }
            try
            {
                await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                Icon? icon = Icon.ExtractAssociatedIcon($"{Application.StartupPath}Mic-04.ico");
                if (icon != null)
                {
                    this.Invoke(new MethodInvoker(delegate { this.Icon = icon; }));
                    //statusDisplay.LabelStatus = labelStatus.Text;
                    //statusDisplay.Text = this.Text;
                    //statusDisplay.Icon = icon;
                    //statusDisplay.Show(this);
                }
            }
            catch (Exception exception)
            {
                await global::System.Console.Out.WriteLineAsync(exception.Message);
            }
            labelStatus.Invoke(new MethodInvoker(delegate { labelStatus.ForeColor = Color.Red; }));
        }

        private void ContinuousSpeech_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate { this.Text = "Closing please wait.."; }));


            recognizer?.Dispose();
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            // Starts continuous recognition. 
            // Uses StopContinuousRecognitionAsync() to stop recognition.
            if (recognizer == null) { return; }
            
            try
            {
                await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
                // This should Toggle DRAGON Microphone
                _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ADD);
                Icon? icon = Icon.ExtractAssociatedIcon($"{Application.StartupPath}Mic-03.ico");
                if (icon != null)
                {
                    this.Invoke(new MethodInvoker(delegate { this.Icon = icon; }));
                    //statusDisplay.Icon = icon;
                    //statusDisplay.LabelStatus = labelStatus.Text;
                    //statusDisplay.Text = this.Text;
                    //statusDisplay.Show(this);

                }
            }
            catch (Exception exception)
            {
                await global::System.Console.Out.WriteLineAsync(exception.Message);
            }
            buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = true; }));
            labelStatus.Invoke(new MethodInvoker(delegate { labelStatus.ForeColor = Color.LightGreen; }));
        }
        private async void SpeechRecognizer_SpeechRecognised(object? sender, SpeechRecognitionEventArgs e)
        {
            {
                SpeechRecognitionResult result = e.Result;
                if (result.Text == "")
                {
                    _counter++;
                }
                else
                {
                    _counter = 0;
                }
                if (_counter >= 10)
                {
                    await StopContinuous().ConfigureAwait(false);
                    TextBoxResults = $"Stopped after 10 empty results: {result.Text}{Environment.NewLine}{TextBoxResults}";
                    _counter = 0;
                }
                //form.LabelStatus = ($"Reason: {result.Reason.ToString()}");
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    //textBoxResultsLocal.Text=($"Final result: Text: {result.Text}.");
                    TextBoxResults = $"Final Recognised result: {result.Text}{Environment.NewLine}{TextBoxResults}";
                    string resultRaw = result.Text;
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
                    var temporary = resultRaw;
                    if (resultRaw.ToLower() == "voice typing")
                    {
                        await StopContinuous().ConfigureAwait(false);
                        buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = false; }));
                        buttonStart.Invoke(new MethodInvoker(delegate { buttonStart.Enabled = true; }));
                    }
                    if (this.TreatAsCommandFirst)
                    {
                        UpdateTheCurrentProcess();
                        if (resultRaw.ToLower().Contains(" split "))
                        {
                            var commands = resultRaw.Split(" split ", StringSplitOptions.TrimEntries);
                            resultRaw = "";
                            foreach (var item in commands)
                            {
                                resultRaw = $"{resultRaw}{_speechCommandsHelper.IdentifyAndRunCommand(item, this, result, currentProcess)}";
                                resultMain = resultRaw;
                                await PerformTextEntryOrLog().ConfigureAwait(false);
                                resultRaw = "";
                            }
                        }
                        else if (resultRaw.ToLower() == "repeat" && lastCommand != null && lastCommand.ToLower() != "repeat")
                        {
                            resultRaw = _speechCommandsHelper.IdentifyAndRunCommand(lastCommand, this, result, currentProcess);
                        }
                        else if (resultRaw.ToLower().StartsWith("repeat ") && lastCommand != null)
                        {
                            var number = SpeechCommandsHelper.GetNumber(resultRaw.Substring(startIndex: 7));
                            for (int i = 0; i < number - 1; i++)
                            {
                                resultRaw = _speechCommandsHelper.IdentifyAndRunCommand(lastCommand, this, result, currentProcess);
                                await PerformTextEntryOrLog().ConfigureAwait(false);
                            }
                        }
                        else
                        {
                            resultRaw = _speechCommandsHelper.IdentifyAndRunCommand(resultRaw, this, result, currentProcess);
                        }
                    }
                    if (!temporary.ToLower().Contains("repeat"))
                    {
                        lastCommand = temporary;
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
                    string[] words = resultRaw.Split(' ');
                    words = resultRaw.Split(' ');
                    if (resultRaw.ToLower().StartsWith("wheel up"))
                    {
                        var number = SpeechCommandsHelper.GetNumber(words[2] ?? "1");
                        PerformMouseWheelDirection("Up", number);
                        resultMain = "{Wheel Up}";
                    }
                    else if (resultRaw.ToLower().StartsWith("wheel down"))
                    {
                        var number = SpeechCommandsHelper.GetNumber(words[2] ?? "1");
                        PerformMouseWheelDirection("Down", number);
                        resultMain = "{Wheel Down}";
                    }
                    await PerformTextEntryOrLog().ConfigureAwait(false);
                }
            }
        }

        private async Task PerformTextEntryOrLog()
        {
            if (resultMain != null && resultMain.Length > 0)
            {
                if (resultMain.StartsWith("{") && resultMain.EndsWith("}"))
                {
                    TextBoxResults = $"Command run: {resultMain}{Environment.NewLine}{TextBoxResults}";
                }
                else if (!resultMain.ToLower().Contains("stop continuous"))
                {
                    if (resultMain != null && resultMain.Trim().Length > 0)
                    {
                        TextBoxResults = $"Text entry: {resultMain}{Environment.NewLine}{TextBoxResults}";
                        _inputSimulator.Keyboard.TextEntry($"{resultMain}".Trim());
                    }
                }
                else
                {
                    await StopContinuous().ConfigureAwait(false);
                    buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = false; }));
                    buttonStart.Invoke(new MethodInvoker(delegate { buttonStart.Enabled = true; }));
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

        private async void buttonCloseApplication_Click(object sender, EventArgs e)
        {
            Text = " Disposing of recogniser please wait! ";
            await StopContinuous().ConfigureAwait(false);
            try
            {
                recognizer?.Dispose();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            Application.Exit();
        }

        private void checkBoxUppercase_Click(object sender, EventArgs e)
        {
            if (checkBoxUppercase.Checked)
            {
                OutputLowercase = false;
            }
        }

        private void checkBoxLowercase_Click(object sender, EventArgs e)
        {
            if (checkBoxLowercase.Checked)
            {
                OutputUppercase = false;
            }
        }

        private void buttonRestartDragon_Click(object sender, EventArgs e)
        {
            CustomMethods customMethods = new CustomMethods();
            customMethods.RestartDragon();
        }

        private void buttonVoiceAdministration_Click(object sender, EventArgs e)
        {
            var uri = "https://localhost:7264";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            Process.Start(psi);
            Process.Start("C:\\Users\\MPhil\\OneDrive\\Documents\\Voice Launcher Blazor.bat");
        }

        private void buttonDatabaseCommands_Click(object sender, EventArgs e)
        {
            var uri = "https://localhost:7264/windowsspeechvoicecommands";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            Process.Start(psi);
        }
    }
}