using System.IO;
using UnityEngine;

namespace SaveLoadCore.Tools
{
    public sealed class FileNameManager
    {
        private readonly string directory = Application.dataPath + "/Saves/";

        public string[] GetSaveFiles()
        {
            return Directory.GetFiles(directory, "*.sav", SearchOption.AllDirectories);
        }
        
        public string GetSaveName(string name)
        {
            return name[0..^4].Remove(0, directory.Length);
        }

        public string GetSaveFile(string name)
        {
            return $"{directory}{name}.sav";
        }
    }
}

