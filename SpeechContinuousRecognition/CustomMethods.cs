using SpeechContinuousRecognition.OpenAI;
using SpeechContinuousRecognition.Repositories;
using System.Configuration;
using System.Diagnostics;

using WindowsInput;

namespace SpeechContinuousRecognition
{
    public class CustomMethods
    {
        IInputSimulator _inputSimulator = new InputSimulator();
        WindowsVoiceCommand windowsVoiceCommand = new WindowsVoiceCommand();
        public CustomMethods()
        {

        }
        public async Task<string> GetOpenAIResponse(string? dictation = null)
        {
            var promptRecord = windowsVoiceCommand.GetDefaultOrSpecificPrompt();
            string  systemPrompt = "When I type some Natural language text convert it into C# code or any other programming language that I specify.  Include comments When appropriate to explain what the code is doing.\r\n";
            if (promptRecord != null  && promptRecord.PromptText.Length>0)
            {
                systemPrompt = promptRecord.PromptText;
            }
            if (dictation == null)
            {
                return $"{{Called Open AI Cancelled as no dictation supplied}}";
            }
            string apiKey = ConfigurationManager.AppSettings.Get("OpenAI") ?? "NotFound!";
            string prompt = $"{systemPrompt}{Environment.NewLine}{dictation}";
            string result = await OpenAIHelper.GetResultAsync(apiKey, prompt);
            //string result = OpenAIHelper.GetResultAzureAsync(apiKey,dictation, systemPrompt).Result;
            result = result.Replace("\r\n", Environment.NewLine);
            result = result.Replace("\n", Environment.NewLine);

            if (result != null && result.Length > 0)
            {
                //_inputSimulator.Keyboard.TextEntry(prompt);
                //_inputSimulator.Keyboard.TextEntry(Environment.NewLine);
                _inputSimulator.Keyboard.TextEntry(result);
                Clipboard.SetText(result);
            }
            return $"{{Called Open AI {dictation}}}";

        }
        public string EnterTimestamp(string? dictation = null)
        {
            var value = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            _inputSimulator.Keyboard.TextEntry(value);
            return $"{{Entered timestamp}}";
        }
        public string RestartDragon(string? dictation = null)
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
            processName = "draAgedgonlogger";
            KillAllProcesses(processName);

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
                    SendKeys.SendWait("^+k");
                }
            }
            catch (Exception exception)
            {
                // 	System.Windows.Forms.MessageBox.Show(exception.Message);
                AutoClosingMessageBox.Show(exception.Message, "Error trying to start a process", 3000);
            }
            return $"{{Dragon Restarted}}";
        }
        public string ShutdownWindows(string dictation)
        {
            if (dictation.ToLower() == "shut down windows" || dictation.ToLower() == "shutdown windows")
            {
                Process.Start("shutdown", "/s /t 10");
            }
            else if (dictation.ToLower() == "restart windows")
            {
                Process.Start("shutdown", "/r /t 10");
            }
            return $"{{Windows has left the building}}";
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
        List<string> phoneticAlphabet = new List<string>() { "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Paper", "Papa", "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "X-ray", "Yankee", "Zulu" };

        public string ProcessCapitalLetters(string dictation)
        {
            return ProcessLetters(dictation, "Upper");
        }
        public string ProcessMixedLetters(string dictation)
        {
            return ProcessLetters(dictation, "Mixed");
        }
        public string ProcessLowerLetters(string dictation)
        {
            return ProcessLetters(dictation, "Lower");
        }
        private string ProcessLetters(string dictation, string caseType)
        {
            foreach (var item in phoneticAlphabet)
            {
                if (dictation.ToLower().Contains(item.ToLower()))
                {
                    dictation = dictation.ToLower().Replace(item.ToLower(), item.Substring(0, 1));
                }
            }
            dictation = dictation.Replace(" ", "");
            if (caseType == "Upper")
            {
                dictation = dictation.ToUpper();
            }
            else if (caseType == "Mixed")
            {
                dictation = dictation.Substring(0, 1).ToUpper() +
                        dictation.ToLower().Substring(1);
            }
            else
            {
                dictation = dictation.ToLower();
            }
            _inputSimulator.Keyboard.TextEntry(dictation);
            return $"{{Letters Processed: {dictation}}}";

        }


    }
}
