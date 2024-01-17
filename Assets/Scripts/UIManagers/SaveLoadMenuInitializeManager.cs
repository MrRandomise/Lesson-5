using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.IO;
using System;

namespace SaveLoadCore.UIView
{
    public sealed class SaveLoadMenuInitializeManager
    {
        private GameObject _saveContainer;
        private List<GameObject> _contetList = new List<GameObject>();
        private GameObject _saveLoadItem;
        private DiContainer _diContainer;
        private SaveLoad _saveLoad;
        private ScreenCamera _screenCamera;

        [Inject]
        private void Construct(ViewService viewService, DiContainer diContainer, SaveLoad saveLoad, ScreenCamera screenCamera)
        {
            _saveContainer = viewService.SaveLoadMenu.SaveLoadContainer;
            _saveLoadItem = viewService.SaveLoadItemPrefab;
            _diContainer = diContainer;
            _saveLoad = saveLoad;
            _screenCamera = screenCamera;
        }

        public void ClearSaveContainer()
        {
            _contetList.Clear();
            for (int i = 0; i < _saveContainer.transform.childCount; i++)
            {
                _contetList.Add(_saveContainer.transform.GetChild(i).gameObject);
                _saveContainer.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public void PreViewSave()
        {
            var Files = Directory.GetFiles(Application.dataPath + "/Saves/", "*.sav", SearchOption.AllDirectories);

            ClearSaveContainer();
            AddNewLoadItem(Files);

            for (int i = 0; i < Files.Length; i++)
            {
                _contetList[i].gameObject.SetActive(true);
                var item = _contetList[i].GetComponent<SaveLoadContent>();
                _saveLoad.Load(Files[i], out var data);
                if(data.SaveName != item.SaveName.text || data.SaveDate.ToString() != item.SaveDate.text) 
                {
                    _screenCamera.TryLoadCameraView(data.SaveScreen, out var screen);
                    item.Screen.sprite = screen;
                    item.SaveName.text = data.SaveName;
                    item.SaveDate.text = data.SaveDate.ToString();
                }
            }
        }

        private void AddNewLoadItem(string[] files)
        {
            if (files.Length > _contetList.Count)
            {
                var newCount = files.Length - _contetList.Count;
                for (int i = 0; i < newCount; i++)
                {
                    var item = _diContainer.InstantiatePrefab(_saveLoadItem, _saveContainer.transform);
                    _contetList.Add(item);
                }
            }
        }
    }
}

