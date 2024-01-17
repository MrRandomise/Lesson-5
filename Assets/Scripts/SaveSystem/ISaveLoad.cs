
public interface ISaveLoad
{
    bool Save(SaveDataStruct saveData);

    bool Load(string filename, out SaveDataStruct saveData);

}
