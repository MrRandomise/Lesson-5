using System;
using System.Collections.Generic;

[Serializable]
public struct SaveDataStruct
{
    public string SaveName;
    public byte[] SaveScreen;
    public DateTime SaveDate;
    public List<Dictionary<string, string>> SaveObjects;
}