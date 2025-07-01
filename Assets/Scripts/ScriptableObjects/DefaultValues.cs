using UnityEngine;

namespace AdrianMunteanTest
{
    [CreateAssetMenu(fileName = "DefaultValues", menuName = "Scriptable Objects/Game Configuration/Default Values", order = 1)]
    public class DefaultValues : ScriptableObject
    {
        public int StartCoins;
        public int TimeOfDayForFreeGoldClaim;
        public int FreeDailyBonusIncrementCoinAmount;
        public int WheelOfFortuneDefaultWinPercentage;
    }
}