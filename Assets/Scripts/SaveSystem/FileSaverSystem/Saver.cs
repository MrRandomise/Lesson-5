using System;
using System.IO;
using UnityEngine;

namespace SaveSystem.FileSaverSystem
{
    public class Saver
    {
        public void Save(string[] data, string savename)
        {
            if(File.Exists(savename))
            {
                File.Delete(savename);
            }
        
            try
            {
                StreamWriter sw = new StreamWriter(savename);
                foreach (var entry in data)
                {
                    sw.WriteLine(entry);
                }
                sw.Close();
            }
            catch(Exception e)
            {
                Debug.Log($"Exception: {e.Message}");
            }
        }
    }
}