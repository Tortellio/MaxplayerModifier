using Rocket.API;
using Steamworks;

namespace Tortellio.MaxplayerModifier
{
    public class Config : IRocketPluginConfiguration
    {
        public bool EnableMaxPlayerOnStart;
        public byte MaxPlayerOnStart;
        public bool EnableFakePlayerOnStart;
        public byte FakePlayerOnStart;
        public bool EnableDynamicMaxPlayer;
        public byte MaximumDynamicSlot;
        public void LoadDefaults()
        {
            EnableMaxPlayerOnStart = true;
            MaxPlayerOnStart = 24;
            EnableFakePlayerOnStart = true;
            FakePlayerOnStart = 1;
            EnableDynamicMaxPlayer = true;
            MaximumDynamicSlot = 40;
        }
    }
}