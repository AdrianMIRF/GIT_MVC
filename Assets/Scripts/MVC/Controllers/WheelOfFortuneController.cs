using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace AdrianMunteanTest
{
    public class WheelOfFortuneController : BaseController
    {
        private WheelOfFortuneView _wheelOfFortuneView;

        private GameDataModel _gameDataModel;
        private GamePlayEventsDataModel _gamePlayEventsDataModel;

        private int _spinCount;

        public WheelOfFortuneController(GamePlayEventsDataModel gamePlayEventsDataModel) : base(gamePlayEventsDataModel)
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

            _gamePlayEventsDataModel.OnStartWheelOfFortuneGame += StartWheelOfFortuneGame;
        }

        private void StartWheelOfFortuneGame(GameDataModel gameDataModel)
        {
            _gameDataModel = gameDataModel;
            _wheelOfFortuneView.UpdateView(_gameDataModel);
        }

        public void Init(GameDataModel gameDataModel, WheelOfFortuneView wheelOfFortuneView)
        {
            _spinCount = 1;

            _gameDataModel = gameDataModel;
            _wheelOfFortuneView = wheelOfFortuneView;

            _wheelOfFortuneView.SetDefaultOption();
            _wheelOfFortuneView.SetActionOnButtons();

            _wheelOfFortuneView.ButtonSpin += OnButtonSpin;
            _wheelOfFortuneView.ButtonBack += OnButtonBack;
            _wheelOfFortuneView.WheelSpinResult += OnWheelSpinResult;
        }

        private void OnButtonSpin(int selectedWheelOption)
        {
            _wheelOfFortuneView.SpinWheelToResult(SetWheelSpinResultBasedOnSettings(selectedWheelOption));
            _spinCount++;
        }

        private int SetWheelSpinResultBasedOnSettings(int selectedWheelOption)
        {
            if (_spinCount == 1 || (_spinCount >= 3 && _spinCount <= 6) || _spinCount == 9)
            {
                return ChooseLoseResult(selectedWheelOption);
            }

            if (_spinCount == 2 || _spinCount == 7 || _spinCount == 8)
            {
                return selectedWheelOption;
            }

            if (_spinCount > 9)
            {
                System.Random rand = new System.Random();
                if (rand.Next(0, 100) <= 5)
                {
                    return selectedWheelOption;
                }
                else
                {
                    return ChooseLoseResult(selectedWheelOption);
                }
            }

            return -1;
        }

        private int ChooseLoseResult(int selectedWheelOption)
        {
            HashSet<int> exclude = new HashSet<int>() { selectedWheelOption };
            IEnumerable<int> range = Enumerable.Range(1, 8).Where(i => !exclude.Contains(i));

            System.Random rand = new System.Random();
            int index = rand.Next(0, 8 - exclude.Count);
            return range.ElementAt(index);
        }

        private void OnWheelSpinResult(bool wheelSpinResult)
        {
            _wheelOfFortuneView.CloseView();

            if (wheelSpinResult)
            {
                //award 8 coins, maybe add to config data?
                _gamePlayEventsDataModel.OnWheelOfFortuneWin?.Invoke(8);
            }
            else
            {
                _gamePlayEventsDataModel.OnSpendOneCoinButtonPress?.Invoke();
            }
        }

        private void OnButtonBack()
        {
            _wheelOfFortuneView.CloseView();
        }
    }
}