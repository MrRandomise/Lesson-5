using System.Collections.Generic;
using SaveSystem.Data;
using UnityEngine;

namespace SaveSystem.Core
{  
    public class SceneSaveManager
    {
        private readonly List<ISaveState> saveAbles;
        private readonly SaveSystem savingSystem;
        private readonly UnitsPrefabStorage prefabStorage;

        public SceneSaveManager(SaveSystem system, List<ISaveState> saveAblesList)
        {
            savingSystem = system;
            saveAbles = saveAblesList;
            Debug.Log($"Found SaveAbles: {saveAbles.Count}");
        }

        public bool LoadScene()
        {
            if (!savingSystem.RestoreState(saveAbles))
            {
                return false;
            }
            Debug.Log("Scene restored");
            return true;
        }

        public void SaveScene()
        {
            savingSystem.CaptureState(saveAbles);
            Debug.Log("Scene saved");
        }
    }
}
