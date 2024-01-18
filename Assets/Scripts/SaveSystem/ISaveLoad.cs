
public interface ISaveLoad
{
    bool SaveFile(SaveDataStruct saveData);

    bool LoadFile(string filename, out SaveDataStruct saveData);

    void SaveData();
    void LoadData();
}
