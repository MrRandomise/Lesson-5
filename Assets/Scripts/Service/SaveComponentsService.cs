using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadCore
{
    [Serializable]
    public sealed class SaveComponentsService
    {
        public List<GameObject> SaveObjectList = new List<GameObject>();

        public GameObject ObjectContainer;

        public GameObject ObjectContainerPrefab;

        public Camera ScreenCamera;
    }
}
