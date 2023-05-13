using DataAccessLibrary.Models;
using NAudio.CoreAudioApi;
using System.Linq;

using Microsoft.CognitiveServices.Speech;

using SpeechContinuousRecognition.Repositories;

using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

using WindowsInput;
using WindowsInput.Native;
using Microsoft.CognitiveServices.Speech.Audio;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SpeechContinuousRecognition
{
    public partial class ContinuousSpeech : Form
    {
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
        private System.Windows.Forms.NotifyIcon? notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip? contextMenu1;
        private System.Windows.Forms.ToolStripMenuItem? menuItem1;
        private System.ComponentModel.IContainer? components2;

        public ContinuousSpeech()
        {
            InitializeComponent();

            this.components2 = new System.ComponentModel.Container();
            this.contextMenu1 = new System.Windows.Forms.ContextMenuStrip();
            this.menuItem1 = new System.Windows.Forms.ToolStripMenuItem();

            // Initialize contextMenu1
            this.contextMenu1.Items.AddRange(
                  new System.Windows.Forms.ToolStripItem[] { this.menuItem1 });
            // Initialize menuItem1
            //this.menuItem1.Index = 0;
            this.menuItem1.Text = "E&xit";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);

            // Set up how the form should be displayed.
            //this.ClientSize = new System.Drawing.Size(292, 266);
            //this.Text = "Notify Icon Example";

            // Create the NotifyIcon.
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components2);

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            notifyIcon1.Icon = new Icon($"{Application.StartupPath}Mic-03.ico");

            // The ContextMenu property sets the menu that will
            // appear when the systray icon is right clicked.
            notifyIcon1.ContextMenuStrip = this.contextMenu1;


            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyIcon1.Text = "Continuous Speech";
            notifyIcon1.Visible = true;


            notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
        }
        private void notifyIcon1_DoubleClick(object? sender, System.EventArgs? e)
        {
            if (sender != null && e != null)
            {
                notifyIcon1_Click(sender, e);
            }
        }
        private void notifyIcon1_Click(object? sender, System.EventArgs? e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            // Activate the form.
            //this.Activate();
            if (LabelStatus.ToLower().Contains("stop") && sender != null && e != null)
            {
                buttonStart_Click(sender, e);
            }

            try
            {
                SendKeys.SendWait("%{Tab}");
            }
            catch (Exception exception)
            {
                AutoClosingMessageBox.Show(exception.Message, "Error when sending keys", 30);
                throw;
            }
            //IntPtr applicationHandle = IntPtr.Zero;
            //string processName = "devenv";
            //Process process = Process.GetProcessesByName(processName)[0];
            //if (process != null)
            //{
            //    applicationHandle = process.Handle;
            //    SetForegroundWindow(applicationHandle);
            //}
        }
        private void menuItem1_Click(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            this.Close();
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
        public bool IncludeSpace
        {
            get => checkBoxIncludeSpace.Checked;
            set
            {
                try
                {
                    checkBoxIncludeSpace.Invoke(new MethodInvoker(delegate { checkBoxIncludeSpace.Checked = value; }));
                }
                catch (Exception exception)
                {
                    global::System.Console.WriteLine(exception.Message);
                }
                if (value)
                {
                    IncludeSpace = false;
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

        public int LastRunCommandId { get; set; } = 0;
        public int EmptyResultsToStopOn { get; } = 7;
        private void ContinuousSpeech_Load(object sender, EventArgs e)
        {
            Screen[] screens = Screen.AllScreens;
            if (Screen.AllScreens.Count() > 1)
            {
                this.SetBounds(1680, 100, this.Width, this.Height);
            }
            UpdateTheCurrentProcess();



            SpeechSetupAsync();
            buttonStart.Enabled = true;
            buttonStartWithoutToggle.Enabled = true;
            buttonStop.Enabled = false;
            buttonStopNoToggle.Enabled = false;
            labelStatus.Invoke(new MethodInvoker(delegate { labelStatus.ForeColor = Color.Red; }));
            this.ShowInTaskbar = true;
            // DisplayRandomCommand();
        }

        private void DisplayRandomCommand()
        {
            var result = _windowsVoiceCommand.GetRandomCommand();
            LastRunCommandId = result?.Id ?? 0;
            if (result != null)
            {
                var actions = _windowsVoiceCommand.GetChildActions(result.Id);
                var action = actions?.FirstOrDefault();
                string? sendKeys = null;
                if (action != null && action.SendKeysValue != null)
                {
                    sendKeys = $"Keys: {action.SendKeysValue}";
                }
                string? spokenFormText = "";
                if (result.SpokenForms != null && result.SpokenForms.Count > 0)
                {
                    var spokenForm = result.SpokenForms.FirstOrDefault();
                    if (spokenForm != null)
                    {
                        spokenFormText = spokenForm.SpokenFormText;
                    }
                }
                buttonRandomCommand.Text = $"Random Command: '{spokenFormText}' Application: {result.ApplicationName} {sendKeys}";
            }
        }

        private void UpdateTheCurrentProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            currentProcess = Process.GetProcessById((int)pid);
            labelCurrentProcess.Invoke(new MethodInvoker(delegate { labelCurrentProcess.Text = currentProcess.ProcessName; }));
        }

        private void SpeechSetupAsync()
        {
            // Creates an instance of a speech config with specified subscription key and region.
            // Replace with your own subscription key and service region (e.g., "westus").
            string? SPEECH__SERVICE__KEY;
            string? SPEECH__SERVICE__REGION;
            if (Environment.MachineName == "J40L4V3" || Environment.MachineName == "SURFACEPRO")
            {
                SPEECH__SERVICE__KEY = ConfigurationManager.AppSettings.Get("SpeechAzureKey");
                SPEECH__SERVICE__REGION = ConfigurationManager.AppSettings.Get("SpeechAzureRegion");
            }
            else
            {
                SPEECH__SERVICE__KEY = textBoxKey.Text;
                SPEECH__SERVICE__REGION = textBoxRegion.Text;

            }
            var config = SpeechConfig.FromSubscription(SPEECH__SERVICE__KEY, SPEECH__SERVICE__REGION);
            // Custom Model
            //config.EndpointId = "861801a0-8d0a-4455-915c-43bebddcaa8a";
            config.EnableAudioLogging();



            // Find all audio input devices
            var deviceEnumerator = new MMDeviceEnumerator();
            var deviceList = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            // Try to get a default microphone from the Table
            var defaultMicrophone = _windowsVoiceCommand.GetMicrophoneFromTable();

            // Select the microphone device you want to use 

            MMDevice? microphoneDevice = null;
            if (defaultMicrophone != null)
            {
                microphoneDevice = deviceList.FirstOrDefault(c => c.DeviceFriendlyName == defaultMicrophone.MicrophoneName);
            }
            else if (deviceList != null && deviceList.Count > 0)
            {
                microphoneDevice = deviceList.FirstOrDefault();
            }
            if (microphoneDevice == null)
            {
                microphoneDevice = deviceList?.FirstOrDefault();
            }
            string audioDeviceId = string.Empty;
            if (microphoneDevice != null)
            {
                // Get the device ID of the selected microphone
                audioDeviceId = microphoneDevice.ID;

            }
            // Configure the audio input source with the selected device ID
            var audioConfig = AudioConfig.FromMicrophoneInput(audioDeviceId);
            Text = $"Azure Cognitive Services - Continuous Speech - Code by Voice in Visual Studio / VSCode Microphone: {microphoneDevice?.DeviceFriendlyName}";

            // Creates a speech recognizer from microphone.
            try
            {
                recognizer = new Microsoft.CognitiveServices.Speech.SpeechRecognizer(config, audioConfig);
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
                            buttonStop.Invoke(new MethodInvoker(delegate { buttonStopNoToggle.Enabled = false; }));
                            buttonStart.Invoke(new MethodInvoker(delegate { buttonStart.Enabled = true; }));
                            buttonStart.Invoke(new MethodInvoker(delegate { buttonStartWithoutToggle.Enabled = true; }));
                        }
                        catch (Exception exception) { global::System.Console.WriteLine(exception.Message); }
                    };

                    // Before starting recognition, add a phrase list to help recognition.
                    //Note does not work for single words has to be a phrase
                    //Now in a database table see Voice Launcher
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

                    // await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
                    //do
                    //{
                    //    LabelStatus = " Say stop continuous to stop ";

                    //    //Console.WriteLine("Press Enter to stop");

                    //} while ( !resultMain!.ToLower().Contains("stop continuous"));

                    // Stops recognition.
                    labelStatus.Invoke(new MethodInvoker(delegate { labelStatus.ForeColor = Color.Green; }));

                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                textBoxResultsLocal.Text = exception.Message;
                labelStatus.Text = "ERROR";
                labelStatus.ForeColor = Color.Red;
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
            string filename = $"{Application.StartupPath}Mic-04.ico";
            if (recognizer == null) { return; }
            try
            {
                await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                Icon? icon = Icon.ExtractAssociatedIcon(filename);
                if (icon != null && notifyIcon1 != null)
                {
                    this.Invoke(new MethodInvoker(delegate { this.Icon = icon; }));
                    notifyIcon1.Icon = new Icon(filename);
                }
            }
            catch (Exception exception)
            {
                await global::System.Console.Out.WriteLineAsync(exception.Message);
            }
            labelStatus.Invoke(new MethodInvoker(delegate { labelStatus.ForeColor = Color.Red; }));
        }

        private static void TurnOnDragonMicrophone()
        {
            //DgnMicBtn gDgnMic = new DgnMicBtn();
            //gDgnMic.Register(0);
            //((IDgnMicBtn)gDgnMic).MicState = DgnMicStateConstants.dgnmicOn;
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
            string fileName = $"{Application.StartupPath}Mic-03.ico";
            try
            {
                await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
                // This should Toggle DRAGON Microphone
                _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ADD);

                Icon? icon = Icon.ExtractAssociatedIcon(fileName);
                if (icon != null && notifyIcon1 != null)
                {
                    this.Invoke(new MethodInvoker(delegate { this.Icon = icon; }));
                    notifyIcon1.Icon = new Icon(fileName);
                }
            }
            catch (Exception exception)
            {
                await global::System.Console.Out.WriteLineAsync(exception.Message);
            }
            buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = true; }));
            buttonStopNoToggle.Invoke(new MethodInvoker(delegate { buttonStopNoToggle.Enabled = true; }));
            labelStatus.Invoke(new MethodInvoker(delegate { labelStatus.ForeColor = Color.Green; }));
        }

        private static void TurnDragonOff()
        {
            //DNSTools.DgnEngineControl engineControl = new DNSTools.DgnEngineControl();
            //engineControl.Register();
            //engineControl.RecognitionMimic("microphone off", 0);
            ////engineControl.RecognitionMimic("microphone on", 0);
            //engineControl.UnRegister(false);
        }

        private async void SpeechRecognizer_SpeechRecognised(object? sender, SpeechRecognitionEventArgs e)
        {
            try
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
                if (_counter >= EmptyResultsToStopOn && notifyIcon1 != null)
                {
                    await StopContinuous().ConfigureAwait(false);
                    TextBoxResults = $"Stopped after {EmptyResultsToStopOn} empty results: {result.Text}{Environment.NewLine}{TextBoxResults}";
                    _counter = 0;
                    notifyIcon1.Icon = SystemIcons.Information;
                    notifyIcon1.BalloonTipTitle = TextBoxResults;
                    notifyIcon1.BalloonTipText = $"Continuous Speech has stopped {DateTime.Now.ToString("HH:mm:ss")}";
                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon1.Visible = true;
                    // This should Toggle DRAGON Microphone
                    _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ADD);
                    notifyIcon1.ShowBalloonTip(30000);
                }
                //form.LabelStatus = ($"Reason: {result.Reason.ToString()}");
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
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
                    if (this.IncludeSpace)
                    {
                        resultRaw = $" {resultRaw}";
                    }
                    var temporary = resultRaw;
                    if (resultRaw.ToLower() == "voice typing")
                    {
                        await StopContinuous().ConfigureAwait(false);
                        buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = false; }));
                        buttonStop.Invoke(new MethodInvoker(delegate { buttonStopNoToggle.Enabled = false; }));
                        buttonStart.Invoke(new MethodInvoker(delegate { buttonStart.Enabled = true; }));
                        buttonStart.Invoke(new MethodInvoker(delegate { buttonStartWithoutToggle.Enabled = true; }));
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
                    if (this.TreatAsCommandFirst)
                    {
                        string temporaryResult = resultRaw;
                        resultRaw = SpeechCommandsHelper.PerformCodeFunctions(resultRaw);
                        if (temporaryResult.ToLower() == resultRaw.ToLower())
                        {
                            resultRaw = temporaryResult;
                        }
                    }
                    resultMain = resultRaw;
                    if (resultRaw.Trim().ToLower() == "use dragon")
                    {
                        await StopContinuous().ConfigureAwait(false);
                        buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = false; }));
                        buttonStop.Invoke(new MethodInvoker(delegate { buttonStopNoToggle.Enabled = false; }));
                        buttonStart.Invoke(new MethodInvoker(delegate { buttonStart.Enabled = true; }));
                        buttonStart.Invoke(new MethodInvoker(delegate { buttonStartWithoutToggle.Enabled = true; }));
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
                    else if (resultRaw.ToLower().StartsWith("wheel down") && words.Length >= 3)
                    {
                        var number = SpeechCommandsHelper.GetNumber(words[2] ?? "1");
                        PerformMouseWheelDirection("Down", number);
                        resultMain = "{Wheel Down}";
                    }
                    await PerformTextEntryOrLog().ConfigureAwait(false);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Exception has occurred:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
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
                        _inputSimulator.Keyboard.TextEntry($"{resultMain}".TrimEnd());
                    }
                }
                else
                {
                    await StopContinuous().ConfigureAwait(false);
                    buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = false; }));
                    buttonStop.Invoke(new MethodInvoker(delegate { buttonStopNoToggle.Enabled = false; }));
                    buttonStart.Invoke(new MethodInvoker(delegate { buttonStart.Enabled = true; }));
                    buttonStart.Invoke(new MethodInvoker(delegate { buttonStartWithoutToggle.Enabled = true; }));
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


        private void buttonDatabaseCommands_Click(object sender, EventArgs e)
        {
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = @"C:\Users\MPhil\source\repos\VoiceLauncherBlazor\VoiceLauncher\bin\Release\net7.0\publish\VoiceLauncher.exe";
            psi.WorkingDirectory = @"C:\Users\MPhil\source\repos\VoiceLauncherBlazor\VoiceLauncher\bin\Release\net7.0\publish\";
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
            Process.Start(psi);
            string commandIdParameter = "";
            if (LastRunCommandId != 0)
            {
                var command = _windowsVoiceCommand.GetCommandById(LastRunCommandId);
                if (command != null)
                {
                    commandIdParameter = $"/{LastRunCommandId}";
                }
            }
            var uri = $"http://localhost:5000/windowsspeechvoicecommands{commandIdParameter}";
            psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            Process.Start(psi);
        }

        private void labelCommandTip_Click(object sender, EventArgs e)
        {

        }

        private void buttonRandomCommand_Click(object sender, EventArgs e)
        {
            DisplayRandomCommand();
        }

        private async void buttonStartWithoutToggle_Click(object sender, EventArgs e)
        {
            if (recognizer == null) { return; }
            string fileName = $"{Application.StartupPath}Mic-03.ico";
            try
            {
                await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
                // This should Toggle DRAGON Microphone
                //_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ADD);
                Icon? icon = Icon.ExtractAssociatedIcon(fileName);
                if (icon != null && notifyIcon1 != null)
                {
                    this.Invoke(new MethodInvoker(delegate { this.Icon = icon; }));
                    notifyIcon1.Icon = new Icon(fileName);
                }
            }
            catch (Exception exception)
            {
                await global::System.Console.Out.WriteLineAsync(exception.Message);
            }
            buttonStop.Invoke(new MethodInvoker(delegate { buttonStop.Enabled = true; }));
            buttonStop.Invoke(new MethodInvoker(delegate { buttonStopNoToggle.Enabled = true; }));
            labelStatus.Invoke(new MethodInvoker(delegate { labelStatus.ForeColor = Color.Green; }));

        }

        private async void buttonStopNoToggle_Click(object sender, EventArgs e)
        {
            await StopContinuous().ConfigureAwait(false);

        }

        private void buttonPrograms_Click(object sender, EventArgs e)
        {
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = @"C:\Users\MPhil\source\repos\SpeechRecognitionHelpers\VoiceLauncher\bin\Release\VoiceLauncher.exe";
            psi.Arguments = "/Launcher /Unknown / ";

            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            Process.Start(psi);

        }
    }
}