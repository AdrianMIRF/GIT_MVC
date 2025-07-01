using System;

namespace AdrianMunteanTest
{
    [Serializable]
    public class GameDataModel
    {
        public int NumberOfCoins;
        public int FreeGoldClaimHour;
        public DateTime NextFreeGoldClaimTime;
        public int AwardedNumberOfCoinsFromAction;
        public int FreeDailyBonusIncrementCoinAmount;
        public int WheelOfFortuneDefaultWinPercentage;
    }
}