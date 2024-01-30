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
        private SaveLoadMediators _saveLoadMediators;
        private const string _secretCryptKey = "1234";
        private const string _infoName = "infoName";
        private const string _infoDate = "infoDate";
        private const string _infoScreen = "infoScreen";
        private const string _dataObject = "dataObjects";

        [Inject]
        public void Construct(SaveLoadMediators saveLoadMediators)
        {
            _saveLoadMediators = saveLoadMediators;
        }

        public void Initialize()
        {
            _saveSetting = new ES3Settings(ES3.EncryptionType.AES, _secretCryptKey);            
        }

        public bool TrySaveFile(string name)
        {
            try
            {
                var data = _saveLoadMediators.GetLoadStruct(name);
                ES3.Save(_infoName, name, data.FileName, _saveSetting);
                ES3.Save(_infoDate, data.SaveDate, data.FileName, _saveSetting);
                ES3.Save(_infoScreen, data.SaveScreen, data.FileName, _saveSetting);
                ES3.Save(_dataObject, data.SaveObjects, data.FileName, _saveSetting);

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
                ES3.Load(_dataObject, filename, new List<GameObject>(), _saveSetting);
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