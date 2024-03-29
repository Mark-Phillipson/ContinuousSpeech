﻿
using DataAccessLibrary.Models;

using Humanizer;

using Microsoft.CognitiveServices.Speech;

using SpeechContinuousRecognition.Models;
using SpeechContinuousRecognition.OpenAI;
using SpeechContinuousRecognition.Repositories;

using System.Configuration;
using System.Diagnostics;
using System.Reflection;

using WindowsInput;
using WindowsInput.Native;

namespace SpeechContinuousRecognition
{
	public partial class SpeechCommandsHelper
	{

		WindowsVoiceCommand windowsVoiceCommand = new WindowsVoiceCommand();
		private const int MOUSEEVENTF_LEFTDOWN = 0x02;
		private const int MOUSEEVENTF_LEFTUP = 0x04;
		private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
		private const int MOUSEEVENTF_RIGHTUP = 0x10;

		const string VOICE_LAUNCHER = @"C:\Users\MPhil\source\repos\SpeechRecognitionHelpers\VoiceLauncher\bin\Release\VoiceLauncher.exe";

		public string ConvertTextToNumber(string lineNumber)
		{
			var possible2 = new List<string> { "two", "to", "too" };
			var possible4 = new List<string> { "four", "for" };
			lineNumber = lineNumber.ToLower();
			if (lineNumber == "one")
			{
				return "1";
			}
			else if (possible2.Contains(lineNumber))
			{
				return "2";
			}
			else if (lineNumber == "three")
			{
				return "3";
			}
			else if (possible4.Contains(lineNumber))
			{
				return "4";
			}
			else if (lineNumber == "five")
			{
				return "5";
			}
			else if (lineNumber == "six")
			{
				return "6";
			}
			else if (lineNumber == "seven")
			{
				return "7";
			}
			else if (lineNumber == "eight")
			{
				return "8";
			}
			else if (lineNumber == "nine")
			{
				return "9";
			}
			else if (lineNumber == "ten")
			{
				return "10";
			}
			return lineNumber;
		}
		public static string RemovePunctuation(string rawResult)
		{
			rawResult = rawResult.Replace(",", "");
			rawResult = rawResult.Replace(";", "");
			rawResult = rawResult.Replace(":", "");
			rawResult = rawResult.Replace("?", "");
			if (rawResult.EndsWith(".")) { rawResult = rawResult.Substring(startIndex: 0, length: rawResult.Length - 1); }
			return rawResult;
		}
		public static string PerformCodeFunctions(string rawResult)
		{
			string[] stringSeparators = new string[] { " " };
			List<string> words = rawResult.Split(stringSeparators, StringSplitOptions.None).ToList();
			if (rawResult.ToLower().StartsWith("camel"))
			{
				rawResult = rawResult.ToLower().Replace("camelize", "");
				rawResult = rawResult.ToLower().Replace("camel", "");
				rawResult = rawResult.Camelize().Trim();
			}
			else if (rawResult.ToLower().StartsWith("variable ") || rawResult.ToLower().StartsWith("pascal "))
			{
				rawResult = rawResult.ToLower().Replace("variable ", "");
				rawResult = rawResult.ToLower().Replace("pascal ", "");
				rawResult = rawResult.Pascalize();
			}
			else if (rawResult.ToLower().StartsWith("period ") || rawResult.ToLower().StartsWith("period."))
			{
				words.RemoveAt(0);
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
			else if (rawResult.ToLower().StartsWith("underscore") || rawResult.StartsWith("_"))
			{
				rawResult = rawResult.Underscore();
			}
			else if (rawResult.ToLower().StartsWith("-ate ") || rawResult.ToLower().StartsWith("hyphenate "))
			{
				rawResult = rawResult.Replace("-ate ", "");
				rawResult = rawResult.ToLower().Replace("hyphenate ", "");
				rawResult = rawResult.Replace(" ", "-");
				//rawResult = rawResult.Hyphenate();
				// rawResult = rawResult.Dasherize();
			}
			else if (rawResult.ToLower().StartsWith("title") || rawResult.ToLower().StartsWith("title."))
			{
				rawResult = rawResult.ToLower().Replace("title ", "");
				rawResult = rawResult.ToLower().Replace("title.", "");
				rawResult = rawResult.Titleize();
			}
			else if (rawResult.ToLower().StartsWith("upper"))
			{
				words.RemoveAt(0);
				string value = "";
				foreach (var word in words)
				{
					value = value + word.ToUpper() + " ";
				}
				rawResult = value;
			}
			else if (rawResult.ToLower().StartsWith("all caps"))
			{
				words.RemoveAt(0);
				words.RemoveAt(0);

				string value = "";
				foreach (var word in words)
				{
					value = value + word.ToUpper() + " ";
				}
				rawResult = value;
			}
			else if (rawResult.ToLower().StartsWith("lower gap"))
			{
				words.RemoveAt(0);
				words.RemoveAt(0);
				string value = "";
				foreach (var word in words)
				{
					value = value + word.ToLower() + " ";
				}
				rawResult = value;
			}
			else if (rawResult.ToLower().StartsWith("lower") || rawResult.ToLower().StartsWith("trim"))
			{
				words.RemoveAt(0);
				string value = "";
				foreach (var word in words)
				{
					value = value + word.ToLower();
				}
				rawResult = value;
			}
			else if (rawResult.ToLower().StartsWith("kebab"))
			{
				rawResult = rawResult.ToLower().Replace("kebabize ", "");
				rawResult = rawResult.ToLower().Replace("kebab ", "");
				rawResult = rawResult.Kebaberize();
			}

			return rawResult;
		}
		public static string ConvertToTitle(string value)
		{
			value = value.Humanize(LetterCasing.Title);
			return value;
		}
		string PerformMouseHorizontalCommand(string resultRaw, List<string> words)
		{
			Win32.POINT p = new Win32.POINT();
			p.x = 100;
			p.y = 100;
			var horizontalCoordinate = words[1].Replace(".","");
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
			else if (horizontalCoordinate == "Mike" || horizontalCoordinate == "Mic")
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
			if (words[0] == "Taskbar")
			{
				p.y = 1030;
			}
			else if (words[0] == "Ribbon" || words[0] == "Menu")
			{
				p.y = 85;
			}
			if (words.Count == 3)
			{
				if (words[2] == "1")
				{
					p.x = p.x + 5;
				}
				else if (words[2] == "2")
				{
					p.x = p.x + (2 * 5);
				}
				else if (words[2] == "3")
				{
					p.x = p.x + (3 * 5);
				}
				else if (words[2] == "4")
				{
					p.x = p.x + (4 * 5);
				}
				else if (words[2] == "5")
				{
					p.x = p.x + (5 * 5);
				}
				else if (words[2] == "6")
				{
					p.x = p.x + (6 * 5);
				}
				else if (words[2] == "7")
				{
					p.x = p.x + (7 * 5);
				}
				else if (words[2] == "8")
				{
					p.x = p.x + (8 * 5);
				}
				else if (words[2] == "9")
				{
					p.x = p.x + (9 * 5);
				}
			}
			Win32.SetCursorPos(p.x, p.y);
			//SpeechUI.SendTextFeedback(e.Result, $" {e.Result.Text} H{p.x} V{p.y}", true);
			Win32.mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.x, (uint)p.y, 0, 0);
			return $"{{Mouse Horizontal {words[0]} {horizontalCoordinate}}}";
		}
		public async Task<string> IdentifyAndRunCommandAsync(string resultRaw, ContinuousSpeech form, SpeechRecognitionResult result, Process? currentProcess)
		{
			string? applicationName = null;
			if (currentProcess != null)
			{
				ApplicationDetail? applicationDetail = windowsVoiceCommand.GetApplicationDetails()?.Where(v => v.ProcessName.ToLower() == currentProcess.ProcessName.ToLower()).FirstOrDefault();
				if (applicationDetail != null)
				{
					applicationName = applicationDetail.ApplicationTitle;
				}
			}
			//fix common recognition problems/idiosyncrasies
			string saveTemporaryResults = resultRaw;
			var idiosyncrasies = windowsVoiceCommand.GetIdiosyncrasies();
			if (idiosyncrasies != null)
			{
				foreach (Idiosyncrasy item in idiosyncrasies)
				{
					bool performIdiosyncrasy = false;
					if (item.StringFormattingMethod == "Just Replace")
					{
						performIdiosyncrasy = true;
					}
					else if (item.StringFormattingMethod == "Equals")
					{
						if (resultRaw.ToLower() == item.FindString.ToLower())
						{
							performIdiosyncrasy = true;
						}
					}
					else if (item.StringFormattingMethod == "Starts With")
					{
						if (resultRaw.ToLower().StartsWith(item.FindString.ToLower()))
						{
							performIdiosyncrasy = true;
						}
					}
					else if (item.StringFormattingMethod == "Ends With")
					{
						if (resultRaw.ToLower().EndsWith(item.FindString.ToLower()))
						{
							performIdiosyncrasy = true;
						}
					}
					else if (item.StringFormattingMethod == "Contains")
					{
						if (resultRaw.ToLower().Contains(item.FindString.ToLower()))
						{
							performIdiosyncrasy = true;
						}
					}
					if (performIdiosyncrasy) { resultRaw = resultRaw.ToLower().Replace(item.FindString.ToLower(), item.ReplaceWith); }
				}
			}
			int numberOfAsterisks = resultRaw.Count(c => c == '*');
			if (numberOfAsterisks > 2)
			{
				return "{Enough of the swearing!}";
			}
			if (resultRaw.ToLower().StartsWith("words "))
			{
				resultRaw = resultRaw.ToLower().Replace(".", "");
			}
			if (resultRaw.ToLower().StartsWith("sql "))
			{
				resultRaw = resultRaw.ToLower().Replace("sql ", "sequel ");
			}

			if (resultRaw.ToLower().StartsWith("a ")) // Useful when Azure Cognitive Services decides to add a Even though it was not said
			{
				resultRaw = resultRaw.Substring(2);
			}
			if (resultRaw.ToLower().StartsWith("the "))//Same As for a above
			{
				resultRaw = resultRaw.Substring(4);
			}
			//if (resultRaw.ToLower().Contains(" the ")) //This is too annoying as it removes every the Even when it's said
			//{
			//  resultRaw = resultRaw.Replace(" the ", " ");
			//}
			if (resultRaw.ToLower() == "don't")
			{
				resultRaw = resultRaw.ToLower().Replace("don't", "dot");
			}
			if (resultRaw.ToLower().StartsWith("fresh ") && resultRaw.ToLower() != "fresh line" && resultRaw.ToLower() != "fresh line above" && resultRaw.ToLower() != "fresh paragraph")
			{
				resultRaw = resultRaw.ToLower().Replace("fresh", "press");
			}
			if (resultRaw.ToLower() == "tap")
			{
				resultRaw = "tab";
			}
			if (resultRaw.ToLower().StartsWith("tabular "))
			{
				resultRaw = resultRaw.ToLower().Replace("tabular ", "tab ");
			}
			if (resultRaw.ToLower() == "type semi colon")
			{
				resultRaw = ";";
				return resultRaw;
			}
			if (resultRaw.ToLower().StartsWith("und") && !resultRaw.Contains(" "))
			{
				resultRaw = "undo";
			}
			if (resultRaw.ToLower() == "moved down" || resultRaw.ToLower() == "moved")
			{
				resultRaw = "Move Down";
			}
			if (resultRaw.ToLower() == "moved up")
			{
				resultRaw = "Move Up";
			}
			if (resultRaw.ToLower().StartsWith("dictation. cap"))
			{
				resultRaw = resultRaw.Replace("dictation", "");
			}
			if (resultRaw.ToLower().StartsWith("cup "))
			{
				resultRaw = resultRaw.Replace("cup ", "Cap ");
			}
			if (resultRaw.ToLower().StartsWith("pressure "))
			{
				resultRaw = resultRaw.ToLower().Replace("pressure ", "press ");
			}
			if (resultRaw.ToLower() == saveTemporaryResults.ToLower())
			{
				resultRaw = saveTemporaryResults;
			}
			if (resultRaw.ToLower().StartsWith("command"))
			{
				resultRaw = await GetOpenAICommandFromPhrase(resultRaw);
			}
			IInputSimulator inputSimulator = new InputSimulator();
			(bool finish, string? commandName, string? errorMessage) = PerformDatabaseCommands(result, resultRaw, inputSimulator, form, applicationName);
			if (finish)
			{
				return "{Database Command Performed:" + $" {commandName} {errorMessage}" + "}";
			}
			List<string> phoneticAlphabet = new List<string>() { "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Paper", "Papa", "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "X-ray", "Yankee", "Zulu" };

			if (resultRaw.ToLower().StartsWith("press"))
			{
				resultRaw = resultRaw.Replace("Press ", "").Trim();
				resultRaw = resultRaw.Replace("press ", "").Trim();
			}
			if (phoneticAlphabet.Any(f => f.ToLower().Equals(resultRaw.ToLower())))
			{
				if (form.OutputUppercase)
				{
					inputSimulator.Keyboard.TextEntry(resultRaw.ToUpper().Substring(0, 1));
				}
				else
				{
					inputSimulator.Keyboard.TextEntry(resultRaw.ToLower().Substring(0, 1));
				}
				return $"{{Press {resultRaw}}}";
			}
			if (resultRaw.Trim().ToLower().StartsWith("numeral"))
			{
				resultRaw = resultRaw.ToLower().Replace("numeral", "");
				var number = GetNumber(resultRaw.Trim());
				if (number >= 0)
				{
					inputSimulator.Keyboard.TextEntry(number.ToString());
					return $"{{Numeral {resultRaw}}}";

				}
			}

			string[] stringSeparators = new string[] { " " };
			List<string> words = resultRaw.Split(stringSeparators, StringSplitOptions.None).ToList();

			resultRaw = ToggleSetting(resultRaw, form);
			if (resultRaw == "")
			{
				return "";
			}
			if (applicationName == "Visual Studio")
			{
				string? visualStudioCommandResult = PerformVisualStudioCommand(resultRaw, words, inputSimulator);
				if (visualStudioCommandResult != null)
				{
					return visualStudioCommandResult;
				}
			}
			resultRaw = PerformSymbolCommand(resultRaw, inputSimulator, words);
			if (resultRaw.StartsWith("{") && resultRaw.EndsWith("}"))
			{
				return resultRaw;
			}
			string firstWord = words[0].Replace(".", "");
			if (firstWord == "Taskbar" || firstWord == "Ribbon" || firstWord == "Menu")
			{
				words[0] = firstWord;
				resultRaw = PerformMouseHorizontalCommand(resultRaw, words);
			}
			resultRaw = PerformSelectOperations(resultRaw, inputSimulator, words);
			if (resultRaw.StartsWith("{") && resultRaw.EndsWith("}"))
			{
				return resultRaw;
			}

			if (resultRaw.ToLower().StartsWith("control ") || resultRaw.ToLower().StartsWith("alt ") || resultRaw.ToLower().StartsWith("shift "))
			{
				string possibleKeys = resultRaw;
				if (resultRaw.ToLower().Contains("control "))
				{
					inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
					possibleKeys = resultRaw.ToLower().Replace("control ", "");
				}
				if (possibleKeys.ToLower().Contains("alt "))
				{
					inputSimulator.Keyboard.KeyDown(VirtualKeyCode.MENU);
					possibleKeys = possibleKeys.Replace("alt ", "");
				}
				if (possibleKeys.ToLower().Contains("shift "))
				{
					inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
					possibleKeys = possibleKeys.Replace("shift ", "");
				}
				if (possibleKeys.ToLower() == "a" || possibleKeys.ToLower() == "alpha")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_A);
				}
				else if (possibleKeys.ToLower() == "b" || possibleKeys.ToLower() == "bravo")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_B);
				}
				else if (possibleKeys.ToLower() == "c" || possibleKeys.ToLower() == "charlie")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_C);
				}
				else if (possibleKeys.ToLower() == "d" || possibleKeys.ToLower() == "delta")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_D);
				}
				else if (possibleKeys.ToLower() == "e" || possibleKeys.ToLower() == "echo")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_E);
				}
				else if (possibleKeys.ToLower() == "f" || possibleKeys.ToLower() == "foxtrot")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_F);
				}
				else if (possibleKeys.ToLower() == "g" || possibleKeys.ToLower() == "golf")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_G);
				}
				else if (possibleKeys.ToLower() == "h" || possibleKeys.ToLower() == "hotel")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_H);
				}
				else if (possibleKeys.ToLower() == "i" || possibleKeys.ToLower() == "india")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_I);
				}
				else if (possibleKeys.ToLower() == "j" || possibleKeys.ToLower() == "juliet")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_J);
				}
				else if (possibleKeys.ToLower() == "k" || possibleKeys.ToLower() == "kilo")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_K);
				}
				else if (possibleKeys.ToLower() == "l" || possibleKeys.ToLower() == "lima")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_L);
				}
				else if (possibleKeys.ToLower() == "m" || possibleKeys.ToLower() == "mike")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_M);
				}
				else if (possibleKeys.ToLower() == "n" || possibleKeys.ToLower() == "november")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_N);
				}
				else if (possibleKeys.ToLower() == "o" || possibleKeys.ToLower() == "oscar")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_O);
				}
				else if (possibleKeys.ToLower() == "p" || possibleKeys.ToLower() == "papa")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_P);
				}
				else if (possibleKeys.ToLower() == "q" || possibleKeys.ToLower() == "qubec")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_Q);
				}
				else if (possibleKeys.ToLower() == "r" || possibleKeys.ToLower() == "romeo")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_R);
				}
				else if (possibleKeys.ToLower() == "s" || possibleKeys.ToLower() == "sierra")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_S);
				}
				else if (possibleKeys.ToLower() == "t" || possibleKeys.ToLower() == "tango")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_T);
				}
				else if (possibleKeys.ToLower() == "u" || possibleKeys.ToLower() == "uniform")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_U);
				}
				else if (possibleKeys.ToLower() == "v" || possibleKeys.ToLower() == "victor")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
				}
				else if (possibleKeys.ToLower() == "w" || possibleKeys.ToLower() == "whiskey")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_W);
				}
				else if (possibleKeys.ToLower() == "x" || possibleKeys.ToLower() == "x-ray")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_X);
				}
				else if (possibleKeys.ToLower() == "y" || possibleKeys.ToLower() == "yankee")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_Y);
				}
				else if (possibleKeys.ToLower() == "z" || possibleKeys.ToLower() == "zulu")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_Z);
				}
				else if (possibleKeys.ToLower() == "1" || possibleKeys.ToLower() == "one")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_1);
				}
				else if (possibleKeys.ToLower() == "to" || possibleKeys.ToLower() == "two" || possibleKeys.ToLower() == "2")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_2);
				}
				else if (possibleKeys.ToLower() == "3" || possibleKeys.ToLower() == "three")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_3);
				}
				else if (possibleKeys.ToLower() == "4" || possibleKeys.ToLower() == "four" || possibleKeys.ToLower() == "for")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_4);
				}
				else if (possibleKeys.ToLower() == "5" || possibleKeys.ToLower() == "five")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_5);
				}
				else if (possibleKeys.ToLower() == "6" || possibleKeys.ToLower() == "six")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_6);
				}
				else if (possibleKeys.ToLower() == "7" || possibleKeys.ToLower() == "seven")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_7);
				}
				else if (possibleKeys.ToLower() == "8" || possibleKeys.ToLower() == "eight")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_8);
				}
				else if (possibleKeys.ToLower() == "9" || possibleKeys.ToLower() == "nine")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_9);
				}
				else if (possibleKeys.ToLower() == "0" || possibleKeys.ToLower() == "zero")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_0);
				}
				else if (possibleKeys.ToLower() == "left")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
				}
				else if (possibleKeys.ToLower() == "right" || possibleKeys.ToLower() == "write")
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
				}
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.MENU);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				return $"{{{resultRaw}}}";
			}

			if (resultRaw.ToLower() == "moved to the bottom" || resultRaw.ToLower() == "move to the bottom")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
			}
			if (resultRaw.ToLower() == "moved to the top" || resultRaw.ToLower() == "move to the top")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
			}
			if (resultRaw.ToLower() == "shift home")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				return "{+{Home}}";
			}
			if (resultRaw.ToLower() == "copy" || resultRaw.ToLower() == "copy that")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_C);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
				return "{^c}";
			}
			if (resultRaw.ToLower() == "escape")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
				return "{Escape}";
			}
			if (resultRaw.ToLower() == "shift end")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				return "{+{End}}";
			}
			if (resultRaw.ToLower() == "shift up")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.UP);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				return "{+{Up}}";
			}
			if (resultRaw.ToLower() == "shift down")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DOWN);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				return "{+{Down}}";
			}
			if (resultRaw.ToLower() == "shift left")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				return "{+{Left}}";
			}
			if (resultRaw.ToLower() == "shift right")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				return "{+{Right}}";
			}
			if (resultRaw.ToLower() == "select line")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				return "{Select Line}";
			}
			if (resultRaw.ToLower() == "clear line")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DELETE);
				return "{Clear Line}";
			}
			if (resultRaw.ToLower() == "fresh line" || resultRaw.ToLower() == "freshline")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
				return "{Moving to fresh line below}";
			}
			if (resultRaw.ToLower() == "fresh line above" || resultRaw.ToLower() == "freshline above")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.UP);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
				return "{Moving to fresh line above}";
			}
			if (resultRaw.ToLower().StartsWith("cap "))
			{
				var resultAfterCapping = "";
				var counter = 0;
				string previousWord = string.Empty;
				foreach (var word in words)
				{
					if (word.ToLower() != "cap" && previousWord.ToLower() == "cap")
					{
						string formattedWord = word.Substring(0, 1).ToUpper() + word.Substring(1);
						resultAfterCapping = $"{resultAfterCapping} {formattedWord}".Trim();
					}
					else if (word.ToLower() != "cap")
					{
						resultAfterCapping = $"{resultAfterCapping} {word}".Trim();
					}
					counter++;
					previousWord = word;
				}
				inputSimulator.Keyboard.TextEntry(resultAfterCapping);
				return $"{{Cap {resultAfterCapping}}}";
			}
			if (resultRaw.ToLower().StartsWith("dictation "))
			{
				var searchTerm = "";
				var counter = 0;
				foreach (var word in words)
				{
					if (counter >= 1)
					{
						searchTerm = $"{searchTerm}{word} ";
					}
					counter++;
				}
				searchTerm = searchTerm.Trim();
				if (!string.IsNullOrWhiteSpace(searchTerm))
				{
					inputSimulator.Keyboard.TextEntry(searchTerm);
					return $"{{Dictation {searchTerm}}}";
				}
			}
			if (resultRaw.ToLower() == "create custom intellisense")
			{
				inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
				var itemToAdd = Clipboard.GetText();
				string arguments = $@"""/ Add New"" ""/{itemToAdd.Trim()}""";
				Process.Start(VOICE_LAUNCHER, arguments);

				return "{Create Custom IntelliSense}";
			}
			if (resultRaw.ToLower().StartsWith("words"))
			{
				var searchTerm = "";
				var counter = 0;
				foreach (var word in words)
				{
					string wordToUse = word;
					if (wordToUse == "too")
					{
						wordToUse = "to";
					}
					if (counter >= 1)
					{
						searchTerm = $"{searchTerm} {wordToUse}";
					}
					counter++;
				}
				var customIntelliSense = windowsVoiceCommand.GetWord(searchTerm.Trim());
				if (customIntelliSense == null)
				{
					return $"{{Error getting word! ({searchTerm})}}";
				}
				string sendkeysValue = customIntelliSense.SendKeys_Value.Replace("(", "{(}");
				sendkeysValue = sendkeysValue.Replace(")", "{)}");

				try
				{
					SendKeys.SendWait(sendkeysValue);
				}
				catch (Exception exception)
				{
					return $"{{Error! ({searchTerm}) {sendkeysValue} {exception.Message}}}";

				}
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
						catch (Exception exception)
						{
							return $"{{{exception.Message}}}";
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
				return $"{{words {resultRaw}}}";

			}
			if (resultRaw.ToLower().Contains("enter") && resultRaw.ToLower().Contains("random numbers"))
			{
				int numberOfWords = 0;
				try
				{
					numberOfWords = GetNumber(words[1]);
				}
				catch (Exception exception)
				{
					return $"{{{exception.Message}}}";
				}
				for (int i = 0; i < numberOfWords - 1; i++)
				{
					Random rnd = new Random();
					int num = rnd.Next(3000);
					inputSimulator.Keyboard.TextEntry(num.ToString());
					inputSimulator.Keyboard.Sleep(100);
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
				}
				return $"{{{resultRaw}}}";
			}

			resultRaw = resultRaw.ToLower().Replace("equals", "=");
			resultRaw = resultRaw.ToLower().Replace("equal", "=");
			resultRaw = resultRaw.ToLower().Replace("not equal to", "!=");
			resultRaw = resultRaw.ToLower().Replace("equal to", "==");
			resultRaw = resultRaw.ToLower().Replace("greater than", ">");
			resultRaw = resultRaw.ToLower().Replace("less than", "<");
			resultRaw = resultRaw.ToLower().Replace(" hyphen ", "-");
			resultRaw = resultRaw.ToLower().Replace("hyphen ", "-");
			if (!resultRaw.ToLower().Contains("hyhens"))
			{
				resultRaw = resultRaw.ToLower().Replace("hyphen", "-");
			}
			resultRaw = resultRaw.ToLower().Replace(" under score ", "_");
			resultRaw = resultRaw.ToLower().Replace("under score ", "_");
			resultRaw = resultRaw.ToLower().Replace(" underscore ", "_");
			resultRaw = resultRaw.ToLower().Replace("underscore ", "_");
			resultRaw = resultRaw.ToLower().Replace("at sign", "@");
			resultRaw = resultRaw.ToLower().Replace("apostrophe", "'");
			resultRaw = resultRaw.ToLower().Replace("pound ", "£");
			resultRaw = resultRaw.ToLower().Replace("dollar ", "$");
			resultRaw = resultRaw.ToLower().Replace("percent ", "%");
			resultRaw = resultRaw.ToLower().Replace("ampersand ", "&");
			resultRaw = resultRaw.ToLower().Replace("asterisks ", "*");
			resultRaw = resultRaw.ToLower().Replace("backtick ", "`");
			resultRaw = resultRaw.ToLower().Replace("pipe ", "|");
			resultRaw = resultRaw.ToLower().Replace("back slash", "\\");
			resultRaw = resultRaw.ToLower().Replace(" dot ", ".");
			if (!resultRaw.Contains("dot net") && !resultRaw.Contains("dotnet"))
			{
				resultRaw = resultRaw.ToLower().Replace("dot ", ".");
			}
			if (resultRaw.ToLower() == saveTemporaryResults.ToLower())
			{
				resultRaw = saveTemporaryResults;
			}

			return resultRaw;
		}

		private static string PerformSelectOperations(string resultRaw, IInputSimulator inputSimulator, List<string> words)
		{
			if (resultRaw.Trim().ToLower() == "left select")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
				return "{Left Select}";
			}
			else if (resultRaw.ToLower().Contains("left select"))
			{
				int number = 0;
				number = GetNumber(words[2]);
				for (int i = 0; i < number; i++)
				{
					inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
					inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
					inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
					inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
				}
				return $"{{Left Select {number.ToString()} }}";

			}
			else if (resultRaw.ToLower().Contains("left") && resultRaw.ToLower().Contains("select"))
			{
				int number = 0;
				number = GetNumber(words[1]);
				for (int i = 0; i < number; i++)
				{
					inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
					inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
					inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
					inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
				}
				return $"{{Left {number.ToString()} Select}}";

			}
			if (resultRaw.Trim().ToLower() == "right select" || resultRaw.Trim().ToLower() == "write select")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
				return "{right Select}";
			}
			else if (resultRaw.ToLower().Contains("right select") || resultRaw.ToLower().Contains("write select"))
			{
				int number = 0;
				number = GetNumber(words[2]);
				for (int i = 0; i < number; i++)
				{
					inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
					inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
					inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
					inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
				}
				return $"{{Right Select {number.ToString()} }}";
			}
			else if (resultRaw.ToLower().Contains("right") && resultRaw.ToLower().Contains("select"))
			{
				int number = 0;
				number = GetNumber(words[1]);
				for (int i = 0; i < number; i++)
				{
					inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
					inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
					inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
					inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
				}
				return $"{{Left {number.ToString()} Select}}";

			}
			else if (resultRaw.ToLower().Contains("select matching") && resultRaw.ToLower() != "select matching")
			{
				int number = 0;
				number = GetNumber(words[2]);
				for (int i = 0; i < number; i++)
				{
					SendKeys.SendWait("%+.");
				}
				return $"{{Select Matching {number.ToString()} }}";
			}
			return resultRaw;
		}

		private static string PerformSymbolCommand(string resultRaw, IInputSimulator inputSimulator, List<string> words)
		{
			if (resultRaw.ToLower().StartsWith("type"))
			{
				resultRaw = resultRaw.ToLower().Replace("type", "");
			}
			if (resultRaw.Trim().ToLower() == "dot")
			{
				inputSimulator.Keyboard.TextEntry(".");
				return "{dot}";
			}
			if (resultRaw.Trim().ToLower() == "hash")
			{
				inputSimulator.Keyboard.TextEntry("#");
				return "{dot}";
			}
			if (resultRaw.Trim().ToLower() == "brackets in")
			{
				inputSimulator.Keyboard.TextEntry("()");
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
				return "{brackets in}";
			}
			if (resultRaw.Trim().ToLower() == "brackets out")
			{
				inputSimulator.Keyboard.TextEntry("()");
				return "{brackets out}";
			}
			if (resultRaw.Trim().ToLower() == "curly brackets in")
			{
				inputSimulator.Keyboard.TextEntry("{}");
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
				return "{Curly brackets in}";
			}
			if (resultRaw.Trim().ToLower() == "curly brackets out")
			{
				inputSimulator.Keyboard.TextEntry("{}");
				return "{Curly brackets out}";
			}
			if (resultRaw.Trim().ToLower() == "apostrophes in")
			{
				inputSimulator.Keyboard.TextEntry("''");
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
				return "{Apostrophes In}";
			}
			if (resultRaw.Trim().ToLower() == "apostrophes out")
			{
				inputSimulator.Keyboard.TextEntry("''");
				return "{Apostrophes out}";
			}
			if (resultRaw.Trim().ToLower() == "chevrons in")
			{
				inputSimulator.Keyboard.TextEntry("<>");
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
				return "{Chevrons In}";
			}
			if (resultRaw.Trim().ToLower() == "chevrons out")
			{
				inputSimulator.Keyboard.TextEntry("''");
				return "{Chevrons out}";
			}
			if (resultRaw.Trim().ToLower() == "control a" || resultRaw.Trim().ToLower() == "select all")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_A);

				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);

				return "{Select All}";
			}
			if (resultRaw.Trim().ToLower() == "quotes in")
			{
				inputSimulator.Keyboard.TextEntry("\"\"");
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
				return "{Quotes In}";
			}
			if (resultRaw.Trim().ToLower() == "quotes out")
			{
				inputSimulator.Keyboard.TextEntry("\"\"");
				return "{Quotes Out}";
			}
			if (resultRaw.ToLower() == "close bracket")
			{
				inputSimulator.Keyboard.TextEntry(")");
				return "{Close Bracket}";
			}
			if (resultRaw.ToLower() == "open bracket")
			{
				inputSimulator.Keyboard.TextEntry("(");
				return "{Open Bracket}";
			}
			if (resultRaw.ToLower() == "close curly bracket")
			{
				inputSimulator.Keyboard.TextEntry("}");
				return "{Close Curly Bracket}";
			}
			if (resultRaw.ToLower() == "open curly bracket")
			{
				inputSimulator.Keyboard.TextEntry("{");
				return "{Open Curley Bracket}";
			}
			if (resultRaw.ToLower() == "semi colon")
			{
				inputSimulator.Keyboard.TextEntry(";");
				return "{Semi Colon}";
			}
			if (resultRaw.ToLower() == "words semi colon")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
				inputSimulator.Keyboard.TextEntry(";");
				return "{Semi Colon}";
			}
			if (resultRaw.ToLower() == "enter")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
				return "{Enter}";
			}
			if (resultRaw.ToLower() == "return")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
				return "{Return}";
			}
			if (resultRaw.ToLower() == "tab")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
				return "{Tab}";
			}
			if (resultRaw.ToLower() == "new paragraph")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
				return "{Return}{Return}";
			}
			if (resultRaw.ToLower() == "new line")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
				return "{Return}";
			}
			if (resultRaw.ToLower() == "dollar")
			{
				inputSimulator.Keyboard.TextEntry("$");
				return "{$}";
			}
			if (resultRaw.ToLower() == "exclamation" || resultRaw.ToLower() == "exclamation mark" || resultRaw.ToLower() == "bang")
			{
				inputSimulator.Keyboard.TextEntry("!");
				return "{!}";
			}
			if (resultRaw.ToLower() == "equals" || resultRaw.ToLower() == "equal")
			{
				inputSimulator.Keyboard.TextEntry("=");
				return "{=}";
			}
			if (resultRaw.ToLower() == "equal to" || resultRaw.ToLower() == "equals to")
			{
				inputSimulator.Keyboard.TextEntry("==");
				return "{==}";
			}
			if (resultRaw.ToLower() == "not equal to" || resultRaw.ToLower() == "not equals to")
			{
				inputSimulator.Keyboard.TextEntry("!=");
				return "{!=}";
			}
			if (resultRaw.ToLower() == "greater than")
			{
				inputSimulator.Keyboard.TextEntry(">");
				return "{>}";
			}
			if (resultRaw.ToLower() == "less than")
			{
				inputSimulator.Keyboard.TextEntry("<");
				return "{<}";
			}
			if (resultRaw.ToLower() == "home")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
				return "{Home}";
			}
			if (resultRaw.ToLower() == "shift home")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				return "{Home}";
			}
			if (resultRaw.ToLower() == "end")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
				return "{End}";
			}
			if (resultRaw.ToLower() == "space")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
				return "{Space}";
			}
			if (resultRaw.ToLower() == "backspace")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
				return "{Backspace}";
			}
			if (resultRaw.ToLower().Contains("backspace"))
			{
				PerformMultipleCommand(resultRaw, inputSimulator, VirtualKeyCode.BACK, words);
				return "{Backspace}";
			}
			if (resultRaw.ToLower().Contains(" space "))
			{
				resultRaw = resultRaw.Replace(" space ", " ");
			}
			else if (resultRaw.ToLower().Contains("space "))
			{
				resultRaw = resultRaw.Replace("space ", " ");
			}
			else if (resultRaw.ToLower().Contains(" space"))
			{
				resultRaw = resultRaw.Replace(" space", " ");
			}
			if (resultRaw.ToLower() == "equals out")
			{
				inputSimulator.Keyboard.TextEntry("==");
				return "{==}";
			}
			if (resultRaw.ToLower().EndsWith("items") && resultRaw.ToLower() != "items" && words.Count == 2)
			{
				var repeatCount = GetNumber(words[0]);
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				for (int i = 0; i < repeatCount; i++)
				{
					inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DOWN);
				}
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				if (repeatCount > 0)
				{
					return "{Select # Item}";
				}
			}
			if (resultRaw.ToLower().EndsWith("items") && words.Count == 3)
			{
				var repeatCount = GetNumber(words[1]);
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
				for (int i = 0; i < repeatCount; i++)
				{
					if (words[0].ToLower() == "left")
					{
						inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
					}
					else
					{
						inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
					}
				}
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
				if (repeatCount > 0)
				{
					return "{Select # Item}";
				}

			}
			if (resultRaw.ToLower() == "undo")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_Z);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
				return "{Ctrl+z}";
			}
			if (resultRaw.ToLower() == "redo")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_Y);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
				return "{Ctrl+y}";
			}
			if (resultRaw.ToLower() == "control home")
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);

				return "{Ctrl+Home}";
			}
			if (resultRaw.ToLower() == "page down")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.NEXT);
				return "{PgDn}";
			}
			if (resultRaw.ToLower() == "page up")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.PRIOR);
				return "{PgUp}";
			}
			if (resultRaw.ToLower() == "delete")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DELETE);
				return "{Del}";
			}
			if (resultRaw.ToLower().StartsWith("delete"))
			{
				PerformMultipleCommand(resultRaw, inputSimulator, VirtualKeyCode.DELETE, words);
				return "{Del}";
			}
			if (resultRaw.ToLower() == "up")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.UP);
				return "{Up}";
			}
			if (resultRaw.ToLower().StartsWith("up "))
			{
				PerformMultipleCommand(resultRaw, inputSimulator, VirtualKeyCode.UP, words);
				return "{Up #}";
			}
			if (resultRaw.ToLower() == "down")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DOWN);
				return "{Down}";
			}
			if (resultRaw.ToLower().StartsWith("down"))
			{
				PerformMultipleCommand(resultRaw, inputSimulator, VirtualKeyCode.DOWN, words);
				return "{Down #}";
			}
			if (resultRaw.ToLower() == "right")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
				return "{Right}";
			}
			if (resultRaw.ToLower().StartsWith("right") && !resultRaw.ToLower().Contains("select"))
			{
				PerformMultipleCommand(resultRaw, inputSimulator, VirtualKeyCode.RIGHT, words);
				return "{Right #}";
			}
			if (resultRaw.ToLower() == "left")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
				return "{Left}";
			}
			if (resultRaw.ToLower().StartsWith("left") && !resultRaw.ToLower().Contains("select"))
			{
				PerformMultipleCommand(resultRaw, inputSimulator, VirtualKeyCode.LEFT, words);
				return "{Left #}";
			}
			if (resultRaw.ToLower() == "comma")
			{
				inputSimulator.Keyboard.TextEntry(",");
				return "{Comma}";
			}

			return resultRaw;
		}

		private static string ToggleSetting(string resultRaw, ContinuousSpeech form)
		{
			if (resultRaw.Trim().ToLower() == "toggle uppercase" || resultRaw.Trim().ToLower() == "toggle upper case")
			{
				form.OutputUppercase = !form.OutputUppercase;
				return "";
			}
			if (resultRaw.Trim().ToLower() == "toggle lowercase" || resultRaw.Trim().ToLower() == "toggle lower case")
			{
				form.OutputLowercase = !form.OutputLowercase;
				return "";
			}
			if (resultRaw.Trim().ToLower() == "toggle treat as command" || resultRaw.Trim().ToLower() == "toggle treat command")
			{
				form.TreatAsCommand = !form.TreatAsCommand;
				return "";
			}
			if (resultRaw.Trim().ToLower() == "toggle treat as command first" || resultRaw.Trim().ToLower() == "toggle command first")
			{
				form.TreatAsCommandFirst = !form.TreatAsCommandFirst;
				return "";
			}
			if (resultRaw.Trim().ToLower() == "toggle remove punctuation" || resultRaw.Trim().ToLower() == "toggle punctuation")
			{
				form.RemovePunctuation = !form.RemovePunctuation;
				return "";
			}
			return resultRaw;
		}

		private string? PerformVisualStudioCommand(string resultRaw, List<string> words, IInputSimulator inputSimulator)
		{
			if (resultRaw.ToLower().StartsWith("go to line"))
			{
				int number = 0;
				number = GetNumber(words[3]);
				inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_G);
				inputSimulator.Keyboard.TextEntry(number.ToString());
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
				return $"{{Go to Line {number.ToString()}}}";
			}
			if (resultRaw.ToLower().StartsWith("move the line")) { resultRaw = resultRaw.Replace("move the line", "move line"); }
			if (resultRaw.ToLower().StartsWith("move line up") || resultRaw.ToLower().StartsWith("move line down"))
			{
				int number = 0;
				number = GetNumber(words[3]);
				var direction = words[2];
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
				if (direction.ToLower() == "down")
				{
					for (int i = 0; i < number - 1; i++)
					{
						inputSimulator.Keyboard.KeyPress(VirtualKeyCode.F1);
						inputSimulator.Keyboard.KeyPress(VirtualKeyCode.F4);
					}
				}
				else // Up
				{
					for (int i = 0; i < number - 1; i++)
					{
						inputSimulator.Keyboard.KeyPress(VirtualKeyCode.F1);
						inputSimulator.Keyboard.KeyPress(VirtualKeyCode.F2);
					}
				}
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
				return $"{{{resultRaw}}}";
			}
			if (resultRaw.Trim().ToLower() == "if")
			{
				inputSimulator.Keyboard.TextEntry("if");
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
				return "{if}";
			}
			if (resultRaw.Trim().ToLower() == "if statement razor")
			{
				inputSimulator.Keyboard.TextEntry("@if");
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
				return "{@if}";
			}
			if (resultRaw.Trim().ToLower() == "new block curly brackets")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
				inputSimulator.Keyboard.TextEntry("{}");
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
				return "{new block curly brackets}";
			}
			if (resultRaw.Trim().ToLower() == "previous method")
			{
				SendKeys.Flush();
				SendKeys.SendWait("^+%m");
				return "{previous method}";
			}
			if (resultRaw.Trim().ToLower() == "following method" || resultRaw.Trim().ToLower() == "next method")
			{
				SendKeys.Flush();
				SendKeys.SendWait("%^+m");
				return "{next method}";
			}
			if (resultRaw.Trim().ToLower() == "search code")
			{
				inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_F);
			}
			if (resultRaw.ToLower().StartsWith("navigate to"))
			{
				var searchTerm = "";
				var counter = 0;
				foreach (var word in words)
				{
					if (counter >= 2)
					{
						searchTerm = $"{searchTerm} {word}";
					}
					counter++;
				}
				SendKeys.SendWait("^,");
				Thread.Sleep(100);
				SendKeys.SendWait(searchTerm);
				return $"{{{resultRaw}}}";

			}
			return null;
		}

		public (bool commandRun, string? commandName, string? errorMessage) PerformDatabaseCommands(SpeechRecognitionResult result, string resultRaw, IInputSimulator inputSimulator, ContinuousSpeech form, string? applicationName)
		{
			bool commandRun = false;
			string? commandName = null;
			string? dictation = null;
			WindowsSpeechVoiceCommand? command = null;
			command = windowsVoiceCommand.GetCommand(resultRaw, applicationName);
			if (command == null)
			{
				var specialCommands = windowsVoiceCommand.GetSpecialCommands(applicationName, "<dictation>");
				foreach (var dictationCommand in specialCommands)
				{
					if (dictationCommand.SpokenForms != null)
					{
						foreach (var item in dictationCommand.SpokenForms)
						{
							string spokenForm = item.SpokenFormText.Replace("<dictation>", "").Trim();
							if (resultRaw.ToLower().StartsWith(spokenForm.ToLower().Trim()))
							{
								command = dictationCommand;
								dictation = resultRaw.ToLower().Replace(spokenForm.ToLower(), "").Trim();
								break;
							}
							if (command != null)
							{
								break;
							}
						}
					}
				}
			}
			if (command == null)
			{
				string? enterCommand = windowsVoiceCommand.GetEnterCommand(resultRaw);
				if (enterCommand != null && enterCommand.Length > 0)
				{
					inputSimulator.Keyboard.TextEntry(enterCommand);
					commandRun = true;
					commandName = $"Enter **********";
					return (commandRun, commandName, null);
				}
			}
			if (command != null)
			{
				List<CustomWindowsSpeechCommand>? actions = windowsVoiceCommand.GetChildActions(command.Id);
				if (actions == null) { return (commandRun, "Nothing", " No action record found "); }
				foreach (var action in actions)
				{
					if (dictation != null)
					{
						dictation = FormatDictation(dictation, action.HowToFormatDictation);
					}
					if (action.WaitTime > 0)
					{
						Thread.Sleep(action.WaitTime);
					}
					if (action.MethodToCall != null && action.MethodToCall.Length > 0)
					{
						object[]? objects = new object[1];
						objects[0] = dictation ?? "";
						CustomMethods customMethods = new CustomMethods();
						Type thisType = customMethods.GetType();
						MethodInfo? theMethod = thisType.GetMethod(action.MethodToCall);
						if (theMethod != null)
						{
							string? methodResult = theMethod.Invoke(customMethods, objects) as string;
						}
					}
					if (action.KeyDownValue != VirtualKeyCode.NONAME)
					{
						inputSimulator.Keyboard.KeyDown(action.KeyDownValue);
					}
					if (!string.IsNullOrWhiteSpace(action.TextToEnter))
					{
						string textEntry = action.TextToEnter.Replace("<dictation>", dictation);
						if (action.TextToEnter.Contains("<clipboard>"))
						{
							string clipboard = Clipboard.GetText();
							textEntry = action.TextToEnter.Replace("<clipboard>", clipboard);
						}

						if (textEntry != null && textEntry.Length > 0)
						{
							inputSimulator.Keyboard.TextEntry(textEntry);
						}
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
						ModifierProcessing(action, true, inputSimulator);
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
						ModifierProcessing(action, false, inputSimulator);
						inputSimulator.Mouse.LeftButtonUp();

					}
					else if (action.MouseCommand == "RightButtonUp")
					{
						inputSimulator.Mouse.RightButtonUp();
					}
					else if (action.MouseCommand == "MiddleButtonClick")
					{
						//inputSimulator.Mouse.MiddleButtonClick();
					}
					else if (action.MouseCommand == "MiddleButtonDoubleClick")
					{
						//inputSimulator.Mouse.MiddleButtonDoubleClick();
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
					if (!string.IsNullOrWhiteSpace(action.ProcessStart) && !string.IsNullOrWhiteSpace(action.CommandLineArguments))
					{
						try
						{
							Process.Start(action.ProcessStart.Replace("<dictation>", dictation), action.CommandLineArguments.Replace("<dictation>", dictation));
						}
						catch (Exception exception)
						{
							commandRun = true;
							return (commandRun, commandName, $"{{{exception.Message}}}");
						}
					}
					else if (!string.IsNullOrWhiteSpace(action.ProcessStart))
					{
						if (action.ProcessStart.StartsWith("http"))
						{
							var psi = new System.Diagnostics.ProcessStartInfo();
							psi.UseShellExecute = true;
							psi.FileName = action.ProcessStart;
							System.Diagnostics.Process.Start(psi);
						}
						else
						{
							try
							{
								Process.Start(action.ProcessStart.Replace("<dictation>", dictation));
							}
							catch (Exception exception)
							{
								commandRun = false;
								return (commandRun, commandName, $"{{{exception.Message}}}");
							}
						}
						commandRun = true;
						return (commandRun, commandName, "");
					}
					if (!string.IsNullOrWhiteSpace(action.SendKeysValue))
					{
						try
						{
							action.SendKeysValue = action.SendKeysValue.Replace("<dictation>", dictation);
							SendKeys.SendWait(action.SendKeysValue);
						}
						catch (Exception exception)
						{
							commandName = $"Exception has occurred: ({exception.Message})";
							commandRun = true;
							return (commandRun, commandName, null);
						}
					}
					if (command != null)
					{
						form.LastRunCommandId = command.Id;
					}
					commandRun = true;
					var spokenForm = command?.SpokenForms?.FirstOrDefault();
					if (spokenForm != null)
					{
						commandName = spokenForm.SpokenFormText;
					}
				}
			}
			string[] stringSeparators = new string[] { " " };
			List<string> words = result.Text.Split(stringSeparators, StringSplitOptions.None).ToList();
			if (result.Text.ToLower().StartsWith("add tag")||result.Text.ToLower().StartsWith("ad tag"))
			{
				PerformHtmlTagsInsertion(result, inputSimulator, resultRaw);
				commandRun = true;
				commandName = "Add Tag";
			}
			if (result.Text.ToLower().StartsWith("find ") && applicationName == "Visual Studio")
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
				inputSimulator.Keyboard.Sleep(100);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SHIFT, VirtualKeyCode.RIGHT);
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_F);
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
				//SendKeys.SendWait("^f");
				inputSimulator.Keyboard.Sleep(200);
				var searchTerm = "";
				var counter = 0;

				foreach (var word in words)
				{
					if (counter >= 2)
					{
						searchTerm = $"{searchTerm} {word.Replace(".", "")}";
					}
					counter++;
				}
				if (!string.IsNullOrWhiteSpace(searchTerm))
				{
					inputSimulator.Keyboard.TextEntry(searchTerm.Trim());
				}
				inputSimulator.Keyboard.Sleep(100);
				if (words[1].ToLower() == "previous")
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
				commandRun = true;
				commandName = $"{result.Text} {searchTerm}";
			}
			return (commandRun, commandName, null);

		}

		private void ModifierProcessing(CustomWindowsSpeechCommand action, bool keyDown, IInputSimulator inputSimulator)
		{
			if (action.ControlKey && keyDown)
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
			}
			if (action.ShiftKey && keyDown)
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
			}
			if (action.AlternateKey && keyDown)
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.MENU);
			}
			if (action.WindowsKey && keyDown)
			{
				inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LWIN);
			}
			if (action.ControlKey && !keyDown)
			{
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
			}
			if (action.ShiftKey && !keyDown)
			{
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
			}
			if (action.AlternateKey && !keyDown)
			{
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.MENU);
			}
			if (action.WindowsKey && !keyDown)
			{
				inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LWIN);
			}
		}

		private string FormatDictation(string dictation, string howToFormatDictation)
		{
			if (howToFormatDictation == "Do Nothing")
			{
				return dictation;
			}
			string[] stringSeparators = new string[] { " " };

			string result = "";
			List<string> words = dictation.Split(stringSeparators, StringSplitOptions.None).ToList();
			if (howToFormatDictation == "Camel")
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
					result = value;
				}
			}
			else if (howToFormatDictation == "Variable")
			{
				string value = "";
				foreach (var word in words)
				{
					if (word.Length > 0)
					{
						value = value + word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
					}
				}
				result = value;
			}
			else if (howToFormatDictation == "dot notation")
			{
				string value = "";
				foreach (var word in words)
				{
					if (word.Length > 0)
					{
						value = value + word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + ".";
					}
				}
				result = value.Substring(0, value.Length - 1);
			}
			else if (howToFormatDictation == "Title")
			{
				string value = "";
				foreach (var word in words)
				{
					if (word.Length > 0)
					{
						value = value + word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
					}
				}
				result = value;
			}
			else if (howToFormatDictation == "Upper")
			{
				string value = "";
				foreach (var word in words)
				{
					value = value + word.ToUpper() + " ";
				}
				result = value;
			}
			else if (howToFormatDictation == "Lower")
			{
				string value = "";
				foreach (var word in words)
				{
					value = value + word.ToLower() + " ";
				}
				result = value;
			}
			return result;
		}

		private static void PerformMultipleCommand(string resultRaw, IInputSimulator inputSimulator, VirtualKeyCode virtualKeyCode, List<string> words)
		{
			int number = 0;
			number = GetNumber(words[1]);
			for (int i = 0; i < number; i++)
			{
				inputSimulator.Keyboard.KeyPress(virtualKeyCode);
			}
		}

		public static int GetNumber(string word)
		{
			int number;
			bool allCharactersInStringAreDigits = word.All(char.IsDigit);
			if (allCharactersInStringAreDigits)
			{
				number = int.Parse(word);
			}
			else
			{
				number = ConvertWordToNumber(word);
			}

			return number;
		}

		private static int ConvertWordToNumber(string word)
		{
			switch (word.ToLower())
			{
				case "one":
					return 1;
				case "two": return 2;
				case "to": return 2;
				case "three": return 3;
				case "four": return 4;
				case "for": return 4;
				case "five": return 5;
				case "six": return 6;
				case "seven": return 7;
				case "eight": return 8;
				case "nine": return 9;
				case "ten": return 10;
				default: return 0;
			}
		}
		private void PerformHtmlTagsInsertion(SpeechRecognitionResult e, IInputSimulator inputSimulator, string resultRaw)
		{
			string[] stringSeparators = new string[] { " " };
			List<string> words = e.Text.Split(stringSeparators, StringSplitOptions.None).ToList();

			var tag = "";
			var counter = 0;
			foreach (var word in words)
			{
				if (counter >= 2)
				{
					tag = $"{tag} {word}";
				}
				counter++;
			}
			tag = SpeechCommandsHelper.RemovePunctuation(tag);
			var result = windowsVoiceCommand.GetHtmlTag(tag.Trim());
			if (result == null)
			{
				return;
			}
			string? tagReturned = result.ListValue?.ToLower();
			string textToType = ""; int moveLeft = 0;
			if (tagReturned == "input" || tagReturned == "br" || tagReturned == "hr")
			{
				textToType = $"<{tagReturned} />";
				moveLeft = 3;
			}
			else if (tagReturned == "img")
			{
				textToType = $"<{tagReturned} src='' />";
				moveLeft = 5;
			}
			else
			{
				textToType = $"<{tagReturned}>";
				moveLeft = 1;// + tagReturned!.Length;
			}
			inputSimulator.Keyboard.TextEntry(textToType);
			for (int i = 1; i < moveLeft; i++)
			{
				inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
				inputSimulator.Keyboard.Sleep(100);
			}
		}

		public async Task<string> GetOpenAICommandFromPhrase(string resultRaw)
		{
			// https://platform.openai.com/docs/guides/fine-tuning
			// Needs fine tuning and cannot join Azure Open AI as I'm not a Big company Enter date today
			// Could look into doing via Python/ Open AI API            
			// https://openai.com/blog/openai-api/


			var promptRecord = windowsVoiceCommand.GetDefaultOrSpecificPrompt(13);
			string systemPrompt = "";
			if (promptRecord != null && promptRecord.PromptText.Length > 0)
			{
				systemPrompt = promptRecord.PromptText;
			}
			if (resultRaw.ToLower().StartsWith("command"))
			{
				resultRaw = resultRaw.Replace("Command", "");
			}
			string apiKey = ConfigurationManager.AppSettings.Get("OpenAI") ?? "NotFound!";
			string prompt = $"{systemPrompt}\n\n{resultRaw}";
			string result = string.Empty;
			try
			{
				result = await OpenAIHelper.GetResultAsync(apiKey, prompt);
			}
			catch (Exception exception)
			{
				await Console.Out.WriteLineAsync(exception.Message);

			}
			//string result = OpenAIHelper.GetResultAzureAsync(apiKey,dictation, systemPrompt).Result;
			if (result.Length > 0)
			{
				result = result.Replace("\r\n", "");
				result = result.Replace("\n", "");
				result = result.Replace(".", "");
				result = result.Replace("Output:", "").Trim();
				result = RemovePunctuation(result);
				//  Clipboard.SetText(result);
			}
#if DEBUG
			InputSimulator inputSimulator = new InputSimulator();
			inputSimulator.Keyboard.TextEntry(result);
#endif
			return result;
		}
	}
}
