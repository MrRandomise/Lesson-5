using System;
using UnityEngine;
using UnityEngine.UI;

namespace SaveLoadCore.UIView
{
    [Serializable]
    public sealed class MainFomComponents
    {
        public GameObject AcceptForm;

        public GameObject SaveForm;

        public GameObject SaveLoadMenu;

        public GameObject SaveLoadContainer;

        public GameObject SaveLoadItemPrefab;

        public Button SaveButton;

        public Button ReSave;

        public Button LoadButton;
    }
}

