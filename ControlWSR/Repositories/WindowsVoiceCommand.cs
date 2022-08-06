using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWSR.Repositories
{
    public class WindowsVoiceCommand
    {
        Model Model= new Model();
        public List<WindowsSpeechVoiceCommand> GetCommands()
        {
            var result = Model.WindowsSpeechVoiceCommands
                .AsNoTracking()
                .ToList();
            return result;
        }
        public WindowsSpeechVoiceCommand GetCommand(string spokenCommand )
        {
            WindowsSpeechVoiceCommand command = Model.WindowsSpeechVoiceCommands
                .AsNoTracking()
                .Where(v => v.SpokenCommand.ToLower()==spokenCommand.ToLower())
                .FirstOrDefault();
            return command;
        }
        public List<CustomWindowsSpeechCommand> GetChildActions(int windowsSpeechVoiceCommandId)
        {
            List<CustomWindowsSpeechCommand> actions = Model.CustomWindowsSpeechCommands
                .AsNoTracking()
                .Where(v => v.WindowsSpeechVoiceCommandId==windowsSpeechVoiceCommandId)
                .ToList();
            return actions;
        }
    }
}
