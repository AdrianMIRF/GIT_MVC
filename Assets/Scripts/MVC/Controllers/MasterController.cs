using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AdrianMunteanTest
{
    public class MasterController
    {
        private Dictionary<Type, ScriptableObject> _loadedConfigGameData;
        private GamePlayEventsDataModel _gamePlayEventsDataModel;

        private MainMenu _mainMenuRef;
        private BlockingWait _blockingWaitView;
        private FreemiumBarView _freemiumBarView;
        private WheelOfFortuneView _wheelOfFortuneView;

        public void SetData(Dictionary<Type, ScriptableObject> loadedConfigGameData, GamePlayEventsDataModel gamePlayEventsDataModel, MainMenu mainMenuRef, BlockingWait blockingWaitView, FreemiumBarView freemiumBarView, WheelOfFortuneView wheelOfFortuneView)
        {
            _loadedConfigGameData = loadedConfigGameData;
            _gamePlayEventsDataModel = gamePlayEventsDataModel;
            _mainMenuRef = mainMenuRef;
            _blockingWaitView = blockingWaitView;
            _freemiumBarView = freemiumBarView;
            _wheelOfFortuneView = wheelOfFortuneView;
        }

        public async Task CreateControllers()
        {
            //Create the controllers
            LoadGameController loadGameController = new LoadGameController(_gamePlayEventsDataModel);
            MainMenuController mainMenuController = new MainMenuController(_gamePlayEventsDataModel);
            SaveGameController saveGameController = new SaveGameController(_gamePlayEventsDataModel);
            BlockingWaitController blockingWaitController = new BlockingWaitController(_gamePlayEventsDataModel);
            FreemiumBarController freemiumBarController = new FreemiumBarController(_gamePlayEventsDataModel);
            WheelOfFortuneController wheelOfFortuneController = new WheelOfFortuneController(_gamePlayEventsDataModel);

            //init them
            await loadGameController.InitializeAsync();
            await mainMenuController.InitializeAsync();
            await saveGameController.InitializeAsync();
            await blockingWaitController.InitializeAsync();
            await freemiumBarController.InitializeAsync();
            await wheelOfFortuneController.InitializeAsync();

            loadGameController.Init(GetLoadedConfigGameData<DefaultValues>());
            mainMenuController.Init(loadGameController.GetGameDataModel(), _mainMenuRef);
            blockingWaitController.Init(loadGameController.GetGameDataModel(), _blockingWaitView);
            freemiumBarController.Init(loadGameController.GetGameDataModel(), _freemiumBarView);
            wheelOfFortuneController.Init(loadGameController.GetGameDataModel(), _wheelOfFortuneView);

            Debug.Log(Time.time + $"<color=cyan>  ### MasterController() -> CreateControllers() -> DONE </color>");
        }

        private T GetLoadedConfigGameData<T>() where T : ScriptableObject
        {
            Debug.Log(".GetData() " + typeof(T));
            if (_loadedConfigGameData.ContainsKey(typeof(T))) return (T)_loadedConfigGameData[typeof(T)];
            Debug.LogError("Missing Data type: " + typeof(T).ToString());
            return default(T);
        }
    }
}