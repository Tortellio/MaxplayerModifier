using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using Logger = Rocket.Core.Logging.Logger;

namespace Tortellio.MaxplayerModifier
{
    public class Main : RocketPlugin<Config>
    {
        public static Main Instance;
		public static string PluginName = "MaxplayerModifier";
        public static string PluginVersion = " 1.0.7";
        public static byte baseMaxPlayers;

        protected override void Load()
        {
            Instance = this;
            baseMaxPlayers = Provider.maxPlayers;
            if (Configuration.Instance.EnableMaxPlayerOnStart)
            {
                baseMaxPlayers = Configuration.Instance.MaxPlayerOnStart;
            }
            if (Configuration.Instance.EnableDynamicMaxPlayer)
            {
                U.Events.OnPlayerConnected += OnPlayerConnect;
                U.Events.OnPlayerDisconnected += OnPlayerDisconnect;
            }
            if (Level.isLoaded)
            {
                ClampMaxPlayers();
            }
            Logger.Log("MaxplayerModifier has been loaded!", ConsoleColor.Yellow);
			Logger.Log(PluginName + PluginVersion, ConsoleColor.Yellow);
            Logger.Log("Made by Tortellio", ConsoleColor.Yellow);

            Level.onPreLevelLoaded += ClampMaxPlayers;
        }

        protected override void Unload()
        {
            Instance = null;
            Provider.maxPlayers = baseMaxPlayers;

            Logger.Log("MaxplayerModifier has been unloaded!");
			Logger.Log("Visit Tortellio Discord for more! https://discord.gg/pzQwsew", ConsoleColor.Yellow);

            Level.onPreLevelLoaded -= ClampMaxPlayers;
            if (Configuration.Instance.EnableDynamicMaxPlayer)
            {
                U.Events.OnPlayerConnected -= OnPlayerConnect;
                U.Events.OnPlayerDisconnected -= OnPlayerDisconnect;
            }
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "mp_set", "Succesfully set max player to : " },
            { "mp_set_normal", "Succesfully set max player to normal" },
            { "mps", "Current server max players : " },
            { "mps_usage", "Error in command. Try /mps or /mplayers" },
            { "mp_usage", "Error in command. Try /mp amount or /setmp amount or /maxplayer amount" },
            { "mp_error", "Something went wrong. Input a number." },
        };

        private void ClampMaxPlayers()
        {
            if (Provider.maxPlayers > 24)
            {
                SteamGameServer.SetMaxPlayerCount(24);
            }
        }

        private void ClampMaxPlayers(int level)
        {
            if (Provider.maxPlayers > 24)
            {
                SteamGameServer.SetMaxPlayerCount(24);
            }
            if (Configuration.Instance.EnableMaxPlayerOnStart)
            {
                Provider.maxPlayers = Configuration.Instance.MaxPlayerOnStart;
            }
        }

        private void OnPlayerConnect(UnturnedPlayer player)
        {
            if (Provider.maxPlayers == Provider.clients.Count && Provider.maxPlayers + 1 != Configuration.Instance.MaximumDynamicSlot)
            {
                Provider.maxPlayers++;
                if (Provider.maxPlayers > 24)
                {
                    SteamGameServer.SetMaxPlayerCount(24);
                }
            }
        }

        private void OnPlayerDisconnect(UnturnedPlayer player)
        {
            if (Provider.clients.Count >= baseMaxPlayers) 
            {
                Provider.maxPlayers--;
                if (Provider.maxPlayers < baseMaxPlayers)
                {
                    Provider.maxPlayers = baseMaxPlayers;
                } 
            }
            if (Provider.maxPlayers > 24)
            {
                SteamGameServer.SetMaxPlayerCount(24);
            }
        }
    }
}