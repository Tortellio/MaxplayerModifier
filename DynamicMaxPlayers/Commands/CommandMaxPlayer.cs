using Rocket.API;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using System.Linq;
using SDG.Unturned;
namespace Tortellio.DynamicMaxPlayers.Commands
{
    public class CommandMaxPlayer : IRocketCommand
    {
        public string Name => "maxplayer";
        public string Help => "Set server max players";
        public string Syntax => "<amount>";
        public List<string> Aliases => new List<string>() { "setmp", "mp" };
        public List<string> Permissions => new List<string>() { "maxplayer" };
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (command.Length != 1)
            {
                DynamicMaxPlayers.Instance.Say(player, "mp_usage");
                return;
            }
            if (!command[0].All(char.IsDigit) || !byte.TryParse(command[0], out byte maxPlayer))
            {
                DynamicMaxPlayers.Instance.Say(player, "mp_error");
                return;
            }
            if (maxPlayer == 0)
            {
                DynamicMaxPlayers.Instance.Say(player, "mp_set_normal");
                DynamicMaxPlayers.Instance.forceMaxPlayer = maxPlayer;
                return;
            }
            DynamicMaxPlayers.Instance.forceMaxPlayer = maxPlayer;
            DynamicMaxPlayers.Instance.Say(player, "mp_set" + maxPlayer.ToString() + " players");
        }
    }
}
