using System.Collections.Generic;

namespace SaveSystem.Core
{
    public interface ISaveLoade
    {
        public void Save(List<Dictionary<string, string>> data, string filename);
        public SaveDataStruct Load(string filename);
        
    }
}
