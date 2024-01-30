using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SaveDataStruct
{
    public string FileName;
    public string SaveName;
    public byte[] SaveScreen;
    public DateTime SaveDate;
    public List<GameObject> SaveObjects;
}