using UnityEngine;
using Zenject;

namespace SaveLoadCore
{
    public sealed class SaveLoad : ISaveLoad, IInitializable
    {
        private ES3Settings _saveSetting;
        private const string _secretCryptKey = "1234";

        //если переместить _saveSetting в Construct то все ломается, поэтому пусть это будет тут )
        public void Initialize()
        {
            _saveSetting = new ES3Settings(ES3.EncryptionType.AES, _secretCryptKey);            
        }

        public bool TrySaveFile<T>(string key, T data, string filename)
        {
            try
            {
                ES3.Save(key, data, filename, _saveSetting);

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

        public bool TryLoadInfo<T>(string key, string filename, out T data)
        {
            try
            {
                data = ES3.Load<T>(key, filename, _saveSetting);

                return true;
            }
            catch (System.IO.IOException)
            {
                Debug.LogWarningFormat("The file is open elsewhere or there was not enough storage space");
                data = default;
                return false;
            }
            catch (System.Security.SecurityException)
            {
                Debug.LogWarningFormat("You do not have the required permissions");
                data = default;
                return false;
            }
        }
    }
}