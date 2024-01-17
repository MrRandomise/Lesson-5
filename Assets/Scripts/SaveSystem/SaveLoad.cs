using Sirenix.Serialization;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class SaveLoad : ISaveLoad, IInitializable
    {
        private ES3Settings _saveSetting;
        private const string _secretCryptKey = "1234";
        private const string _info = "SaveInfo";

        public void Initialize()
        {
            _saveSetting = new ES3Settings(ES3.EncryptionType.AES, _secretCryptKey);
        }

        public bool Save(SaveDataStruct saveData)
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

        public bool Load(string filename, out SaveDataStruct loadData)
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
    }
}
