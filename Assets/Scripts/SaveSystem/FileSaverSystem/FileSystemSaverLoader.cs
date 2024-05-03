using System.Collections.Generic;
using Newtonsoft.Json;
using SaveSystem.Core;
using SaveSystem.Data;
using SaveLoadCore.Tools;

namespace SaveSystem.FileSaverSystem
{
    public class FileSystemSaverLoader : ISaveLoade
    {
        private readonly AesEncryptionProvider _encryptionProvider = new();
        private readonly Saver _saver;
        private readonly Reader _reader;
        private readonly SaveLoadInfoManager _saveFileManager;
        private readonly FileNameManager _fileNameManager;

        public FileSystemSaverLoader(SaveLoadInfoManager saveFileManager, FileNameManager fileNameManager)
        {
            _reader = new Reader();
            _saver = new Saver();
            _saveFileManager = saveFileManager;
            _fileNameManager = fileNameManager;
        }

        public void Save(List<Dictionary<string, string>> data, string name)
        {
            var dataStruct = _saveFileManager.CaptureState(data, name);
            name = _fileNameManager.GetSaveFile(name);
            var str = Crypto(dataStruct);
            _saver.Save(str, name);
        }

        public SaveDataStruct Load(string filename)
        {
            filename = _fileNameManager.GetSaveFile(filename);
            var loadStruct = new SaveDataStruct();
            var loadedData = _reader.Load(filename);
            foreach (var data in loadedData)
            {
                loadStruct = JsonConvert.DeserializeObject<SaveDataStruct>(_encryptionProvider.AesDecryption(data));
            }
            return loadStruct;
        }

        public string Crypto(SaveDataStruct data)
        {
            var str = JsonConvert.SerializeObject(data);
            return _encryptionProvider.AesEncryption(str);
        }

    }
}
