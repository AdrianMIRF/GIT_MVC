using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdrianMunteanTest
{
    public class MainMenu : MonoBehaviour, IView
    {
        private const string CLAIM_FREE_COIN = "Claim Free Coin ";
        private const string FREE_COIN_COOLDOWN = "Free Coin Cooldown ";

        public Action SpendOneCoin;
        public Action GetExtraCoin;
        public Action ClaimFreeCoin;

        [SerializeField] private Button _spendOneCoinButton;
        [SerializeField] private Button _freeCoinButton;
        [SerializeField] private TextMeshProUGUI _freeCoinButtonLabel;

        [SerializeField] private BlockingWait _blockingWaitPopup;

        private GameDataModel _gameDataModel;

        public void UpdateView(GameDataModel gameDataModel)
        {
            _gameDataModel = gameDataModel;
            _spendOneCoinButton.interactable = _gameDataModel.NumberOfCoins > 0;
        }

        public void Action_SpendOneCoin()
        {
            SpendOneCoin.Invoke();
            //Debug.Log("MainMenu:Action_SpendOneCoin");
        }

        public void Action_GetExtraCoin()
        {
            GetExtraCoin.Invoke();
            //should be handled by BlockingWaitController -> BlockingWait
            //_blockingWaitPopup.gameObject.SetActive(true);
            //Debug.Log("MainMenu:Action_GetExtraCoin");
        }

        public void Action_ClaimFreeCoin()
        {
            ClaimFreeCoin.Invoke();
            _freeCoinButton.interactable = false;
            //Debug.Log("MainMenu:Action_ClaimFreeCoin");
        }

        public void SetCanClaimFreeDailyBonus()
        {
            _freeCoinButtonLabel.text = CLAIM_FREE_COIN + _gameDataModel.FreeDailyBonusIncrementCoinAmount;
            _freeCoinButton.interactable = true;
        }

        public void UpdateFreeCoinCoolDown(string coolDownTimer)
        {
            _freeCoinButtonLabel.text = FREE_COIN_COOLDOWN + coolDownTimer;
            _freeCoinButton.interactable = false;
        }
    }
}