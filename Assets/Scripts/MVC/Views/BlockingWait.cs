using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdrianMunteanTest
{
    public class BlockingWait : MonoBehaviour, IView
    {
        public const string YOU_GOT_A_COIN = "You got a Coin!";
        public const string YOU_GOT_COINS = "You got Coins: ";
        public const string YOU_LOST_A_COIN = "You lost a Coin!";
        public const string NO_MORE_COINS = "No more coins!";

        [SerializeField] private Slider _sliderRef;
        [SerializeField] private GameObject _congratulationsPopup;
        [SerializeField] private GameObject _lostCoinsPopup;
        [SerializeField] private GameObject _noMoreCoinsPopup;

        [SerializeField] private TextMeshProUGUI _congratulationsPopupLabel;
        [SerializeField] private TextMeshProUGUI _lostCoinsPopupLabel;
        [SerializeField] private TextMeshProUGUI _noMoreCoinsPopupLabel;

        private GameDataModel _gameDataModel;

        public void CloseBlockingScreen()
        {
            _congratulationsPopup.SetActive(false);
            _lostCoinsPopup.SetActive(false);
            _noMoreCoinsPopup.SetActive(false);

            StartPleaseWaitSlider();

            Debug.Log("BlockingWait:CloseBlockingScreen");
        }

        public void UpdateView(GameDataModel gameDataModel)
        {
            _gameDataModel = gameDataModel;

            _congratulationsPopupLabel.text = _gameDataModel.AwardedNumberOfCoinsFromAction > 1 ? YOU_GOT_COINS + _gameDataModel.AwardedNumberOfCoinsFromAction : YOU_GOT_A_COIN;
            _lostCoinsPopupLabel.text = YOU_LOST_A_COIN;
            _noMoreCoinsPopupLabel.text = NO_MORE_COINS;

            gameObject.SetActive(true);
        }

        private void StartPleaseWaitSlider()
        {
            _sliderRef.value = 0f;
            _sliderRef.gameObject.SetActive(true);

            StartCoroutine(SliderAnimation(0f, 1f, 1f));
        }

        private IEnumerator SliderAnimation(float start, float end, float duration)
        {
            float percent = 0f;
            float timeFactor = 1f / duration;
            while (percent < 1f)
            {
                percent += Time.deltaTime * timeFactor;
                _sliderRef.value = Mathf.Lerp(start, end, Mathf.SmoothStep(0f, 1f, percent));
                yield return null;
            }

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _congratulationsPopup.SetActive(_gameDataModel.AwardedNumberOfCoinsFromAction > 0);
            _lostCoinsPopup.SetActive(_gameDataModel.AwardedNumberOfCoinsFromAction < 0);
            _noMoreCoinsPopup.SetActive(_gameDataModel.AwardedNumberOfCoinsFromAction == 0 && _gameDataModel.NumberOfCoins == 0);

            _sliderRef.gameObject.SetActive(false);

            Debug.Log("BlockingWait:OnEnable");
        }
    }
}
