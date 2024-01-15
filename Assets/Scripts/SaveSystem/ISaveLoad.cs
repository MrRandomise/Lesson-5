
public interface ISaveLoad
{
    void SaveAll(SaveDataStruct saveData);

    void SaveInfo(SaveDataStructInfo saveInfo, string name);

    void SaveScreen(SaveDataStructImage saveImage, string name);

    void SaveData(SaveDataStructData saveData, string name);

    SaveDataStruct LoadAll(string filename);

    SaveDataStructInfo GetLoadInfo(string filename);

    SaveDataStructImage GetLoadScreen(string filename);

    SaveDataStructData GetLoadObjects(string filename);
}
