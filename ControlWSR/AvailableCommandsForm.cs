using ControlWSR.Speech;
using ControlWSR.Speech.Azure;

using Microsoft.CognitiveServices.Speech;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

using WindowsInput;
using WindowsInput.Native;

namespace ControlWSR
{
    public partial class AvailableCommandsForm : Form
    {
        private readonly System.Speech.Recognition.SpeechRecognizer speechRecogniser = new System.Speech.Recognition.SpeechRecognizer();
        private readonly PerformVoiceCommands performVoiceCommands = new PerformVoiceCommands();
        private readonly SpeechSetup speechSetup = new SpeechSetup();
        private readonly InputSimulator inputSimulator = new InputSimulator();
        DictateSpeech dictateSpeech = new DictateSpeech();
        public bool UseAzureSpeech { get; private set; }
        private Microsoft.CognitiveServices.Speech.SpeechRecognizer AzureRecognizer;
        string textBoxResultsLocal;
        private string availableCommands;
        private string richTextBoxAvailableCommandsLocal;
        private string _rejected;

        public string TextBoxResults
        {
            get => textBoxResultsLocal; set { textBoxResultsLocal = value; textBoxResults.Text = value; }
        }
        public string LabelStatus
        {
            get => labelStatus.Text; set { labelStatus.Text = value; }
        }
        public bool OutputUppercase
        {
            get => checkBoxUppercase.Checked;
            set
            {
                checkBoxUppercase.Checked = value;
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
                checkBoxLowercase.Checked = value;
                if (value)
                {
                    checkBoxUppercase.Checked = false;
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
            set => checkBoxConvertWordsToSymbols.Checked = value;
        }
        public bool RemovePunctuation
        {
            get => checkBoxRemovePunctuation.Checked;
            set => checkBoxRemovePunctuation.Checked = value;
        }
        public string AvailableCommands
        { get => availableCommands; set { availableCommands = value; richTextBoxAvailableCommands.Text = value; } }
        public string RichTextBoxAvailableCommands
        {
            get => richTextBoxAvailableCommandsLocal;
            set { richTextBoxAvailableCommandsLocal = value; richTextBoxAvailableCommands.Text = value; }
        }
        public AvailableCommandsForm()
        {
            string SPEECH__SERVICE__KEY;
            string SPEECH__SERVICE__REGION;
            SPEECH__SERVICE__KEY = ConfigurationManager.AppSettings.Get("SpeechAzureKey");
            SPEECH__SERVICE__REGION = ConfigurationManager.AppSettings.Get("SpeechAzureRegion");
            if (SPEECH__SERVICE__KEY == "TBC" || SPEECH__SERVICE__REGION == "TBC")
            {
                UseAzureSpeech = false;
                AutoClosingMessageBox.Show("Please register the Speech Service on Windows Azure and enter the key and region into the application settings file, and then try again to use this service!", "Accurate Dictation via the Cloud", 8000);
            }
            else
            {
                UseAzureSpeech = true;
                var config = SpeechConfig.FromSubscription(SPEECH__SERVICE__KEY, SPEECH__SERVICE__REGION);
                AzureRecognizer = new Microsoft.CognitiveServices.Speech.SpeechRecognizer(config);
                AzureRecognizer.Recognizing += AzureRecognizer_Recognizing;
                AzureRecognizer.Canceled += AzureRecognizer_Canceled;
            }

            PerformVoiceCommands.ToggleSpeechRecognitionListeningMode(inputSimulator);
            InitializeComponent();
            speechRecogniser = speechSetup.StartWindowsSpeechRecognition();
            var availableCommands = speechSetup.SetUpMainCommands(speechRecogniser, UseAzureSpeech);
            richTextBoxAvailableCommands.Text = availableCommands;
            speechRecogniser.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognizer_SpeechRecognised);
            speechRecogniser.SpeechRecognitionRejected += SpeechRecogniser_SpeechRecognitionRejected;
            speechRecogniser.SpeechHypothesized += SpeechRecogniser_SpeechHypothesized;
            speechRecogniser.StateChanged += SpeechRecogniser_StateChanged;
        }

        private void AzureRecognizer_Canceled(object sender, SpeechRecognitionCanceledEventArgs e)
        {
            this.textBoxResults.Text = $"Azure Speech Recognition Cancelled: {e.ErrorDetails}";
        }

        private void AzureRecognizer_Recognizing(object sender, SpeechRecognitionEventArgs e)
        {
            try
            {
                this.textBoxResults.Text = $"Azure Recognising {e.Result.Text}";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void SpeechRecogniser_StateChanged(object sender, StateChangedEventArgs e)
        {
            this.textBoxResults.Text = "State has changed to: " + e.RecognizerState.ToString();
        }

        private void SpeechRecogniser_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {

            this.textBoxResults.Text = "Hypothesized. I'm listening... (" + e.Result.Text + " " + Math.Round(e.Result.Confidence * 100) + "%)";
            if (string.IsNullOrEmpty(_rejected))
            {
                if (e.Result.Text != "yes" && e.Result.Confidence > 0.5F)
                {
                    _rejected = e.Result.Text;
                    this.textBoxResults.Text = "Confirm.  Did you mean " + e.Result.Text + "? (say yes or no)";
                }
            }

        }

        private void SpeechRecogniser_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            if (e.Result.Text != "yes" && e.Result.Confidence > 0.5F)
            {
                _rejected = e.Result.Text;
                this.textBoxResults.Text = "Rejected.  Did you mean " + e.Result.Text + "? (say yes or no)";
            }
        }

        private void SpeechRecognizer_SpeechRecognised(object sender, SpeechRecognizedEventArgs e)
        {

            speechRecogniser.PauseRecognizerOnRecognition = true;

            if (e.Result.Grammar.Name == "no")
            {
                _rejected = null;
                return;
            }
            if (e.Result.Grammar.Name == "yes")
            {
                speechRecogniser.EmulateRecognizeAsync(_rejected);
                _rejected = null;
                return;
            }

            textBoxResults.Text = "";
            var text = $"{e.Result.Text} {e.Result.Confidence:P}{Environment.NewLine}{Environment.NewLine}";
            foreach (var alternate in e.Result.Alternates)
            {
                text += $"{alternate.Text} {alternate.Confidence:P}{Environment.NewLine}";
            }
            textBoxResults.Text = text;
            performVoiceCommands.PerformCommand(e, this, speechRecogniser, dictateSpeech, AzureRecognizer);
            try
            {
                speechRecogniser.PauseRecognizerOnRecognition = false;
            }
            catch (Exception)
            {
                // Ignore this error
            }
        }

        private void TestingBtn_Click(object sender, EventArgs e)
        {
            //AutoClosingMessageBox.Show("Testing", "This should close in three seconds", 3000);
            //performVoiceCommands.PerformContinuousDictation(this, speechRecogniser);
            //var result = speechRecogniser.EmulateRecognize("continuous");
        }

        private void AvailableCommandsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PerformVoiceCommands.ToggleSpeechRecognitionListeningMode(inputSimulator);
            try
            {
                speechRecogniser.Enabled = false;
                speechRecogniser.Dispose();

            }
            catch (Exception)
            {
                // Just ignore
            }
        }
        private void AvailableCommandsForm_Load(object sender, EventArgs e)
        {
            // Console.CursorVisible = true;//Not sure if this works yet 19/08/2022 08:50:09

            BackColor = Color.FromArgb(38, 38, 38);
            ForeColor = Color.White;
            FontFamily fontFamily = new FontFamily("Cascadia Code");
            Font font = new Font(fontFamily, (float)11, FontStyle.Bold, GraphicsUnit.Point);
            richTextBoxAvailableCommands.Font = font;
            richTextBoxAvailableCommands.BackColor = Color.FromArgb(38, 38, 38);
            richTextBoxAvailableCommands.ForeColor = Color.White;
            textBoxResults.Font = font;
            textBoxResults.BackColor = Color.Black;
            textBoxResults.ForeColor = Color.White;
            var processes = Process.GetProcessesByName("KBPro");
            PerformVoiceCommands performVoiceCommands = new PerformVoiceCommands();
            //System.Windows.Forms.MessageBox.Show("processes: " + processes.Length);
            if (processes.Length == 0)
            {
                IntPtr hwnd = PerformVoiceCommands.GetForegroundWindow();
                uint pid;
                PerformVoiceCommands.GetWindowThreadProcessId(hwnd, out pid);
                Process currentProcess = Process.GetProcessById((int)pid);
                List<string> keysKB = new List<string>(new string[] { "^+k" });
                performVoiceCommands.SendKeysCustom(null, null, keysKB, currentProcess.ProcessName);
            }
            //send Dragon to sleep
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DIVIDE);

            PerformVoiceCommands.ToggleSpeechRecognitionListeningMode(inputSimulator);
            inputSimulator.Mouse.MoveMouseTo(500, 500);
            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
            inputSimulator.Mouse.MoveMouseBy(50, 60);
            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
            inputSimulator.Mouse.MoveMouseBy(50, 60);
            //var result = speechRecogniser.EmulateRecognize("Continuous");

            //performVoiceCommands.PerformContinuousDictation(this, speechRecogniser);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            richTextBoxAvailableCommands.SelectAll();
            richTextBoxAvailableCommands.SelectionBackColor = Color.Black;
            if (textBoxSearch.Text == null || textBoxSearch.Text.Length < 4)
            {
                return;
            }
            var searchFrom = 0;
            var position = 0;
            var successfulFinds = 0;
            while (position >= 0)
            {
                position = FindMyText(textBoxSearch.Text, searchFrom, richTextBoxAvailableCommands.Text.Length);
                if (position >= 0)
                {
                    successfulFinds++;
                    searchFrom = position + 1;
                    richTextBoxAvailableCommands.SelectionStart = position;
                    richTextBoxAvailableCommands.SelectionLength = textBoxSearch.Text.Length;
                    richTextBoxAvailableCommands.SelectionBackColor = Color.Red;
                    // scroll it automatically
                    richTextBoxAvailableCommands.ScrollToCaret();
                }
            }
            richTextBoxAvailableCommands.DeselectAll();
        }
        public int FindMyText(string searchText, int searchStart, int searchEnd)
        {
            // Initialize the return value to false by default.
            int returnValue = -1;

            // Ensure that a search string and a valid starting point are specified.
            if (searchText.Length > 0 && searchStart >= 0)
            {
                // Ensure that a valid ending value is provided.
                if (searchEnd > searchStart || searchEnd == -1)
                {
                    // Obtain the location of the search string in richTextBox1.
                    int indexToText = richTextBoxAvailableCommands.Find(searchText, searchStart, searchEnd, RichTextBoxFinds.MatchCase);
                    // Determine whether the text was found in richTextBox1.
                    if (indexToText >= 0)
                    {
                        // Return the index to the specified search text.
                        returnValue = indexToText;
                    }
                }
            }

            return returnValue;
        }

        private void checkBoxUppercase_Click(object sender, EventArgs e)
        {
            if (checkBoxUppercase.Checked) { checkBoxLowercase.Checked = false; }
        }

        private void checkBoxLowercase_Click(object sender, EventArgs e)
        {
            if (checkBoxLowercase.Checked) { checkBoxUppercase.Checked = false; }
        }
    }
}
