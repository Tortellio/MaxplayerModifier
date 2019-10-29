using Rocket.API;

namespace Tortellio.DynamicMaxPlayers
{
    public class Config : IRocketPluginConfiguration
    {
        public bool Enable;
        public byte ChangeMaxPlayerPercentage;
        public byte IncreasedMaxPlayersAmount;
        public void LoadDefaults()
        {
            Enable = true;
            ChangeMaxPlayerPercentage = 90;
            IncreasedMaxPlayersAmount = 24;
        }
    }
}