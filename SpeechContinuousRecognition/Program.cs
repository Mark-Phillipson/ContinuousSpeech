using System.Diagnostics;

namespace SpeechContinuousRecognition;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        var processes = Process.GetProcesses();
        if (processes.Count(p => p.ProcessName == "SpeechContinuousRecognition")>1)
        {
            //MessageBox.Show("Instance already running");
            return;
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new ContinuousSpeech());


    }
}