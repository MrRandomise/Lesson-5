using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using SaveSystem.Core;

namespace SaveSystem.FileSaverSystem
{
    public class FileSystemSaverLoader : ISaveLoade
    {
        private readonly AesEncryptionProvider _encryptionProvider = new();
        private readonly Saver _saver;
        private readonly Reader _reader;
        public FileSystemSaverLoader()
        {
            _reader = new Reader();
            _saver = new Saver();
        }

        public void Save(List<Dictionary<string, string>> data, string filename)
        {
            filename = $"{Application.dataPath}/Saves/{filename}.sav";
            var strList = new List<string>();
            foreach (var obj in data)
            {
                var str = JsonConvert.SerializeObject(obj);
                strList.Add(_encryptionProvider.AesEncryption(str));
            }
            _saver.Save(strList.ToArray(), filename);
        }

        public List<Dictionary<string, string>> Load(string filename)
        {
            filename = $"{Application.dataPath}/Saves/{filename}.sav";
            var result = new List<Dictionary<string, string>>();
            if (!_reader.IsSaveFileExist(filename))
            {
                return result;
            }

            var loadedData = _reader.Load(filename);
            foreach (var data in loadedData)
            {
                var obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(_encryptionProvider.AesDecryption(data));
                if(obj != null) result.Add(obj);
            }
            return result;
        }
    }
}
