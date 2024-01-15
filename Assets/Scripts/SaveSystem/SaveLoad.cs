using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class SaveLoad : ISaveLoad, IInitializable
    {
        private ES3Settings _saveSetting;
        private const string _secretCryptKey = "1234";
        private const string _info = "Info";
        private const string _image = "Image";
        private const string _data = "Data";

        public void Initialize()
        {
            _saveSetting = new ES3Settings(ES3.EncryptionType.AES, _secretCryptKey);
        }

        public void SaveAll(SaveDataStruct saveData)
        {
            TrySave(saveData.SaveInfo, _info, saveData.SaveInfo.SaveName);
            TrySave(saveData.Screen, _image, saveData.SaveInfo.SaveName);
        }

        public void SaveInfo(SaveDataStructInfo saveInfo, string name)
        {
            TrySave(saveInfo, _info, name);
        }

        public void SaveScreen(SaveDataStructImage saveImage, string name)
        {
            TrySave(saveImage, _image, name);
        }

        public void SaveData(SaveDataStructData saveData, string name)
        {
            TrySave(saveData, _data, name);
        }

        private bool TrySave(object saveData, string saveId, string name)
        {
            var filename = $"{Application.dataPath}/Saves/{name}.sav";
            try
            {
                ES3.Save(saveId, saveData, filename, _saveSetting);
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

        public SaveDataStruct LoadAll(string filename)
        {
            var loadData = new SaveDataStruct();

            loadData.SaveInfo = GetLoadInfo(filename);
            loadData.Screen = GetLoadScreen(filename);
            loadData.Data = GetLoadObjects(filename);   

            return loadData;
        }

        public async Task<SaveDataStructInfo> GetLoadInfo(string filename)
        {
            var loadData = new object();

            loadData = await Task.Run(() => TryLoad(_info, filename));

            return (SaveDataStructInfo)loadData;
        }

        public async Task<SaveDataStructImage> GetLoadScreen(string filename)
        {
            var loadData = new object();

            loadData = await Task.Run(() =>  TryLoad(_image, filename));

            return (SaveDataStructImage)loadData;
        }

        public async Task<SaveDataStructData> GetLoadObjects(string filename)
        {
            var loadData = new object();

            loadData = await Task.Run(() => TryLoad(_data, filename));

            return (SaveDataStructData)loadData;
        }

        private object TryLoad(string saveId, string name)
        {
            var filename = $"{Application.dataPath}/Saves/{name}.sav";
            try
            {
                return ES3.Load<object>(saveId, filename, _saveSetting);
            }
            catch (System.IO.IOException)
            {
                Debug.LogWarningFormat("The file is open elsewhere or there was not enough storage space");
                return null;
            }
            catch (System.Security.SecurityException)
            {
                Debug.LogWarningFormat("You do not have the required permissions");
                return null;
            }
        }
    }
}
