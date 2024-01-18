using System;
using System.Collections.Generic;
using UnityEngine;

public struct SaveDataStruct
{
    public DateTime SaveDate;
    public string SaveName;
    public byte[] SaveScreen;
    public List<List<GameObject>> SaveObjects;
}