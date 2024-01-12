using System;
using UnityEngine;

namespace SaveLoadCore.UIView
{
    [Serializable]
    public sealed class ViewService
    {
        public SaveLoadComponents SaveLoadMenu;
        public SaveLoadProcessing SaveLoadProcessing;
        public SaveLoadContent SaveLoadContent;
        public GameObject SaveLoadItemPrefab;
    }
}

