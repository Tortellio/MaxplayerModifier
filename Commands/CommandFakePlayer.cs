using Rocket.API;
using System.Collections.Generic;
using System.Linq;
using SDG.Unturned;
using Rocket.Unturned.Chat;
using Steamworks;

namespace Tortellio.MaxplayerModifier.Commands
{
    public class CommandFakePlayer : IRocketCommand
    {
        public string Name => "fakeplayers";
        public string Help => "Set server fake players";
        public string Syntax => "<amount>";
        public List<string> Aliases => new List<string>() { "setfp", "fp" };
        public List<string> Permissions => new List<string>() { "fakeplayer" };
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public void Execute(IRocketPlayer caller, string[] command)
        {

            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, Main.Instance.Translate("fp_usage"));
                return;
            }
            if (!command[0].All(char.IsDigit) || !byte.TryParse(command[0], out byte fakePlayer))
            {
                UnturnedChat.Say(caller, Main.Instance.Translate("fp_error"));
                return;
            }
            SteamGameServer.SetBotPlayerCount(fakePlayer);
            UnturnedChat.Say(caller, Main.Instance.Translate("fp_set") + fakePlayer.ToString() + " players");
        }
    }
}
