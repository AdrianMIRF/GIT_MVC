using TMPro;
using UnityEngine;

namespace AdrianMunteanTest
{
    public class FreemiumBarView : MonoBehaviour, IView
    {
        private const string COINS_LABEL = "Coins: ";

        [SerializeField] private TextMeshProUGUI _coinLabel;
        private GameDataModel _gameDataModel;

        public void UpdateView(GameDataModel gameDataModel)
        {
            _gameDataModel = gameDataModel;
            _coinLabel.text = COINS_LABEL + _gameDataModel.NumberOfCoins.ToString();
        }
    }
}