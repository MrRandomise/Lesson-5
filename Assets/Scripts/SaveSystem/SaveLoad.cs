using SaveLoadCore.UIView;
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
        private ViewService _viewService;
        private List<GameObject> _saveObjectList;

        [Inject]
        private void Construct(ScreenCamera screenCamer, SaveComponentsService saveComponentsService, ViewService viewService)
        {
            _screenCamer = screenCamer;
            _camera = saveComponentsService.ScreenCamera;
            _viewService = viewService;
            _saveObjectList = saveComponentsService.SaveObjectList;
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
                loadData = ES3.Load<SaveDataStruct>(_info, filename, _saveSetting);
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

        public void SaveData()
        {
            var data = new SaveDataStruct();
            var obj = new List<GameObject>();

            data.SaveName = _viewService.SaveLoadMenu.SaveFormInputName.text;
            data.SaveDate = DateTime.Now;
            data.SaveScreen = _screenCamer.TrySaveCameraView(_camera);
            data.SaveObjects = new List<List<GameObject>>();


            foreach (GameObject components in _saveObjectList)
            {
                for (int i = 0; i < components.transform.childCount; i++)
                {
                    obj.Add(components.transform.GetChild(i).gameObject);
                }
                data.SaveObjects.Add(obj);
            }

            SaveFile(data);
        }

        public void LoadData()
        {

        }
    }
}
