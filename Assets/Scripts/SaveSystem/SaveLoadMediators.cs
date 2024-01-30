using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadCore
{
    [Serializable]
    public class SaveLoadMediators
    {
        public Camera _camera;

        public List<GameObject> _saveObjectList;
        private ScreenCamera _screenCamer = new ScreenCamera();

        public SaveDataStruct GetLoadStruct(string name)
        {
            var data = new SaveDataStruct();
            _saveObjectList.Add(GameObject.Find("<Resources>"));
            _saveObjectList.Add(GameObject.Find("<Units>"));

            data.FileName = $"{Application.dataPath}/Saves/{name}.sav";
            data.SaveScreen = _screenCamer.TrySaveCameraView(_camera);
            data.SaveName = name;
            data.SaveDate = DateTime.Now;
            data.SaveObjects = _saveObjectList;

            return data;
        }
    }
}