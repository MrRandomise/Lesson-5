
public interface ISaveLoad
{
    bool TrySaveFile(string name);

    bool TryLoadInfo(string filename, out SaveDataStruct data);

    bool TryLoadGameObject(string filename);
}
