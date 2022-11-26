using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WindowsInput;

namespace SpeechContinuousRecognition
{
    public class CustomMethods
    {
        IInputSimulator _inputSimulator = new InputSimulator();
        public CustomMethods()
        {

        }
        public string EnterTimestamp(string? dictation = null)
        {
            var value = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            _inputSimulator.Keyboard.TextEntry(value);
            return $"{{Entered timestamp}}";
        }

        public string RestartDragon( string ? dictation= null )
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
    }
}
