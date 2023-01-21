using DataAccessLibrary.Models;

using Microsoft.CognitiveServices.Speech;
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
        VoiceAdminDbContext? Model = null;
        public WindowsVoiceCommand()
        {
            if (System.Environment.MachineName == "J40L4V3")
            {
                Model = new VoiceAdminDbContext("Data Source=J40L4V3;Initial Catalog=VoiceLauncher;Integrated Security=True;Connect Timeout=120;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
            else if (Environment.MachineName == "SURFACEPRO")
            {
                Model = new VoiceAdminDbContext("Data Source=Localhost\\SqlExpress;Initial Catalog=VoiceLauncher;Integrated Security=True;Connect Timeout=120;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }
        public List<WindowsSpeechVoiceCommand> GetCommands()
        {
            var result = Model.WindowsSpeechVoiceCommands
                .AsNoTracking()
                .Where(v => v.ApplicationName == "Global")
                .ToList();
            return result;
        }
        public List<WindowsSpeechVoiceCommand> GetDictationCommands(string? applicationName)
        {
            if (applicationName == null)
            {
                var result = Model.WindowsSpeechVoiceCommands
                    .AsNoTracking()
                    .Where(v => v.ApplicationName == "Global" && v.SpokenCommand.EndsWith("<dictation>"))
                    .ToList();
                return result;
            }
            else
            {
                var result = Model.WindowsSpeechVoiceCommands
                    .AsNoTracking()
                    .Where(v => (v.ApplicationName == "Global" || v.ApplicationName == applicationName) && v.SpokenCommand.EndsWith("<dictation>"))
                    .ToList();
                return result;
            }
        }
        public WindowsSpeechVoiceCommand? GetRandomCommand()
        {
            if (Model == null)
            {
                return null;
            }
            //Get a random voice command
            var result = Model.WindowsSpeechVoiceCommands
                .AsNoTracking()
                .Where(v => v.AutoCreated == false)
                .OrderBy(r => Guid.NewGuid())
                .FirstOrDefault();
            return result;
        }
        public WindowsSpeechVoiceCommand? GetCommand(string spokenCommand, string? applicationName)
        {
            if (Model==null)
            {
                return null;
            }
            if (applicationName != null && Model != null)
            {
                WindowsSpeechVoiceCommand? applicationCommand = Model.WindowsSpeechVoiceCommands
                    .AsNoTracking()
                    .Where(v => v.SpokenCommand.ToLower() == spokenCommand.ToLower() && v.ApplicationName == applicationName)
                    .FirstOrDefault();
                if (applicationCommand != null)
                {
                    return applicationCommand;
                }
            }
            WindowsSpeechVoiceCommand? command = Model.WindowsSpeechVoiceCommands
                .AsNoTracking()
                .Where(v => v.SpokenCommand.ToLower() == spokenCommand.ToLower() && v.ApplicationName == "Global")
                .FirstOrDefault();
            return command;
        }
        public List<CustomWindowsSpeechCommand>? GetChildActions(int windowsSpeechVoiceCommandId)
        {
            var results = Model.CustomWindowsSpeechCommands
                .AsNoTracking()
                .Where(v => v.WindowsSpeechVoiceCommandId == windowsSpeechVoiceCommandId);
            if (results != null)
            {
                var actions = results.ToList();
                return actions;
            }
            return null;
        }
        public List<PhraseListGrammarStorage>? GetPhraseListGrammars()
        {
            var results = Model.PhraseListGrammars.AsNoTracking();
            if (results != null)
            {
                var phraseListGrammars = results.ToList();
                return phraseListGrammars;
            }
            return null;
        }
        public List<DataAccessLibrary.Models.ApplicationDetail>? GetApplicationDetails()
        {
            var results = Model.ApplicationDetails.AsNoTracking();
            if (results != null)
            {
                var applicationDetails = results.ToList();
                return applicationDetails;
            }
            return null;
        }
        public List<Idiosyncrasy>? GetIdiosyncrasies()
        {
            var results = Model.Idiosyncrasies.AsNoTracking();
            if (results != null)
            {
                var idiosyncrasies = results.ToList();
                return idiosyncrasies;
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

        public CustomIntelliSense? GetWord(string searchTerm)
        {
            var result = Model.CustomIntelliSenses.Where(i => i.LanguageID == 1 && i.CategoryID == 39 && i.Display_Value.ToLower() == searchTerm.ToLower()).OrderBy(v => v.Display_Value).FirstOrDefault();
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
