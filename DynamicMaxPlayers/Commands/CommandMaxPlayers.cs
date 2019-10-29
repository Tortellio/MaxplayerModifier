using Rocket.API;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using SDG.Unturned;
namespace Tortellio.DynamicMaxPlayers.Commands
{
    public class CommandMaxPlayers : IRocketCommand
    {
        public string Name => "mplayers";
        public string Help => "Check server max players";
        public string Syntax => "";
        public List<string> Aliases => new List<string>() { "mps" };
        public List<string> Permissions => new List<string>() { "maxplayer.check" };
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (command.Length != 0)
            {
                DynamicMaxPlayers.Instance.Say(player, "mps_usage");
                return;
            }
            DynamicMaxPlayers.Instance.Say(player, "mps" + Provider.maxPlayers.ToString() + " players");
        }
    }
}
