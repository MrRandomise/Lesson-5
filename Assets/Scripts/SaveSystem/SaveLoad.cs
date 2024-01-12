using System;
using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class SaveLoad : ISaveLoad, IInitializable
    {
        private Camera _camera;
        private ScreenCamera _screenCamer;
        private ES3Settings _saveSetting;
        public void Initialize()
        { 
            Save();
        }

        [Inject]
        private void Construct(ScreenCamera screenCamer, SaveComponentsService saveComponentsService)
        {
            _screenCamer = screenCamer;
            _camera = saveComponentsService.ScreenCamera;
            _saveSetting = new ES3Settings(ES3.EncryptionType.AES, "1234");
        }

        public void Save()
        {
            var saveData = new SaveDataStruct();

            saveData.SaveScreen = _screenCamer.TrySaveCameraView(_camera);
            saveData.SaveDate = DateTime.Now;
            saveData.SaveName = "123213";

            try
            {
                ES3.Save("Save1", saveData, Application.dataPath+"/Saves/Save1.sav", _saveSetting);
            }
            catch (System.IO.IOException)
            {
                Debug.LogWarningFormat("The file is open elsewhere or there was not enough storage space");
            }
            catch (System.Security.SecurityException)
            {
                Debug.LogWarningFormat("You do not have the required permissions");
            }
        }

        public SaveDataStruct Load(string name)
        {
            var loadData = new SaveDataStruct();
            ES3.Load(name, Application.dataPath + "/Saves/" + name, loadData, _saveSetting);
            return loadData;
        }
    }
}
