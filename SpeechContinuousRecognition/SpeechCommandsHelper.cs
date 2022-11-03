
using Microsoft.CognitiveServices.Speech;

using SpeechContinuousRecognition.Models;
using SpeechContinuousRecognition.Repositories;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

using WindowsInput;
using WindowsInput.Native;

namespace SpeechContinuousRecognition
{
    public partial class SpeechCommandsHelper
    {
        WindowsVoiceCommand windowsVoiceCommand = new WindowsVoiceCommand();

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
            rawResult = rawResult.Replace(".", "");
            return rawResult;
        }
        public static string PerformCodeFunctions(string rawResult)
        {
            string[] stringSeparators = new string[] { " " };
            List<string> words = rawResult.Split(stringSeparators, StringSplitOptions.None).ToList();
            if (rawResult.ToLower().StartsWith("camel"))
            {
                words.RemoveAt(0);
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
            else if (rawResult.ToLower().StartsWith("variable"))
            {
                words.RemoveAt(0);
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
            else if (rawResult.ToLower().StartsWith("dot notation"))
            {
                words.RemoveAt(0);
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
            else if (rawResult.ToLower().StartsWith("title"))
            {
                words.RemoveAt(0);
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
            else if (rawResult.ToLower().StartsWith("lower"))
            {
                words.RemoveAt(0);
                string value = "";
                foreach (var word in words)
                {
                    value = value + word.ToLower() + " ";
                }
                rawResult = value;
            }
            //if (!rawResult.ToLower().StartsWith("short") && rawResult.ToLower() != "dictation")
            //{
            //    rawResult = rawResult.Trim();
            //}

            return rawResult;
        }
        public static string ConvertToTitle(string value)
        {
            string[] stringSeparators = new string[] { " " };
            List<string> words = value.Split(stringSeparators, StringSplitOptions.None).ToList();
            value = "";
            foreach (var word in words)
            {
                if (word.Length > 0)
                {
                    value = value + word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                }
            }
            return value;
        }
        public string ConvertWordsToSymbols(string resultRaw, ContinuousSpeech form, SpeechRecognitionResult result)
        {
            IInputSimulator inputSimulator = new InputSimulator();
            (bool finish, string? commandName) = PerformDatabaseCommands(result, resultRaw, inputSimulator, form);
            if (finish)
            {
                return "{Database command Performed" + $" {commandName}" +"}";
            }
            if (resultRaw.Trim().ToLower().StartsWith("press"))
            {
                resultRaw = resultRaw.Replace("Press", "").Trim();
                resultRaw = resultRaw.Replace("press", "").Trim();
            }
            string[] stringSeparators = new string[] { " " };
            List<string> words = resultRaw.Split(stringSeparators, StringSplitOptions.None).ToList();

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
            if (resultRaw.Trim().ToLower() == "toggle convert words to symbols" || resultRaw.Trim().ToLower() == "toggle convert words")
            {
                form.ConvertWordsToSymbols = !form.ConvertWordsToSymbols;
                return "";
            }
            if (resultRaw.Trim().ToLower() == "toggle remove punctuation" || resultRaw.Trim().ToLower() == "toggle punctuation")
            {
                form.RemovePunctuation = !form.RemovePunctuation;
                return "";
            }

            if (resultRaw.Trim().ToLower() == "if")
            {
                inputSimulator.Keyboard.TextEntry("if");
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
                return "{if}";
            }
            if (resultRaw.Trim().ToLower() == "drop marker")
            {
                SendKeys.Flush();
                SendKeys.SendWait("%{Home}");
                return "{drop marker}";
            }
            if (resultRaw.Trim().ToLower() == "collect marker")
            {
                SendKeys.Flush();
                SendKeys.SendWait("%{End}");
                return "{Collect marker}";
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
                SendKeys.SendWait("^+m");
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

            if (resultRaw.Trim().ToLower() == "dot")
            {
                inputSimulator.Keyboard.TextEntry(".");
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
            }
            if (resultRaw.Trim().ToLower() == "quotes out")
            {
                inputSimulator.Keyboard.TextEntry("\"\"");
            }
            if (true)
            {

            }
            if (resultRaw.Trim().ToLower() == "left select")
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                return "{Left Select}";
            }
            if (resultRaw.Trim().ToLower() == "right select")
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                return "{right Select}";
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
            if (resultRaw.ToLower() == "equals out")
            {
                inputSimulator.Keyboard.TextEntry("==");
                return "{==}";
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
            if (resultRaw.ToLower().EndsWith("items") && resultRaw.ToLower() != "items")
            {
                var repeatCount = GetNumber(words[0]);
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                for (int i = 0; i < repeatCount; i++)
                {
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DOWN);
                }
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                return "{Select # Item}";
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
            if (resultRaw.ToLower().StartsWith("up"))
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
            if (resultRaw.ToLower().StartsWith("right"))
            {
                PerformMultipleCommand(resultRaw, inputSimulator, VirtualKeyCode.RIGHT, words);
                return "{Right #}";
            }
            if (resultRaw.ToLower() == "left")
            {
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
                return "{Left}";
            }
            if (resultRaw.ToLower().StartsWith("left"))
            {
                PerformMultipleCommand(resultRaw, inputSimulator, VirtualKeyCode.LEFT, words);
                return "{Left #}";
            }
            if (resultRaw.ToLower() == "shift home")
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                return "+{Home}";
            }
            if (resultRaw.ToLower() == "shift end")
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                return "+{End}";
            }
            if (resultRaw.ToLower() == "shift up")
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.UP);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                return "+{Up}";
            }
            if (resultRaw.ToLower() == "shift down")
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DOWN);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                return "+{Down}";
            }
            if (resultRaw.ToLower() == "shift left")
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                return "+{Left}";
            }
            if (resultRaw.ToLower() == "shift right")
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                return "+{Right}";
            }
            if (resultRaw.ToLower() == "select line")
            {
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                return "{Selecting Line}";
            }
            if (resultRaw.ToLower() == "clear line")
            {
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DELETE);
                return "{Selecting Line}";
            }
            if (resultRaw.ToLower() == "fresh line")
            {
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                return "{Moving to fresh line below}";
            }
            if (resultRaw.ToLower() == "fresh line above")
            {
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.UP);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.END);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                return "{Moving to fresh line above}";
            }




            //resultRaw = resultRaw.ToLower().Replace(" enter ", "{Enter}");
            //resultRaw = resultRaw.ToLower().Replace(" return ", "{Return}");
            //resultRaw = resultRaw.ToLower().Replace(" tab ", "{Tab}");
            //resultRaw = resultRaw.ToLower().Replace(" new line", "{Return}");
            //resultRaw = resultRaw.ToLower().Replace(" new paragraph", "{Return}{Return}");
            //resultRaw = resultRaw.ToLower().Replace(" end ", "{Home}");
            //resultRaw = resultRaw.ToLower().Replace(" undo ", "^z");
            //resultRaw = resultRaw.ToLower().Replace(" end ", "{End}");
            //resultRaw = resultRaw.ToLower().Replace(" space ", "{Space}");

            return resultRaw;
        }

        public (bool commandRun, string? commandName) PerformDatabaseCommands(SpeechRecognitionResult result, string resultRaw, IInputSimulator inputSimulator, ContinuousSpeech form)
        {
            bool commandRun = false;
             string? commandName = null;
            var command = windowsVoiceCommand.GetCommand(resultRaw);
            if (command != null)
            {
                List<CustomWindowsSpeechCommand>? actions = windowsVoiceCommand.GetChildActions(command.Id);
                if (actions == null) { return (commandRun,"Nothing"); }
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
                    if (!string.IsNullOrWhiteSpace(action.ProcessStart))
                    {
                        Process.Start(action.ProcessStart, action?.CommandLineArguments);
                    }
                    if (!string.IsNullOrWhiteSpace(action.SendKeysValue))
                    {
                        SendKeys.SendWait(action.SendKeysValue);
                    }
                    commandRun = true;
                    commandName = command.SpokenCommand;
                }
            }

            if (result.Text.ToLower().StartsWith("add tag"))
            {
                PerformHtmlTagsInsertion(result, inputSimulator,resultRaw);
                commandRun = true;
                commandName = "Add Tag";
            }
            if (result.Text.ToLower().StartsWith("find code") || result.Text.ToLower().StartsWith("control f"))
            {
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.HOME);
                inputSimulator.Keyboard.Sleep(100);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SHIFT, VirtualKeyCode.RIGHT);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_F);
                inputSimulator.Keyboard.Sleep(100);
                var searchTerm = "";
                var counter = 0;
                string[] stringSeparators = new string[] { " " };
                List<string> words = result.Text.Split(stringSeparators, StringSplitOptions.None).ToList();

                foreach (var word in words)
                {
                    if (counter >= 2)
                    {
                        searchTerm = $"{searchTerm} {word}";
                    }
                    counter++;
                }
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    inputSimulator.Keyboard.TextEntry(searchTerm);
                }
                inputSimulator.Keyboard.Sleep(100);
                if (words[1] == "Previous")
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
                commandName = "Find Code";
            }
            return (commandRun,commandName);
            //if (result.Text.ToLower() == "use dragon")
            //{
            //    //ToggleSpeechRecognitionListeningMode(inputSimulator);
            //    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.ADD);
            //}
            //else if (e.Result.Grammar.Name.Contains("Phonetic Alphabet")) // Could be lower, mixed or upper
            //{
            //    ProcessKeyboardCommand(e);
            //}

        }

        private void PerformMultipleCommand(string resultRaw, IInputSimulator inputSimulator, VirtualKeyCode virtualKeyCode, List<string> words)
        {
            int number = 0;
            number = GetNumber(words[1]);
            for (int i = 0; i < number; i++)
            {
                inputSimulator.Keyboard.KeyPress(virtualKeyCode);
            }
        }

        private int GetNumber(string word)
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

        private int ConvertWordToNumber(string word)
        {
            switch (word.ToLower())
            {
                case "one":
                    return 1;
                case "two": return 2;
                case "three": return 3;
                case "four": return 4;
                case "five": return 5;
                case "six": return 6;
                case "seven": return 7;
                case "eight": return 8;
                case "nine": return 9;
                case "ten": return 10;
                default: return 0;
            }
        }
        private void PerformHtmlTagsInsertion(SpeechRecognitionResult e, IInputSimulator inputSimulator,  string resultRaw)
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
            if (result== null )
            {
                return;
            }
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
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
                inputSimulator.Keyboard.Sleep(100);
            }
        }
    }
}
