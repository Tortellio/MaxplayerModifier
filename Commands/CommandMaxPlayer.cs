using Rocket.API;
using System.Collections.Generic;
using System.Linq;
using SDG.Unturned;
using Rocket.Unturned.Chat;

namespace Tortellio.DynamicMaxPlayers.Commands
{
    public class CommandMaxPlayer : IRocketCommand
    {
        public string Name => "setmaxplayers";
        public string Help => "Set server max players";
        public string Syntax => "<amount>";
        public List<string> Aliases => new List<string>() { "setmp", "mp" };
        public List<string> Permissions => new List<string>() { "maxplayer" };
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public void Execute(IRocketPlayer caller, string[] command)
        {
            
            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, DynamicMaxPlayers.Instance.Translate("mp_usage"));
                return;
            }
            if (!command[0].All(char.IsDigit) || !byte.TryParse(command[0], out byte maxPlayer))
            {
                UnturnedChat.Say(caller, DynamicMaxPlayers.Instance.Translate("mp_error"));
                return;
            }
            if (maxPlayer == 0)
            {
                UnturnedChat.Say(caller, DynamicMaxPlayers.Instance.Translate("mp_set_normal"));
                DynamicMaxPlayers.forceMaxPlayer = maxPlayer;
                return;
            }
            Provider.maxPlayers = maxPlayer;
            DynamicMaxPlayers.forceMaxPlayer = maxPlayer;
            UnturnedChat.Say(caller, DynamicMaxPlayers.Instance.Translate("mp_set") + maxPlayer.ToString() + " players");
        }
    }
}
