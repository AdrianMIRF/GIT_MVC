using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace AdrianMunteanTest
{
    public class LoadGameController : BaseController
    {
        private const string SAVE_FILE_NAME = "SaveData.json";

        private int _defaultCoinsFromScriptableInfo;
        private int _defaultFreeGoldClaimedDayHourFromScriptableInfo;

        private SaveData _saveData;
        private GameDataModel _gameDataModel;
        private DefaultValues _defaultValues;
        private GamePlayEventsDataModel _gamePlayEventsDataModel;

        public LoadGameController(GamePlayEventsDataModel gamePlayEventsDataModel) : base(gamePlayEventsDataModel)
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
        }

        private void SetupGameDataModel()
        {
            _gameDataModel = new GameDataModel();

            _gameDataModel.NumberOfCoins = _saveData.SavedNumberOfCoins;
            TimerUtility.SetFirstDT(TimerUtility.ConvertDateFromString(_saveData.NextFreeGoldClaimTime));
            _gameDataModel.NextFreeGoldClaimTime = TimerUtility.GetFirstDT();
            _gameDataModel.FreeDailyBonusIncrementCoinAmount = _defaultValues.FreeDailyBonusIncrementCoinAmount;
            _gameDataModel.WheelOfFortuneDefaultWinPercentage = _defaultValues.WheelOfFortuneDefaultWinPercentage;
        }

        public void Init(DefaultValues defaultValues)
        {
            _defaultValues = defaultValues;
            _defaultCoinsFromScriptableInfo = _defaultValues.StartCoins;
            _defaultFreeGoldClaimedDayHourFromScriptableInfo = _defaultValues.TimeOfDayForFreeGoldClaim;

            LoadSaveDataFromJSON();
            SetupGameDataModel();
        }

        public GameDataModel GetGameDataModel()
        {
            return _gameDataModel;
        }

        public SaveData GetSaveData()
        {
            return _saveData;
        }

        private void LoadSaveDataFromJSON()
        {
            string path = GetPathTo(SAVE_FILE_NAME);

            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);

                _saveData = new SaveData();
                _saveData = JsonUtility.FromJson<SaveData>(sr.ReadToEnd());

                sr.Close();
            }
            else
            {
                SetDefaultValues();
            }
        }

        private void SetDefaultValues()
        {
            TimerUtility.SetFirstDT(_defaultFreeGoldClaimedDayHourFromScriptableInfo);

            _saveData = new SaveData
            {
                SavedNumberOfCoins = _defaultCoinsFromScriptableInfo,
                NextFreeGoldClaimTime = TimerUtility.GetFirstDT().ToString()
            };

            _gamePlayEventsDataModel.OnSaveGameData?.Invoke(_saveData);
        }

        private string GetPathTo(string fileName)
        {
            return Path.Combine(Application.persistentDataPath, fileName);
        }
    }
}