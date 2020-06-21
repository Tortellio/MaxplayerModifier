using Rocket.API;

namespace Tortellio.MaxplayerModifier
{
    public class Config : IRocketPluginConfiguration
    {
        public bool EnableMaxPlayerOnStart;
        public byte MaxPlayerOnStart;
        public bool EnableDynamicMaxPlayer;
        public byte MaximumDynamicSlot;
        public void LoadDefaults()
        {
            EnableMaxPlayerOnStart = true;
            MaxPlayerOnStart = 24;
            EnableDynamicMaxPlayer = true;
            MaximumDynamicSlot = 40;
        }
    }
}