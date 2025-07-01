using System.Threading.Tasks;

namespace AdrianMunteanTest
{
    public class MainMenuController : BaseController
    {
        private MainMenu _mainMenuView;

        private GameDataModel _gameDataModel;
        private GamePlayEventsDataModel _gamePlayEventsDataModel;

        public MainMenuController(GamePlayEventsDataModel gamePlayEventsDataModel) : base(gamePlayEventsDataModel)
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

            _gamePlayEventsDataModel.OnTickOneSecond += CheckIfCanGiveDailyBonus;
            _gamePlayEventsDataModel.OnUpdateNumberOfCoins += UpdateNumberOfCoins;
        }

        private void UpdateNumberOfCoins(GameDataModel gameDataModel)
        {
            _gameDataModel = gameDataModel;
            _mainMenuView.UpdateView(_gameDataModel);
        }

        public void Init(GameDataModel gameDataModel, MainMenu mainMenu)
        {
            _gameDataModel = gameDataModel;
            _mainMenuView = mainMenu;

            _mainMenuView.UpdateView(_gameDataModel);

            _mainMenuView.SpendOneCoin += OnSpendOneCoin;
            _mainMenuView.GetExtraCoin += OnGetExtraCoin;
            _mainMenuView.ClaimFreeCoin += OnClaimFreeCoin;
        }

        private void OnSpendOneCoin()
        {
            _gamePlayEventsDataModel.OnSpendOneCoinButtonPress?.Invoke();
        }

        private void OnGetExtraCoin()
        {
            _gamePlayEventsDataModel.OnGetExtraCoinButtonPress?.Invoke();
        }

        private void OnClaimFreeCoin()
        {
            _gamePlayEventsDataModel.OnClaimFreeCoinButtonPress?.Invoke();
        }

        private void CheckIfCanGiveDailyBonus()
        {
            double totalSeconds = -TimerUtility.ConvertDateTimeToTimestamp(TimerUtility.CurrentTime);
            if (totalSeconds <= 0)
            {
                _mainMenuView.SetCanClaimFreeDailyBonus();
            }
            else
            {
                _mainMenuView.UpdateFreeCoinCoolDown(TimerUtility.GetFormatedTimeToCoolDown(totalSeconds));
            }
        }
    }
}