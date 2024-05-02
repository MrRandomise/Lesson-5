using System;
using System.IO;
using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class SaveLoadInfo
    {
        //private ISaveLoad _saveLoad;
        //private ScreenCamera _screenCamer = new ScreenCamera();
        //private const string _info = "info";

        //[Inject]
        //private void construct(ISaveLoad saveLoad)
        //{
        //    _saveLoad = saveLoad;
        //}

        //public void SaveInfo(string name)
        //{
        //    var data = new SaveInfoStruct();
        //    var fileName = $"{Application.dataPath}/Saves/{name}.sav";
        //    data.SaveScreen = _screenCamer.TrySaveCameraView();
        //    data.SaveName = name;
        //    data.SaveDate = DateTime.Now;

        //    _saveLoad.TrySaveFile(_info, data, fileName);
        //}

        //public SaveInfoStruct LoadInfo(string filename)
        //{
        //    var data = new SaveInfoStruct();

        //    _saveLoad.TryLoadInfo(_info, filename, out data);

        //    return data;
        //}
    }
}
