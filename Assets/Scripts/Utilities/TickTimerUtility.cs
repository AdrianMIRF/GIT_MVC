using UnityEngine;

namespace AdrianMunteanTest
{
    public class TickTimerUtility : MonoBehaviour
    {
        private GamePlayEventsDataModel _gamePlayEventsDataModel;

        public void Init(GamePlayEventsDataModel gamePlayEventsDataModel)
        {
            _gamePlayEventsDataModel = gamePlayEventsDataModel;

            InvokeRepeating(nameof(TickOneSecond), 1f, 1f);
        }

        private void TickOneSecond()
        {
            _gamePlayEventsDataModel.OnTickOneSecond?.Invoke();
        }
    }
}