using System.Collections.Generic;

namespace SaveSystem.Core
{  
    public class SceneSaveManager
    {
        private readonly List<ISaveState> _saveStates;
        private readonly SavingSystem _savingSystem;

        public SceneSaveManager(SavingSystem system, List<ISaveState> saveStatesList)
        {
            _savingSystem = system;
            _saveStates = saveStatesList;
        }

        public bool LoadScene(string name)
        {
            if (!_savingSystem.RestoreState(_saveStates, name))
            {
                return false;
            }
            return true;
        }

        public void SaveScene(string name)
        {
            _savingSystem.CaptureState(_saveStates, name);
        }
    }
}
