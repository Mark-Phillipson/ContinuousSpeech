using ControlWSR.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWSR.Repositories
{
    public class WindowsVoiceCommand
    {
        Model Model = new Model();
        public List<WindowsSpeechVoiceCommand> GetCommands()
        {
            var result = Model.WindowsSpeechVoiceCommands
                .AsNoTracking()
                .Where(v => v.ApplicationName == "Global")
                .ToList();
            return result;
        }
        public WindowsSpeechVoiceCommand GetCommand(string spokenCommand)
        {
            WindowsSpeechVoiceCommand command = Model.WindowsSpeechVoiceCommands
                .AsNoTracking()
                .Where(v => v.SpokenCommand.ToLower() == spokenCommand.ToLower())
                .FirstOrDefault();
            return command;
        }
        public List<CustomWindowsSpeechCommand> GetChildActions(int windowsSpeechVoiceCommandId)
        {
            List<CustomWindowsSpeechCommand> actions = Model.CustomWindowsSpeechCommands
                .AsNoTracking()
                .Where(v => v.WindowsSpeechVoiceCommandId == windowsSpeechVoiceCommandId)
                .ToList();
            return actions;
        }
        public List<GrammarItem> GetListItems(string grammarName)
        {
            var result = Model.GrammarNames.Where(v => v.NameOfGrammar == grammarName).FirstOrDefault();
            List<GrammarItem> items = Model.GrammarItems.Where(v => v.GrammarNameId == result.Id).ToList();
            return items;
        }
        public HtmlTag GetHtmlTag(string tag)
        {
            var result = Model.HtmlTags.Where(v => v.SpokenForm == tag).FirstOrDefault();
            return result;
        }
        public List<HtmlTag> GetHtmlTags()
        {
            var result = Model.HtmlTags.Where(v => v.SpokenForm != null).OrderBy(v => v.SpokenForm).ToList();
            return result;
        }
    }
}
