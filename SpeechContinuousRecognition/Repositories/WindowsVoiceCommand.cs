using Microsoft.EntityFrameworkCore;

using SpeechContinuousRecognition.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechContinuousRecognition.Repositories
{
    public class WindowsVoiceCommand
    {
        Model Model = new Model("Data Source=DESKTOP-UROO8T1;Initial Catalog=VoiceLauncher;Integrated Security=True;Connect Timeout=120;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public List<WindowsSpeechVoiceCommand> GetCommands()
        {
            var result = Model.WindowsSpeechVoiceCommands
                .AsNoTracking()
                .Where(v => v.ApplicationName == "Global")
                .ToList();
            return result;
        }
        public WindowsSpeechVoiceCommand? GetCommand(string spokenCommand)
        {
            WindowsSpeechVoiceCommand? command = Model.WindowsSpeechVoiceCommands
                .AsNoTracking()
                .Where(v => v.SpokenCommand.ToLower() == spokenCommand.ToLower())
                .FirstOrDefault();
            return command;
        }
        public List<CustomWindowsSpeechCommand>? GetChildActions(int windowsSpeechVoiceCommandId)
        {
            var results = Model.CustomWindowsSpeechCommands
                //.AsNoTracking()
                .Where(v => v.WindowsSpeechVoiceCommandId == windowsSpeechVoiceCommandId);
            if (results != null)
            {
                var actions = results.ToList();
                return actions;
            }
            return null;
        }
        public List<GrammarItem>? GetListItems(string grammarName)
        {
            var result = Model.GrammarNames.Where(v => v.NameOfGrammar == grammarName).FirstOrDefault();
            if (result != null)
            {
                List<GrammarItem> items = Model.GrammarItems.Where(v => v.GrammarNameId == result.Id).ToList();
                return items;
            }
            return null;
        }
        public HtmlTag? GetHtmlTag(string tag)
        {
            var result = Model.HtmlTags.Where(v => v.SpokenForm == tag).FirstOrDefault();
            return result;
        }
        public List<HtmlTag> GetHtmlTags()
        {
            var result = Model.HtmlTags.Where(v => v.SpokenForm != null).OrderBy(v => v.SpokenForm).ToList();
            return result;
        }

        public CustomIntelliSense GetWord(string searchTerm)
        {
            var result = Model.CustomIntelliSenses.Where(i => i.LanguageID == 1 && i.CategoryID == 39 && i.Display_Value == searchTerm).OrderBy(v => v.Display_Value).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public List<AdditionalCommand> GetAdditionalCommands(int id)
        {
            var result = Model.AdditionalCommands.Where(v => v.CustomIntelliSenseID == id).ToList();
            return result;
        }
    }
}
