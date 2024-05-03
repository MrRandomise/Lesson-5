using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaveSystem.Core
{
    public class SavingSystem
    {
        private readonly ISaveLoade _saveLoader;
        private readonly List<Dictionary<string, string>> otherSceneEntries = new();
        private readonly SceneObjectsManager _sceneStorage;
        public SavingSystem(ISaveLoade sl, SceneObjectsManager sceneStorage)
        {
            _saveLoader = sl;
            _sceneStorage = sceneStorage;
        }

        public bool RestoreState(List<ISaveState> saveStates, string name)
        {
            var loadedData = _saveLoader.Load(name);
            if (!loadedData.SaveObjects.Any())
            {
                Debug.Log("No data loaded!");
                return false;
            }
            var currentSceneData = SortLoadedData(loadedData.SaveObjects);
            Debug.Log($"Loaded {loadedData.SaveName}!");
            if (!currentSceneData.Any())
            {
                Debug.Log("No data from current scene loaded!");
                return false;
            }
            
            foreach (var saveState in saveStates)
            {
                saveState.RestoreState(currentSceneData);
            }

            return true;
        }

        private List<Dictionary<string, string>> SortLoadedData(List<Dictionary<string, string>> loadedData)
        {
            otherSceneEntries.Clear();
            var sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            var currentSceneData = new List<Dictionary<string, string>>();

            foreach (var data in loadedData)
            {
                var index = data["Scene"];
                if (Convert.ToInt32(index) != sceneIndex)
                {
                    otherSceneEntries.Add(data);
                    continue;
                }

                currentSceneData.Add(data);
            }

            return currentSceneData;
        }

        public void CaptureState(List<ISaveState> saveStates, string name)
        {
            _sceneStorage.Init();
            
            var state = new List<Dictionary<string, string>>();

            foreach (var saveState in saveStates)
            {
                state.AddRange(saveState.CaptureState());
            }
            state.AddRange(otherSceneEntries);
            Debug.Log($"Save!");
            _saveLoader.Save(state, name);
        }
    }
}
