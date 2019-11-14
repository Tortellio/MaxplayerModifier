using Rocket.API;

namespace Tortellio.DynamicMaxPlayers
{
    public class Config : IRocketPluginConfiguration
    {
        public bool Enable;
        public byte IncreasedMaxPlayersAmount;
        public void LoadDefaults()
        {
            Enable = true;
            IncreasedMaxPlayersAmount = 24;
        }
    }
}