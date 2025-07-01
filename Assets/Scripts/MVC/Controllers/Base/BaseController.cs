using System.Threading.Tasks;
using UnityEngine;

namespace AdrianMunteanTest
{
    public abstract class BaseController
    {
        public GamePlayEventsDataModel GamePlayEventsDataModel { get; }

        public BaseController(GamePlayEventsDataModel gamePlayEventsDataModel)
        {
            GamePlayEventsDataModel = gamePlayEventsDataModel;
        }

        public virtual Task InitializeAsync()
        {
            Debug.Log(this.GetType().ToString() + $"<color=green>.InitializeAsync</color>");
            SetCallbacks();
            return Task.CompletedTask;
        }

        public virtual void SetCallbacks()
        {

        }
    }
}