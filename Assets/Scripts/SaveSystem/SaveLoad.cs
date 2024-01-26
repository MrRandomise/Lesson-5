using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

namespace SaveLoadCore
{
    public sealed class SaveLoad : ISaveLoad, IInitializable
    {
        private ES3Settings _saveSetting;
        private const string _secretCryptKey = "1234";
        private const string _infoName = "infoName";
        private const string _infoDate = "infoDate";
        private const string _infoScreen = "infoScreen";
        private const string _dataObject = "dataObjects";
        private Camera _camera;
        private ScreenCamera _screenCamer;
        private List<GameObject> _saveObjectList;
        public string _saveName = "autosave.sav";

        [Inject]
        private void Construct(ScreenCamera screenCamer, SaveComponentsService saveComponentsService)
        {
            _screenCamer = screenCamer;
            _camera = saveComponentsService.ScreenCamera;
            _saveObjectList = saveComponentsService.SaveObjectList;
        }

        public void Initialize()
        {
            _saveSetting = new ES3Settings(ES3.EncryptionType.AES, _secretCryptKey);
        }

        public bool TrySaveFile(string name)
        {
            try
            {
                var filename = $"{Application.dataPath}/Saves/{name}.sav";
                var SaveScreen = _screenCamer.TrySaveCameraView(_camera);

                ES3.Save(_infoName, name, filename, _saveSetting);
                ES3.Save(_infoDate, DateTime.Now, filename, _saveSetting);
                ES3.Save(_infoScreen, SaveScreen, filename, _saveSetting);
                ES3.Save(_dataObject, _saveObjectList, filename, _saveSetting);

                return true;
            }
            catch (System.IO.IOException)
            {
                Debug.LogWarningFormat("The file is open elsewhere or there was not enough storage space");
                return false;
            }
            catch (System.Security.SecurityException)
            {
                Debug.LogWarningFormat("You do not have the required permissions");
                return false;
            }
        }

        public bool TryLoadInfo(string filename, out SaveDataStruct data)
        {
            try
            {
                var obj = new SaveDataStruct();
                obj.SaveDate = ES3.Load<DateTime>(_infoDate, filename, _saveSetting);
                obj.SaveName = ES3.Load<string>(_infoName, filename, _saveSetting);
                obj.SaveScreen = ES3.Load<Byte[]>(_infoScreen, filename, _saveSetting);

                data = obj;
                return true;
            }
            catch (System.IO.IOException)
            {
                Debug.LogWarningFormat("The file is open elsewhere or there was not enough storage space");
                data = new SaveDataStruct();
                return false;
            }
            catch (System.Security.SecurityException)
            {
                Debug.LogWarningFormat("You do not have the required permissions");
                data = new SaveDataStruct();
                return false;
            }
        }

        public bool TryLoadGameObject(string filename)
        {
            try
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                var list = ES3.Load(_dataObject, filename, new List<GameObject>(), _saveSetting);
                return true;
            }
            catch (System.IO.IOException)
            {
                Debug.LogWarningFormat("The file is open elsewhere or there was not enough storage space");
                return false;
            }
            catch (System.Security.SecurityException)
            {
                Debug.LogWarningFormat("You do not have the required permissions");
                return false;
            }
        }
    }
}