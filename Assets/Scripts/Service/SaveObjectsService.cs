using System;
using UnityEngine;
using System.Collections.Generic;

namespace Service
{
    [Serializable]
    public sealed class SaveObjectsService
    {
        public List<GameObject> SaveObjectList = new List<GameObject>();

        public Camera ScreenCamera;
    }
}

