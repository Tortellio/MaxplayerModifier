using Rocket.API;
using System.Collections.Generic;
using SDG.Unturned;
using Rocket.Unturned.Chat;

namespace Tortellio.DynamicMaxPlayers.Commands
{
    public class CommandMaxPlayers : IRocketCommand
    {
        public string Name => "checkmaxplayers";
        public string Help => "Check server max players";
        public string Syntax => "";
        public List<string> Aliases => new List<string>() { "checkmp", "mps" };
        public List<string> Permissions => new List<string>() { "maxplayer.check" };
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public void Execute(IRocketPlayer caller, string[] command)
        {

            if (command.Length != 0)
            {
                UnturnedChat.Say(caller, DynamicMaxPlayers.Instance.Translate("mps_usage"));
                return;
            }
            UnturnedChat.Say(caller, DynamicMaxPlayers.Instance.Translate("mps") + Provider.maxPlayers.ToString() + " players");
        }
    }
}
