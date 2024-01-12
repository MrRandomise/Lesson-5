using System;
using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class SaveLoad : ISaveLoad, IInitializable
    {
        private ES3Settings _saveSetting;
        private const string _secretCryptKey = "1234";
        private const string _secretSaveIdKey = "1256";

        public void Initialize()
        {
            _saveSetting = new ES3Settings(ES3.EncryptionType.AES, _secretCryptKey);
        }

        public void Save(SaveDataStruct saveData)
        {
            try
            {
                var filename = $"{Application.dataPath}/Saves/{saveData.SaveName}.sav";
                ES3.Save(_secretSaveIdKey, saveData, filename, _saveSetting);
                Debug.Log("Сохранено");
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

        public SaveDataStruct Load(string filename)
        {
            var loadData = new SaveDataStruct();
            loadData = ES3.Load<SaveDataStruct>(_secretSaveIdKey, filename, _saveSetting);
            return loadData;
        }
    }
}
