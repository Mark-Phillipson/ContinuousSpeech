﻿using ControlWSR.Models;
using ControlWSR.Repositories;

using Microsoft.CognitiveServices.Speech;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ControlWSR.Speech
{
    public class SpeechSetup
    {
        WindowsVoiceCommand windowsVoiceCommand = new WindowsVoiceCommand();
        readonly SpeechCommandsHelper SpeechCommandsHelper = new SpeechCommandsHelper();
        public string SetUpMainCommands(System.Speech.Recognition.SpeechRecognizer speechRecogniser, bool UseAzureSpeech)
        {
            speechRecogniser.UnloadAllGrammars();
            // Simple commands will also create a Grammar of the same name
            List<string> simpleCommands = new List<string>()
            {
                 "Shutdown Windows", "Quit Application", "Restart Windows", "Restart Dragon", "Studio","Use Dragon"
            };


            var availableCommands = "";
            foreach (var simpleCommand in simpleCommands)
            {
                availableCommands = $"{availableCommands}\n{simpleCommand}";
                CreateDictationGrammar(speechRecogniser, simpleCommand, simpleCommand);
            }
            var commands = windowsVoiceCommand.GetCommands(); ;
            availableCommands = $"{availableCommands}\n<Direction> <1to30> Click";
            foreach (var command in commands.OrderBy(v => v.SpokenCommand))
            {
                simpleCommands.Add(command.SpokenCommand);
                if (command.ApplicationName == "Global" && command.AutoCreated==false)
                {
                    availableCommands = $"{availableCommands}\n{command.SpokenCommand}";
                }
                if (command.SpokenCommand.EndsWith("<dictation>"))
                {
                    var initialPhrase = command.SpokenCommand.Substring(0, command.SpokenCommand.IndexOf("<dictation>") - 1);
                    CreateDictationGrammar(speechRecogniser, initialPhrase, initialPhrase, true);
                }
                else
                {
                    CreateDictationGrammar(speechRecogniser, command.SpokenCommand, command.SpokenCommand);
                }
            }
            if (Environment.MachineName == "J40L4V3")// These are only really applicable for my machine
            {
                CreateDictationGrammar(speechRecogniser, "Default Box", "Default Box", false);
                availableCommands = $"{availableCommands}\nDefault Box (MSP)";
                CreateDictationGrammar(speechRecogniser, "Dictation Box", "Dictation Box", false);
                availableCommands = $"{availableCommands}\nDictation Box (Speech Productivity)";
                CreateDictationGrammar(speechRecogniser, "Search Union", "Search Union", true);
                availableCommands = $"{availableCommands}\nSearch Union <dictation>";
                CreateDictationGrammar(speechRecogniser, "List Items", "List Items", true);
                availableCommands = $"{availableCommands}\nList Items <dictation>";
                CreateDictationGrammar(speechRecogniser, "Create Custom IntelliSense", "Create Custom IntelliSense");
                availableCommands = $"{availableCommands}\nCreate Custom IntelliSense";
                CreateDictationGrammar(speechRecogniser, "Serenade", "Serenade");
                availableCommands = $"{availableCommands}\nSerenade";
            }
            CreateDictationGrammar(speechRecogniser, "Find Following", "Find Code", true);
            CreateDictationGrammar(speechRecogniser, "Find Previous", "Find Code", true);
            CreateDictationGrammar(speechRecogniser, "Words", "Words Dictation", true);
            availableCommands = $"{availableCommands}\nWords <dictation>";

            CreateDictationGrammar(speechRecogniser, "Dictation", "Dictation", true);
            CreateDictationGrammar(speechRecogniser, "Cap", "Cap", true);
            if (UseAzureSpeech)
            {
                CreateDictationGrammar(speechRecogniser, "Dictation", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Punctuation", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Camel", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Camel Dictation", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Title", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Title Dictation", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Variable", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Variable Dictation", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Upper Dictation", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Upper", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Dot Notation", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Lower Dictation", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Lower", "Short Dictation");
                CreateDictationGrammar(speechRecogniser, "Continuous", "Continuous Dictation");

                availableCommands = $"{availableCommands}\nAzure: Upper Dictation/Title/Camel/Variable/Dictation/Punctuation or Dot Notation (Pause for sound)";
                availableCommands = $"{availableCommands}\nContinuous ";
            }
            CreateDictationGrammar(speechRecogniser, "Select Left", "Selection");
            CreateDictationGrammar(speechRecogniser, "Select Right", "Selection");
            CreateDictationGrammar(speechRecogniser, "Left Select", "Selection");
            CreateDictationGrammar(speechRecogniser, "Right Select", "Selection");
            CreateDictationGrammar(speechRecogniser, "Go To Line", "Go To Line", true);
            CreateDictationGrammar(speechRecogniser, "Line", "Go To Line", true);
            CreateDictationGrammar(speechRecogniser, "Find Following", "Search Code", true);
            CreateDictationGrammar(speechRecogniser, "Find Previous", "Search Code", true);
            availableCommands = $"{availableCommands}\nFind Following/Previous <dictation>";
            BuildPhoneticAlphabetGrammars(speechRecogniser);
            //LoadMoveCommandsGrammar(speechRecogniser);
                                                                                                                          
            SpeechCommandsHelper.CreateRepeatableCommand(speechRecogniser, "Backspace", "Repeat Keys", 30);
            SpeechCommandsHelper.CreateRepeatableCommand(speechRecogniser, "Left", "Repeat Keys", 30);
            SpeechCommandsHelper.CreateRepeatableCommand(speechRecogniser, "Right", "Repeat Keys", 30);
            SpeechCommandsHelper.CreateRepeatableCommand(speechRecogniser, "Down", "Repeat Keys", 30);
            SpeechCommandsHelper.CreateRepeatableCommand(speechRecogniser, "Press Up", "Repeat Keys", 30);
            SpeechCommandsHelper.CreateRepeatableCommand(speechRecogniser, 
                "Tabular", "Repeat Keys", 30);
            SpeechCommandsHelper.CreateRepeatableCommand(speechRecogniser, "Delete", "Repeat Keys", 30);
            SpeechCommandsHelper.CreateRepeatableCommand(speechRecogniser, "Enter", "Repeat Keys", 30);
            SpeechCommandsHelper.CreateRepeatableCommand(speechRecogniser, "Press Page Down", "Repeat Keys", 30);
            SpeechCommandsHelper.CreateRepeatableCommand(speechRecogniser, "Press Page Up", "Repeat Keys", 30);
            SpeechCommandsHelper.CreateRepeatableCommand(speechRecogniser, "Step Over", "Repeat Keys", 30);
            availableCommands = $"{availableCommands}\n<1to30> Items";
            SpeechCommandsHelper.CreateItemCommands(speechRecogniser, "Items", "Select Items", 30);
            SetUpSymbolGrammarCommands(speechRecogniser);
            availableCommands = $"{availableCommands}\nSymbols In/Out/Space";
            SetupAddTagHtmlCommands(speechRecogniser);
            availableCommands = $"{availableCommands}\nAdd Tag <HtmlTag>";
            LoadGrammarMouseCommands(speechRecogniser);
            availableCommands = $"{availableCommands}\nMOUSE COMMANDS";
            availableCommands = $"{availableCommands}\nClick: Say <Click/Double-Click/Right Click/Mouse Click> ";
            CreateMouseMoveAndClickCommandGrammar(speechRecogniser);
            availableCommands = $"{availableCommands}\nPosition: Say <Left/Right> <Alpha-7> <Alpha-Tango>";
            LoadGrammarMouseHorizontalPositionCommands(speechRecogniser);
            availableCommands = $"{availableCommands}\nPosition / Click: Say <Taskbar/Ribbon/Menu> <Alpha-7> [1-9]";
            SetupEnterRandomNumbersCommand(speechRecogniser);
            availableCommands = $"{availableCommands}\nEnter <1to20> Random Numbers";

            return availableCommands;
        }

        private void SetupCommandWithLists(string simpleCommand, System.Speech.Recognition.SpeechRecognizer speechRecognizer)
        {
            string command = simpleCommand;
            GrammarBuilder builder = new GrammarBuilder();
            do
            {
                int startWord = simpleCommand.IndexOf("<");
                int endWord = simpleCommand.IndexOf(">");
                var list = command.Substring(startWord + 1, (endWord - startWord) - 1);
                string wordsBefore = "";
                if (startWord > 0)
                {
                    wordsBefore = command.Substring(0, startWord - 1);
                }
                builder = IncludeChoicesInGrammer(builder, list, wordsBefore);
                command = command.Substring(endWord + 1);
            } while (command.Contains("<") && command.Contains(">"));
            System.Speech.Recognition.Grammar grammar = new System.Speech.Recognition.Grammar(builder) { Name = simpleCommand };
            speechRecognizer.LoadGrammarAsync(grammar);
            //The problem with this approach is the actual command has to be hardcoded and it does not support more than one list.
        }

        public System.Speech.Recognition.SpeechRecognizer StartWindowsSpeechRecognition()
        {
            try
            {
                System.Speech.Recognition.SpeechRecognizer speechRecognizer = new System.Speech.Recognition.SpeechRecognizer();
                return speechRecognizer;
            }
            catch (Exception exception)
            {
                System.Windows.MessageBox.Show($"Error loading Windows Speech Recognition {exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        private void CreateDictationGrammar(System.Speech.Recognition.SpeechRecognizer speechRecognizer, string initialPhrase, string grammarName, bool openEnded = false)
        {
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(new Choices(initialPhrase));
            if (openEnded)
            {
                grammarBuilder.AppendDictation();
            }

            System.Speech.Recognition.Grammar grammar = new System.Speech.Recognition.Grammar((GrammarBuilder)grammarBuilder);
            grammar.Name = grammarName;
            speechRecognizer.LoadGrammarAsync(grammar);
        }
        public string SetupConfirmationCommands(string originalCommand, System.Speech.Recognition.SpeechRecognizer speechRecogniser, AvailableCommandsForm availableCommandsForm)
        {
            speechRecogniser.UnloadAllGrammars();
            CreateDictationGrammar(speechRecogniser, "Yes Please", "Confirmed");
            CreateDictationGrammar(speechRecogniser, "No Thank You", "Denied");
            var availableCommands = $"{originalCommand.ToUpper()}";
            availableCommands = $"{availableCommands}\n\nYes Please";
            availableCommands = $"{availableCommands}\nNo Thank You";
            PerformVoiceCommands.SetForegroundWindow(availableCommandsForm.Handle);
            return availableCommands;
        }
        public void LoadGrammarMouseCommands(System.Speech.Recognition.SpeechRecognizer speechRecognizer)
        {
            List<string> screenCoordinates = new List<string> { "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Qubec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "X-ray", "Yankee", "Zulu", "1", "2", "3", "4", "5", "6", "7", "Zero" };
            Choices choices = new Choices();
            List<string> monitorNames = new List<string> { "Left", "Right", "Click", "Touch" };
            foreach (var item in screenCoordinates)
            {
                foreach (var monitorName in monitorNames)
                {
                    foreach (var item2 in screenCoordinates)
                    {
                        if (item2 == "Uniform")
                        {
                            break;
                        }
                        choices.Add($"{monitorName} {item} {item2}");
                    }
                }
            }
            GrammarBuilder grammarBuilder = new GrammarBuilder(choices);
            System.Speech.Recognition.Grammar grammar = new System.Speech.Recognition.Grammar((GrammarBuilder)grammarBuilder);
            grammar.Name = "Mouse";
            speechRecognizer.LoadGrammarAsync(grammar);
        }
        public void LoadGrammarMouseHorizontalPositionCommands(System.Speech.Recognition.SpeechRecognizer speechRecognizer)
        {
            List<string> screenCoordinates = new List<string> { "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Qubec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "X-ray", "Yankee", "Zulu", "1", "2", "3", "4", "5", "6", "7", "Zero" };
            Choices choices = new Choices();
            List<string> horizontalPositions = new List<string> { "Taskbar", "Ribbon", "Menu" };
            foreach (var screenCoordinate in screenCoordinates)
            {
                foreach (var horizontalPosition in horizontalPositions)
                {
                    choices.Add($"{horizontalPosition} {screenCoordinate}");
                }
            }
            GrammarBuilder grammarBuilder = new GrammarBuilder(choices);
            System.Speech.Recognition.Grammar grammar = new System.Speech.Recognition.Grammar((GrammarBuilder)grammarBuilder);
            grammar.Name = "Horizontal Position Mouse";
            speechRecognizer.LoadGrammarAsync(grammar);
            Choices precision = new Choices(new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
            GrammarBuilder precisionGrammar = new GrammarBuilder();
            precisionGrammar.Append(choices);
            precisionGrammar.Append(precision);
            System.Speech.Recognition.Grammar grammar1 = new System.Speech.Recognition.Grammar((GrammarBuilder)precisionGrammar);
            grammar1.Name = "Horizontal Position Mouse";
            speechRecognizer.LoadGrammarAsync(grammar1);
        }
        public void BuildPhoneticAlphabetGrammars(System.Speech.Recognition.SpeechRecognizer speechRecogniser)
        {
            Choices phoneticAlphabet = new Choices(new string[] { "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Qubec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "X-ray", "Yankee", "Zulu", "Space" });
            GrammarBuilder grammarBuilder2 = new GrammarBuilder();
            grammarBuilder2.Append(phoneticAlphabet);
            grammarBuilder2.Append(phoneticAlphabet);
            GrammarBuilder grammarBuilder3 = new GrammarBuilder();
            grammarBuilder3.Append(phoneticAlphabet);
            grammarBuilder3.Append(phoneticAlphabet);
            grammarBuilder3.Append(phoneticAlphabet);
            GrammarBuilder grammarBuilder4 = new GrammarBuilder();
            grammarBuilder4.Append(phoneticAlphabet);
            grammarBuilder4.Append(phoneticAlphabet);
            grammarBuilder4.Append(phoneticAlphabet);
            grammarBuilder4.Append(phoneticAlphabet);
            GrammarBuilder grammarBuilder5 = new GrammarBuilder();
            grammarBuilder5.Append(phoneticAlphabet);
            grammarBuilder5.Append(phoneticAlphabet);
            grammarBuilder5.Append(phoneticAlphabet);
            grammarBuilder5.Append(phoneticAlphabet);
            grammarBuilder5.Append(phoneticAlphabet);
            GrammarBuilder grammarBuilder6 = new GrammarBuilder();
            grammarBuilder6.Append(phoneticAlphabet);
            grammarBuilder6.Append(phoneticAlphabet);
            grammarBuilder6.Append(phoneticAlphabet);
            grammarBuilder6.Append(phoneticAlphabet);
            grammarBuilder6.Append(phoneticAlphabet);
            grammarBuilder6.Append(phoneticAlphabet);
            GrammarBuilder grammarBuilder7 = new GrammarBuilder();
            grammarBuilder7.Append(phoneticAlphabet);
            grammarBuilder7.Append(phoneticAlphabet);
            grammarBuilder7.Append(phoneticAlphabet);
            grammarBuilder7.Append(phoneticAlphabet);
            grammarBuilder7.Append(phoneticAlphabet);
            grammarBuilder7.Append(phoneticAlphabet);
            grammarBuilder7.Append(phoneticAlphabet);
            Choices phoneticAlphabet2to7 = new Choices(new GrammarBuilder[] { grammarBuilder2, grammarBuilder3, grammarBuilder4, grammarBuilder5, grammarBuilder6, grammarBuilder7 });
            System.Speech.Recognition.Grammar grammarPhoneticAlphabets = new System.Speech.Recognition.Grammar((GrammarBuilder)phoneticAlphabet2to7);
            grammarPhoneticAlphabets.Name = "Phonetic Alphabet";
            speechRecogniser.LoadGrammarAsync(grammarPhoneticAlphabets);
            Choices choicesLower = new Choices("Lower");
            BuildPhoneticAlphabetGrammars(speechRecogniser, phoneticAlphabet, choicesLower, "Phonetic Alphabet Lower");
            Choices choicesMixed = new Choices("Mixed");
            BuildPhoneticAlphabetGrammars(speechRecogniser, phoneticAlphabet, choicesMixed, "Phonetic Alphabet Mixed");
        }
        private static void BuildPhoneticAlphabetGrammars(System.Speech.Recognition.SpeechRecognizer speechRecognizer, Choices phoneticAlphabet, Choices choices, string grammarName)
        {
            GrammarBuilder grammarBuilderLower2 = new GrammarBuilder();
            grammarBuilderLower2.Append(choices);
            grammarBuilderLower2.Append(phoneticAlphabet);
            grammarBuilderLower2.Append(phoneticAlphabet);
            GrammarBuilder grammarBuilderLower3 = new GrammarBuilder();
            grammarBuilderLower3.Append(choices);
            grammarBuilderLower3.Append(phoneticAlphabet);
            grammarBuilderLower3.Append(phoneticAlphabet);
            grammarBuilderLower3.Append(phoneticAlphabet);
            GrammarBuilder grammarBuilderLower4 = new GrammarBuilder();
            grammarBuilderLower4.Append(choices);
            grammarBuilderLower4.Append(phoneticAlphabet);
            grammarBuilderLower4.Append(phoneticAlphabet);
            grammarBuilderLower4.Append(phoneticAlphabet);
            grammarBuilderLower4.Append(phoneticAlphabet);
            GrammarBuilder grammarBuilderLower5 = new GrammarBuilder();
            grammarBuilderLower5.Append(choices);
            grammarBuilderLower5.Append(phoneticAlphabet);
            grammarBuilderLower5.Append(phoneticAlphabet);
            grammarBuilderLower5.Append(phoneticAlphabet);
            grammarBuilderLower5.Append(phoneticAlphabet);
            grammarBuilderLower5.Append(phoneticAlphabet);
            GrammarBuilder grammarBuilderLower6 = new GrammarBuilder();
            grammarBuilderLower6.Append(choices);
            grammarBuilderLower6.Append(phoneticAlphabet);
            grammarBuilderLower6.Append(phoneticAlphabet);
            grammarBuilderLower6.Append(phoneticAlphabet);
            grammarBuilderLower6.Append(phoneticAlphabet);
            grammarBuilderLower6.Append(phoneticAlphabet);
            grammarBuilderLower6.Append(phoneticAlphabet);
            GrammarBuilder grammarBuilderLower7 = new GrammarBuilder();
            grammarBuilderLower7.Append(choices);
            grammarBuilderLower7.Append(phoneticAlphabet);
            grammarBuilderLower7.Append(phoneticAlphabet);
            grammarBuilderLower7.Append(phoneticAlphabet);
            grammarBuilderLower7.Append(phoneticAlphabet);
            grammarBuilderLower7.Append(phoneticAlphabet);
            grammarBuilderLower7.Append(phoneticAlphabet);
            grammarBuilderLower7.Append(phoneticAlphabet);
            Choices phoneticAlphabetLower2to7 = new Choices(new GrammarBuilder[] { grammarBuilderLower2, grammarBuilderLower3, grammarBuilderLower4, grammarBuilderLower5, grammarBuilderLower6, grammarBuilderLower7 });
            System.Speech.Recognition.Grammar grammarPhoneticAlphabets = new System.Speech.Recognition.Grammar((GrammarBuilder)phoneticAlphabetLower2to7);
            grammarPhoneticAlphabets.Name = grammarName;
            speechRecognizer.LoadGrammarAsync(grammarPhoneticAlphabets);
        }

        //public void LoadMoveCommandsGrammar(System.Speech.Recognition.SpeechRecognizer speechRecognizer)
        //{
        //    Choices choices = new Choices();
        //    choices.Add("Move Down");
        //    choices.Add("Move Up");
        //    choices.Add("Move Left");
        //    choices.Add("Move Right");
        //    for (int counter = 1; counter < 50; counter++)
        //    {
        //        choices.Add($"Move Down {counter}");
        //        choices.Add($"Move Up {counter}");
        //        choices.Add($"Move Left {counter}");
        //        choices.Add($"Move Right {counter}");
        //    }
        //    System.Speech.Recognition.Grammar grammar = new System.Speech.Recognition.Grammar(choices) { Name = "Move Command" };
        //    speechRecognizer.LoadGrammarAsync(grammar);
        //}
        public void CreateMouseMoveAndClickCommandGrammar(System.Speech.Recognition.SpeechRecognizer speechRecognizer)
        {
            Choices choices = new Choices();
            for (int counter = 1; counter < 100; counter++)
            {
                choices.Add($"Mouse Down {counter}");
                choices.Add($"Mouse Up {counter}");
                choices.Add($"Mouse Left {counter}");
                choices.Add($"Mouse Right {counter}");
            }
            for (int counter = 150; counter < 800; counter = counter + 50)
            {
                choices.Add($"Mouse Down {counter}");
                choices.Add($"Mouse Up {counter}");
                choices.Add($"Mouse Left {counter}");
                choices.Add($"Mouse Right {counter}");
            }
            System.Speech.Recognition.Grammar grammar = new System.Speech.Recognition.Grammar(choices);
            grammar.Name = "Mouse Move";
            speechRecognizer.LoadGrammarAsync(grammar);
            Choices choicesClick = new Choices();
            choicesClick.Add("Mouse Click");
            choicesClick.Add("Click");
            choicesClick.Add("Left Click");
            choicesClick.Add("Right Click");
            choicesClick.Add("Double Click");
            System.Speech.Recognition.Grammar grammarClick = new System.Speech.Recognition.Grammar(choicesClick);
            grammarClick.Name = "Mouse Click";
            speechRecognizer.LoadGrammarAsync(grammarClick);
        }
        public void SetupEnterRandomNumbersCommand(System.Speech.Recognition.SpeechRecognizer speechRecognizer)
        {
            Choices choices = new Choices();
            for (int i = 1; i < 30; i++)
            {
                choices.Add(i.ToString());
            }
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append("Enter");
            grammarBuilder.Append(choices);
            grammarBuilder.Append("Random Numbers");
            System.Speech.Recognition.Grammar grammar = new System.Speech.Recognition.Grammar(grammarBuilder) { Name = "Enter Random Numbers" };
            speechRecognizer.LoadGrammarAsync(grammar);

        }
        public void SetUpSymbolGrammarCommands(System.Speech.Recognition.SpeechRecognizer speechRecognizer)
        {
            Choices choices = new Choices();
            choices.Add("Square Brackets");
            choices.Add("Brackets");
            choices.Add("Curly Brackets");
            choices.Add("Single Quotes");
            choices.Add("Apostrophes");
            choices.Add("Quotes");
            choices.Add("At Signs");
            choices.Add("Chevrons");
            choices.Add("Equals");
            choices.Add("Not Equal");
            choices.Add("Plus");
            choices.Add("Dollar");
            choices.Add("Hash");
            choices.Add("Pipes");
            choices.Add("Ampersands");
            choices.Add("Question Marks");

            Choices choicesInOut = new Choices("In", "Out", "Space");
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(choices);
            grammarBuilder.Append(choicesInOut);
            System.Speech.Recognition.Grammar grammarSymbols = new System.Speech.Recognition.Grammar(grammarBuilder) { Name = "Symbols" };
            speechRecognizer.LoadGrammarAsync(grammarSymbols);
        }
        public void SetupAddTagHtmlCommands(System.Speech.Recognition.SpeechRecognizer speechRecognizer)
        {
            var results = windowsVoiceCommand.GetHtmlTags();
            Choices choices = new Choices();
            if (results!= null )
            {
                foreach (var item in results)
                {
                    choices.Add(item.SpokenForm);
                }
            }
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append("Add Tag");
            grammarBuilder.Append(choices);
            System.Speech.Recognition.Grammar addATagHtml = new System.Speech.Recognition.Grammar(grammarBuilder) { Name = "Add Html Tags" };
            speechRecognizer.LoadGrammarAsync(addATagHtml);
        }
        public GrammarBuilder IncludeChoicesInGrammer(GrammarBuilder grammarBuilder, string grammarName, string wordsBefore)
        {
            List<GrammarItem> items = windowsVoiceCommand.GetListItems(grammarName);
            Choices choices = new Choices();
            foreach (GrammarItem item in items)
            {
                string newChoice = $"{wordsBefore} {item.Value}".Trim();
                choices.Add(newChoice);
            }
            grammarBuilder.Append(choices);
            return grammarBuilder;
        }

    }
}
