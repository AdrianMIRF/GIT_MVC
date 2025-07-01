using System.Threading.Tasks;

namespace AdrianMunteanTest
{
    public class FreemiumBarController : BaseController
    {
        private FreemiumBarView _freemiumBarView;

        private GameDataModel _gameDataModel;
        private GamePlayEventsDataModel _gamePlayEventsDataModel;

        public FreemiumBarController(GamePlayEventsDataModel gamePlayEventsDataModel) : base(gamePlayEventsDataModel)
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

            _gamePlayEventsDataModel.OnUpdateNumberOfCoins += UpdateNumberOfCoins;
        }

        public void Init(GameDataModel gameDataModel, FreemiumBarView freemiumBarView)
        {
            _gameDataModel = gameDataModel;
            _freemiumBarView = freemiumBarView;

            _freemiumBarView.UpdateView(_gameDataModel);
        }

        private void UpdateNumberOfCoins(GameDataModel gameDataModel)
        {
            _gameDataModel = gameDataModel;
            _freemiumBarView.UpdateView(_gameDataModel);
        }
    }
}