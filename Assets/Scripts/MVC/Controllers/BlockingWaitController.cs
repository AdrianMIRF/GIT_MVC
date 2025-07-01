using System.Threading.Tasks;
using UnityEngine;

namespace AdrianMunteanTest
{
    public class BlockingWaitController : BaseController
    {
        private BlockingWait _blockingWaitView;

        private GameDataModel _gameDataModel;
        private GamePlayEventsDataModel _gamePlayEventsDataModel;

        public BlockingWaitController(GamePlayEventsDataModel gamePlayEventsDataModel) : base(gamePlayEventsDataModel)
        {
            _gamePlayEventsDataModel = gamePlayEventsDataModel;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }

        public override void SetCallbacks()
        {
            base.SetCallbacks();

            _gamePlayEventsDataModel.OnSpendOneCoinButtonPress += SpendOneCoinButtonPress;
            _gamePlayEventsDataModel.OnGetExtraCoinButtonPress += GetExtraCoinButtonPress;
            _gamePlayEventsDataModel.OnClaimFreeCoinButtonPress += ClaimFreeCoinButtonPress;
            _gamePlayEventsDataModel.OnWheelOfFortuneWin += WheelOfFortuneWin;
        }

        public void Init(GameDataModel gameDataModel, BlockingWait blockingWaitView)
        {
            _gameDataModel = gameDataModel;
            _blockingWaitView = blockingWaitView;
        }

        private void SpendOneCoinButtonPress()
        {
            UpdateNumberOfCoins(-1);
            SaveGameData();
        }

        private void UpdateNumberOfCoins(int coinIncrementValue)
        {
            _gameDataModel.AwardedNumberOfCoinsFromAction = coinIncrementValue;
            _gameDataModel.NumberOfCoins += _gameDataModel.AwardedNumberOfCoinsFromAction;
            _blockingWaitView.UpdateView(_gameDataModel);

            _gamePlayEventsDataModel.OnUpdateNumberOfCoins?.Invoke(_gameDataModel);
        }

        private void GetExtraCoinButtonPress()
        {
            if (_gameDataModel.NumberOfCoins > 0)
            {
                _gamePlayEventsDataModel.OnStartWheelOfFortuneGame?.Invoke(_gameDataModel);
            }
            else
            {
                _gameDataModel.AwardedNumberOfCoinsFromAction = 0;
                _blockingWaitView.UpdateView(_gameDataModel);
            }
        }

        private void ClaimFreeCoinButtonPress()
        {
            UpdateNumberOfCoins(1);
            _gameDataModel.NextFreeGoldClaimTime = TimerUtility.GetNextDT(_gameDataModel.FreeGoldClaimHour);
            SaveGameData();

            TimerUtility.SetFirstDT(TimerUtility.GetNextDT(_gameDataModel.FreeGoldClaimHour));
        }

        private void WheelOfFortuneWin(int wheelCoinWinAmount)
        {
            UpdateNumberOfCoins(wheelCoinWinAmount);
            SaveGameData();
        }

        private void SaveGameData()
        {
            SaveData saveData = new SaveData
            {
                SavedNumberOfCoins = _gameDataModel.NumberOfCoins,
                NextFreeGoldClaimTime = _gameDataModel.NextFreeGoldClaimTime.ToString()
            };

            //Debug.Log(Time.time + "         ###        SaveGameDataSaveGameData -> NextFreeGoldClaimTime -> " + saveData.NextFreeGoldClaimTime);

            _gamePlayEventsDataModel.OnSaveGameData?.Invoke(saveData);
        }
    }
}