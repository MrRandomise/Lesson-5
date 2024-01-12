
public interface ISaveLoad
{
    void Save(SaveDataStruct saveData);
    SaveDataStruct Load(string filename);
}
