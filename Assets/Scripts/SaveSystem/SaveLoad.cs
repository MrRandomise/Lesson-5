using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using UnityEditor;

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

        [Inject]
        private void Construct(ScreenCamera screenCamer, SaveComponentsService saveComponentsService, LoadManager loadManager)
        {
            _screenCamer = screenCamer;
            _camera = saveComponentsService.ScreenCamera;
            _saveObjectList = saveComponentsService.SaveObjectList;
            _loadManager = loadManager;
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
                var test = new SaveDataStruct();
                ES3.LoadInto(_info, filename, test, _saveSetting);
                loadData = test;
                Debug.Log(loadData.SaveObjects[0][0].name + " " + loadData.SaveObjects[0][0].transform.position.x);
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
            var obj = new List<GameObject>();

            data.SaveName = name;
            data.SaveDate = DateTime.Now;
            data.SaveScreen = _screenCamer.TrySaveCameraView(_camera);
            data.SaveObjects = new List<List<GameObject>>();

            for(int i = 0; i < _saveObjectList.Count; i++) 
            {
                var list = new List<GameObject>();
                for (int j = 0; j < _saveObjectList[i].transform.childCount; j++)
                {
                    list.Add(_saveObjectList[i].transform.GetChild(j).gameObject);
                }
                data.SaveObjects.Add(list);
            }
            SaveFile(data);
        }

        public void LoadData(string name)
        {
            LoadFile(name, out var data);
            //_loadManager.LoadNewObjectToScene(data.SaveObjects);
        }
    }
}
