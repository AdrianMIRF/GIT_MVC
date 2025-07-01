using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AdrianMunteanTest
{
    public class GameDataLoader : MonoBehaviour
    {
        [SerializeField] private ScriptableObject[] _gameConfigData;
        private Dictionary<Type, ScriptableObject> _loadedConfigGameData;

        public async Task LoadDataObjects()
        {
            //simulate conection to some Back End service..
            await Task.Delay(100);

            for (int i = 0; i < _gameConfigData.Length; i++)
            {
                if (_loadedConfigGameData == null) _loadedConfigGameData = new Dictionary<Type, ScriptableObject>();
                {
                    if (_loadedConfigGameData.ContainsKey(_gameConfigData[i].GetType()))
                    {
                        Debug.LogError("Duplicate Key in LoadedGameDataAssets dictionary! Data should be unique!");
                        return;
                    }

                    _loadedConfigGameData.Add(_gameConfigData[i].GetType(), _gameConfigData[i]);
                }
            }
        }

        public Dictionary<Type, ScriptableObject> GetLoadedData()
        {
            return _loadedConfigGameData;
        }
    }
}