using System.Collections.Generic;
using Zenject;

namespace SaveSystem.Core
{  
    public class SceneSaveManager
    {
        private List<ISaveState> _saveStates;
        private SavingSystem _savingSystem;

        [Inject]
        private void Construct(SavingSystem system, List<ISaveState> saveStatesList)
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
