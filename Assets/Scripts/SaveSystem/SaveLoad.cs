using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class SaveLoad : ISaveLoad, IInitializable
    {
        private ES3Settings _saveSetting;
        private const string _secretCryptKey = "1234";
        private const string _info = "SaveInfo";
        private Camera _camera;
        private ScreenCamera _screenCamer;
        private List<GameObject> _saveObjectList;
        private LoadManager _loadManager;
        
        private List<GameObject> prefabInstances = new List<GameObject>();

        [Inject]
        private void Construct(ScreenCamera screenCamer, SaveComponentsService saveComponentsService, LoadManager loadManager, buttonTest buttonTest)
        {
            _screenCamer = screenCamer;
            _camera = saveComponentsService.ScreenCamera;
            _saveObjectList = saveComponentsService.SaveObjectList;
            _loadManager = loadManager;
            buttonTest.SaveButton.onClick.AddListener(SaveTest);
            buttonTest.LoadButton.onClick.AddListener(LoadTest);
        }

        public void Initialize()
        {
            _saveSetting = new ES3Settings(ES3.EncryptionType.AES, _secretCryptKey);
        }

        public bool SaveFile(SaveDataStruct saveData)
        {
            var filename = $"{Application.dataPath}/Saves/{saveData.SaveName}.sav";
            try
            {
                ES3.Save(_info, saveData, filename, _saveSetting);
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

        public bool LoadFile(string filename, out SaveDataStruct loadData)
        {
            try
            {
                loadData = ES3.Load(_info, filename, new SaveDataStruct(), _saveSetting);
                return true;
            }
            catch (System.IO.IOException)
            {
                Debug.LogWarningFormat("The file is open elsewhere or there was not enough storage space");
                loadData = new SaveDataStruct();
                return false;
            }
            catch (System.Security.SecurityException)
            {
                Debug.LogWarningFormat("You do not have the required permissions");
                loadData = new SaveDataStruct();
                return false;
            }
        }

        public void SaveData(string name)
        {
            var data = new SaveDataStruct();

            data.SaveName = name;
            data.SaveDate = DateTime.Now;
            data.SaveScreen = _screenCamer.TrySaveCameraView(_camera);
            data.SaveObjects = new List<GameObject>();

            foreach(GameObject list in _saveObjectList)
            {
                data.SaveObjects.Add(list);
            }
            SaveFile(data);
        }

        public void LoadData(string name)
        {
            LoadFile(name, out var data);
        }


        public void SaveTest()
        {
            var filename = $"{Application.dataPath}/Saves/123.sav";
            foreach (GameObject list in _saveObjectList)
            {
                prefabInstances.Add(list);
            }
            
            ES3.Save("prefabInstances", prefabInstances, filename, _saveSetting);
        }

        public void LoadTest()
        {
            var filename = $"{Application.dataPath}/Saves/123.sav";
            prefabInstances = ES3.Load("prefabInstances", filename, new List<GameObject>(), _saveSetting);
        }
    }
}
