using System;
using System.Collections.Generic;
using SaveSystem.Core;
using Zenject;

namespace SaveSystem.Data
{
    public class SaveLoadInfoManager
    {
        private ScreenCamera _screenCamer;
        private ISaveLoade _saveLoader;

        [Inject]
        private void Construct(ISaveLoade saveLoader, ScreenCamera screenCamer)
        {
            _screenCamer = screenCamer;
            _saveLoader = saveLoader;
        }

        public SaveDataStruct CaptureState(List<Dictionary<string, string>> obj, string name)
        {
            var data = new SaveDataStruct();
            data.SaveName = name;
            data.SaveDate = DateTime.Now;
            data.SaveScreen = _screenCamer.TrySaveCameraView();
            data.SaveObjects = obj;
            return data;
        }

        public SaveDataStruct LoadInfo(string name)
        {
            return _saveLoader.Load(name);
        }
    }
}
