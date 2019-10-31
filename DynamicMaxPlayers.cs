using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;
using Math = System.Math;

namespace Tortellio.DynamicMaxPlayers
{
    public class DynamicMaxPlayers : RocketPlugin<Config>
    {
        public static DynamicMaxPlayers Instance;
		public static string PluginName = "DynamicMaxPlayers";
        public static string PluginVersion = "1.0.0";
        private DateTime lastCalled;
        public byte oldMaxPlayer;
        public byte forceMaxPlayer = 0;
        protected override void Load()
        {
            Instance = this;
            oldMaxPlayer = Provider.maxPlayers;
            Logger.Log("DynamicMaxPlayer has been loaded!");
			Logger.Log(PluginName + PluginVersion, ConsoleColor.Yellow);
            Logger.Log("Made by Tortellio", ConsoleColor.Yellow);
            if (!Configuration.Instance.Enable)
            {
                Logger.Log("DynamicMaxPlayer is disabled in configuration!", ConsoleColor.Red);
                return;
            }
            Logger.Log("Server Max Players changed to " + Provider.maxPlayers.ToString() + "Players", ConsoleColor.Yellow);
        }

        protected override void Unload()
        {
            Instance = null;
            Provider.maxPlayers = oldMaxPlayer;
            Logger.Log("DynamicMaxPlayer has been unloaded!");
			Logger.Log("Visit Tortellio Discord for more! https://discord.gg/pzQwsew", ConsoleColor.Yellow);
            if (!Configuration.Instance.Enable) { return; }
            Logger.Log("Server Max Players changed back to normal! (" + Provider.maxPlayers.ToString() + "Players)", ConsoleColor.Yellow);
        }
        public void Say(UnturnedPlayer player, string key, params object[] args) => UnturnedChat.Say(player, Translate(key, args));
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "mp_set", "Succesfully set max player to : " },
            { "mp_set_normal", "Succesfully set max player to normal" },
            { "mps", "Current server max players : " },
            { "mps_usage", "Error in command. Try /mps or /mplayers" },
            { "mp_usage", "Error in command. Try /mp amount or /setmp amount or /maxplayer amount" },
            { "mp_error", "Something went wrong. Input a number." },

        };
        public void FixedUpdate()
        {
            if (!Configuration.Instance.Enable) { return; }
            if (forceMaxPlayer != 0) { return; }
            if ((DateTime.Now - lastCalled).TotalSeconds > 1)
            {
                lastCalled = DateTime.Now;
                decimal valueMin = Configuration.Instance.ChangeMaxPlayerPercentage / 100;
                int maxPlayers = Convert.ToInt32(Math.Round(valueMin * Provider.maxPlayers));
                if (Provider.clients.Count >= maxPlayers && Provider.maxPlayers + Configuration.Instance.IncreasedMaxPlayersAmount < 255)
                    Provider.maxPlayers = (byte)(Provider.maxPlayers + Configuration.Instance.IncreasedMaxPlayersAmount);
                else if (Provider.clients.Count <= Provider.maxPlayers - Configuration.Instance.IncreasedMaxPlayersAmount)
                    Provider.maxPlayers = (byte)(Provider.maxPlayers - Configuration.Instance.IncreasedMaxPlayersAmount);
            }
        }
    }
}