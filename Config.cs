using Rocket.API;

namespace Tortellio.DynamicMaxPlayers
{
    public class Config : IRocketPluginConfiguration
    {
        public bool Enable;
        public byte MaxSlots;
        public bool MaxPlayerOnStartEnable;
        public byte MaxPlayerOnStart;
        public void LoadDefaults()
        {
            Enable = true;
            MaxSlots = 48;
            MaxPlayerOnStartEnable = true;
            MaxPlayerOnStart = 24;
        }
    }
}