using System;
using System.Collections.Generic;
using System.IO;

namespace SaveSystem.FileSaverSystem
{
    public class Reader
    {

        public bool IsSaveFileExist(string loadname)
        {
            return File.Exists(loadname);
        }

        public string[] Load(string loadname)
        {
            var resultList = new List<string>();
            if (!IsSaveFileExist(loadname))
                return resultList.ToArray();
            try
            {
                StreamReader sr = new StreamReader(loadname);
                var line = sr.ReadLine();
                while (line != null)
                {
                    resultList.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
                Console.WriteLine("Loading success");
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return resultList.ToArray();
        }
    }
}