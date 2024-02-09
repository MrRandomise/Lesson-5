
public interface ISaveLoad
{
    bool TrySaveFile<T>(string key, T data, string filename);

    bool TryLoadInfo<T>(string key, string filename, out T data);
}
