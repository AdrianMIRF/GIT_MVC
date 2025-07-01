using System.Threading.Tasks;
using UnityEngine;

namespace AdrianMunteanTest
{
    public class StartApp : MonoBehaviour
    {
        [SerializeField] private TickTimerUtility _tickTimerUtility;
        [SerializeField] private MainMenu _mainMenuRef;
        [SerializeField] private BlockingWait _blockingWaitView;
        [SerializeField] private FreemiumBarView _freemiumBarView;
        [SerializeField] private WheelOfFortuneView _wheelOfFortuneView;

        private GamePlayEventsDataModel _gamePlayEventsDataModel;

        void Start()
        {
            _gamePlayEventsDataModel = new GamePlayEventsDataModel();
            _tickTimerUtility.Init(_gamePlayEventsDataModel);

            _ = StartMasterController();
        }

        private async Task StartMasterController()
        {
            GameDataLoader gameDataLoader = FindObjectOfType<GameDataLoader>();
            await gameDataLoader.LoadDataObjects();

            MasterController masterController = new MasterController();
            masterController.SetData(gameDataLoader.GetLoadedData(), _gamePlayEventsDataModel, _mainMenuRef, _blockingWaitView, _freemiumBarView, _wheelOfFortuneView);
            await masterController.CreateControllers();
        }
    }
}