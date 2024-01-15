using System;
using System.Collections.Generic;
using UnityEngine;

public struct SaveDataStructInfo
{
        public DateTime SaveDate;
        public string SaveName;
}

public struct SaveDataStructImage
{
    public byte[] SaveScreen;
}


public struct SaveDataStructData
{
    public List<GameObject> SaveObjects;
}

public struct SaveDataStruct
{
    public SaveDataStructInfo SaveInfo;
    public SaveDataStructImage Screen;
    public SaveDataStructData Data;
}