using System;

namespace AdrianMunteanTest
{
    public class GamePlayEventsDataModel
    {
        public Action OnLoadSavedGameData;
        public Action<SaveData> OnSaveGameData;

        public Action OnSpendOneCoinButtonPress;
        public Action OnGetExtraCoinButtonPress;
        public Action OnClaimFreeCoinButtonPress;

        public Action<GameDataModel> OnUpdateNumberOfCoins;
        public Action<GameDataModel> OnStartWheelOfFortuneGame;

        public Action<int> OnWheelOfFortuneWin;

        public Action OnTickOneSecond;
    }
}