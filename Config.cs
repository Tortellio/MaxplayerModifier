using Rocket.API;

namespace Tortellio.DynamicMaxPlayers
{
    public class Config : IRocketPluginConfiguration
    {
        public bool Enable;
        public byte MaxPlayersSlot;
        public bool MaxPlayerOnStartEnable;
        public byte MaxPlayerOnStart;
        public void LoadDefaults()
        {
            Enable = true;
            MaxPlayersSlot = 48;
            MaxPlayerOnStartEnable = true;
            MaxPlayerOnStart = 24;
        }
    }
}