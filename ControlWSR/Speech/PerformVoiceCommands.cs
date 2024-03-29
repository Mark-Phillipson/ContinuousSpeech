﻿using ControlWSR.Repositories;
using ControlWSR.Speech.Azure;

using Microsoft.CognitiveServices.Speech;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

using WindowsInput;
using WindowsInput.Native;

namespace ControlWSR.Speech
{
    public class PerformVoiceCommands
    {
        WindowsVoiceCommand windowsVoiceCommand = new WindowsVoiceCommand();
        readonly SpeechCommandsHelper SpeechCommandsHelper = new SpeechCommandsHelper();
        readonly InputSimulator inputSimulator = new InputSimulator();
        //readonly WindowsSpeechVoiceCommandDataService _windowsSpeechVoiceCommandDataService = new WindowsSpeechVoiceCommandDataService();
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
        public string CommandToBeConfirmed { get; set; } = null;
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
        private readonly SpeechSetup speechSetup = new SpeechSetup();
        public PerformVoiceCommands()
        {
            UpdateCurrentProcess();
        }
        public async void PerformCommand(SpeechRecognizedEventArgs e, AvailableCommandsForm form, System.Speech.Recognition.SpeechRecognizer speechRecogniser, DictateSpeech dictateSpeech, Microsoft.CognitiveServices.Speech.SpeechRecognizer speechRecognizer)
        {
            UpdateCurrentProcess();
            try
            {
                SpeechUI.SendTextFeedback(e.Result, $"Recognised: {e.Result.Text} {e.Result.Confidence:P1}", true);
            }
            catch (Exception exception)
            {
                AutoClosingMessageBox.Show(exception.Message, "Error Sending feedback to speech recognition", 3000);
            }


            var command = windowsVoiceCommand.GetCommand(e.Result.Grammar.Name);
            if (command != null)
            {
                List<CustomWindowsSpeechCommand> actions = windowsVoiceCommand.GetChildActions(command.Id);
                foreach (var action in actions)
                {
                    if (action.WaitTime > 0)
                    {
                        Thread.Sleep(action.WaitTime);
                    }
                    if (action.KeyDownValue != VirtualKeyCode.NONAME)
                    {
                        inputSimulator.Keyboard.KeyDown(action.KeyDownValue);
                    }
                    if (!string.IsNullOrWhiteSpace(action.TextToEnter))
                    {
                        inputSimulator.Keyboard.TextEntry(action.TextToEnter);
                    }
                    if (action.ControlKey || action.AlternateKey || action.ShiftKey || action.WindowsKey)
                    {
                        var modifiers = new List<VirtualKeyCode>();
                        if (action.ControlKey)
                        {
                            modifiers.Add(VirtualKeyCode.CONTROL);
                        }
                        if (action.AlternateKey)
                        {
                            modifiers.Add(VirtualKeyCode.MENU);
                        }
                        if (action.ShiftKey)
                        {
                            modifiers.Add(VirtualKeyCode.SHIFT);
                        }
                        if (action.WindowsKey)
                        {
                            modifiers.Add(VirtualKeyCode.LWIN);
                        }
                        if (action.KeyPressValue != VirtualKeyCode.NONAME)
                        {
                            inputSimulator.Keyboard.ModifiedKeyStroke(modifiers, action.KeyPressValue);
                        }
                    }
                    else if (action.KeyPressValue != VirtualKeyCode.NONAME)
                    {
                        inputSimulator.Keyboard.KeyPress(action.KeyPressValue);
                    }
                    if (action.KeyUpValue != VirtualKeyCode.NONAME)
                    {
                        inputSimulator.Keyboard.KeyUp(action.KeyUpValue);
                    }
                    if (action.MouseCommand == "LeftButtonDown")
                    {
                        inputSimulator.Mouse.LeftButtonDown();
                    }
                    else if (action.MouseCommand == "RightButtonDown")
                    {
                        inputSimulator.Mouse.RightButtonDown();
                    }
                    else if (action.MouseCommand == "LeftButtonDoubleClick")
                    {
                        inputSimulator.Mouse.LeftButtonDoubleClick();
                    }
                    else if (action.MouseCommand == "RightButtonDoubleClick")
                    {
                        inputSimulator.Mouse.RightButtonDoubleClick();
                    }
                    else if (action.MouseCommand == "LeftButtonUp")
                    {
                        inputSimulator.Mouse.LeftButtonUp();
                    }
                    else if (action.MouseCommand == "RightButtonUp")
                    {
                        inputSimulator.Mouse.RightButtonUp();
                    }
                    else if (action.MouseCommand == "MiddleButtonClick")
                    {
                        inputSimulator.Mouse.MiddleButtonClick();
                    }
                    else if (action.MouseCommand == "MiddleButtonDoubleClick")
                    {
                        inputSimulator.Mouse.MiddleButtonDoubleClick();
                    }
                    else if (action.MouseCommand == "HorizontalScroll")
                    {
                        inputSimulator.Mouse.HorizontalScroll(action.ScrollAmount);
                    }
                    else if (action.MouseCommand == "VerticalScroll")
                    {
                        inputSimulator.Mouse.VerticalScroll(action.ScrollAmount);
                    }
                    else if (action.MouseCommand == "MoveMouseBy")
                    {
                        inputSimulator.Mouse.MoveMouseBy(action.MouseMoveX, action.MouseMoveY);
                    }
                    else if (action.MouseCommand == "MoveMouseTo")
                    {
                        inputSimulator.Mouse.MoveMouseTo(action.AbsoluteX, action.AbsoluteY);
                    }
                    if (!string.IsNullOrWhiteSpace(action.ProcessStart))
                    {
                        Process.Start(action.ProcessStart, action?.CommandLineArguments);
                    }
                    if (!string.IsNullOrWhiteSpace(action.SendKeysValue))
                    {
                        SendKeys.Send(action.SendKeysValue);
                    }
                }
                return;
            }
            if (e.Result.Grammar.Name == "Add Html Tags" && e.Result.Confidence > 0.4)
            {
                PerformHtmlTagsInsertion(e);
            }
            if (e.Result.Grammar.Name == "Continuous Dictation" && e.Result.Confidence > 0.2)
            {
                //await PerformContinuousDictation(form, speechRecogniser);
            }
            if (e.Result.Grammar.Name == "Find Code" && e.Result.Confidence > 0.4)
            {
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
                inputSimulator.Keyboard.Sleep(100);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SHIFT, VirtualKeyCode.RIGHT);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_F);
                inputSimulator.Keyboard.Sleep(100);
                var searchTerm = "";
                var counter = 0;
                foreach (var word in e.Result.Words)
                {
                    if (counter >= 2)
                    {
                        searchTerm = $"{searchTerm} {word.Text}";
                    }
                    counter++;
                }
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    inputSimulator.Keyboard.TextEntry(searchTerm);
                }
                inputSimulator.Keyboard.Sleep(100);
                if (e.Result.Words[1].Text == "Previous")
                {
                    inputSimulator.Keyboard.Sleep(100);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SHIFT, VirtualKeyCode.F3);
                    inputSimulator.Keyboard.Sleep(100);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
                }
                else
                {
                    inputSimulator.Keyboard.Sleep(100);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
                }
            }
            //if (e.Result.Grammar.Name == "Step Into" && e.Result.Confidence > 0.4)
            //{
            //    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.F11);
            //}
            //if (e.Result.Grammar.Name == "Reset Code" && e.Result.Confidence > 0.4)
            //{
            //    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.F5);
            //}
            //else if (e.Result.Grammar.Name == "Toggle Mouse" && e.Result.Confidence > 0.4)
            //{
            //    inputSimulator.Keyboard.ModifiedKeyStroke(controlAndAlt, VirtualKeyCode.F9);
            //}
            //else if (e.Result.Grammar.Name == "Centre Mouse" && e.Result.Confidence > 0.4)
            //{
            //    inputSimulator.Keyboard.ModifiedKeyStroke(controlAndAlt, VirtualKeyCode.F12);
            //}
            if (e.Result.Grammar.Name == "Use Dragon" && e.Result.Confidence > 0.4)
            {
                ToggleSpeechRecognitionListeningMode(inputSimulator);
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.ADD);
            }
            //else if (e.Result.Grammar.Name == "Window Monitor Switch" && e.Result.Confidence > 0.4)
            //{
            //    inputSimulator.Keyboard.ModifiedKeyStroke(windowAndShift, VirtualKeyCode.RIGHT);
            //}
            //else if (e.Result.Grammar.Name == "Select Line" && e.Result.Confidence > 0.4)
            //{
            //    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
            //    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.END);
            //}
            //else if (e.Result.Grammar.Name == "Mouse Down" && e.Result.Confidence > 0.4)
            //{
            //    inputSimulator.Mouse.LeftButtonDown();
            //}
            else if (e.Result.Grammar.Name == "Shutdown Windows" && e.Result.Confidence > 0.4)
            {
                CommandToBeConfirmed = e.Result.Grammar.Name;
                SetupConfirmationCommands(speechRecogniser, form);
            }
            else if (e.Result.Grammar.Name == "Restart Windows" && e.Result.Confidence > 0.4)
            {
                CommandToBeConfirmed = e.Result.Grammar.Name;
                SetupConfirmationCommands(speechRecogniser, form);
            }
            else if (e.Result.Grammar.Name == "Confirmed")
            {

                if (CommandToBeConfirmed == "Shutdown Windows")
                {
                    Process.Start("shutdown", "/s /t 10");
                }
                else if (CommandToBeConfirmed == "Restart Windows")
                {
                    Process.Start("shutdown", "/r /t 10");
                }
                PerformQuitApplicationCommand(e);
            }
            else if (e.Result.Grammar.Name == "Short Dictation" && e.Result.Confidence > 0.4)
            {
                await PerformShortDictation(e, form, dictateSpeech, speechRecognizer);
            }
            else if (e.Result.Grammar.Name == "Serenade" && e.Result.Confidence > 0.4)
            {
                PerformSerenadeCommand(speechRecogniser);
            }
            else if (e.Result.Grammar.Name == "Denied")
            {
                var availableCommands = speechSetup.SetUpMainCommands(speechRecogniser, form.UseAzureSpeech);
                form.RichTextBoxAvailableCommands = availableCommands;
            }
            else if (e.Result.Grammar.Name == "Studio" && e.Result.Confidence > 0.4)
            {
                RunVisualStudioCommand(speechRecogniser);
            }
            //else if (e.Result.Grammar.Name == "Default Box" && e.Result.Confidence > 0.5)
            //{
            //    Process.Start(@"C:\Users\MPhil\Source\Repos\SpeechRecognitionHelpers\DictationBoxMSP\bin\Release\DictationBoxMSP.exe");
            //}
            //else if (e.Result.Grammar.Name == "Dictation Box" && e.Result.Confidence > 0.5)
            //{
            //    Process.Start(@"C:\Program Files (x86)\Speech Productivity\dictation box default\dictation box.exe");
            //}
            //else if (e.Result.Grammar.Name == "Get and Set" && e.Result.Confidence > 0.5)
            //{
            //    inputSimulator.Keyboard.TextEntry(" { get; set; }");
            //}
            else if (e.Result.Grammar.Name.Contains("Phonetic Alphabet")) // Could be lower, mixed or upper
            {
                ProcessKeyboardCommand(e);
            }
            //else if (e.Result.Grammar.Name == "Show Recent" && e.Result.Confidence > 0.5)
            //{
            //    inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LMENU, VirtualKeyCode.VK_F);
            //    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_J);
            //}
            //else if (e.Result.Grammar.Name == "Fresh Line" && e.Result.Confidence > 0.5)
            //{
            //    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
            //    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            //}
            // where the grammar name is the same as the method Without the perform and command, with the spaces remove use reflection to call it
            else if (e.Result.Confidence > 0.4)
            {
                string methodName = $"Perform{e.Result.Grammar.Name.Replace(" ", "")}Command";
                Type thisType = this.GetType();
                //MethodInfo theMethod = thisType.GetMethod(methodName,BindingFlags.NonPublic  | BindingFlags.Instance);
                try
                {
                    thisType.InvokeMember(methodName,
                        BindingFlags.DeclaredOnly |
        BindingFlags.Public | BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.InvokeMethod
                , null, this, new Object[] { e });

                }
                catch (Exception)
                {
                    //AutoClosingMessageBox.Show(exception.Message, $"Error Running a method {exception.Source}", 3000);
                    //System.Windows.Forms.MessageBox.Show(exception.Message, "Error running a method", MessageBoxButtons.OK);
                }
            }
        }

        public async Task PerformContinuousDictation(AvailableCommandsForm form, System.Speech.Recognition.SpeechRecognizer speechRecogniser)
        {
            ToggleSpeechRecognitionListeningMode(inputSimulator);
            //ContinuousSpeech continuousSpeech = new ContinuousSpeech();
            form.LabelStatus = "Continuous Dictation STARTED";
            var result = await SpeechContinuousRecognitionAsync(form, inputSimulator, speechRecogniser);
            if (result != null)
            {
                form.TextBoxResults = result;
                var rawResult = result.Replace("Stop continuous.", "").Trim();
                //rawResult = RemovePunctuation(rawResult);
                if (rawResult.Length > 0)
                {
                    //inputSimulator.Keyboard.TextEntry(rawResult);
                    Clipboard.SetText(rawResult);
                    ToggleSpeechRecognitionListeningMode(inputSimulator);
                }
            }
        }

        private void PerformHtmlTagsInsertion(SpeechRecognizedEventArgs e)
        {
            var tag = "";
            var counter = 0;
            foreach (var word in e.Result.Words)
            {
                if (counter >= 2)
                {
                    tag = $"{tag} {word.Text}";
                }
                counter++;
            }
            var result = windowsVoiceCommand.GetHtmlTag(tag.Trim());
            string tagReturned = result.ListValue.ToLower();
            string textToType = ""; int moveLeft = 0;
            if (tagReturned == "input" || tagReturned == "br" || tagReturned == "hr")
            {
                textToType = $"<{tagReturned} />";
                moveLeft = 3;
            }
            else if (tagReturned == "img")
            {
                textToType = $"<{tagReturned} src='' />";
                moveLeft = 4;
            }
            else
            {
                textToType = $"<{tagReturned}></{tagReturned}>";
                moveLeft = 4 + tagReturned.Length;
            }
            inputSimulator.Keyboard.TextEntry(textToType);
            for (int i = 1; i < moveLeft; i++)
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LEFT);
            }
        }

        private void PerformSelectionCommand(SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text.ToLower().Contains("left"))
            {
                inputSimulator.Keyboard.ModifiedKeyStroke(controlAndShift, VirtualKeyCode.LEFT);
            }
            else if (e.Result.Text.ToLower().Contains("right"))
            {
                inputSimulator.Keyboard.ModifiedKeyStroke(controlAndShift, VirtualKeyCode.RIGHT);
            }
        }

        private void PerformGoToLineCommand(SpeechRecognizedEventArgs e)
        {
            var wordStartPosition = 3;
            if (e.Result.Text.StartsWith("Line"))
            {
                wordStartPosition = 1;
            }
            var lineNumber = "";
            for (int i = wordStartPosition; i < e.Result.Words.Count; i++)
            {
                lineNumber += e.Result.Words[i].Text + " ";
            }
            var numericLineNumberTest = WordsToNumbers.ConvertToNumbers(lineNumber.ToString());
            lineNumber = numericLineNumberTest.ToString();
            //lineNumber = SpeechCommandsHelper.ConvertTextToNumber(lineNumber);
            bool isANumber = int.TryParse(lineNumber, out int numericLineNumber);
            if (isANumber)
            {
                inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_G);
                Thread.Sleep(100);
                inputSimulator.Keyboard.TextEntry(numericLineNumber.ToString());
                Thread.Sleep(100);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            }
        }

        private void PerformRepeatKeysCommand(SpeechRecognizedEventArgs e)
        {
            List<string> keys = new List<string>();
            SpeechCommandsHelper.BuildRepeatSendkeys(e, keys);
            SendKeysCustom(null, null, keys, currentProcess.ProcessName);
        }

        private void PerformSearchCodeCommand(SpeechRecognizedEventArgs e)
        {
            inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_F);
            var searchTerm = "";
            var counter = 0;
            foreach (var word in e.Result.Words)
            {
                if (counter >= 2)
                {
                    searchTerm = $"{searchTerm} {word.Text}";
                }
                counter++;
            }
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                inputSimulator.Keyboard.TextEntry(searchTerm.Trim());
            }
            if (e.Result.Words[1].Text == "Previous")
            {
                inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.F3);
            }
            Thread.Sleep(100);
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        }

        private void PerformCreateCustomIntelliSenseCommand(SpeechRecognizedEventArgs e)
        {
            inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
            var itemToAdd = Clipboard.GetText();
            string arguments = $@"""/ Add New"" ""/{itemToAdd.Trim()}""";
            Process.Start(VOICE_LAUNCHER, arguments);
        }

        private void PerformWordsDictationCommand(SpeechRecognizedEventArgs e)
        {
            var searchTerm = "";
            var counter = 0;
            foreach (var word in e.Result.Words)
            {
                if (counter >= 1)
                {
                    searchTerm = $"{searchTerm} {word.Text}";
                }
                counter++;
            }
            var customIntelliSense = windowsVoiceCommand.GetWord(searchTerm.Trim());
            if (customIntelliSense == null)
            {
                return;
            }
            string sendkeysValue = customIntelliSense.SendKeys_Value.Replace("(", "{(}");
            sendkeysValue = sendkeysValue.Replace(")", "{)}");

            SendKeys.SendWait(sendkeysValue);
            var additionalCommands = windowsVoiceCommand.GetAdditionalCommands(customIntelliSense.ID);
            foreach (var additionalCommand in additionalCommands)
            {
                if (additionalCommand.WaitBefore > 0)
                {
                    int milliseconds = (int)(additionalCommand.WaitBefore * 1000);
                    Thread.Sleep(milliseconds);
                }
                if (additionalCommand.DeliveryType == "Copy and Paste")
                {
                    try
                    {
                        string clipboardBackup = Clipboard.GetText();
                        Clipboard.SetText(additionalCommand.SendKeys_Value);
                    }
                    catch (Exception)
                    {
                        return;
                    }
                    inputSimulator.Keyboard.Sleep(200);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                }
                else if (additionalCommand.DeliveryType == "Send Keys")
                {
                    sendkeysValue = additionalCommand.SendKeys_Value.Replace("(", "{(}");
                    sendkeysValue = sendkeysValue.Replace(")", "{)}");
                    SendKeys.SendWait(sendkeysValue);
                }
            }
        }
        private void PerformEnterRandomNumbersCommand(SpeechRecognizedEventArgs e)
        {
            int numberOfWords = 0;
            try
            {
                numberOfWords = int.Parse(e.Result.Words[1].Text);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            for (int i = 0; i < numberOfWords - 1; i++)
            {
                Random rnd = new Random();
                int num = rnd.Next(3000);
                inputSimulator.Keyboard.TextEntry(num.ToString());
                inputSimulator.Keyboard.Sleep(100);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
            }
        }
        private void PerformCapCommand(SpeechRecognizedEventArgs e)
        {
            var searchTerm = "";
            var counter = 0;
            foreach (var word in e.Result.Words)
            {
                if (counter >= 1)
                {
                    searchTerm = $"{searchTerm} {word.Text}".Trim();
                }
                counter++;
            }
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Substring(0, 1).ToUpper() + searchTerm.Substring(1);
                inputSimulator.Keyboard.TextEntry(searchTerm);
            }
        }
        private void PerformDictationCommand(SpeechRecognizedEventArgs e)
        {
            var searchTerm = "";
            var counter = 0;
            foreach (var word in e.Result.Words)
            {
                if (counter >= 1)
                {
                    searchTerm = $"{searchTerm} {word.Text}";
                }
                counter++;
            }
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                inputSimulator.Keyboard.TextEntry(searchTerm);
            }
        }
        private void PerformListItemsCommand(SpeechRecognizedEventArgs e)
        {
            var searchTerm = "";
            var counter = 0;
            foreach (var word in e.Result.Words)
            {
                if (counter >= 2)
                {
                    searchTerm = $"{searchTerm} {word.Text}";
                }
                counter++;
            }
            string arguments = $@"""/ Unknown "" ""/ Unknown "" ""/{searchTerm.Trim()}""";
            Process.Start(VOICE_LAUNCHER, arguments);
        }

        private void PerformSearchUnionCommand(SpeechRecognizedEventArgs e)
        {
            var searchTerm = "";
            var counter = 0;
            foreach (var word in e.Result.Words)
            {
                if (counter >= 2)
                {
                    searchTerm = $"{searchTerm} {word.Text}";
                }
                counter++;
            }
            string arguments = $@"""/ Union "" ""/{searchTerm.Trim()}""";
            Process.Start(VOICE_LAUNCHER, arguments);
        }
        private void PerformSearchListCommand(SpeechRecognizedEventArgs e)
        {
            var searchTerm = "";
            var counter = 0;
            foreach (var word in e.Result.Words)
            {
                if (counter >= 2)
                {
                    searchTerm = $"{searchTerm} {word.Text}";
                }
                counter++;
            }
            string arguments = $@"""/ SearchIntelliSense "" ""/{searchTerm.Trim()}""";
            Process.Start(VOICE_LAUNCHER, arguments);
        }
        private void PerformNavigateToCommand(SpeechRecognizedEventArgs e)
        {
            var searchTerm = "";
            var counter = 0;
            foreach (var word in e.Result.Words)
            {
                if (counter >= 2)
                {
                    searchTerm = $"{searchTerm} {word.Text}";
                }
                counter++;
            }
            SendKeys.Send("^,");
            Thread.Sleep(100);
            SendKeys.Send(searchTerm);
        }

        private void PerformSerenadeCommand(System.Speech.Recognition.SpeechRecognizer speechRecogniser)
        {
            Process.Start(@"C:\Users\MPhil\AppData\Local\Programs\Serenade\Serenade.exe");
            speechRecogniser.EmulateRecognize("minimise speech recognition");
            ToggleSpeechRecognitionListeningMode(inputSimulator);
            List<string> keys = new List<string>() { "^% " };
            SendKeysCustom(null, null, keys, currentProcess.ProcessName);
        }

        private void RunVisualStudioCommand(System.Speech.Recognition.SpeechRecognizer speechRecogniser)
        {
            if (currentProcess.ProcessName == "devenv")
            {
                List<string> keys = new List<string>() { "%v" };
                SendKeysCustom(null, null, keys, currentProcess.ProcessName);
                ToggleSpeechRecognitionListeningMode(inputSimulator);
                Thread.Sleep(2000);
                ToggleSpeechRecognitionListeningMode(inputSimulator);
            }
            else
            {
                speechRecogniser.EmulateRecognize("Switch to Visual Studio");
            }

        }

        private async Task PerformShortDictation(SpeechRecognizedEventArgs e, AvailableCommandsForm form, DictateSpeech dictateSpeech, Microsoft.CognitiveServices.Speech.SpeechRecognizer azureSpeechRecogniser)
        {
            ToggleSpeechRecognitionListeningMode(inputSimulator);
            var result = await dictateSpeech.RecognizeSpeechAsync(azureSpeechRecogniser);
            form.TextBoxResults = result.Text;
            var rawResult = result.Text;

            if (!e.Result.Text.ToLower().Contains("punctuation"))
            {
                rawResult = SpeechCommandsHelper.RemovePunctuation(rawResult);
            }
            string[] stringSeparators = new string[] { " " };
            List<string> words = rawResult.Split(stringSeparators, StringSplitOptions.None).ToList();
            if (e.Result.Text.ToLower().Contains("camel"))
            {
                var counter = 0; string value = "";
                foreach (var word in words)
                {
                    counter++;
                    if (counter != 1)
                    {
                        value = value + word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
                    }
                    else
                    {
                        value += word.ToLower();
                    }
                    rawResult = value;
                }
            }
            else if (e.Result.Text.ToLower().Contains("variable"))
            {
                string value = "";
                foreach (var word in words)
                {
                    if (word.Length > 0)
                    {
                        value = value + word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
                    }
                }
                rawResult = value;
            }
            else if (e.Result.Text.ToLower().Contains("dot notation"))
            {
                string value = "";
                foreach (var word in words)
                {
                    if (word.Length > 0)
                    {
                        value = value + word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + ".";
                    }
                }
                rawResult = value.Substring(0, value.Length - 1);
            }
            else if (e.Result.Text.ToLower().Contains("title"))
            {
                string value = "";
                foreach (var word in words)
                {
                    if (word.Length > 0)
                    {
                        value = value + word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    }
                }
                rawResult = value;
            }
            else if (e.Result.Text.ToLower().StartsWith("upper"))
            {
                string value = "";
                foreach (var word in words)
                {
                    value = value + word.ToUpper() + " ";
                }
                rawResult = value;
            }
            else if (e.Result.Text.ToLower().StartsWith("lower"))
            {
                string value = "";
                foreach (var word in words)
                {
                    value = value + word.ToLower() + " ";
                }
                rawResult = value;
            }
            if (!e.Result.Text.ToLower().StartsWith("short") && e.Result.Text.ToLower() != "dictation")
            {
                rawResult = rawResult.Trim();
            }
            if (rawResult.Length > 0)
            {
                inputSimulator.Keyboard.TextEntry(rawResult);
                //SendKeys.Send(rawResult);
            }
            ToggleSpeechRecognitionListeningMode(inputSimulator);
        }



        private void PerformSelectItemsCommand(SpeechRecognizedEventArgs e)
        {
            var repeatCount = Int32.Parse(e.Result.Words[0].Text);
            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
            for (int i = 0; i < repeatCount; i++)
            {
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DOWN);
            }
            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
        }

        private void PerformRestartDragonCommand(SpeechRecognizedEventArgs e)
        {
            var processName = "nsbrowse";
            KillAllProcesses(processName);
            processName = "dragonbar";
            KillAllProcesses(processName);
            processName = "natspeak";
            KillAllProcesses(processName);
            processName = "ProcHandler";
            KillAllProcesses(processName);
            processName = "KBPro";
            KillAllProcesses(processName);
            processName = "dragonlogger";
            KillAllProcesses(processName);
            ToggleSpeechRecognitionListeningMode(inputSimulator);

            try
            {
                Process process = new Process();
                var filename = "C:\\Program Files(x86)\\KnowBrainer\\KnowBrainer Professional 2017\\KBPro.exe";
                if (File.Exists(filename))
                {
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WorkingDirectory = "C:\\Program Files (x86)\\KnowBrainer\\KnowBrainer Professional 2017\\";
                    process.StartInfo.FileName = filename;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    process.Start();
                }
                else
                {
                    IntPtr hwnd = GetForegroundWindow();
                    uint pid;
                    GetWindowThreadProcessId(hwnd, out pid);
                    Process currentProcess = Process.GetProcessById((int)pid);
                    List<string> keysKB = new List<string>(new string[] { "^+k" });
                    SendKeysCustom(null, null, keysKB, currentProcess.ProcessName);
                }
            }
            catch (Exception exception)
            {
                // 	System.Windows.Forms.MessageBox.Show(exception.Message);
                AutoClosingMessageBox.Show(exception.Message, "Error trying to start a process", 3000);
            }
        }
        private void KillAllProcesses(string name)
        {
            var processName = (name);
            if (processName.Length > 0)
            {
                foreach (var process in Process.GetProcessesByName(processName))
                {
                    try
                    {
                        process.Kill();
                    }
                    catch (Exception)
                    {
                        //System.Windows.MessageBox.Show(exception.Message);
                    }
                }
            }
        }
        public static void ToggleSpeechRecognitionListeningMode(InputSimulator inputSimulator)
        {
            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LWIN);
            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
            inputSimulator.Keyboard.Sleep(200);
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        }

        void SetupConfirmationCommands(System.Speech.Recognition.SpeechRecognizer speechRecogniser, AvailableCommandsForm availableCommandsForm)
        {
            var availableCommands = speechSetup.SetupConfirmationCommands(CommandToBeConfirmed, speechRecogniser, availableCommandsForm);
            availableCommandsForm.AvailableCommands = availableCommands;
        }
        private void UpdateCurrentProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            currentProcess = Process.GetProcessById((int)pid);
        }
        public void PerformQuitApplicationCommand(SpeechRecognizedEventArgs e)
        {
            //inputSimulator.Keyboard.KeyDown(VirtualKeyCode.DIVIDE);
            var processes = Process.GetProcessesByName("sapisvr");
            if (processes != null)
            {
                foreach (var process in processes)
                {
                    process.Kill();
                    //process.CloseMainWindow();
                    //process.Close();
                }
            }
            try
            {
                System.Windows.Forms.Application.Exit();
            }
            catch (Exception exception)
            {
                System.Windows.MessageBox.Show(exception.Message, "Error trying to shut down", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void SendKeysCustom(string applicationClass, string applicationCaption, List<string> keys, string processName, string applicationToLaunch = "", int delay = 0)
        {
            // Get a handle to the application. The window class
            // and window name can be obtained using the Spy++ tool.
            IntPtr applicationHandle = IntPtr.Zero;
            while (true)
            {
                if (applicationClass != null || applicationCaption != null)
                {
                    applicationHandle = FindWindow(applicationClass, applicationCaption);
                }

                // Verify that Application is a running process.
                if (applicationHandle == IntPtr.Zero)
                {
                    if (applicationToLaunch != null && applicationToLaunch.Length > 0)
                    {
                        Process.Start(applicationToLaunch);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        //       System.Windows.MessageBox.Show($"{applicationCaption} is not running.");
                        //ActivateApp(processName);
                        Process process = Process.GetProcessesByName(processName)[0];
                        applicationHandle = process.Handle;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            // Make Application the foreground application and send it
            // a set of Keys.
            SetForegroundWindow(applicationHandle);
            foreach (var item in keys)
            {
                Thread.Sleep(delay);
                try
                {
                    var temporary = item.Replace("(", "{(}");
                    temporary = temporary.Replace(")", "{)}");

                    SendKeys.SendWait(temporary);
                }
                catch (Exception exception)
                {
                    System.Windows.MessageBox.Show(exception.Message, "Error trying to shut down", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void PerformHorizontalPositionMouseCommand(SpeechRecognizedEventArgs e)
        {
            Win32.POINT p = new Win32.POINT();
            p.x = 100;
            p.y = 100;
            var horizontalCoordinate = e.Result.Words[1].Text;
            if (horizontalCoordinate == "Zero")
            {
                p.x = 5;
            }
            else if (horizontalCoordinate == "Alpha")
            {
                p.x = 50;
            }
            else if (horizontalCoordinate == "Bravo")
            {
                p.x = 100;
            }
            else if (horizontalCoordinate == "Charlie")
            {
                p.x = 150;
            }
            else if (horizontalCoordinate == "Delta")
            {
                p.x = 200;
            }
            else if (horizontalCoordinate == "Echo")
            {
                p.x = 250;
            }
            else if (horizontalCoordinate == "Foxtrot")
            {
                p.x = 300;
            }
            else if (horizontalCoordinate == "Golf")
            {
                p.x = 350;
            }
            else if (horizontalCoordinate == "Hotel")
            {
                p.x = 400;
            }
            else if (horizontalCoordinate == "India")
            {
                p.x = 450;
            }
            else if (horizontalCoordinate == "Juliet")
            {
                p.x = 500;
            }
            else if (horizontalCoordinate == "Kilo")
            {
                p.x = 550;
            }
            else if (horizontalCoordinate == "Lima")
            {
                p.x = 600;
            }
            else if (horizontalCoordinate == "Mike")
            {
                p.x = 650;
            }
            else if (horizontalCoordinate == "November")
            {
                p.x = 700;
            }
            else if (horizontalCoordinate == "Oscar")
            {
                p.x = 750;
            }
            else if (horizontalCoordinate == "Papa")
            {
                p.x = 800;
            }
            else if (horizontalCoordinate == "Qubec")
            {
                p.x = 850;
            }
            else if (horizontalCoordinate == "Romeo")
            {
                p.x = 900;
            }
            else if (horizontalCoordinate == "Sierra")
            {
                p.x = 950;
            }
            else if (horizontalCoordinate == "Tango")
            {
                p.x = 1000;
            }
            else if (horizontalCoordinate == "Uniform")
            {
                p.x = 1050;
            }
            else if (horizontalCoordinate == "Victor")
            {
                p.x = 1100;
            }
            else if (horizontalCoordinate == "Whiskey")
            {
                p.x = 1150;
            }
            else if (horizontalCoordinate == "X-ray")
            {
                p.x = 1200;
            }
            else if (horizontalCoordinate == "Yankee")
            {
                p.x = 1250;
            }
            else if (horizontalCoordinate == "Zulu")
            {
                p.x = 1300;
            }
            else if (horizontalCoordinate == "1")
            {
                p.x = 1350;
            }
            else if (horizontalCoordinate == "2")
            {
                p.x = 1400;
            }
            else if (horizontalCoordinate == "3")
            {
                p.x = 1450;
            }
            else if (horizontalCoordinate == "4")
            {
                p.x = 1500;
            }
            else if (horizontalCoordinate == "5")
            {
                p.x = 1550;
            }
            else if (horizontalCoordinate == "6")
            {
                p.x = 1600;
            }
            else if (horizontalCoordinate == "7")
            {
                p.x = 1650;
            }
            if (e.Result.Words[0].Text == "Taskbar")
            {
                p.y = 1030;
            }
            else if (e.Result.Words[0].Text == "Ribbon" || e.Result.Words[0].Text == "Menu")
            {
                p.y = 85;
            }
            if (e.Result.Words.Count == 3)
            {
                if (e.Result.Words[2].Text == "1")
                {
                    p.x = p.x + 5;
                }
                else if (e.Result.Words[2].Text == "2")
                {
                    p.x = p.x + (2 * 5);
                }
                else if (e.Result.Words[2].Text == "3")
                {
                    p.x = p.x + (3 * 5);
                }
                else if (e.Result.Words[2].Text == "4")
                {
                    p.x = p.x + (4 * 5);
                }
                else if (e.Result.Words[2].Text == "5")
                {
                    p.x = p.x + (5 * 5);
                }
                else if (e.Result.Words[2].Text == "6")
                {
                    p.x = p.x + (6 * 5);
                }
                else if (e.Result.Words[2].Text == "7")
                {
                    p.x = p.x + (7 * 5);
                }
                else if (e.Result.Words[2].Text == "8")
                {
                    p.x = p.x + (8 * 5);
                }
                else if (e.Result.Words[2].Text == "9")
                {
                    p.x = p.x + (9 * 5);
                }
            }
            Win32.SetCursorPos(p.x, p.y);
            SpeechUI.SendTextFeedback(e.Result, $" {e.Result.Text} H{p.x} V{p.y}", true);
            Win32.mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.x, (uint)p.y, 0, 0);
        }
        private void PerformMouseCommand(SpeechRecognizedEventArgs e)
        {
            Win32.POINT p = new Win32.POINT();
            p.x = 100;
            p.y = 100;
            var horizontalCoordinate = e.Result.Words[1].Text;
            if (horizontalCoordinate == "Zero")
            {
                p.x = 5;
            }
            else if (horizontalCoordinate == "Alpha")
            {
                p.x = 50;
            }
            else if (horizontalCoordinate == "Bravo")
            {
                p.x = 100;
            }
            else if (horizontalCoordinate == "Charlie")
            {
                p.x = 150;
            }
            else if (horizontalCoordinate == "Delta")
            {
                p.x = 200;
            }
            else if (horizontalCoordinate == "Echo")
            {
                p.x = 250;
            }
            else if (horizontalCoordinate == "Foxtrot")
            {
                p.x = 300;
            }
            else if (horizontalCoordinate == "Golf")
            {
                p.x = 350;
            }
            else if (horizontalCoordinate == "Hotel")
            {
                p.x = 400;
            }
            else if (horizontalCoordinate == "India")
            {
                p.x = 450;
            }
            else if (horizontalCoordinate == "Juliet")
            {
                p.x = 500;
            }
            else if (horizontalCoordinate == "Kilo")
            {
                p.x = 550;
            }
            else if (horizontalCoordinate == "Lima")
            {
                p.x = 600;
            }
            else if (horizontalCoordinate == "Mike")
            {
                p.x = 650;
            }
            else if (horizontalCoordinate == "November")
            {
                p.x = 700;
            }
            else if (horizontalCoordinate == "Oscar")
            {
                p.x = 750;
            }
            else if (horizontalCoordinate == "Papa")
            {
                p.x = 800;
            }
            else if (horizontalCoordinate == "Qubec")
            {
                p.x = 850;
            }
            else if (horizontalCoordinate == "Romeo")
            {
                p.x = 900;
            }
            else if (horizontalCoordinate == "Sierra")
            {
                p.x = 950;
            }
            else if (horizontalCoordinate == "Tango")
            {
                p.x = 1000;
            }
            else if (horizontalCoordinate == "Uniform")
            {
                p.x = 1050;
            }
            else if (horizontalCoordinate == "Victor")
            {
                p.x = 1100;
            }
            else if (horizontalCoordinate == "Whiskey")
            {
                p.x = 1150;
            }
            else if (horizontalCoordinate == "X-ray")
            {
                p.x = 1200;
            }
            else if (horizontalCoordinate == "Yankee")
            {
                p.x = 1250;
            }
            else if (horizontalCoordinate == "Zulu")
            {
                p.x = 1300;
            }
            else if (horizontalCoordinate == "1")
            {
                p.x = 1350;
            }
            else if (horizontalCoordinate == "2")
            {
                p.x = 1400;
            }
            else if (horizontalCoordinate == "3")
            {
                p.x = 1450;
            }
            else if (horizontalCoordinate == "4")
            {
                p.x = 1500;
            }
            else if (horizontalCoordinate == "5")
            {
                p.x = 1550;
            }
            else if (horizontalCoordinate == "6")
            {
                p.x = 1600;
            }
            else if (horizontalCoordinate == "7")
            {
                p.x = 1650;
            }
            var verticalCoordinate = e.Result.Words[2].Text;
            if (verticalCoordinate == "Zero")
            {
                p.y = 5;
            }
            else if (verticalCoordinate == "Alpha")
            {
                p.y = 50;
            }
            else if (verticalCoordinate == "Bravo")
            {
                p.y = 100;
            }
            else if (verticalCoordinate == "Charlie")
            {
                p.y = 150;
            }
            else if (verticalCoordinate == "Delta")
            {
                p.y = 200;
            }
            else if (verticalCoordinate == "Echo")
            {
                p.y = 250;
            }
            else if (verticalCoordinate == "Foxtrot")
            {
                p.y = 300;
            }
            else if (verticalCoordinate == "Golf")
            {
                p.y = 350;
            }
            else if (verticalCoordinate == "Hotel")
            {
                p.y = 400;
            }
            else if (verticalCoordinate == "India")
            {
                p.y = 450;
            }
            else if (verticalCoordinate == "Juliet")
            {
                p.y = 500;
            }
            else if (verticalCoordinate == "Kilo")
            {
                p.y = 550;
            }
            else if (verticalCoordinate == "Lima")
            {
                p.y = 600;
            }
            else if (verticalCoordinate == "Mike")
            {
                p.y = 650;
            }
            else if (verticalCoordinate == "November")
            {
                p.y = 700;
            }
            else if (verticalCoordinate == "Oscar")
            {
                p.y = 750;
            }
            else if (verticalCoordinate == "Papa")
            {
                p.y = 800;
            }
            else if (verticalCoordinate == "Qubec")
            {
                p.y = 850;
            }
            else if (verticalCoordinate == "Romeo")
            {
                p.y = 900;
            }
            else if (verticalCoordinate == "Sierra")
            {
                p.y = 950;
            }
            else if (verticalCoordinate == "Tango")
            {
                p.y = 1000;
            }
            else if (verticalCoordinate == "Uniform")
            {
                p.y = 1050;
            }
            else if (verticalCoordinate == "Victor")
            {
                p.y = 1100;
            }
            else if (verticalCoordinate == "Whiskey")
            {
                p.y = 1150;
            }
            else if (verticalCoordinate == "X-ray")
            {
                p.y = 1200;
            }
            else if (verticalCoordinate == "Yankee")
            {
                p.y = 1250;
            }
            else if (verticalCoordinate == "Zulu")
            {
                p.y = 1300;
            }
            else if (verticalCoordinate == "1")
            {
                p.y = 1350;
            }
            else if (verticalCoordinate == "2")
            {
                p.y = 1400;
            }
            else if (verticalCoordinate == "3")
            {
                p.y = 1450;
            }
            else if (verticalCoordinate == "4")
            {
                p.y = 1500;
            }
            else if (verticalCoordinate == "5")
            {
                p.y = 1550;
            }
            else if (verticalCoordinate == "6")
            {
                p.y = 1600;
            }
            else if (verticalCoordinate == "7")
            {
                p.y = 1650;
            }
            var screen = e.Result.Words[0].Text;
            if (screen == "Right" || screen == "Touch")
            {
                p.x += 1680;
            }

            Win32.SetCursorPos(p.x, p.y);
            SpeechUI.SendTextFeedback(e.Result, $" {e.Result.Text} H{p.x} V{p.y}", true);
            if (screen == "Click" || screen == "Touch")
            {
                Win32.mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.x, (uint)p.y, 0, 0);
            }
        }
        private void ProcessKeyboardCommand(SpeechRecognizedEventArgs e)
        {
            var value = e.Result.Text;
            List<string> phoneticAlphabet = new List<string> { "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Qubec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "X-ray", "Yankee", "Zulu" };
            foreach (var item in phoneticAlphabet)
            {
                if (value.IndexOf("Shift") > 0)
                {
                    value = value.Replace(item, item.ToUpper().Substring(0, 1));
                }
                else
                {
                    value = value.Replace(item, item.ToLower().Substring(0, 1));
                }
            }
            value = value.Replace("Press ", "");
            value = value.Replace("Semicolon", ";");
            value = value.Replace("Control", "^");
            value = value.Replace("Alt Space", "% ");
            value = value.Replace("Alt", "%");
            value = value.Replace("Escape", "{Esc}");
            value = value.Replace("Zero", "0");
            value = value.Replace("Stop", ".");
            value = value.Replace("Tab", "{Tab}");
            value = value.Replace("Backspace", "{Backspace}");
            value = value.Replace("Enter", "{Enter}");
            value = value.Replace("Page Down", "{PgDn}");
            if (value.IndexOf("Page Up") >= 0)
            {
                value = value.Replace("Page Up", "{PgUp}");
            }
            else
            {
                value = value.Replace("Up", "{Up}");
            }
            value = value.Replace("Right", "{Right}");
            value = value.Replace("Left", "{Left}");
            value = value.Replace("Down", "{Down}");
            value = value.Replace("Delete", "{Del}");
            value = value.Replace("Home", "{Home}");
            value = value.Replace("End", "{End}");
            value = value.Replace("Hyphen", "-");
            value = value.Replace("Colon", ":");
            value = value.Replace("Ampersand", "&");
            value = value.Replace("Dollar", "$");
            value = value.Replace("Exclamation Mark", "!");
            value = value.Replace("Double Quote", "\"");
            value = value.Replace("Pound", "£");
            value = value.Replace("Asterix", "*");
            value = value.Replace("Apostrophe", "'");
            value = value.Replace("Equal", "=");
            value = value.Replace("Open Bracket", "(");
            value = value.Replace("Close Bracket", ")");


            for (int i = 12; i > 0; i--)
            {
                value = value.Replace($"Function {i}", "{F" + i + "}");
            }
            value = value.Replace("Shift", "+");
            if (value != "% ")
            {
                value = value.Replace(" ", "");
            }
            if (value.ToLower().Contains("space"))
            {
                value = value.ToLower().Replace("space", " ");
            }
            if (value.Contains("{Up}") && IsNumber(value.Substring(value.IndexOf("}") + 1)))
            {
                value = "{Up " + value.Substring(value.IndexOf("}") + 1) + "}";
            }
            if (value.Contains("{Down}") && IsNumber(value.Substring(value.IndexOf("}") + 1)))
            {
                value = "{Down " + value.Substring(value.IndexOf("}") + 1) + "}";
            }
            if (value.Contains("{Left}") && IsNumber(value.Substring(value.IndexOf("}") + 1)))
            {
                value = "{Left " + value.Substring(value.IndexOf("}") + 1) + "}";
            }
            if (value.Contains("{Right}") && IsNumber(value.Substring(value.IndexOf("}") + 1)))
            {
                value = "{Right " + value.Substring(value.IndexOf("}") + 1) + "}";
            }
            value = value.Replace("Percent", "{%}");
            value = value.Replace("Plus", "{+}");
            if (e.Result.Grammar.Name.Contains("Phonetic Alphabet"))
            {
                value = Get1stLetterFromPhoneticAlphabet(e, value);
            }

            //this.WriteLine($"*****Sending Keys: {value.Replace("{", "").Replace("}", "").ToString()}*******");

            List<string> keys = new List<string>(new string[] { value });
            SendKeysCustom(null, null, keys, currentProcess.ProcessName);
        }
        private static string Get1stLetterFromPhoneticAlphabet(SpeechRecognizedEventArgs e, string value)
        {
            if (e.Result.Grammar.Name == "Phonetic Alphabet")
            {
                value = "";
                foreach (var word in e.Result.Words)
                {
                    if (word.Text != "Space")
                    {
                        value = value + word.Text.Substring(0, 1);
                    }
                    else
                    {
                        value += " ";
                    }
                }
            }
            else if (e.Result.Grammar.Name == "Phonetic Alphabet Lower")
            {
                value = "";
                foreach (var word in e.Result.Words)
                {
                    if (word.Text != "Lower")
                    {
                        if (word.Text != "Space")
                        {
                            value = value + word.Text.ToLower().Substring(0, 1);
                        }
                        else
                        {
                            value += " ";
                        }
                    }
                }
            }
            else if (e.Result.Grammar.Name == "Phonetic Alphabet Mixed")
            {
                value = ""; var counter = 0;
                foreach (var word in e.Result.Words)
                {
                    if (word.Text != "Mixed")
                    {
                        counter++;
                        if (word.Text != "Space")
                        {
                            if (counter == 1)
                            {
                                value = value + word.Text.ToUpper().Substring(0, 1);
                            }
                            else
                            {
                                value = value + word.Text.ToLower().Substring(0, 1);
                            }
                        }
                        else
                        {
                            value += " ";
                        }
                    }
                }
            }
            else if (e.Result.Grammar.Name == "Replace Letters")
            {
                value = "";
                var upper = false;
                foreach (var word in e.Result.Words)
                {
                    if (word.Text == "Upper")
                    {
                        upper = true;
                    }
                    else if (word.Text == "Replace" || word.Text == "With" || word.Text == "this")
                    {
                        //Do nothing
                    }
                    else
                    {
                        if (upper == true)
                        {
                            value = value + word.Text.ToUpper().Substring(0, 1);
                            upper = false;
                        }
                        else
                        {
                            value = value + word.Text.ToLower().Substring(0, 1);
                        }
                    }
                }
            }
            return value;
        }
        public Boolean IsNumber(String value)
        {
            return value.All(Char.IsDigit);
        }
        private void PerformMouseMoveCommand(SpeechRecognizedEventArgs e)
        {
            Win32.POINT p = new Win32.POINT();
            Win32.GetCursorPos(out p);
            var direction = e.Result.Words[1].Text;
            var counter = int.Parse(e.Result.Words[2].Text);
            if (direction == "Down")
            {
                p.y = p.y + counter;
            }
            else if (direction == "Up")
            {
                p.y = p.y - counter;
            }
            else if (direction == "Left")
            {
                p.x = p.x - counter;
            }
            else if (direction == "Right")
            {
                p.x = p.x + counter;
            }
            Win32.SetCursorPos(p.x, p.y);
        }
        private void PerformMouseClickCommand(SpeechRecognizedEventArgs e)
        {
            Win32.POINT p = new Win32.POINT();
            Win32.GetCursorPos(out p);
            if (e.Result.Text == "Left Click" || e.Result.Text == "Mouse Click" || e.Result.Text == "Click")
            {
                Win32.mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.x, (uint)p.y, 0, 0);
            }
            else if (e.Result.Text == "Double Click")
            {
                Win32.mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.x, (uint)p.y, 0, 0);
                Win32.mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.x, (uint)p.y, 0, 0);
            }
            else
            {
                Win32.mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, (uint)p.x, (uint)p.y, 0, 0);
            }
        }
        private void PerformSymbolsCommand(SpeechRecognizedEventArgs e)
        {
            List<string> keys = new List<string>();
            var text = e.Result.Text.ToLower();
            if (text.Contains("square brackets"))
            {
                keys.Add("[]");
            }
            else if (text.Contains("curly brackets"))
            {
                keys.Add("{{}");
                keys.Add("{}}");
            }
            else if (text.Contains("brackets"))
            {
                keys.Add("(");
                keys.Add(")");
            }
            else if (text.Contains("apostrophes"))
            {
                keys.Add("''");
            }
            else if (text.Contains("quotes"))
            {
                keys.Add("\"");
                keys.Add("\"");
            }
            else if (text.Contains("at signs"))
            {
                keys.Add("@@");
            }
            else if (text.Contains("chevrons"))
            {
                keys.Add("<>");
            }
            else if (text.Contains("equals"))
            {
                keys.Add("==");
            }
            else if (text.Contains("not equal"))
            {
                keys.Add("!=");
            }
            else if (text.Contains("plus"))
            {
                keys.Add("++");
            }
            else if (text.Contains("dollar"))
            {
                keys.Add("$$");
            }
            else if (text.Contains("hash"))
            {
                keys.Add("##");
            }
            else if (text.Contains("question marks"))
            {
                keys.Add("??");
            }
            else if (text.Contains("pipes"))
            {
                keys.Add("||");
            }
            else if (text.Contains("ampersand"))
            { keys.Add("&&"); }
            SendKeysCustom(null, null, keys, currentProcess.ProcessName);
            if (text.EndsWith("in"))
            {
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
            }
            else if (text.EndsWith("space"))
            {
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
            }
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
                     form.TextBoxResults = $" RECOGNISING: Text= {e.Result.Text}";
                };

                recognizer.Recognized += (s, e) =>
                {
                    SpeechRecognitionResult result = e.Result;
                    //form.LabelStatus = ($"Reason: {result.Reason.ToString()}");
                    if (result.Reason == ResultReason.RecognizedSpeech)
                    {
                        form.TextBoxResults=($"Final result: Text: {result.Text}");
                        string resultRaw = result.Text;
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
                        if (form.ConvertWordsToSymbols)
                        {
                            resultRaw = SpeechCommandsHelper.ConvertWordsToSymbols(resultRaw, inputSimulator,form,result);
                        }
                        resultRaw = SpeechCommandsHelper.PerformCodeFunctions(resultRaw);

                        
                        resultMain = resultRaw;


                        if (resultMain.Trim().ToLower().StartsWith("command") || form.TreatAsCommand)
                        {
                            resultMain = SpeechCommandsHelper.RemovePunctuation(resultMain);

                            resultMain = resultMain.Replace("Command", "").Trim();
                            resultMain = SpeechCommandsHelper.ConvertToTitle(resultMain);
                            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LWIN);
                            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                            //inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
                            form.TextBoxResults = resultMain;
                            inputSimulator.Keyboard.Sleep(400);
                            var resultEmulated = speechRecognizer.EmulateRecognize(resultMain);
                            //Thread.Sleep(300);
                            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LWIN);
                            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                            //inputSimulator.Keyboard.Sleep(200);
                            //inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);

                        }
                        else
                        {
                            if (resultMain.Contains("{") || resultMain.Contains("^") || resultMain.Contains("%") || resultMain.Contains("+"))
                            {
                                form.TextBoxResults = $"Send Keys Value: {resultMain}";
                                //SendKeys.Send(resultMain);
                            }
                            else
                            {
                                form.TextBoxResults = $"Text Entry Value: {resultMain}";
                                if (!resultMain.ToLower().Contains("stop continuous"))
                                {
                                    inputSimulator.Keyboard.TextEntry($"{resultMain}");
                                }
                            }
                        }
                    }
                };

                recognizer.Canceled += (s, e) =>
                {
                    //form.LabelStatus = $"\n    Canceled. Reason: {e.Reason.ToString()}, CanceledReason: {e.Reason}";
                };

                recognizer.SessionStarted += (s, e) =>
                {
                    form.LabelStatus = "Continuous Dictation STARTED";
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
                form.TextBoxResults = resultMain;
                form.LabelStatus = "Continuous Stopped";
                await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);

                return resultMain;
            }
        }

    }
}
