using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace AdrianMunteanTest
{
    public class SaveGameController : BaseController
    {
        private const string SAVE_FILE_NAME = "SaveData.json";

        private GamePlayEventsDataModel _gamePlayEventsDataModel;

        public SaveGameController(GamePlayEventsDataModel gamePlayEventsDataModel) : base(gamePlayEventsDataModel)
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

            _gamePlayEventsDataModel.OnSaveGameData += SaveGameData;
        }

        private void SaveGameData(SaveData data)
        {
            string path = GetPathTo(SAVE_FILE_NAME);

            StreamWriter writer = new StreamWriter(path, false);
            writer.Write(JsonUtility.ToJson(data));
            writer.Close();
        }

        private string GetPathTo(string fileName)
        {
            return Path.Combine(Application.persistentDataPath, fileName);
        }
    }
}