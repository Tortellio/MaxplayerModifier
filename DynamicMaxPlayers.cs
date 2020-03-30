using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Permissions;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;
using Math = System.Math;

namespace Tortellio.DynamicMaxPlayers
{
    public class DynamicMaxPlayers : RocketPlugin<Config>
    {
        public static DynamicMaxPlayers Instance;
		public static string PluginName = "DynamicMaxPlayers";
        public static string PluginVersion = " 1.0.3";

        public byte baseMaxPlayers;
        public byte forceMaxPlayer = 0;
        protected override void Load()
        {
            Instance = this;
            baseMaxPlayers = Provider.maxPlayers;
            if (Configuration.Instance.MaxPlayerOnStartEnable)
            {
                Provider.maxPlayers = Configuration.Instance.MaxPlayerOnStart;
                baseMaxPlayers = Configuration.Instance.MaxPlayerOnStart;
            }
            Logger.Log("DynamicMaxPlayer has been loaded!");
			Logger.Log(PluginName + PluginVersion, ConsoleColor.Yellow);
            Logger.Log("Made by Tortellio", ConsoleColor.Yellow);
            if (!Configuration.Instance.Enable)
            {
                Logger.Log("DynamicMaxPlayer is disabled in configuration!", ConsoleColor.Red);
                Unload();
                return;
            }

            UnturnedPermissions.OnJoinRequested += OnPlayerConnect;
            U.Events.OnPlayerConnected += OnPlayerJoin;
        }

        protected override void Unload()
        {
            Instance = null;
            Provider.maxPlayers = baseMaxPlayers;
            Logger.Log("DynamicMaxPlayer has been unloaded!");
			Logger.Log("Visit Tortellio Discord for more! https://discord.gg/pzQwsew", ConsoleColor.Yellow);
            if (!Configuration.Instance.Enable) { return; }
            Logger.Log("Server Max Players changed back to normal! (" + Provider.maxPlayers.ToString() + " Players)", ConsoleColor.Yellow);

            UnturnedPermissions.OnJoinRequested -= OnPlayerConnect;
            U.Events.OnPlayerConnected -= OnPlayerJoin;
        }
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "mp_set", "Succesfully set max player to : " },
            { "mp_set_normal", "Succesfully set max player to normal" },
            { "mps", "Current server max players : " },
            { "mps_usage", "Error in command. Try /mps or /mplayers" },
            { "mp_usage", "Error in command. Try /mp amount or /setmp amount or /maxplayer amount" },
            { "mp_error", "Something went wrong. Input a number." }
        };

        private void OnPlayerConnect(CSteamID steamID, ref ESteamRejection? rejection)
        {
            Provider.maxPlayers = Configuration.Instance.MaxSlots;
            Logger.Log(Translate("mps") + (Provider.clients.Count).ToString() + "/" + Provider.maxPlayers.ToString(), ConsoleColor.Yellow);
        }
        
        private void OnPlayerJoin(UnturnedPlayer player)
        {
            StartCoroutine(Count());
        }

        private IEnumerator<WaitForSeconds> Count()
        {
            yield return new WaitForSeconds(1f);
            if (forceMaxPlayer == 0)
            {
                Provider.maxPlayers = baseMaxPlayers;
            }
            else if (forceMaxPlayer != 0)
            {
                Provider.maxPlayers = forceMaxPlayer;
            }
            Logger.Log(Translate("mps") + (Provider.clients.Count).ToString() + "/" + Provider.maxPlayers.ToString(), ConsoleColor.Yellow);
        }
    }
}